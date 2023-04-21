using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiberCommon.Common
{
    public class EntityDto<TPrimary>
    {
        [Key]
        public TPrimary Id { get; set; }
    }
}
