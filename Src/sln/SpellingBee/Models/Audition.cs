using System;
using System.Collections.Generic;

namespace SpellingBee.Model.Models
{
  [System.ComponentModel.DataAnnotations.Schema.Table("SpB.Audition")]
    public class Audition
    {
        public int ID { get; set; }
        public string PlayerAnswer { get; set; }
        public int ReReadCount { get; set; }
        public double TimeToAnswerSec { get; set; }
        public bool IsCorrect { get; set; }
        public Nullable<bool> IsBadSaying { get; set; }
        public System.DateTime DoneAt { get; set; }
        public string DoneBy { get; set; }
        public int Problem_ID { get; set; }
        public string Player_ID { get; set; }
        public virtual Player Player { get; set; }
        public virtual Problem Problem { get; set; }
    }
}
