<<<<<<< HEAD
<<<<<<< HEAD
namespace InclusaoDiversidade.Domain;

public partial class TbCandidato
{
    public int IdCandidato { get; set; }

    public string Nome { get; set; } = null!;

    public decimal? ScoreDiversidade { get; set; }

    public int? FkVaga { get; set; }

    public virtual TbVaga? FkVagaNavigation { get; set; }
}
=======
﻿using System;
using System.Collections.Generic;

namespace InclusaoDiversidade.Domain;

public partial class TbCandidato
{
    public int IdCandidato { get; set; }

    public string Nome { get; set; } = null!;

    public decimal? ScoreDiversidade { get; set; }

    public int? FkVaga { get; set; }

    public virtual TbVaga? FkVagaNavigation { get; set; }
}
>>>>>>> 9afd1eb92a612d68c42265b614376f950340cdde
=======
﻿using System;
using System.Collections.Generic;

namespace InclusaoDiversidade.Domain;

public partial class TbCandidato
{
    public int IdCandidato { get; set; }

    public string Nome { get; set; } = null!;

    public decimal? ScoreDiversidade { get; set; }

    public int? FkVaga { get; set; }

    public virtual TbVaga? FkVagaNavigation { get; set; }
}
>>>>>>> 9afd1eb92a612d68c42265b614376f950340cdde
