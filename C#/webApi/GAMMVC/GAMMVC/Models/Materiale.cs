using System;
using System.Collections.Generic;

namespace GAMMVC.Models;

public partial class Materiale
{
    public int Id { get; set; }

    public string? Denominazione { get; set; }

    public virtual ICollection<Opera> Opere { get; set; } = new List<Opera>();
}
