using System;
using System.Collections.Generic;

namespace guitarshop.Models;

public partial class PosOrder
{
    public int IdPosOrder { get; set; }

    public int IdOrder { get; set; }

    public int IdCatalogProd { get; set; }

    public int OrderCount { get; set; }

    public virtual CatalogProd IdCatalogProdNavigation { get; set; } = null!;

    public virtual Order IdOrderNavigation { get; set; } = null!;
}
