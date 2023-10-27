using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models;

public partial class Cit07Context : DbContext
{
    public Cit07Context()
    {
    }

    public Cit07Context(DbContextOptions<Cit07Context> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<SearchHistory> SearchHistories { get; set; }

    public virtual DbSet<UserBookmark> UserBookmarks { get; set; }

    public virtual DbSet<UserRating> UserRatings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=cit.ruc.dk;Database=cit07;Username=cit07;Password=GdSpVBqksHbh");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("db_user");

            entity.HasIndex(e => e.Email, "db_user_email_idx");

            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('db_user_user_id_seq1'::regclass)")
                .HasColumnName("user_id");
            entity.Property(e => e.UserName).HasColumnName("user_name");
        });

        modelBuilder.Entity<SearchHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("search_history");

            entity.HasIndex(e => e.TConst, "search_history_t_const_idx");

            entity.HasIndex(e => e.UserId, "search_history_user_id_idx");

            entity.Property(e => e.SearchInput).HasColumnName("search_input");
            entity.Property(e => e.TConst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("t_const");
            entity.Property(e => e.Timestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<UserBookmark>(entity =>
        {
            entity.HasKey(e => e.BookmarkId).HasName("user_bookmark_pkey");

            entity.ToTable("user_bookmark");

            entity.HasIndex(e => e.NConst, "user_bookmark_n_const_idx");

            entity.HasIndex(e => e.TConst, "user_bookmark_t_const_idx");

            entity.HasIndex(e => e.UserId, "user_bookmark_user_id_idx");

            entity.Property(e => e.BookmarkId).HasColumnName("bookmark_id");
            entity.Property(e => e.BookmarkComment).HasColumnName("bookmark_comment");
            entity.Property(e => e.NConst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("n_const");
            entity.Property(e => e.TConst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("t_const");
            entity.Property(e => e.Timestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<UserRating>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("user_rating");

            entity.HasIndex(e => e.TConst, "user_rating_t_const_idx");

            entity.HasIndex(e => e.UserId, "user_rating_user_id_idx");

            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.TConst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("t_const");
            entity.Property(e => e.Timestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
