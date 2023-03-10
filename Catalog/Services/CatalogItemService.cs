using System;
using Catalog.Repositories;
using Catalog.Models;
using System.Text;

namespace Catalog.Services;

public class CatalogItemService
{
    private readonly ApplicationDbContext dbContext;
    public CatalogItemService(ApplicationDbContext context)
    {
        dbContext = context;   
    }
    
    public async Task<bool> CreateAsync(CatalogItem item, Guid? parentId)
    {   
        if(parentId != null)
        {
            var parent = await dbContext.Categories.FindAsync(parentId);
            if(parent == null)
                return false;
            parent.Children.Add(item);
        }
        await dbContext.AddAsync(item);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public void Delete(CatalogItem item)
    {
        var children = dbContext.CatalogItems.Where(c => c.ParentId.Equals(item.Id));
        foreach(var child in children)
            Delete(child);

        dbContext.CatalogItems.Remove(item);
        dbContext.SaveChanges();
    }
}