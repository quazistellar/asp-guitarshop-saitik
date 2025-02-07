using System;
using System.Collections.Generic;

namespace guitarshop.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual CatalogProd Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
