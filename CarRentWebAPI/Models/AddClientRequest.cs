using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentWebAPI.Models
{
    public class AddClientRequest
    {
       public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
