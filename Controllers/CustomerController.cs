using Microsoft.AspNetCore.Mvc;
using Online.DTOs;
using Online.Models;
using Online.Repositories;

namespace Online.Controllers;

[ApiController]
[Route("api/customer")]

public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerRepository _customer;
    private readonly IOrderRepository _order;
    public CustomerController(ILogger<CustomerController> logger, ICustomerRepository  customer,IOrderRepository order)
    {
        _logger = logger; 
        _customer = customer;
        _order = order;
    }
    
   [HttpGet]
    public async Task<ActionResult<List<CustomerDTO>>> GetList()
    {
        var customerList = (await _customer.GetList()).Select(x => x.asDto);
        return Ok(customerList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDTO>> GetById([FromRoute] long id)
    {
        var customer = (await _customer.GetById(id));
        if(customer == null) return NotFound("No customer with given id");
        var res = customer.asDto;
        res.Orders= (await _order.GetOrdersForCustomer(id)).Select(x => x.asDto).ToList();
        return Ok(res);
    }


     [HttpPost]
    public async Task<ActionResult<CustomerDTO>> Create([FromBody] CustomerCreateDTO Data)
    {
         if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
            return BadRequest("Gender value is not recognized");
        var createCustomer = new Customer
        {
          Name = Data.Name,
          Password = Data.Password,
          Gender = Data.Gender,
          DateOfBirth = Data.DateOfBirth,
          Email = Data.Email,
          Mobile = Data.Mobile,
        };
       var createdCustomer = await _customer.Create(createCustomer);

         return StatusCode(StatusCodes.Status201Created,createdCustomer.asDto);

    }

     [HttpPut("{id}")]
    public async Task<ActionResult<CustomerDTO>> Update([FromRoute] long id, [FromBody] CustomerUpdateDTO Data)
    {
        var existing = await _customer.GetById(id);
        if(existing is null)
        return NotFound("No user found with given user id");
        var toUpdateUser = existing with
        {
            Email = Data.Email,
        };
        var didUpdate = await _customer.Update(toUpdateUser);
        if(!didUpdate)
        return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");
        return NoContent();
    }

}
     
