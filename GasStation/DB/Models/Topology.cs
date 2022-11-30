using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation
{
    public class Topology
    {
        public int ID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [Required]
        public string Construction { get; set; }
    }
}
