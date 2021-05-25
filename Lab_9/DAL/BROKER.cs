namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BROKER")]
    public partial class BROKER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BROKER()
        {
            BIDDINGs = new HashSet<BIDDING>();
            DEALs = new HashSet<DEAL>();
            ORDERRs = new HashSet<ORDERR>();
            PROFITs = new HashSet<PROFIT>();
        }

        [Key]
        public int broker_id { get; set; }

        [Required]
        [StringLength(255)]
        public string broker_surname { get; set; }

        [Required]
        [StringLength(255)]
        public string broker_name { get; set; }

        [Required]
        [StringLength(255)]
        public string broker_patronymic { get; set; }

        public byte broker_experience { get; set; }

        [Required]
        [StringLength(20)]
        public string broker_tel { get; set; }

        [Required]
        [StringLength(320)]
        public string broker_email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BIDDING> BIDDINGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEAL> DEALs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDERR> ORDERRs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROFIT> PROFITs { get; set; }
    }
}
