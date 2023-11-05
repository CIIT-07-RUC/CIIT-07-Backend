using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class Wi
{
    public string Tconst { get; set; } = null!;

    public string Word { get; set; } = null!;

    public char Field { get; set; }

    public string? Lexeme { get; set; }
}
