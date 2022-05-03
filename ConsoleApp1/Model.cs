using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAttribute
{
    public class Model
    {
        [StringLength(6, 10, "Property {0} Length between {1} and {2}")]
        // [Required("{0} is required")]
        public string Name { get; set; }

        [Required("{0} is Required")]
        public string Code { get; set; }

    }
}
