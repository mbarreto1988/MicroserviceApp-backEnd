using FinalLabAppWebServ.Entities;

namespace FinalLabAppWebServ.Entities
{
    public class OrderDetails
    {
        public int OrderDetailId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDni { get; set; }
        public  string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime CustomerRegistrationDate { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public decimal TotalPrice => ProductPrice * ProductQuantity;
    }
}
