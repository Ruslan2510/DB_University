using System.Collections.Generic;

namespace LinqLayer
{
    public class Client
    {
        public short Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Client()
        {
            Orders = new List<Order>();
        }
    }
}
