using System;
using System.Collections.Generic;

namespace guitarshop.Models;

public partial class TypeGuitar
{
    public int IdType { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<CatalogProd> CatalogProds { get; set; } = new List<CatalogProd>();
}
