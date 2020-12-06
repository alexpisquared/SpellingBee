using SpellingBee.Model.Models;
using System.Data.Entity;

namespace SpellingBee.DbIni
{
	public class SpbDbContext_FOR_DB_INI_ONLY : DbContext
	{
		public SpbDbContext_FOR_DB_INI_ONLY() : base("zDbIni05") { }

		public DbSet<LkuLevel> Levels { get; set; }
		public DbSet<LkuLanguage> Languages { get; set; }
		public DbSet<LkuSubject> Subjects { get; set; }
		public DbSet<Player> Players { get; set; }
		public DbSet<Problem> Problems { get; set; }
		public DbSet<Audition> Auditions { get; set; }
		public DbSet<TombStone> TombStones { get; set; }
		
		void foo()
		{

		}

		//needs redoing DS: 
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
		}

	}

}
