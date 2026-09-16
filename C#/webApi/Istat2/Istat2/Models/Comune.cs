using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Istat2.Models;

public partial class Comune
{
    public int Id { get; set; }

    public int IdProvincia { get; set; }

    public string Denominazione { get; set; } = null!;
    [DisplayName("Codice Catastale")]

    public string CodiceCatastale { get; set; } = null!;

    [DisplayName("Capoluogo")]

    public bool ComuneCapoluogo { get; set; }

    [DisplayName("Altitudine")]

    public int AltitudineCentro { get; set; }

    [DisplayName("Zona Litoranea")]

    public bool ZonaLitoranea { get; set; }

    public int IdZonaAltimetrica { get; set; }

    public string IdZonaMontana { get; set; } = null!;

    public double Superficie { get; set; }

    public int Popolazione2001 { get; set; }

    public int Popolazione2011 { get; set; }

    public virtual Provincium IdProvinciaNavigation { get; set; } = null!;

    public virtual ZonaAltimetrica IdZonaAltimetricaNavigation { get; set; } = null!;

    public virtual ZonaMontana IdZonaMontanaNavigation { get; set; } = null!;
}
