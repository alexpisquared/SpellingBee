namespace SpellingBee.Db.DbModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpB.Player")]
    public partial class Player
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            Auditions = new HashSet<Audition>();
            TombStones = new HashSet<TombStone>();
        }

        public string ID { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string Desc { get; set; }

        public DateTime AddedAt { get; set; }

        public string AddedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string DeletedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Audition> Auditions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TombStone> TombStones { get; set; }
    }
}
