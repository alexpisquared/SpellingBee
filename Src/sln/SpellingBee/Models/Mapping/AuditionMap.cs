using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SpellingBee.Model.Models.Mapping
{
    public class AuditionMap : EntityTypeConfiguration<Audition>
    {
        public AuditionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Player_ID)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Audition");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.PlayerAnswer).HasColumnName("PlayerAnswer");
            this.Property(t => t.ReReadCount).HasColumnName("ReReadCount");
            this.Property(t => t.TimeToAnswerSec).HasColumnName("TimeToAnswerSec");
            this.Property(t => t.IsCorrect).HasColumnName("IsCorrect");
            this.Property(t => t.IsBadSaying).HasColumnName("IsBadSaying");
            this.Property(t => t.DoneAt).HasColumnName("DoneAt");
            this.Property(t => t.DoneBy).HasColumnName("DoneBy");
            this.Property(t => t.Problem_ID).HasColumnName("Problem_ID");
            this.Property(t => t.Player_ID).HasColumnName("Player_ID");

            // Relationships
            this.HasOptional(t => t.Player)
                .WithMany(t => t.Auditions)
                .HasForeignKey(d => d.Player_ID);
            this.HasRequired(t => t.Problem)
                .WithMany(t => t.Auditions)
                .HasForeignKey(d => d.Problem_ID);

        }
    }
}
