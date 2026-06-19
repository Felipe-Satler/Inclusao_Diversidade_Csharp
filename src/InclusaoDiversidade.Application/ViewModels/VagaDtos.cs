using System.ComponentModel.DataAnnotations;

namespace InclusaoDiversidade.Application.ViewModels;

public record VagaDto(
    int Id,
    string Cargo,
    string? Status,
    string? FlagAfirmativa,
    short? FkDep);

/// <summary>Dados para abertura de uma vaga (POST /vagas).</summary>
public class AbrirVagaRequest
{
    [Required, StringLength(50)]
    public string Cargo { get; set; } = string.Empty;

    [Required]
    public short FkDep { get; set; }

    [Required]
    [RegularExpression("^[SNsn]$", ErrorMessage = "FlagAfirmativa deve ser 'S' ou 'N'.")]
    public string FlagAfirmativa { get; set; } = "N";
}

/// <summary>Dados para atualização de status da vaga (PATCH /vagas/{id}/status).</summary>
public class AtualizarStatusVagaRequest
{
    [Required, StringLength(20)]
    public string Status { get; set; } = string.Empty;
}
