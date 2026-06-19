# Inclusão & Diversidade API

API REST em **ASP.NET Core 8** para o tema ESG **Inclusão e diversidade corporativa**.
A aplicação integra-se a um banco **Oracle** com **gatilhos PL/SQL** que automatizam
contratação por mérito social, bonificação de candidatos afirmativos, matrícula em
treinamento de compliance e bloqueio de vagas não inclusivas.

## Arquitetura (separação em camadas / MVVM)

```
InclusaoDiversidade.sln
├── src/
│   ├── InclusaoDiversidade.Domain          → Entidades (Model) — POCOs, sem dependência de framework
│   ├── InclusaoDiversidade.Application      → DTOs/ViewModels + contratos de serviço (ViewModel)
│   ├── InclusaoDiversidade.Infrastructure   → EF Core (AppDbContext), serviços de negócio, JWT
│   └── InclusaoDiversidade.Api              → Controllers REST (View) + composição/pipeline
├── tests/
│   └── InclusaoDiversidade.Tests            → Testes xUnit (integração de controllers)
├── database/
│   └── script_inclusao_diversidade.sql      → Tabelas, sequences e as 4 triggers PL/SQL
├── Dockerfile / docker-compose.yml
└── Directory.Build.props / global.json
```

Regra de dependência: `Api → Application/Infrastructure → Domain`. Os controllers
dependem de **interfaces** (`Application/Common/Interfaces`) e **DTOs**, nunca do
`AppDbContext` diretamente. As implementações dos serviços vivem na Infraestrutura
(onde o `AppDbContext` reside).

## Pré-requisitos

- .NET 8 SDK
- Acesso ao banco Oracle da FIAP (ou um Oracle XE local)
- (Opcional) Docker e Docker Compose

## Configuração da connection string

Para fins acadêmicos/de teste, a connection string (com usuário e senha) está
**hardcoded** no `appsettings.json`. Em um cenário real, o recomendado é mantê-la
**fora do repositório**, por um destes meios:

**Desenvolvimento — User Secrets** (recomendado; o arquivo fica fora do projeto):

```bash
cd src/InclusaoDiversidade.Api
dotnet user-secrets set "ConnectionStrings:OracleConnection" "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=SEU_RM;Password=SUA_SENHA;"
```

**Docker / produção — variável de ambiente** (o `__` vira `:`):

```bash
export ConnectionStrings__OracleConnection="Data Source=...;User Id=SEU_RM;Password=SUA_SENHA;"
```

> ⚠️ **Importante:** a senha real foi versionada anteriormente no `appsettings.json`.
> Como ela continua no **histórico do Git**, trate-a como exposta: se for a senha do
> portal/banco da FIAP, **troque-a**. Remover do histórico exige reescrever commits
> (ex.: `git filter-repo`) e um novo push.

## Como executar

```bash
dotnet restore
dotnet build
dotnet run --project src/InclusaoDiversidade.Api
```

A aplicação **abre direto no Swagger**: `https://localhost:5001/swagger/index.html`
(ou `http://localhost:5000/swagger/index.html`). A raiz (`/`) também redireciona para o
Swagger. Na primeira execução com HTTPS, rode `dotnet dev-certs https --trust` se o
navegador reclamar do certificado.

### Via Docker

```bash
docker compose up --build
# API em http://localhost:8080/swagger
```

## Endpoints

| Método | Rota                              | Auth          | Descrição |
|--------|-----------------------------------|---------------|-----------|
| GET    | `/departamentos`                  | Público       | Lista departamentos + meta de diversidade (paginado) |
| POST   | `/vagas`                          | Administrador | Abre vaga (espelha a Trigger 4) |
| PATCH  | `/vagas/{id}/status`              | Administrador | Atualiza status; `PREENCHIDA` dispara Triggers 1 e 3 |
| GET    | `/vagas/{id}/candidatos`          | Público       | Candidatos da vaga, ordenados por Score (desc), paginado |
| POST   | `/vagas/{id}/candidatos`          | Público       | Inscreve candidato (Trigger 2 bonifica vaga afirmativa) |
| GET    | `/candidatos`                     | Público       | Lista todos os candidatos (paginado, ordenado por Score) |
| GET    | `/colaboradores/treinamentos`     | Público       | Colaboradores + treinamentos (paginado; filtro `status`) |
| POST   | `/auth/token`                     | Público       | Token JWT de demonstração |
| GET    | `/health`                         | Público       | Verificação de saúde |

Paginação por query string: `?pagina=1&tamanho=10`.

### Formato de resposta (envelope padronizado)

Toda resposta segue o mesmo envelope, com `sucesso`, `mensagem` contextual e `dados`.
Em listagens, os metadados de paginação ficam **aninhados** em `paginacao`:

A coleção de dados tem um **nome contextual** por endpoint (`departamentos`,
`candidatos`, `colaboradores`, `vaga`, `candidato`, `autenticacao`...) e os campos
são descritivos. Exemplo de `GET /departamentos`:

```json
{
  "sucesso": true,
  "mensagem": "Lista de departamentos resgatada com sucesso.",
  "departamentos": [
    { "idDepartamento": 1, "nomeDepartamento": "Tecnologia", "metaDiversidadeDecimal": 0.5, "metaDiversidadePercentual": 50 }
  ],
  "paginacao": {
    "pagina": 1,
    "tamanho": 10,
    "totalItens": 10,
    "totalPaginas": 1,
    "temProxima": false,
    "temAnterior": false
  }
}
```

Erros usam o mesmo padrão (`sucesso: false`), com mensagens contextuais:

- **401** — sem token em endpoint protegido, ou credenciais inválidas no login.
- **403** — autenticado, mas sem o perfil `Administrador`.
- **404** — recurso não encontrado (ex.: vaga inexistente).
- **400** — validação de entrada ou bloqueio de regra de negócio (ex.: Trigger 4).

## Autenticação

Endpoints sensíveis usam `[Authorize(Roles = "Administrador")]`. Para obter um token
de demonstração:

```
POST /auth/token
{ "username": "admin", "password": "admin123" }
```

Use o token no botão **Authorize** do Swagger (`Bearer {token}`).

## Testes

```bash
dotnet test
```

Os testes sobem a API com `WebApplicationFactory<Program>` e trocam o provider Oracle
por **EF Core InMemory** (na `CustomWebApplicationFactory`), validando status **200**
em cada controller (`Departamentos`, `Vagas`, `Colaboradores`, `Auth`, `Health`).

## Banco de dados e triggers

O script `database/script_inclusao_diversidade.sql` cria as tabelas, as sequences
(`SEQ_COLAB`, `SEQ_TREINAMENTO_LOG`) e as 4 triggers PL/SQL:

1. **TRG_CONTRATACAO_DIVERSIDADE** — ao marcar a vaga como `PREENCHIDA`, contrata o
   candidato de maior Score.
2. **TRG_PRIORIDADE_CANDIDATO** — em vaga afirmativa, soma +2 ao Score (limite 10).
3. **TRG_MATRICULA_COMPLIANCE** — todo novo colaborador é matriculado no treinamento
   de cultura inclusiva.
4. **TRG_BLOQUEIA_VAGA_NAO_INCLUSIVA** — bloqueia vaga não afirmativa em departamento
   com `META_DIVERSIDADE < 0.10`.

> O scaffold do EF Core foi gerado **database-first** sobre o schema `RM561859`.
> Como `TB_VAGAS` e `TB_CANDIDATOS` não têm sequence para a PK, a aplicação gera o
> próximo ID (`MAX(id)+1`) ao inserir — ver `VagaService`/`CandidatoService`.

## Migrações do EF Core

O projeto é **database-first** (o banco já existe). Caso precise gerar migrações:

```bash
dotnet ef migrations add NomeDaMigration \
  --project src/InclusaoDiversidade.Infrastructure \
  --startup-project src/InclusaoDiversidade.Api

dotnet ef database update \
  --project src/InclusaoDiversidade.Infrastructure \
  --startup-project src/InclusaoDiversidade.Api
```

## Coleção de testes (Insomnia/Postman)

Veja `collection/` — há exportações para **Insomnia** e **Postman**, além de um
`collection/README.md` com o passo a passo (login, fluxo dos endpoints e como
demonstrar cada trigger).

## Pendências (próximos passos)

1. **Demonstração da Trigger 4**: criar um departamento com `META_DIVERSIDADE < 0.10`
   e tentar abrir vaga não afirmativa para ver o bloqueio (os dados de seed têm todas
   as metas ≥ 0.25). Passo a passo em `collection/README.md`.
2. Revisar `docker-compose.yml` se for subir um Oracle XE local.
