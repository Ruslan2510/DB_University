using System.Collections.Generic;

namespace Layer.Models
{
    public class MeasureInfo
    {
        public short Id { get; set; }
        public string Measure { get; set; }
        public ICollection<StorageUnit> StorageUnits { get; set; }

        public MeasureInfo()
        {
            StorageUnits = new List<StorageUnit>();
        }
    }
}
