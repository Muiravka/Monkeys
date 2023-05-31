using System;
using System.Collections.Generic;

namespace Monkeys.Models;

public partial class Author
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
