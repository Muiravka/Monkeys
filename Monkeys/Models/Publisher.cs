using System;
using System.Collections.Generic;

namespace Monkeys.Models;

public partial class Publisher
{
    public int Id { get; set; }

    public string PublisherName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
