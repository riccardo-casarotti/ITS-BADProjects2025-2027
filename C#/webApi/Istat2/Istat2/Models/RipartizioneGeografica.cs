using System;
using System.Collections.Generic;

namespace Istat2.Models;

public partial class RipartizioneGeografica
{
    public int Id { get; set; }

    public string Denominazione { get; set; } = null!;

    public virtual ICollection<Regione> Regioni { get; set; } = new List<Regione>();
}
