using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.DB
{
    public class Transport
    {
        public int ID { get; set; }
        [Required]
       
        public int FuelVolume { get; set; }
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [Required]
      
        public int FuelID { get; set; }
        [ForeignKey("FuelID")]
        public Fuel Fuel { get; set; }


    }
}
