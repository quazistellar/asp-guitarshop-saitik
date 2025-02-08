using System;
using System.Collections.Generic;

namespace guitarshop.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string LoginUser { get; set; } = null!;

    public string PasswordUser { get; set; } = null!;

    public string NameClient { get; set; } = null!;

    public string SurnameClient { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PatronimClient { get; set; }

    public int IdRole { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
