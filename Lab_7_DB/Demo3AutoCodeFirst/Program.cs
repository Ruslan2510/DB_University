using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3AutoCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UserContext db = new UserContext())
            {
                foreach (User u in db.Users)
                    Console.WriteLine("{0}.{1} - {2}", u.Id, u.Name, u.Age);
            }
            Console.ReadKey();
        }
    }
}
