using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqLayer.Contexts
{
    class ContextInitializer : DropCreateDatabaseAlways<OrderContext>
    {
        protected override void Seed(OrderContext context)
        {
            //Clients
            Client client1 = new Client
            {
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = "Иванович",
                Email = "Ivanov@gmail.com",
                Phone = "+3804884844",
                Balance = 3000.00M
            };

            Client client2 = new Client
            {
                Surname = "Петров",
                Name = "Пётр",
                Patronymic = "Петрович",
                Email = "Petrov@gmail.com",
                Phone = "+380597734732",
                Balance = 7000.00M
            };

            Client client3 = new Client
            {
                Surname = "Сидоров",
                Name = "Андрей",
                Patronymic = "Виктрович",
                Email = "Sidirov@gmail.com",
                Phone = "+380343434342",
                Balance = 5000.00M
            };

            context.Clients.Add(client1);
            context.Clients.Add(client2);
            context.Clients.Add(client3);

            context.SaveChanges();


            //Brokers
            Broker broker1 = new Broker
            {
                Surname = "Николаев",
                Name = "Алексей",
                Patronymic = "Григорьевич",
                Email = "Nickolaev@gmail.com",
                Phone = "+38056565656",
                Experience = 5
            };

            Broker broker2 = new Broker
            {
                Surname = "Александрова",
                Name = "Алина",
                Patronymic = "Евгеньевна",
                Email = "Alexandrova@gmail.com",
                Phone = "+380777787878",
                Experience = 2
            };

            Broker broker3 = new Broker
            {
                Surname = "Артамонова",
                Name = "София",
                Patronymic = "Арсеньевна",
                Email = "Artamonova@gmail.com",
                Phone = "+3807770007771",
                Experience = 7
            };

            context.Brokers.Add(broker1);
            context.Brokers.Add(broker2);
            context.Brokers.Add(broker3);

            context.SaveChanges();

            //Orders
            Order order1 = new Order
            {
                Comment = "Продажа акций",
                Status = OrderStatus.Completed,
                Client = client1,
                Broker = broker2
            };

            Order order2 = new Order
            {
                Comment = "Необходимо приобрести до 24 мая",
                Status = OrderStatus.Adopted,
                Client = client2,
                Broker = broker2
            };

            Order order3 = new Order
            {
                Comment = "Уведомить о наличии ценных бумаг",
                Status = OrderStatus.PendingProcessing,
                Client = client3,
                Broker = broker1
            };

            Order order4 = new Order
            {
                Comment = "Не перезванивать",
                Status = OrderStatus.Completed,
                Client = client1,
                Broker = broker1
            };

            Order order5 = new Order
            {
                Comment = "В процессе покупки",
                Status = OrderStatus.Adopted,
                Client = client3,
                Broker = broker3
            };

            context.Orders.AddRange(new List<Order>() {order1, order2, order3, order4, order5});
            context.SaveChanges();
        }
    }
}