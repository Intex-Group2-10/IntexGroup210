using System;
using System.Collections.Generic;

namespace IntexGroup210.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool TwoFactor { get; set; }

    public bool Admin { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
