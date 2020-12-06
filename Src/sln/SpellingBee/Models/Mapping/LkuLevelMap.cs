using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SpellingBee.Model.Models.Mapping
{
    public class LkuLevelMap : EntityTypeConfiguration<LkuLevel>
    {
        public LkuLevelMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("LkuLevel");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Desc).HasColumnName("Desc");
        }
    }
}
