using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Istat2.Models;

public partial class Provincium
{
    public int Id { get; set; }

    public string Denominazione { get; set; } = null!;

    public string Sigla { get; set; } = null!;

    [DisplayName("Codice Città")]
    public int? CodiceCittaMetropolitana { get; set; }

    
    public int IdRegione { get; set; }

    public virtual ICollection<Comune> Comuni { get; set; } = new List<Comune>();


    [DisplayName("ID Regione")]
    public virtual Regione IdRegioneNavigation { get; set; } = null!;
}
