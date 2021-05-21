using System.Collections.Generic;


namespace Layer.Models
{
    public class Material
    {
        public short Id { get; set; }
        public short GroupId { get; set; }
        public string Name { get; set; }
        public ICollection<StorageUnit> StorageUnits { get; set; }

        public Material()
        {
            StorageUnits = new List<StorageUnit>();
        }
    }
}
