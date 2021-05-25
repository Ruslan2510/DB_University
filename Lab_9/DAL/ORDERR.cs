namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ORDERR")]
    public partial class ORDERR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDERR()
        {
            BIDDINGs = new HashSet<BIDDING>();
            DEALs = new HashSet<DEAL>();
            PROFITs = new HashSet<PROFIT>();
        }

        [Key]
        public int order_id { get; set; }

        public int? broker_id { get; set; }

        public int? client_id { get; set; }

        [StringLength(255)]
        public string order_comment { get; set; }

        [Required]
        [StringLength(10)]
        public string order_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BIDDING> BIDDINGs { get; set; }

        public virtual BROKER BROKER { get; set; }

        public virtual CLIENT CLIENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEAL> DEALs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROFIT> PROFITs { get; set; }
    }
}
