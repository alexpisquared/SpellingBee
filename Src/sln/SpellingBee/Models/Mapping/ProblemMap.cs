using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SpellingBee.Model.Models.Mapping
{
    public class ProblemMap : EntityTypeConfiguration<Problem>
    {
        public ProblemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Problem");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ProblemText).HasColumnName("ProblemText");
            this.Property(t => t.SolutionText).HasColumnName("SolutionText");
            this.Property(t => t.HintMessage).HasColumnName("HintMessage");
            this.Property(t => t.BatchSource).HasColumnName("BatchSource");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.Grade).HasColumnName("Grade");
            this.Property(t => t.AddedAt).HasColumnName("AddedAt");
            this.Property(t => t.AddedBy).HasColumnName("AddedBy");
            this.Property(t => t.DeletedAt).HasColumnName("DeletedAt");
            this.Property(t => t.DeletedBy).HasColumnName("DeletedBy");
            this.Property(t => t.Level_ID).HasColumnName("Level_ID");
            this.Property(t => t.Language_ID).HasColumnName("Language_ID");

            // Relationships
            this.HasOptional(t => t.LkuLanguage)
                .WithMany(t => t.Problems)
                .HasForeignKey(d => d.Language_ID);
            this.HasOptional(t => t.LkuLevel)
                .WithMany(t => t.Problems)
                .HasForeignKey(d => d.Level_ID);

        }
    }
}
