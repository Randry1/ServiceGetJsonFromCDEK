using Microsoft.AspNetCore.Mvc;
using WebSwager.Model;
using System.Collections.Generic;

namespace WebSwager.Controllers;

[Route("/api/[controller]")]
public class ProductsController : Controller
{
    private static List<Product> _products = new List<Product>(
        new []
        {
            new Product() { Id = 1, Name = "Notebook", Price = 10000},
            new Product(){Id = 2, Name = "car", Price = 2000000},
            new Product() {Id = 3, Name = "Apple", Price = 30}
        }
    );
    
    [HttpGet]
    public IEnumerable<Product> Get() => _products;

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = _products.SingleOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _products.Remove( _products.SingleOrDefault(p => p.Id == id));
        return Ok(new {Message = "test"});
    }

    private int NextProductId =>
        _products.Count() == 0 ? 1 : _products.Max(x => x.Id) + 1;

    [HttpGet("GetNextProductId")]
    public int GetNextProductId()
    {
        return NextProductId;
    }

    [HttpPost]
    public IActionResult Post(Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        product.Id = NextProductId;
        _products.Add(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPost("AddProdust")]
    public IActionResult PostBody([FromBody] Product product) => Post(product);

    [HttpPut]
    public IActionResult Put(Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var storedProduct = _products.SingleOrDefault(p => p.Id == product.Id);
        if (storedProduct == null)
        {
            return NotFound();
        }
        storedProduct.Name = product.Name;
        storedProduct.Price = product.Price;
        return Ok(storedProduct);
    }

    [HttpPut("EditProductJson")]
    public IActionResult PutBody([FromBody] Product product) => Put(product);
    // GET
    // public IActionResult Index()
    // {
    //     return View();
    // }

}

