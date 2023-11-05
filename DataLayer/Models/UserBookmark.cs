using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class UserBookmark
{
    public int? UserId { get; set; }

    public string? TConst { get; set; }

    public string? NConst { get; set; }

    public string? BookmarkComment { get; set; }

    public DateTime? Timestamp { get; set; }

    public int BookmarkId { get; set; }
}
