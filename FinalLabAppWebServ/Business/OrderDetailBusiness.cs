using FinalLabAppWebServ.Context;
using FinalLabAppWebServ.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalLabAppWebServ.Business
{
    public class OrderDetailBusiness
    {
        private readonly AppDbContext _context;

        public OrderDetailBusiness(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDetails>> GetAllAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetails?> GetByIdAsync(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        }
    }
}
