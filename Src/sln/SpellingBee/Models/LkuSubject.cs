using System;
using System.Collections.Generic;

namespace SpellingBee.Model.Models
{
  [System.ComponentModel.DataAnnotations.Schema.Table("SpB.LkuSubject")]
  public class LkuSubject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}
