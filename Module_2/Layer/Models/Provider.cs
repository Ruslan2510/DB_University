using System.Collections.Generic;

namespace Layer.Models
{
    public class Provider
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public string IdentificationCode { get; set; }
        public string Address { get; set; }
        public string BankAccountNumber { get; set; }

        public ICollection<StorageUnit> StorageUnits { get; set; }

        public Provider()
        {
            StorageUnits = new List<StorageUnit>();
        }
    }
}
