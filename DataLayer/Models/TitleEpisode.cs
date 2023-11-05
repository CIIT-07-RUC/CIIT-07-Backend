using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class TitleEpisode
{
    public string? TConst { get; set; }

    public string? ParentTconst { get; set; }

    public int? SeasonNumber { get; set; }

    public int? EpisodeNumber { get; set; }
}
