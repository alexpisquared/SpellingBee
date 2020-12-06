namespace SpellingBee.Db.DbModel
{
  using System;
  using System.Data.Entity;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;

  public partial class A0DbContext : DbContext
  {
    //public A0DbContext()        : base("name=A0DbContext")    {    }

    public virtual DbSet<Audition> Auditions { get; set; }
    public virtual DbSet<LkuLanguage> LkuLanguages { get; set; }
    public virtual DbSet<LkuLevel> LkuLevels { get; set; }
    public virtual DbSet<LkuSubject> LkuSubjects { get; set; }
    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<Problem> Problems { get; set; }
    public virtual DbSet<TombStone> TombStones { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<LkuLanguage>()
          .HasMany(e => e.Problems)
          .WithOptional(e => e.LkuLanguage)
          .HasForeignKey(e => e.Language_ID);

      modelBuilder.Entity<LkuLevel>()
          .HasMany(e => e.Problems)
          .WithOptional(e => e.LkuLevel)
          .HasForeignKey(e => e.Level_ID);

      modelBuilder.Entity<Player>()
          .HasMany(e => e.Auditions)
          .WithOptional(e => e.Player)
          .HasForeignKey(e => e.Player_ID);

      modelBuilder.Entity<Player>()
          .HasMany(e => e.TombStones)
          .WithOptional(e => e.Player)
          .HasForeignKey(e => e.Player_ID);

      modelBuilder.Entity<Problem>()
          .HasMany(e => e.Auditions)
          .WithRequired(e => e.Problem)
          .HasForeignKey(e => e.Problem_ID);
    }
  }
}
