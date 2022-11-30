using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.DB
{
    public class User
    {
        public int ID { get; set; }
        [Required]
    
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserType UserRole { get; set; }
    }
    public enum UserType
    {
        Admin,
        Moderator
    }
}
