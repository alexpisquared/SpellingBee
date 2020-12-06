using System;
using System.Collections.Generic;

namespace SpellingBee.Model.Models
{
  [System.ComponentModel.DataAnnotations.Schema.Table("SpB.TombStone")]
  public class TombStone
    {
        public string ID { get; set; }
        public string User { get; set; }
        public string Player_ID { get; set; }
        public virtual Player Player { get; set; }
    }
}
