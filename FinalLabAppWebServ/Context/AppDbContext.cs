using FinalLabAppWebServ.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalLabAppWebServ.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customers>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Products>()
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<Products>()
                .Property(p => p.ProductPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetails>()
                .HasKey(od => od.OrderDetailId);

            modelBuilder.Entity<OrderDetails>()
                .Property(od => od.ProductPrice)
                .HasPrecision(18, 2);
        }
        public DbSet<FinalLabAppWebServ.Entities.Register> Register { get; set; } = default!;
    }
}
