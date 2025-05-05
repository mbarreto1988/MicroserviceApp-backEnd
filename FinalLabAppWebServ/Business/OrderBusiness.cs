using FinalLabAppWebServ.DAL.Context;
using FinalLabAppWebServ.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalLabAppWebServ.Business
{
    public class OrderBusiness
    {
        private readonly AppDbContext _context;

        public OrderBusiness(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<object?> CreateAsync(Order order)
        {
            var product = await _context.Products.FindAsync(order.ProductId);
            var customer = await _context.Customers.FindAsync(order.CustomerId);

            if (product == null) return "Producto no encontrado";
            if (customer == null) return "Cliente no encontrado";
            if (product.ProductStock == 0) return "Sin stock";
            if (order.OrderQuantity > product.ProductStock) return "No hay stock suficiente";

            product.ProductStock -= order.OrderQuantity;

            _context.Entry(order).Property(o => o.ProductId).IsModified = false;
            _context.Entry(order).Property(o => o.CustomerId).IsModified = false;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderDetails = new OrderDetails
            {
                CustomerName = customer.CustomerName,
                CustomerDni = customer.CustomerDni,
                CustomerEmail = customer.CustomerEmail,
                CustomerAddress = customer.CustomerAddress,
                CustomerRegistrationDate = customer.CustomerRegistrationDate,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                ProductQuantity = order.OrderQuantity
            };

            _context.OrderDetails.Add(orderDetails);
            await _context.SaveChangesAsync();

            return new
            {
                OrderId = order.OrderId,
                OrderQuantity = order.OrderQuantity,
                TotalPrice = orderDetails.TotalPrice,
                Customer = new
                {
                    customer.CustomerName,
                    customer.CustomerEmail,
                    customer.CustomerAddress,
                    customer.CustomerDni,
                    customer.CustomerRegistrationDate
                },
                Product = new
                {
                    product.ProductName,
                    product.ProductDescription,
                    product.ProductPrice
                }
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(e => e.OrderId == id);
        }
    }
}
