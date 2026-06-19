using InclusaoDiversidade.Application.Common.Models;

namespace InclusaoDiversidade.Application.ViewModels;

public class RespostaDepartamentos
{
    public bool Sucesso { get; init; } = true;
    public string Mensagem { get; init; } = string.Empty;
    public IReadOnlyCollection<DepartamentoDto> Departamentos { get; init; } = Array.Empty<DepartamentoDto>();
    public PaginacaoMetadados Paginacao { get; init; } = new();
}

public class RespostaCandidatos
{
    public bool Sucesso { get; init; } = true;
    public string Mensagem { get; init; } = string.Empty;
    public IReadOnlyCollection<CandidatoDto> Candidatos { get; init; } = Array.Empty<CandidatoDto>();
    public PaginacaoMetadados Paginacao { get; init; } = new();
}

public class RespostaColaboradores
{
    public bool Sucesso { get; init; } = true;
    public string Mensagem { get; init; } = string.Empty;
    public IReadOnlyCollection<ColaboradorTreinamentoDto> Colaboradores { get; init; } = Array.Empty<ColaboradorTreinamentoDto>();
    public PaginacaoMetadados Paginacao { get; init; } = new();
}
