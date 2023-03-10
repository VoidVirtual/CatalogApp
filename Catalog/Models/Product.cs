using Microsoft.EntityFrameworkCore;
namespace Catalog.Models;

public class Product: CatalogItem
{
    [Precision(18, 2)]
    public decimal Price {get; set;}
}