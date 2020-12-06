using AAV.Sys.Helpers;
using SpellingBee.Model.Models.Mapping;
using System;
using System.Data.Entity;

namespace SpellingBee.Model.Models
{
//  [Obsolete("use  SpellingBee.Db  instead", true)]
//  public partial class SpellBeeDbContext : DbContext
//  {
//    public static SpellBeeDbContext Create() => new SpellBeeDbContext();
//    SpellBeeDbContext() : base(_dbName = SqlConStrHelper.ConStr("OneBase", _dbgRls, _dbLocation, "","")) { }

//    static string _dbName = "Unknown yet: use dbx once to see.";
//    public static string DbNameOnly => SqlConStrHelper.DbNameFind(_dbName);

//    const string _dbgRls =
//#if DEBUG
//          "";//"Dbg";
//#else
//          "";//"Rls";
//#endif

//    const SqlConStrHelper.DbLocation _dbLocation =
//#if AZURE_IS_AFFORDABLE                             
//      SqlConStrHelper.DbLocation.Azure; // need to make invoices from Ofc!!!   /// May 23, 2019: apparently, TimeTrackDb..._GP (gen.purp. DB) takes $2/day !!! ...but if it is once a month, then it is better than $.10/day. Final decision pending on either auto stop after 6 hr works its miracle.
//#elif ONEDRIVE_LOCALDB                              
//      SqlConStrHelper.DbLocation.Local; // need to make invoices from Ofc!!!
//#else //SQL_DB_INSTANCE
//    SqlConStrHelper.DbLocation.DbIns;   // keep as a fallback for dev-t (codefirst/datafirst model gen, etc.)
//#endif
//  }

  //public partial class SpellBeeDbContext : DbContext
  //{
  //  // public SpellBeeDbContext() => Database.Connection.ConnectionString = $@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename={App.Dbfn};Integrated Security=True;Connect Timeout=5;";

  //  public DbSet<Audition> Auditions { get; set; }
  //  public DbSet<LkuLanguage> LkuLanguages { get; set; }
  //  public DbSet<LkuLevel> LkuLevels { get; set; }
  //  public DbSet<LkuSubject> LkuSubjects { get; set; }
  //  public DbSet<Player> Players { get; set; }
  //  public DbSet<Problem> Problems { get; set; }
  //  public DbSet<TombStone> TombStones { get; set; }

  //  protected override void OnModelCreating(DbModelBuilder modelBuilder)
  //  {
  //    modelBuilder.Configurations.Add(new AuditionMap());
  //    modelBuilder.Configurations.Add(new LkuLanguageMap());
  //    modelBuilder.Configurations.Add(new LkuLevelMap());
  //    modelBuilder.Configurations.Add(new LkuSubjectMap());
  //    modelBuilder.Configurations.Add(new PlayerMap());
  //    modelBuilder.Configurations.Add(new ProblemMap());
  //    modelBuilder.Configurations.Add(new TombStoneMap());
  //  }
  //}
}

