<<<<<<< HEAD
<<<<<<< HEAD
namespace InclusaoDiversidade.Domain;

public partial class TbDepartamento
{
    public short IdDep { get; set; }

    public string NomeDep { get; set; } = null!;

    public decimal? MetaDiversidade { get; set; }

    public virtual ICollection<TbColaboradore> TbColaboradores { get; set; } = new List<TbColaboradore>();

    public virtual ICollection<TbVaga> TbVagas { get; set; } = new List<TbVaga>();
}
=======
﻿using System;
using System.Collections.Generic;

namespace InclusaoDiversidade.Domain;

public partial class TbDepartamento
{
    public short IdDep { get; set; }

    public string NomeDep { get; set; } = null!;

    public decimal? MetaDiversidade { get; set; }

    public virtual ICollection<TbColaboradore> TbColaboradores { get; set; } = new List<TbColaboradore>();

    public virtual ICollection<TbVaga> TbVagas { get; set; } = new List<TbVaga>();
}
>>>>>>> 9afd1eb92a612d68c42265b614376f950340cdde
=======
﻿using System;
using System.Collections.Generic;

namespace InclusaoDiversidade.Domain;

public partial class TbDepartamento
{
    public short IdDep { get; set; }

    public string NomeDep { get; set; } = null!;

    public decimal? MetaDiversidade { get; set; }

    public virtual ICollection<TbColaboradore> TbColaboradores { get; set; } = new List<TbColaboradore>();

    public virtual ICollection<TbVaga> TbVagas { get; set; } = new List<TbVaga>();
}
>>>>>>> 9afd1eb92a612d68c42265b614376f950340cdde
