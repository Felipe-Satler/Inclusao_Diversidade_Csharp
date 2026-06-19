namespace InclusaoDiversidade.Application.ViewModels;

public class RespostaVaga
{
    public bool Sucesso { get; init; } = true;
    public string Mensagem { get; init; } = string.Empty;
    public VagaDto Vaga { get; init; } = default!;
}

public class RespostaCandidato
{
    public bool Sucesso { get; init; } = true;
    public string Mensagem { get; init; } = string.Empty;
    public CandidatoDto Candidato { get; init; } = default!;
}

/// <summary>Dados de autenticação devolvidos por POST /auth/token.</summary>
public record TokenDto(
    string AccessToken,
    string TokenType,
    string Perfil,
    DateTime ExpiraEm);

public class RespostaToken
{
    public bool Sucesso { get; init; } = true;
    public string Mensagem { get; init; } = string.Empty;
    public TokenDto Autenticacao { get; init; } = default!;
}
