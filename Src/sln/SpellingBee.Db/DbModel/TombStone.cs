namespace SpellingBee.Db.DbModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpB.TombStone")]
    public partial class TombStone
    {
        public string ID { get; set; }

        public string User { get; set; }

        [StringLength(128)]
        public string Player_ID { get; set; }

        public virtual Player Player { get; set; }
    }
}
