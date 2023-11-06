using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class User
{
    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? UserName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public int Id { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsAccountDeactivated { get; set; }

}
