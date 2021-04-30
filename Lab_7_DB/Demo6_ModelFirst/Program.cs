using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo6_ModelFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UserContainer db = new UserContainer())
            {
                // добавление элементов
                db.Users.Add(new User { Name = "Tom", Age = 45 });
                db.Users.Add(new User { Name = "John", Age = 22 });
                db.SaveChanges();
                // получение элементов
                var users = db.Users;
                foreach (User u in users)
                    Console.WriteLine("{0}.{1} - {2}", u.Id, u.Name, u.Age);
            }
            Console.Read();
        }
    }
}
