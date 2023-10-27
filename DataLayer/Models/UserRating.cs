using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class UserRating
{
    public int? UserId { get; set; }

    public string? TConst { get; set; }

    public DateTime? Timestamp { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }
}
