using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Monkeys.Models;

public partial class MonkeysContext : DbContext
{
    public MonkeysContext()
    {
    }

    public MonkeysContext(DbContextOptions<MonkeysContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookGenre> BookGenres { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Authors_pkey");

            entity.ToTable("Authors", "Monkeys");

            entity.Property(e => e.FirstName).HasMaxLength(60);
            entity.Property(e => e.LastName).HasMaxLength(60);
            entity.Property(e => e.MiddleName).HasMaxLength(60);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Books_pkey");

            entity.ToTable("Books", "Monkeys");

            entity.Property(e => e.BookName).HasMaxLength(100);
            entity.Property(e => e.PopularityRating).HasDefaultValueSql("0");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_To_Author");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_To_Publisher");
        });

        modelBuilder.Entity<BookGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Book_Genre_pkey");

            entity.ToTable("Book_Genre", "Monkeys");

            entity.HasOne(d => d.Book).WithMany(p => p.BookGenres)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK_Book_Genre_To_Book");

            entity.HasOne(d => d.Genre).WithMany(p => p.BookGenres)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_Book_Genre_To_Genre");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Genre_pkey");

            entity.ToTable("Genre", "Monkeys");

            entity.Property(e => e.GenreName).HasMaxLength(30);
            entity.Property(e => e.IsChecked).HasDefaultValueSql("false");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Publishers_pkey");

            entity.ToTable("Publishers", "Monkeys");

            entity.Property(e => e.PublisherName).HasMaxLength(35);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
