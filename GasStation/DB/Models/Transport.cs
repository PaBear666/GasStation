using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.DB
{
    [Serializable]
    public class Transport
    {
        [Key]
        public int ID { get; set; }
        [Required]
       
        public int FuelVolume { get; set; }
      
        public string Name { get; set; }

     
        public int? FuelId { get; set; }

        public virtual Fuel Fuel { get; set; }


    }
}
