using FinalLabAppWebServ.DAL.Context;
using FinalLabAppWebServ.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalLabAppWebServ.Business
{
    public class CustomerBusiness
    {
        private readonly AppDbContext _context;

        public CustomerBusiness(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customers>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customers?> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customers> CreateAsync(Customers customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> UpdateAsync(int id, Customers customer)
        {
            if (id != customer.CustomerId) return false;

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Customers.AnyAsync(e => e.CustomerId == id))
                    return false;
                else
                    throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
