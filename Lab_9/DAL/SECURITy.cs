namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SECURITIES")]
    public partial class SECURITy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SECURITy()
        {
            DEALs = new HashSet<DEAL>();
            PROFITs = new HashSet<PROFIT>();
        }

        [Key]
        public int securities_id { get; set; }

        [Required]
        [StringLength(255)]
        public string securities_name { get; set; }

        [Column(TypeName = "money")]
        public decimal securities_purchase_rate { get; set; }

        [Column(TypeName = "money")]
        public decimal securities_sales_rate { get; set; }

        public int securities_amount { get; set; }

        [StringLength(1)]
        public string securities_owner { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEAL> DEALs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROFIT> PROFITs { get; set; }
    }
}
