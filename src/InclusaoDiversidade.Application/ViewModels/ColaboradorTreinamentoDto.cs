namespace InclusaoDiversidade.Application.ViewModels;

public record TreinamentoItemDto(
    string TipoTreinamento,
    DateTime? DataConclusao,
    string Status);

public record ColaboradorTreinamentoDto(
    int IdColaborador,
    string NomeColaborador,
    short? IdDepartamento,
    IReadOnlyCollection<TreinamentoItemDto> Treinamentos);
