namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PROFIT")]
    public partial class PROFIT
    {
        [Key]
        public int profit_id { get; set; }

        public int? broker_id { get; set; }

        public int? securities_id { get; set; }

        public int? order_id { get; set; }

        public int? client_id { get; set; }

        public int? bidding_id { get; set; }

        public int? deal_id { get; set; }

        [Column(TypeName = "money")]
        public decimal profit_amount { get; set; }

        public virtual BIDDING BIDDING { get; set; }

        public virtual BROKER BROKER { get; set; }

        public virtual CLIENT CLIENT { get; set; }

        public virtual DEAL DEAL { get; set; }

        public virtual ORDERR ORDERR { get; set; }

        public virtual SECURITy SECURITy { get; set; }
    }
}
