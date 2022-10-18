using System;
using System.Collections.Generic;

namespace lab3.Cafe.Domain;

public partial class IngridientWarehousesInformation
{
    public string Ingridient { get; set; } = null!;

    public int? Weight { get; set; }

    public DateTime ReleaseDate { get; set; }

    public DateTime StorageLife { get; set; }

    public string Provider { get; set; } = null!;
}
