using System.Text;
using Catalog.Models;
using Catalog.Repositories;

namespace Catalog.Services;
using Util;

public class CatalogService
{
    private readonly ApplicationDbContext dbContext;

    public CatalogService(ApplicationDbContext context)
    {
        dbContext = context;
    }

    public byte[] SerializeCatalog(int maxProductsPerCategoryCount)
    {
        var sb = new StringBuilder();
        var counter = new SumCounter();
        var roots = dbContext.CatalogItems.Where(c => c.ParentId == null);
        foreach(var root in roots)
        {
            SerializeCatalog(root, sb, counter, maxProductsPerCategoryCount, 0);
        }
        sb.Append("Sum: " + counter.Sum + "\n")
          .Append("Each 2nd product sum: " + counter.EachSecondProductSum + "\n");
        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private void SerializeCatalog(CatalogItem item, StringBuilder sb, SumCounter sumCounter, 
                                                            int maxProductsPerCategoryCount,
                                                            int depth)
    { 
        for(int i = 0; i < depth; ++i)
                sb.Append("   ");
        sb.Append(item.Name);

        if(item is Product)
        {
            var product = item as Product;
            sb.Append("    " + product.Price);
            sumCounter.Increase(product.Price);
        }

        sb.Append("\n");
        if(item is Category)
        {
            var children = dbContext.CatalogItems.Where(i => i.ParentId == item.Id);
            int productsCount = 0;
            foreach(var child in children)
            {
                if(child is Product)
                    ++productsCount;   

                if(productsCount <= maxProductsPerCategoryCount || child is not Product)
                    SerializeCatalog(child, sb, sumCounter, maxProductsPerCategoryCount, depth + 1);
            }
        }
    }
}