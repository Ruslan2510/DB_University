using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class NewDeal
    {
        public int BiddingId { get; set; }
        public int SecuritiesId { get; set; }
        public int SecuritiesAmount { get; set; }
        public string DealDate { get; set; }
        public string DealCustomer { get; set; }
        public string DealType { get; set; }
    }
}
