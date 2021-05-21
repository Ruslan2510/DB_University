namespace Layer.Models
{
    public class StorageUnit
    {
        public short Id { get; set; }
        public short OrderId { get; set; }
        public string Date { get; set; }
        public short ProviderId { get; set; }
        public Provider Provider { get; set; }
        public short MaterialId { get; set; }
        public Material Material { get; set; }
        public short MeasureInfoId { get; set; }
        public MeasureInfo MeasureInfo { get; set; }
        public decimal Cost { get; set; }
        public int Amount { get; set; }
    }
}
