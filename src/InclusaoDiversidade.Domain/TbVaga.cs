
namespace InclusaoDiversidade.Domain;

public partial class TbVaga
{
    public int IdVaga { get; set; }

    public string Cargo { get; set; } = null!;

    public string? Status { get; set; }

    public string? FlagAfirmativa { get; set; }

    public short? FkDep { get; set; }

    public virtual TbDepartamento? FkDepNavigation { get; set; }

    public virtual ICollection<TbCandidato> TbCandidatos { get; set; } = new List<TbCandidato>();
}
