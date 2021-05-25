namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CLIENT")]
    public partial class CLIENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENT()
        {
            BIDDINGs = new HashSet<BIDDING>();
            DEALs = new HashSet<DEAL>();
            ORDERRs = new HashSet<ORDERR>();
            PROFITs = new HashSet<PROFIT>();
        }

        [Key]
        public int client_id { get; set; }

        [Required]
        [StringLength(255)]
        public string client_surname { get; set; }

        [Required]
        [StringLength(255)]
        public string client_name { get; set; }

        [Required]
        [StringLength(255)]
        public string client_patronymic { get; set; }

        [Required]
        [StringLength(20)]
        public string client_tel { get; set; }

        [Required]
        [StringLength(320)]
        public string client_email { get; set; }

        [Column(TypeName = "money")]
        public decimal client_fortune { get; set; }

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
