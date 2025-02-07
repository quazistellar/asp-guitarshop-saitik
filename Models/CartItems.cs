using Microsoft.AspNetCore.Mvc.Rendering;

namespace guitarshop.Models
{
    public class CartItems
    {
        public IEnumerable<CatalogProd> Guitars { get; set; }
        public SelectList Types { get; set; }
        public SelectList Forms { get; set; }
        public string SearchString { get; set; } 
        public int? GuitarTypeId { get; set; } 
        public int? GuitarFormId { get; set; } 
        public string NameSortParm { get; set; }
        public string PriceSortParm { get; set; }
        public Dictionary<int, bool> ItemAddedToCart { get; set; } = new Dictionary<int, bool>();
    }

}
