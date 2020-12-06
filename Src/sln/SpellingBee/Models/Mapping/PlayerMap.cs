using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SpellingBee.Model.Models.Mapping
{
    public class PlayerMap : EntityTypeConfiguration<Player>
    {
        public PlayerMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Player");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Desc).HasColumnName("Desc");
            this.Property(t => t.AddedAt).HasColumnName("AddedAt");
            this.Property(t => t.AddedBy).HasColumnName("AddedBy");
            this.Property(t => t.DeletedAt).HasColumnName("DeletedAt");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
        }
    }
}
