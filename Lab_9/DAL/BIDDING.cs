namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BIDDING")]
    public partial class BIDDING
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BIDDING()
        {
            DEALs = new HashSet<DEAL>();
            PROFITs = new HashSet<PROFIT>();
        }

        [Key]
        public int bidding_id { get; set; }

        public int? order_id { get; set; }

        public int? broker_id { get; set; }

        public int? client_id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime bidding_date { get; set; }

        public virtual BROKER BROKER { get; set; }

        public virtual CLIENT CLIENT { get; set; }

        public virtual ORDERR ORDERR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEAL> DEALs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROFIT> PROFITs { get; set; }
    }
}
