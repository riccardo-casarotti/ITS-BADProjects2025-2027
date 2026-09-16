using System;
using System.Collections.Generic;

namespace Istat2.Models;

public partial class ZonaAltimetrica
{
    public int Id { get; set; }

    public string Denominazione { get; set; } = null!;

    public virtual ICollection<Comune> Comuni { get; set; } = new List<Comune>();
}
