using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.Models;

public class AppUser:IdentityUser
{
  
    public ICollection<Order> Orders { get; set; }
}
