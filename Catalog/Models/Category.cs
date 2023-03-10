namespace Catalog.Models;
public class Category: CatalogItem
{
    public virtual ICollection<CatalogItem> Children { get; set; } =  new HashSet<CatalogItem> ();
}