namespace SpellingBee.Db.DbModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpB.Problem")]
    public partial class Problem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Problem()
        {
            Auditions = new HashSet<Audition>();
        }

        public int ID { get; set; }

        public string ProblemText { get; set; }

        public string SolutionText { get; set; }

        public string HintMessage { get; set; }

        public string BatchSource { get; set; }

        public string Notes { get; set; }

        public int Grade { get; set; }

        public DateTime AddedAt { get; set; }

        public string AddedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string DeletedBy { get; set; }

        public int? Level_ID { get; set; }

        public int? Language_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Audition> Auditions { get; set; }

        public virtual LkuLanguage LkuLanguage { get; set; }

        public virtual LkuLevel LkuLevel { get; set; }
    }
}
