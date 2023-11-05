using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models;

public partial class ImdbContext : DbContext
{
    public ImdbContext()
    {
    }

    public ImdbContext(DbContextOptions<ImdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActorTitle> ActorTitles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<NameBasic> NameBasics { get; set; }

    public virtual DbSet<NewTable> NewTables { get; set; }

    public virtual DbSet<OmdbDatum> OmdbData { get; set; }

    public virtual DbSet<SearchHistory> SearchHistories { get; set; }

    public virtual DbSet<TitleAka> TitleAkas { get; set; }

    public virtual DbSet<TitleBasic> TitleBasics { get; set; }

    public virtual DbSet<TitleCrew> TitleCrews { get; set; }

    public virtual DbSet<TitleEpisode> TitleEpisodes { get; set; }

    public virtual DbSet<TitleExtended> TitleExtendeds { get; set; }

    public virtual DbSet<TitlePrincipal> TitlePrincipals { get; set; }

    public virtual DbSet<TitleRating> TitleRatings { get; set; }

    public virtual DbSet<UserRating> UserRatings { get; set; }

    public virtual DbSet<Wi> Wis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=imdb;User Id=postgres;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActorTitle>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("actor_titles");

            entity.Property(e => e.Insert)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("insert");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("db_user");

            entity.HasKey(e => e.Id); // Specify the primary key
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
                .ValueGeneratedOnAdd()
                .HasColumnName("user_id");
            entity.Property(e => e.UserName).HasColumnName("user_name");
        });

        modelBuilder.Entity<NameBasic>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("name_basics");

            entity.Property(e => e.Birthyear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("birthyear");
            entity.Property(e => e.Deathyear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("deathyear");
            entity.Property(e => e.Knownfortitles)
                .HasMaxLength(256)
                .HasColumnName("knownfortitles");
            entity.Property(e => e.NameRating)
                .HasPrecision(10, 2)
                .HasColumnName("name_rating");
            entity.Property(e => e.Nconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("nconst");
            entity.Property(e => e.Primaryname)
                .HasMaxLength(256)
                .HasColumnName("primaryname");
            entity.Property(e => e.Primaryprofession)
                .HasMaxLength(256)
                .HasColumnName("primaryprofession");
        });

        modelBuilder.Entity<NewTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("new_table");

            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.Primaryname)
                .HasMaxLength(256)
                .HasColumnName("primaryname");
        });

        modelBuilder.Entity<OmdbDatum>(entity =>
        {
            entity.HasKey(e => e.Tconst).HasName("omdb_data_pkey");

            entity.ToTable("omdb_data");

            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
            entity.Property(e => e.Actors)
                .HasMaxLength(256)
                .HasColumnName("actors");
            entity.Property(e => e.Awards)
                .HasMaxLength(80)
                .HasColumnName("awards");
            entity.Property(e => e.Boxoffice)
                .HasMaxLength(100)
                .HasColumnName("boxoffice");
            entity.Property(e => e.Country)
                .HasMaxLength(256)
                .HasColumnName("country");
            entity.Property(e => e.Director).HasColumnName("director");
            entity.Property(e => e.Dvd)
                .HasMaxLength(80)
                .HasColumnName("dvd");
            entity.Property(e => e.Episode)
                .HasMaxLength(80)
                .HasColumnName("episode");
            entity.Property(e => e.Genre)
                .HasMaxLength(80)
                .HasColumnName("genre");
            entity.Property(e => e.Imdbrating)
                .HasMaxLength(80)
                .HasColumnName("imdbrating");
            entity.Property(e => e.Imdbvotes)
                .HasMaxLength(100)
                .HasColumnName("imdbvotes");
            entity.Property(e => e.Language)
                .HasMaxLength(200)
                .HasColumnName("language");
            entity.Property(e => e.Metascore)
                .HasMaxLength(100)
                .HasColumnName("metascore");
            entity.Property(e => e.Plot).HasColumnName("plot");
            entity.Property(e => e.Poster)
                .HasMaxLength(180)
                .HasColumnName("poster");
            entity.Property(e => e.Production)
                .HasMaxLength(80)
                .HasColumnName("production");
            entity.Property(e => e.Rated)
                .HasMaxLength(80)
                .HasColumnName("rated");
            entity.Property(e => e.Ratings)
                .HasMaxLength(180)
                .HasColumnName("ratings");
            entity.Property(e => e.Released)
                .HasMaxLength(80)
                .HasColumnName("released");
            entity.Property(e => e.Response)
                .HasMaxLength(80)
                .HasColumnName("response");
            entity.Property(e => e.Runtime)
                .HasMaxLength(80)
                .HasColumnName("runtime");
            entity.Property(e => e.Season)
                .HasMaxLength(80)
                .HasColumnName("season");
            entity.Property(e => e.Seriesid)
                .HasMaxLength(80)
                .HasColumnName("seriesid");
            entity.Property(e => e.Title)
                .HasMaxLength(256)
                .HasColumnName("title");
            entity.Property(e => e.Totalseasons)
                .HasMaxLength(100)
                .HasColumnName("totalseasons");
            entity.Property(e => e.Type)
                .HasMaxLength(80)
                .HasColumnName("type");
            entity.Property(e => e.Website)
                .HasMaxLength(100)
                .HasColumnName("website");
            entity.Property(e => e.Writer).HasColumnName("writer");
            entity.Property(e => e.Year)
                .HasMaxLength(100)
                .HasColumnName("year");
        });

        modelBuilder.Entity<SearchHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("search_history");

            entity.Property(e => e.SearchInput).HasColumnName("search_input");
            entity.Property(e => e.TConst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("t_const");
            entity.Property(e => e.Timestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("user_id");

            entity.HasOne(d => d.TConstNavigation).WithMany()
                .HasForeignKey(d => d.TConst)
                .HasConstraintName("t_const");
        });

        modelBuilder.Entity<TitleAka>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("title_akas");

            entity.Property(e => e.Attributes)
                .HasMaxLength(256)
                .HasColumnName("attributes");
            entity.Property(e => e.Isoriginaltitle).HasColumnName("isoriginaltitle");
            entity.Property(e => e.Language)
                .HasMaxLength(10)
                .HasColumnName("language");
            entity.Property(e => e.Ordering).HasColumnName("ordering");
            entity.Property(e => e.Region)
                .HasMaxLength(10)
                .HasColumnName("region");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Titleid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("titleid");
            entity.Property(e => e.Types)
                .HasMaxLength(256)
                .HasColumnName("types");
        });

        modelBuilder.Entity<TitleBasic>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("title_basics");

            entity.Property(e => e.Endyear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("endyear");
            entity.Property(e => e.Genres)
                .HasMaxLength(256)
                .HasColumnName("genres");
            entity.Property(e => e.Isadult).HasColumnName("isadult");
            entity.Property(e => e.Originaltitle).HasColumnName("originaltitle");
            entity.Property(e => e.Primarytitle).HasColumnName("primarytitle");
            entity.Property(e => e.Runtimeminutes).HasColumnName("runtimeminutes");
            entity.Property(e => e.Startyear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("startyear");
            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
            entity.Property(e => e.Titletype)
                .HasMaxLength(20)
                .HasColumnName("titletype");
        });

        modelBuilder.Entity<TitleCrew>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("title_crew");

            entity.Property(e => e.Directors).HasColumnName("directors");
            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
            entity.Property(e => e.Writers).HasColumnName("writers");
        });

        modelBuilder.Entity<TitleEpisode>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("title_episode");

            entity.Property(e => e.EpisodeNumber).HasColumnName("episode_number");
            entity.Property(e => e.ParentTconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("parent_tconst");
            entity.Property(e => e.SeasonNumber).HasColumnName("season_number");
            entity.Property(e => e.TConst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("t_const");
        });

        modelBuilder.Entity<TitleExtended>(entity =>
        {
            entity.HasKey(e => e.TConst).HasName("title_extended_pkey");

            entity.ToTable("title_extended");

            entity.Property(e => e.TConst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("t_const");
            entity.Property(e => e.AverageRating)
                .HasPrecision(5, 1)
                .HasColumnName("average_rating");
            entity.Property(e => e.EndYear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("end_year");
            entity.Property(e => e.Genres)
                .HasMaxLength(256)
                .HasColumnName("genres");
            entity.Property(e => e.IsAdult).HasColumnName("is_adult");
            entity.Property(e => e.NumVotes).HasColumnName("num_votes");
            entity.Property(e => e.OriginalTitle).HasColumnName("original_title");
            entity.Property(e => e.Plot).HasColumnName("plot");
            entity.Property(e => e.Poster).HasColumnName("poster");
            entity.Property(e => e.PrimaryTitle).HasColumnName("primary_title");
            entity.Property(e => e.RuntimeMinutes).HasColumnName("runtime_minutes");
            entity.Property(e => e.StartYear)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("start_year");
            entity.Property(e => e.TitleType)
                .HasMaxLength(20)
                .HasColumnName("title_type");
        });

        modelBuilder.Entity<TitlePrincipal>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("title_principals");

            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Characters).HasColumnName("characters");
            entity.Property(e => e.Job).HasColumnName("job");
            entity.Property(e => e.Nconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("nconst");
            entity.Property(e => e.Ordering).HasColumnName("ordering");
            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
        });

        modelBuilder.Entity<TitleRating>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("title_ratings");

            entity.Property(e => e.Averagerating)
                .HasPrecision(5, 1)
                .HasColumnName("averagerating");
            entity.Property(e => e.Numvotes).HasColumnName("numvotes");
            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
        });

        modelBuilder.Entity<UserRating>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("user_rating");

            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.TConst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("t_const");
            entity.Property(e => e.Timestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("user_id");

            entity.HasOne(d => d.TConstNavigation).WithMany()
                .HasForeignKey(d => d.TConst)
                .HasConstraintName("t_const");
        });

        modelBuilder.Entity<Wi>(entity =>
        {
            entity.HasKey(e => new { e.Tconst, e.Word, e.Field }).HasName("wi_pkey");

            entity.ToTable("wi");

            entity.Property(e => e.Tconst)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tconst");
            entity.Property(e => e.Word).HasColumnName("word");
            entity.Property(e => e.Field)
                .HasMaxLength(1)
                .HasColumnName("field");
            entity.Property(e => e.Lexeme).HasColumnName("lexeme");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
