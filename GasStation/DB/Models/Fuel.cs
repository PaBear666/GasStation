using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.DB
{
    public class Fuel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        public string Type { get; set; }
        [Required]
        public double  Cost { get; set; }
    }
}
