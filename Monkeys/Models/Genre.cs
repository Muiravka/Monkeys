using System;
using System.Collections.Generic;

namespace Monkeys.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string GenreName { get; set; } = null!;

    public bool? IsChecked { get; set; }

    public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
}
