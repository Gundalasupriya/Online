using Microsoft.AspNetCore.Mvc;
using Online.DTOs;
using Online.Models;
using Online.Repositories;

namespace Online.Controllers;

[ApiController]
[Route("api/Product")]

public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductRepository _product;
    private readonly ITagsRepository _tags;
    public ProductController(ILogger<ProductController> logger ,IProductRepository product, ITagsRepository tags)
    {
        _logger = logger;
        _product = product; 
        _tags = tags;
    }
    
   [HttpGet]
    public async Task<ActionResult<List<ProductDTO>>> GetList()
    {
        var productList = (await _product.GetList()).Select(x => x.asDto);
        return Ok(productList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetById([FromRoute] long id)
    {
         var product = (await _product.GetById(id));
        if(product == null) return NotFound("No Product with given id");
        var res = product.asDto;
        res.Tags= (await _tags.GetTagsForProduct(id)).Select(x => x.asDto).ToList();
        return Ok(res);
    }

     [HttpPost]
    public async Task<ActionResult<ProductDTO>> Create([FromBody] ProductCreatetDTO Data)
    {
        var createProduct = new Product
        {
            Name = Data.Name,
            Price = Data.Price,
        };
         var createdProduct = await _product.Create(createProduct);
        return StatusCode(StatusCodes.Status201Created,createdProduct);

    }

     [HttpPut("{id}")]
    public Task<ProductDTO> Update([FromRoute] long id, [FromBody] Product Data)
    {
        return null;
    }
     
}