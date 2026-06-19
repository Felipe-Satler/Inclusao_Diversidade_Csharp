<<<<<<< HEAD
namespace InclusaoDiversidade.Domain;

public partial class TbColaboradore
{
    public int IdColab { get; set; }

    public string Nome { get; set; } = null!;

    public string? Genero { get; set; }

    public string? Etnia { get; set; }

    public string? Pcd { get; set; }

    public short? FkDep { get; set; }

    public virtual TbDepartamento? FkDepNavigation { get; set; }

    public virtual ICollection<TbTreinamentosLog> TbTreinamentosLogs { get; set; } = new List<TbTreinamentosLog>();
}
=======
﻿using System;
using System.Collections.Generic;

namespace InclusaoDiversidade.Domain;

public partial class TbColaboradore
{
    public int IdColab { get; set; }

    public string Nome { get; set; } = null!;

    public string? Genero { get; set; }

    public string? Etnia { get; set; }

    public string? Pcd { get; set; }

    public short? FkDep { get; set; }

    public virtual TbDepartamento? FkDepNavigation { get; set; }

    public virtual ICollection<TbTreinamentosLog> TbTreinamentosLogs { get; set; } = new List<TbTreinamentosLog>();
}
>>>>>>> 9afd1eb92a612d68c42265b614376f950340cdde
