//using AAV.Sys.Helpers;
using AAV.Sys.Helpers;
using SpellingBee.Db.Properties;
using System.Data.Entity;

namespace SpellingBee.Db.DbModel
{
  public partial class A0DbContext : DbContext
  {
    public static A0DbContext Create() => new A0DbContext();
    A0DbContext() : base(_dbName = SqlConStrHelper.ConStr("OneBase", _dbgRls, _dbLocation, Settings.Default.ReadOnlyUsr, Settings.Default.ReadOnlyKey)) { }

    static string _dbName = "Unknown yet: use dbx once to see.";
    public static string DbNameOnly => SqlConStrHelper.DbNameFind(_dbName);

    const string _dbgRls =
#if DEBUG
          "";//"Dbg";
#else
          "";//"Rls";
#endif

    const SqlConStrHelper.DbLocation _dbLocation =
#if AZURE_IS_AFFORDABLE
      SqlConStrHelper.DbLocation.Azure; // need to make invoices from Ofc!!!   /// May 23, 2019: apparently, TimeTrackDb..._GP (gen.purp. DB) takes $2/day !!! ...but if it is once a month, then it is better than $.10/day. Final decision pending on either auto stop after 6 hr works its miracle.
#elif ONEDRIVE_LOCALDB
      SqlConStrHelper.DbLocation.Local; // need to make invoices from Ofc!!!
#else //SQL_DB_INSTANCE
    SqlConStrHelper.DbLocation.DbIns;   // keep as a fallback for dev-t (codefirst/datafirst model gen, etc.)
#endif
  }
}