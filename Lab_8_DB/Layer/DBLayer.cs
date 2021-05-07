using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Layer
{
    public class DBLayer
    {
        //public void Initialize()
        //{
        //    OrderModelContainer context = new OrderModelContainer();
        //    Client client1 = new Client
        //    {
        //        Surname = "Иванов",
        //        Name = "Иван",
        //        Patronymic = "Иванович",
        //        Email = "Ivanov@gmail.com",
        //        Phone = "+3804884844",
        //        Balance = 3000.00M
        //    };

        //    Client client2 = new Client
        //    {
        //        Surname = "Петров",
        //        Name = "Пётр",
        //        Patronymic = "Петрович",
        //        Email = "Petrov@gmail.com",
        //        Phone = "+380597734732",
        //        Balance = 7000.00M
        //    };

        //    Client client3 = new Client
        //    {
        //        Surname = "Сидоров",
        //        Name = "Андрей",
        //        Patronymic = "Виктрович",
        //        Email = "Sidirov@gmail.com",
        //        Phone = "+380343434342",
        //        Balance = 5000.00M
        //    };

        //    context.ClientSet.Add(client1);
        //    context.ClientSet.Add(client2);
        //    context.ClientSet.Add(client3);

        //    context.SaveChanges();


        //    //Brokers
        //    Broker broker1 = new Broker
        //    {
        //        Surname = "Николаев",
        //        Name = "Алексей",
        //        Patronymic = "Григорьевич",
        //        Email = "Nickolaev@gmail.com",
        //        Phone = "+38056565656",
        //        Experience = "5 лет"
        //    };

        //    Broker broker2 = new Broker
        //    {
        //        Surname = "Александрова",
        //        Name = "Алина",
        //        Patronymic = "Евгеньевна",
        //        Email = "Alexandrova@gmail.com",
        //        Phone = "+380777787878",
        //        Experience = "2 года"
        //    };

        //    Broker broker3 = new Broker
        //    {
        //        Surname = "Артамонова",
        //        Name = "София",
        //        Patronymic = "Арсеньевна",
        //        Email = "Artamonova@gmail.com",
        //        Phone = "+3807770007771",
        //        Experience = "7 лет"
        //    };

        //    context.BrokerSet.Add(broker1);
        //    context.BrokerSet.Add(broker2);
        //    context.BrokerSet.Add(broker3);

        //    context.SaveChanges();

        //    //Orders
        //    Order order1 = new Order
        //    {
        //        Comment = "Продажа акций",
        //        Status = OrderStatus.Completed.ToString(),
        //        Client = client1,
        //        Broker = broker2
        //    };

        //    Order order2 = new Order
        //    {
        //        Comment = "Необходимо приобрести до 24 мая",
        //        Status = OrderStatus.Adopted.ToString(),
        //        Client = client2,
        //        Broker = broker2
        //    };

        //    Order order3 = new Order
        //    {
        //        Comment = "Уведомить о наличии ценных бумаг",
        //        Status = OrderStatus.PendingProcessing.ToString(),
        //        Client = client3,
        //        Broker = broker1
        //    };

        //    Order order4 = new Order
        //    {
        //        Comment = "Не перезванивать",
        //        Status = OrderStatus.Completed.ToString(),
        //        Client = client1,
        //        Broker = broker1
        //    };

        //    Order order5 = new Order
        //    {
        //        Comment = "В процессе покупки",
        //        Status = OrderStatus.Adopted.ToString(),
        //        Client = client3,
        //        Broker = broker3
        //    };

        //    context.OrderSet.AddRange(new List<Order>() { order1, order2, order3, order4, order5 });
        //    context.SaveChanges();
        //}

        OrderModelContainer orderContext = new OrderModelContainer();

        public async Task<List<Client>> GetClientsAsync()
        {
            var result = await orderContext.ClientSet.ToListAsync();
            return result;
        }

        public async Task<List<Broker>> GetBrokersAsync()
        {
            var result = await orderContext.BrokerSet.ToListAsync();
            return result;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var result = await orderContext.OrderSet.Include(x => x.Client).ToListAsync();
            return result;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await orderContext.ClientSet.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
        }

        public async Task<Broker> GetBrokerByIdAsync(int id)
        {
            return await orderContext.BrokerSet.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
        }

        public async Task AddOrderAsync(Order order)
        {
            try
            {
                if (order != null)
                {
                    orderContext.OrderSet.Add(order);
                    await orderContext.SaveChangesAsync(CancellationToken.None);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task UpdateOrderAsync(Order newOrder)
        {

            using (var transaction = orderContext.Database.BeginTransaction())
            {
                try
                {
                    var order = await orderContext.OrderSet.FirstAsync(x => x.Id == newOrder.Id, CancellationToken.None);

                    if (order != null)
                    {
                        order = new Order
                        {
                            Comment = newOrder.Comment,
                            Status = newOrder.Status,
                            Broker = newOrder.Broker,
                            Client = newOrder.Client
                        };

                        await orderContext.SaveChangesAsync(CancellationToken.None);
                        transaction.Commit();
                    }
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task DeleteOrderByIdAsync(Order order)
        {
            if (order != null)
            {
                orderContext.OrderSet.Remove(order);
                await orderContext.SaveChangesAsync(CancellationToken.None);
            }
        }


        //Фильтрация и выборка
        public async Task<List<Client>> GetFilteredClientsAsync(string searchedText)
        {
            searchedText = searchedText.ToLower();

            var clients = await GetClientsAsync();
            var result = clients
                .Where(x => x.Surname.ToLower().Contains(searchedText) ||
                    x.Name.ToLower().Contains(searchedText) ||
                    x.Patronymic.ToLower().Contains(searchedText) ||
                    x.Email.ToLower().Contains(searchedText) ||
                    x.Phone.ToLower().Contains(searchedText) ||
                    Convert.ToString(x.Balance).ToLower().Contains(searchedText))
                .ToList();

            return result;
        }

        public void Initialize()
        {
            EFGenericRepository<Order> orderRepo = new EFGenericRepository<Order>(new OrderModelContainer());
            IEnumerable<Order> phones = orderRepo.GetWithInclude(x => x.Client.Name.StartsWith("S"), p => p.Client);
        }
    }
}
