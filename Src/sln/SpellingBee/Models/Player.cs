using System;
using System.Collections.Generic;

namespace SpellingBee.Model.Models
{
  [System.ComponentModel.DataAnnotations.Schema.Table("SpB.Player")]
  public class Player
    {
        public Player()
        {
            this.Auditions = new List<Audition>();
            this.TombStones = new List<TombStone>();
        }

        public string ID { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Desc { get; set; }
        public System.DateTime AddedAt { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public virtual ICollection<Audition> Auditions { get; set; }
        public virtual ICollection<TombStone> TombStones { get; set; }
    }
}
