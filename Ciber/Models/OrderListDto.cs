using Microsoft.Build.Framework;

namespace Ciber.Models
{
    public class OrderListDto
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDateStr => OrderDate.ToString("dd-MMM-yyyy");
        public decimal Amount { get; set; }
    }
    public class OrderInsertInput
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public string OrderName { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
