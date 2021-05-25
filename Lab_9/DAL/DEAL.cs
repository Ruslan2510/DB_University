namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEAL")]
    public partial class DEAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DEAL()
        {
            PROFITs = new HashSet<PROFIT>();
        }

        [Key]
        public int deal_id { get; set; }

        public int? securities_id { get; set; }

        public int? order_id { get; set; }

        public int? broker_id { get; set; }

        public int? client_id { get; set; }

        public int? bidding_id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime deal_date { get; set; }

        [StringLength(1)]
        public string deal_customer { get; set; }

        [StringLength(10)]
        public string deal_type { get; set; }

        public int? securities_amount { get; set; }

        public virtual BIDDING BIDDING { get; set; }

        public virtual BROKER BROKER { get; set; }

        public virtual CLIENT CLIENT { get; set; }

        public virtual ORDERR ORDERR { get; set; }

        public virtual SECURITy SECURITy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROFIT> PROFITs { get; set; }
    }
}
