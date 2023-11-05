using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class SearchHistory
{
    public string? UserId { get; set; }

    public string? TConst { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? SearchInput { get; set; }

    public virtual TitleExtended? TConstNavigation { get; set; }
}
