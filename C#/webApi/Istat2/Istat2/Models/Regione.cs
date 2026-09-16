using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Istat2.Models;

public partial class Regione
{
    public int Id { get; set; }

    public string Denominazione { get; set; } = null!;

   
    public int IdRipartizione { get; set; }

    [DisplayName("Ripartizione")]
    public virtual RipartizioneGeografica IdRipartizioneNavigation { get; set; } = null!;

    public virtual ICollection<Provincium> Province { get; set; } = new List<Provincium>();
}
