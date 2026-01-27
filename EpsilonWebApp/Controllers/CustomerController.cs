using EpsilonWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requires JWT for all endpoints
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _context;

    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        => await _context.Customers.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(Guid id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return NotFound();
        return customer;
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
    {
        customer.Id = Guid.NewGuid();
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(Guid id, Customer customer)
    {
        if (id != customer.Id) return BadRequest();

        var existing = await _context.Customers.FindAsync(id);
        if (existing == null) return NotFound();

        existing.CompanyName = customer.CompanyName;
        existing.ContactName = customer.ContactName;
        existing.Address = customer.Address;
        existing.City = customer.City;
        existing.Region = customer.Region;
        existing.PostalCode = customer.PostalCode;
        existing.Country = customer.Country;
        existing.Phone = customer.Phone;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        var existing = await _context.Customers.FindAsync(id);
        if (existing == null) return NotFound();

        _context.Customers.Remove(existing);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
