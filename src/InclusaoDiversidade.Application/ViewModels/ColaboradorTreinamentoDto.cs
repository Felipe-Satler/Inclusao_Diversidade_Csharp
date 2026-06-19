namespace InclusaoDiversidade.Application.ViewModels;

public record TreinamentoItemDto(
    string TipoTreinamento,
    DateTime? DataConclusao,
    string Status);

public record ColaboradorTreinamentoDto(
    int Id,
    string Nome,
    short? FkDep,
    IReadOnlyCollection<TreinamentoItemDto> Treinamentos);
