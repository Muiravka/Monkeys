using System;
using System.Collections.Generic;

namespace Monkeys.Models;

public partial class Book
{
    public int Id { get; set; }

    public string BookName { get; set; } = null!;

    public DateOnly PublicationYear { get; set; }

    public int PublisherId { get; set; }

    public int AuthorId { get; set; }

    public int? PopularityRating { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();

    public virtual Publisher Publisher { get; set; } = null!;
}
