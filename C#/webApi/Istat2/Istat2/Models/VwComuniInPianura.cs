using System;
using System.Collections.Generic;

namespace Istat2.Models;

public partial class VwComuniInPianura
{
    public string Comune { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public string Regione { get; set; } = null!;

    public double SuperficieDelComune { get; set; }
}
