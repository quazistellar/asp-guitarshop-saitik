using System;
using System.Collections.Generic;

namespace guitarshop.Models;

public partial class CatalogProd
{
    public int IdGuitar { get; set; }

    public string NameGuitar { get; set; } = null!;

    public string? GuitarDescription { get; set; }

    public string? GuitarImage { get; set; }

    public decimal GuitarPrice { get; set; }

    public int GuitarFormId { get; set; }

    public int GuitarTypeId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual GuitarForm GuitarForm { get; set; } = null!;

    public virtual TypeGuitar GuitarType { get; set; } = null!;

    public virtual ICollection<PosOrder> PosOrders { get; set; } = new List<PosOrder>();


}
