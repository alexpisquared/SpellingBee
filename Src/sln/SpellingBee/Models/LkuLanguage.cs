using System;
using System.Collections.Generic;

namespace SpellingBee.Model.Models
{
  [System.ComponentModel.DataAnnotations.Schema.Table("SpB.LkuLanguage")]
  public class LkuLanguage
    {
        public LkuLanguage()
        {
            this.Problems = new List<Problem>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public virtual ICollection<Problem> Problems { get; set; }
    }
}
