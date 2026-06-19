<<<<<<< HEAD
<<<<<<< HEAD
namespace InclusaoDiversidade.Domain;

public partial class TbTreinamentosLog
{
    public long IdLog { get; set; }

    public DateTime? DataConclusao { get; set; }

    public string TipoTreinamento { get; set; } = null!;

    public int? FkColab { get; set; }

    public virtual TbColaboradore? FkColabNavigation { get; set; }
}
=======
﻿using System;
using System.Collections.Generic;

namespace InclusaoDiversidade.Domain;

public partial class TbTreinamentosLog
{
    public long IdLog { get; set; }

    public DateTime? DataConclusao { get; set; }

    public string TipoTreinamento { get; set; } = null!;

    public int? FkColab { get; set; }

    public virtual TbColaboradore? FkColabNavigation { get; set; }
}
>>>>>>> 9afd1eb92a612d68c42265b614376f950340cdde
=======
﻿using System;
using System.Collections.Generic;

namespace InclusaoDiversidade.Domain;

public partial class TbTreinamentosLog
{
    public long IdLog { get; set; }

    public DateTime? DataConclusao { get; set; }

    public string TipoTreinamento { get; set; } = null!;

    public int? FkColab { get; set; }

    public virtual TbColaboradore? FkColabNavigation { get; set; }
}
>>>>>>> 9afd1eb92a612d68c42265b614376f950340cdde
