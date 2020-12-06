using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SpellingBee.Model.Models.Mapping
{
    public class TombStoneMap : EntityTypeConfiguration<TombStone>
    {
        public TombStoneMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Player_ID)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("TombStone");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.User).HasColumnName("User");
            this.Property(t => t.Player_ID).HasColumnName("Player_ID");

            // Relationships
            this.HasOptional(t => t.Player)
                .WithMany(t => t.TombStones)
                .HasForeignKey(d => d.Player_ID);

        }
    }
}
