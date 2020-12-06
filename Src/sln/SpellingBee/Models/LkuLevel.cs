using System;
using System.Collections.Generic;

namespace SpellingBee.Model.Models
{
  [System.ComponentModel.DataAnnotations.Schema.Table("SpB.LkuLevel")]
  public class LkuLevel
    {
        public LkuLevel()
        {
            this.Problems = new List<Problem>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public virtual ICollection<Problem> Problems { get; set; }
    }
}
