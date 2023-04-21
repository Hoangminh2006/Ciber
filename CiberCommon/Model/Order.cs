using CiberCommon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiberCommon.Model
{
    [Table("Orders")]
    public class Order:EntityDto<int>
    {
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")] 
        public Customer Customer { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public decimal Amount { get; set; }
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
