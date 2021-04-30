namespace LinqLayer
{
    public class Order
    {
        public short Id { get; set; }
        public string Comment { get; set; }
        public OrderStatus Status { get; set; }
        
        public short ClientId { get; set; }
        public Client Client { get; set; }

        public short BrokerId { get; set; }
        public Broker Broker { get; set; }
        public decimal ClientBalance { get; set; } 
    }

    public enum OrderStatus
    {
        //Принят
        Adopted = 0,
        //Готов
        Completed = 1,
        //Ожидает обработки
        PendingProcessing = -1
    }
}
