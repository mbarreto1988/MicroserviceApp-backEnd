namespace FinalLabAppWebServ.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public Customers? Customer { get; set; }
        public int ProductId { get; set; }
        public Products? Product { get; set; }
        public int OrderQuantity { get; set; }
        public DateTime OrderRegistrationDate { get; set; } = DateTime.Now;
    }
}
