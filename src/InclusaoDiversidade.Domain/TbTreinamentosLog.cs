
namespace InclusaoDiversidade.Domain;

public partial class TbTreinamentosLog
{
    public long IdLog { get; set; }

    public DateTime? DataConclusao { get; set; }

    public string TipoTreinamento { get; set; } = null!;

    public int? FkColab { get; set; }

    public virtual TbColaboradore? FkColabNavigation { get; set; }
}
