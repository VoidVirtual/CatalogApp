using Microsoft.AspNetCore.Mvc;
using Catalog.Models;
using Catalog.Repositories;
using Catalog.Services;


[ApiController]
[Route("[controller]")]
public class CategoryController: ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    private readonly CatalogItemService service;
    public CategoryController(ApplicationDbContext context)
    {
        dbContext = context;
        service = new CatalogItemService(context);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] Category category, Guid? parentId)
    {
        return await service.CreateAsync(category, parentId) ? Ok() 
                                                   : BadRequest();
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Get(Guid id)
    {
        var item = await dbContext.Categories.FindAsync(id);
        return (item == null) ? NotFound()
                              : Ok(item);
    }


    [HttpPut]
    [Route("[action]")]

    /** Родительский id неизменяем, во избежание циклов*/
    public async Task<IActionResult> Edit(Guid id, [FromBody] Category newCategory)
    {
        var oldCategory = await dbContext.Categories.FindAsync(id);

        if(oldCategory == null)
            return NotFound();
        
        oldCategory.Name = newCategory.Name;
        await dbContext.SaveChangesAsync();
        return Ok();
    }
    
    [HttpDelete]
    [Route("[action]")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await dbContext.CatalogItems.FindAsync(id);
        if(item != null)
             service.Delete(item);
        return Ok();
    }       
}