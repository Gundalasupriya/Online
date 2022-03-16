using Microsoft.AspNetCore.Mvc;
using Online.DTOs;
using Online.Models;
using Online.Repositories;

namespace Online.Controllers;

[ApiController]
[Route("api/Order")]

public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderRepository _order;
    private readonly IProductRepository _product;
    public OrderController(ILogger<OrderController> logger, IOrderRepository order, IProductRepository product)
    {
        _logger = logger;
        _order = order;
        _product = product;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDTO>>> GetList()
    {
       var orderList = (await _order.GetList()).Select(x => x.asDto);
        return Ok(orderList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetById([FromRoute] long id)
    {
      var order = (await _order.GetById(id));
        if(order == null) return NotFound("No Customer with given id");
        var res = order.asDto;
        res.Product= (await _product.GetProductForOrder(id)).Select(x => x.asDto).ToList();
        return Ok(res);
        
    }
    

    [HttpPost]
    public async Task<ActionResult<OrderDTO>>Create([FromBody] OrderCreateDTO Data)
    {
        var createOrder = new Order
        {
          CustomerId = Data.CustomerId,
          OrderedAt = Data.OrderedAt,
          TotalPrice = Data.TotalPrice,
          ModeOfPayment = Data.ModeOfPayment,
        };
        var createdOrder = await _order.Create(createOrder);
        return StatusCode(StatusCodes.Status201Created,createdOrder.asDto);

       
    }

   
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
         var existing = await _order.GetById(id);
        if (existing is null)
            return NotFound("No Order found with given user name");

        var didDelete = await _order.Delete(id);

        return NoContent();
    }

}