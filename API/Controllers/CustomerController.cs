using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _repository;
    public CustomerController(ICustomerRepository repository)
    {
        this._repository = repository;
    }
    [HttpGet]
    public async Task<IReadOnlyList<Customer>> Get()
    {
        return await _repository.GetAllAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> Get(Guid id)
    {
        var customer = await _repository.GetByIdAsync(id);
        return customer is null ? NotFound() : Ok(customer);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, Customer customer)
    {
        if (id != customer.Id)
        {
            return BadRequest();
        }
        await _repository.UpdateAsync(customer);
        return NoContent();
    }
    [HttpPost]
    public async Task<ActionResult<Customer>> Post(Customer customer)
    {
        await _repository.AddAsync(customer);
        return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<Customer>> Delete(Guid id)
    {
        var customer = await _repository.GetByIdAsync(id);
        if (customer is null)
        {
            return NotFound();
        }
        await _repository.DeleteAsync(customer);
        return customer;
    }
}