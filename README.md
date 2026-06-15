<<<<<<< HEAD
# Inclusão & Diversidade API

API REST em **ASP.NET Core 8** para o tema ESG **Inclusão e diversidade corporativa**.
A aplicação integra-se a um banco de dados com **gatilhos PL/SQL** que automatizam
contratação por mérito social, bonificação de candidatos afirmativos, matrícula em
treinamento de compliance e bloqueio de vagas não inclusivas.

> **Status atual:** esqueleto do projeto pronto (estrutura, dependências, autenticação,
> Swagger, tratamento de exceções, paginação e teste-modelo). As **entidades/DbContext**
> e o **provider de banco** ainda serão definidos — ver seção *Pendências*.

## Arquitetura (separação em camadas / MVVM)

```
InclusaoDiversidade.sln
├── src/
│   ├── InclusaoDiversidade.Domain          → Entidades e interfaces de repositório (Model)
│   ├── InclusaoDiversidade.Application      → ViewModels/DTOs, serviços, validators (ViewModel)
│   ├── InclusaoDiversidade.Infrastructure   → EF Core (DbContext), repositórios, JWT
│   └── InclusaoDiversidade.Api              → Controllers REST (View) + composição/pipeline
├── tests/
│   └── InclusaoDiversidade.Tests            → Testes xUnit (integração de controllers)
├── database/
│   └── script_inclusao_diversidade.sql      → Tabelas, sequences e as 4 triggers PL/SQL
├── Dockerfile / docker-compose.yml
└── Directory.Build.props / global.json
```

Regra de dependência: `Api → Application/Infrastructure → Domain`. O domínio não conhece
nenhuma outra camada nem frameworks.

## Pré-requisitos

- .NET 8 SDK
- (Opcional) Docker e Docker Compose

## Como executar

```bash
# Restaurar e compilar
dotnet restore
dotnet build

# Rodar a API (Swagger em https://localhost:5001/swagger)
dotnet run --project src/InclusaoDiversidade.Api
```

### Via Docker

```bash
docker compose up --build
# API disponível em http://localhost:8080/swagger
```

### No Visual Studio Code

1. Instale o **.NET 8 SDK** e a extensão **C# Dev Kit** (ao abrir a pasta, o VS Code
   sugere as extensões recomendadas em `.vscode/extensions.json`).
2. Abra a **pasta raiz** do projeto (`File > Open Folder...`). O C# Dev Kit carrega
   automaticamente o `InclusaoDiversidade.sln`.
3. Atalhos já configurados em `.vscode/`:
   - **Ctrl+Shift+B** → build da solução (task `build`).
   - **F5** → executa a API em modo debug (config `.NET Core Launch (API)`),
     abrindo o Swagger no navegador.
   - Tasks `test` e `run` disponíveis em *Terminal > Run Task...*.

## Testes

```bash
dotnet test
```

O projeto de testes usa `WebApplicationFactory<Program>` (pacote
`Microsoft.AspNetCore.Mvc.Testing`). Há um teste-modelo em
`HealthControllerTests` validando o status 200 — **cada novo controller deve
incluir um teste no mesmo formato**.

## Autenticação

Endpoints sensíveis devem usar `[Authorize(Roles = "Administrador")]`.
Para obter um token de demonstração:

```
POST /auth/token
{ "username": "admin", "password": "admin123" }
```

Use o token retornado no botão **Authorize** do Swagger (`Bearer {token}`).

## Migrações do EF Core (após definir o banco)

```bash
dotnet ef migrations add InitialCreate \
  --project src/InclusaoDiversidade.Infrastructure \
  --startup-project src/InclusaoDiversidade.Api

dotnet ef database update \
  --project src/InclusaoDiversidade.Infrastructure \
  --startup-project src/InclusaoDiversidade.Api
```

## Pendências (TODOs)

Buscar por `TODO` no código. Principais:

1. **Provider de banco** — descomentar o pacote escolhido em
   `InclusaoDiversidade.Infrastructure.csproj` (Oracle ou SQL Server).
2. **DbContext + entidades** — criar `InclusaoDbContext` e mapear
   `Departamento`, `Vaga`, `Colaborador`, `Candidato`, `TreinamentoLog`, e
   registrá-lo em `Infrastructure/DependencyInjection.cs`.
3. **Connection string** — preencher `ConnectionStrings:DefaultConnection` no
   `appsettings.json`.
4. **Endpoints de negócio** — implementar os 4+ endpoints (departamentos, vagas,
   contratação, treinamentos, candidatos) com paginação e os respectivos testes.
5. **Fábrica de testes** — trocar o provider por EF Core InMemory em
   `CustomWebApplicationFactory` quando o DbContext existir.
=======
# Inclusao_Diversidade_Csharp
>>>>>>> 0dc48a3cc7f5952f80f52443f3776b2347955faa
