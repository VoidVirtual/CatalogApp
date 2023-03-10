using Microsoft.AspNetCore.Mvc;
using Catalog.Services;
using Catalog.Repositories;

namespace Catalog.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController: ControllerBase
{
    private readonly CatalogService service;

    public CatalogController(ApplicationDbContext context)
    {
        service = new CatalogService(context);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetList()
    { 
        var bytes = service.SerializeCatalog(int.MaxValue);
        return File(new MemoryStream(bytes), "application/txt", "catalog.txt");
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetListTwoProductsPerCategory()
    {
        var bytes = service.SerializeCatalog(2);
        return File(new MemoryStream(bytes), "application/txt", "catalog_two_products_per_category.txt");
    }
}