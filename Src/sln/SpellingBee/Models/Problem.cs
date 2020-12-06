using System;
using System.Collections.Generic;

namespace SpellingBee.Model.Models
{
  [System.ComponentModel.DataAnnotations.Schema.Table("SpB.Problem")]
  public class Problem
    {
        public Problem()
        {
            this.Auditions = new List<Audition>();
        }

        public int ID { get; set; }
        public string ProblemText { get; set; }
        public string SolutionText { get; set; }
        public string HintMessage { get; set; }
        public string BatchSource { get; set; }
        public string Notes { get; set; }
        public int Grade { get; set; }
        public System.DateTime AddedAt { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<int> Level_ID { get; set; }
        public Nullable<int> Language_ID { get; set; }
        public virtual ICollection<Audition> Auditions { get; set; }
        public virtual LkuLanguage LkuLanguage { get; set; }
        public virtual LkuLevel LkuLevel { get; set; }
    }
}
