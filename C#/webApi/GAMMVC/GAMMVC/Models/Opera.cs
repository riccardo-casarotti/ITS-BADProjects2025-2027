using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GAMMVC.Models;

public partial class Opera
{
    public int Id { get; set; }

    public string? Inventario { get; set; }


    public int? IdAutore { get; set; }

    [DisplayName("Ambito Cutlurale")]
    public string? AmbitoCulturale { get; set; }

    public string? Datazione { get; set; }
    [DisplayName("Titolo")]
    public string? TitoloSoggetto { get; set; }

    public string? Immagine { get; set; }

    public string? Lsreferenceby { get; set; }

    [DisplayName("Autore")]
    public virtual Autore? IdAutoreNavigation { get; set; }

    public virtual ICollection<Materiale> Materiali { get; set; } = new List<Materiale>();
}
