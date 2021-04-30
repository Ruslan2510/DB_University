using System.Collections.Generic;

namespace LinqLayer
{
    public class Broker
    {
        public short Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public short Experience { get; set; }
    
        public ICollection<Order> Orders { get; set; }
        
        public Broker()
        {
            Orders = new List<Order>();
        }
    
    }
}