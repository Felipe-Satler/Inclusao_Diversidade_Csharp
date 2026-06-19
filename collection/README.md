# Coleção de testes — Inclusão & Diversidade API

Dois formatos equivalentes (use o que o professor pedir):

- `InclusaoDiversidade.insomnia_v4.json` — importe no **Insomnia** (Application → Import).
- `InclusaoDiversidade.postman_collection.json` — importe no **Postman** (Import).

## Variáveis

| Variável   | Valor padrão            |
|------------|-------------------------|
| `base_url` | `http://localhost:5000` |
| `token`    | (vazio; preenchido após o login) |

> O perfil **http** roda em `http://localhost:5000`. Para HTTPS use `https://localhost:5001`.

## Fluxo sugerido

1. Suba a API: `dotnet run --project src/InclusaoDiversidade.Api`.
2. **Auth → Gerar token (admin)** (`admin` / `admin123`).
   - No **Postman**, o `access_token` é salvo automaticamente em `{{token}}` (script de teste).
   - No **Insomnia**, copie o `dados.access_token` da resposta e cole na variável `token` do Base Environment.
3. **Departamentos → Listar** — endpoint público e paginado.
4. **Vagas → Abrir vaga (Administrador)** — usa o Bearer token.
5. **Vagas → Registrar candidato** — em vaga afirmativa, a Trigger 2 soma +2 ao score.
6. **Vagas → Atualizar status → PREENCHIDA** — dispara a Trigger 1 (contratação) e a Trigger 3 (matrícula em compliance).
7. **Vagas → Listar candidatos** e **Colaboradores → Treinamentos** para conferir os efeitos.

## Demonstrar a Trigger 4 (bloqueio)

Os dados de seed têm todos os departamentos com meta ≥ 0.25. Para ver o bloqueio,
crie um departamento com meta < 0.10 e tente abrir uma vaga **não** afirmativa nele:

```sql
INSERT INTO TB_DEPARTAMENTOS (ID_DEP, NOME_DEP, META_DIVERSIDADE) VALUES (99, 'Teste Baixa Meta', 0.05);
COMMIT;
```

Depois, em **Abrir vaga**, use `{ "cargo": "...", "fkDep": 99, "flagAfirmativa": "N" }`.
A API retorna **400** com a mensagem de governança ESG (regra espelhada no `VagaService`
e garantida pela trigger no banco).
