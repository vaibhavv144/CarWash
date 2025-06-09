using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ApplicationUser : IdentityUser
{
    public bool IsAvailable { get; set; } = true;
    public bool IsActive { get; set; } = true;
    public ICollection<Car> Cars { get; set; }
    public ICollection<Order> Orders { get; set; }

    public ApplicationUser()
    {
        Cars = new List<Car>();
        Orders = new List<Order>();
    }
}
