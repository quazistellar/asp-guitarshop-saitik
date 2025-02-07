using System;
using System.Collections.Generic;

namespace guitarshop.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public int IdUser { get; set; }

    public DateTime OrderDate { get; set; }

    public string? OrderAddress { get; set; }

    public string? OrderComment { get; set; }

    public decimal OrderTotalCost { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<PosOrder> PosOrders { get; set; } = new List<PosOrder>();
}
