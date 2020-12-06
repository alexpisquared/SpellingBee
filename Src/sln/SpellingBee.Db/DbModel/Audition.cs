namespace SpellingBee.Db.DbModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpB.Audition")]
    public partial class Audition
    {
        public int ID { get; set; }

        public string PlayerAnswer { get; set; }

        public int ReReadCount { get; set; }

        public double TimeToAnswerSec { get; set; }

        public bool IsCorrect { get; set; }

        public bool? IsBadSaying { get; set; }

        public DateTime DoneAt { get; set; }

        public string DoneBy { get; set; }

        public int Problem_ID { get; set; }

        [StringLength(128)]
        public string Player_ID { get; set; }

        public virtual Player Player { get; set; }

        public virtual Problem Problem { get; set; }
    }
}
