using Microsoft.AspNetCore.Mvc;
using Catalog.Models;
using Catalog.Repositories;
using Catalog.Services;

[ApiController]
[Route("[controller]")]
public class ProductController: ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    private readonly CatalogItemService service;
    public ProductController(ApplicationDbContext context)
    {
        dbContext = context;
        service = new CatalogItemService(context);
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Get(Guid id)
    {
        var product = await dbContext.Products.FindAsync(id);
        return product == null ? NotFound() 
                               : Ok(product);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] Product product, Guid? parentId)
    {
        var created = await service.CreateAsync(product, parentId);
        return created ? Ok() 
                       : BadRequest();
    }

    /** Родительский id неизменяем, во избежание циклов*/
    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> Edit(Guid id, [FromBody] Product newProduct)
    {
        var oldProduct = await dbContext.Products.FindAsync(id);
        if(oldProduct == null)
            return NotFound();

        oldProduct.Name = newProduct.Name;
        oldProduct.Price = newProduct.Price;

        await dbContext.SaveChangesAsync();
        return Ok();
    }

    
    [HttpDelete]
    [Route("[action]")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await dbContext.Products.FindAsync(id);
        if(product == null)
            return Ok();
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
}