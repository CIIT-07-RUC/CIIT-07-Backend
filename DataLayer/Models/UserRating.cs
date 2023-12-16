using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class UserRating
{
    public int UserId { get; set; } = 0!;

    public string TConst { get; set; } = null!;

    public DateTime? Timestamp { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public virtual TitleExtended? TConstNavigation { get; set; }
}
