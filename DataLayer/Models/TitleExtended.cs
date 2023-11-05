using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class TitleExtended
{
    public string TConst { get; set; } = null!;

    public string? TitleType { get; set; }

    public string? PrimaryTitle { get; set; }

    public string? OriginalTitle { get; set; }

    public bool? IsAdult { get; set; }

    public string? StartYear { get; set; }

    public string? EndYear { get; set; }

    public int? RuntimeMinutes { get; set; }

    public string? Genres { get; set; }

    public string? Plot { get; set; }

    public string? Poster { get; set; }

    public decimal? AverageRating { get; set; }

    public int? NumVotes { get; set; }
}
