using CiberCommon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiberCommon.Model
{
    [Table("Customers")]
    public  class Customer: EntityDto<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Order> Orders { get; set; }  
    }
}
