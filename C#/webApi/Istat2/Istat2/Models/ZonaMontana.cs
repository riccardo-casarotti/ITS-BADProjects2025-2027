using System;
using System.Collections.Generic;

namespace Istat2.Models;

public partial class ZonaMontana
{
    public string Id { get; set; } = null!;

    public string Denominazione { get; set; } = null!;

    public virtual ICollection<Comune> Comuni { get; set; } = new List<Comune>();
}
