using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class DbUser
{
    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? UserName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public int UserId { get; set; }
}
