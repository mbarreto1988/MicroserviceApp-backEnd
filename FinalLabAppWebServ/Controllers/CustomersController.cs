using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalLabAppWebServ.DAL.Context;
using FinalLabAppWebServ.Business;
using FinalLabAppWebServ.DAL.Entities;

namespace FinalLabAppWebServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerBusiness _business;

        public CustomersController(CustomerBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
        {
            return await _business.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customers>> GetCustomer(int id)
        {
            var customer = await _business.GetByIdAsync(id);
            if (customer == null)
                return NotFound("Cliente inexistente");
            return customer;
        }

        [HttpPost]
        public async Task<ActionResult<Customers>> PostCustomer(Customers customer)
        {
            var created = await _business.CreateAsync(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = created.CustomerId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customers customer)
        {
            var result = await _business.UpdateAsync(id, customer);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _business.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
