using CiberCommon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiberCommon.Model
{
    [Table("Products")]
    public class Product:EntityDto<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public decimal Quantity { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
