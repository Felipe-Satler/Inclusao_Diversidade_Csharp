using System.ComponentModel.DataAnnotations;

namespace InclusaoDiversidade.Application.ViewModels;

public record CandidatoDto(
    int Id,
    string Nome,
    decimal? ScoreDiversidade,
    int? FkVaga);

/// <summary>Dados para inscrição de um candidato em uma vaga (POST /vagas/{id}/candidatos).</summary>
public class RegistrarCandidatoRequest
{
    [Required, StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Range(0, 10, ErrorMessage = "O Score de Diversidade deve estar entre 0 e 10.")]
    public decimal ScoreDiversidade { get; set; }
}
