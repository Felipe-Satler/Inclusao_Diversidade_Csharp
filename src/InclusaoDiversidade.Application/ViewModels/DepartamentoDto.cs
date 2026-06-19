namespace InclusaoDiversidade.Application.ViewModels;

/// <summary>Departamento e sua meta de diversidade (0–1) com o equivalente em %.</summary>
public record DepartamentoDto(
    short Id,
    string Nome,
    decimal? MetaDiversidade,
    decimal? MetaDiversidadePercentual);
