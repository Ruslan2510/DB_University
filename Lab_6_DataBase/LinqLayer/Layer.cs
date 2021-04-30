using LinqLayer.Contexts;
using SimpleLogger;
using SimpleLogger.Logging.Handlers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LinqLayer
{
    public class Layer
    {
        OrderContext orderContext = new OrderContext();
        Stopwatch stopWatch = null;
        const int NUMBER = 1000;

        public async Task<List<Client>> GetClientsAsync()
        {
            var result = await orderContext.Clients.ToListAsync();
            return result;
        }

        public async Task<List<Broker>> GetBrokersAsync()
        {
            var result = await orderContext.Brokers.ToListAsync();
            return result;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var result = await orderContext.Orders.Include(x => x.Client).ToListAsync();
            return result;
        }

        //Проекиция из БД
        //Сортировка
        public async Task<List<Order>> GetOrdersSampleAsync()
        {
            var orders = await GetOrdersAsync();
            var result = orders
                .Select(x => new Order
                    {
                        Id = x.Id,
                        Status = x.Status,
                        Comment = x.Comment,
                        ClientId = x.Client.Id,
                        BrokerId = x.Broker.Id,
                    }).OrderBy(x => x.Status).ToList();

            return result;
        }

        //Соединение таблиц 
        public async Task<List<Order>> JoinClientsAndOrdersAsync()
        {
            var clients = await GetClientsAsync();
            var orders = await GetOrdersAsync();
            var result = orders.Join(clients,
                o => o.ClientId,
                c => c.Id,
                (o, c) => new Order
                {
                    Id = o.Id,
                    Status = o.Status,
                    Comment = o.Comment,
                    ClientBalance = c.Balance
                }).ToList();
            return result;
        }

        //Группировка
        public async Task<string> GroupByStatusAsync()
        {
            var orders = await GetOrdersAsync();
            var groups = orders
                .GroupBy(x => x.Status)
                .Select(x => new
                {
                    Status = x.Key,
                    Count = x.Count()
                }).ToList();

            string result = "";
            foreach (var item in groups)
            {
                result += $"Количество заказов со статусом {item.Status} составляет {item.Count} шт.\n";
            }
            return result;
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

        //Объединение
        public async Task<List<Client>> UnionAsync()
        {
            var clients = await GetClientsAsync();

            var selector1 = clients.Where(x => x.Balance > 3000);
            var selector2 = clients.Where(x => x.Surname.Contains("ов"));

            return selector1.Union(selector2).ToList();
        }

        //Пересечение
        public async Task<List<Client>> IntersectAsync()
        {
            var clients = await GetClientsAsync();

            var selector1 = clients.Where(x => x.Balance > 3000);
            var selector2 = clients.Where(x => x.Surname.Contains("ов"));

            return selector1.Intersect(selector2).ToList();
        }

        //Разность
        public async Task<List<Client>> ExceptAsync()
        {
            var clients = await GetClientsAsync();

            var selector1 = clients.Where(x => x.Balance > 3000);
            var selector2 = clients.Where(x => x.Surname.Contains("ов"));

            return selector1.Except(selector2).ToList();
        }

        //Агрегатные функции
        public async Task<string> AggregateFunctionsAsync()
        {
            var brokers = await GetBrokersAsync();
            var sum = brokers.Sum(x => x.Experience);
            var result = $"Общий опыт все сотрудников составляет: {sum}\n";
            var max = brokers.Max(x => x.Experience);
            result += $"Максимальный опыт работы составляет: {max}\n";
            var min = brokers.Min(x => x.Experience);
            result += $"Минимальный опыт работы составляет: {min}\n";

            return result;
        }

        public string GetIEnumOnTimeAsync()
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < NUMBER; i++)
            {
                IEnumerable<Client> clientsEnum = orderContext.Clients;
                var res = clientsEnum.Where(x => x.Id == -1).ToList();
            }
            stopWatch.Stop();
            var result = stopWatch.ElapsedTicks.ToString() + " проц. тик.";

            return result;
        }

        public string GetIQueryOnTimeAsync()
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < NUMBER; i++)
            {
                IQueryable<Client> clientsQuery = orderContext.Clients;
                var res = clientsQuery.Where(x => x.Id == -1).ToList();
            }
            stopWatch.Stop();
            var result = stopWatch.ElapsedTicks.ToString() + " проц. тик.";

            return result;
        }

        public string GetAsNoTrackingOnTimeAsync()
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < NUMBER; i++)
            {
                IEnumerable<Client> clientsAsNoTracking = orderContext.Clients.AsNoTracking();
                var res = clientsAsNoTracking.Where(x => x.Id == -1).ToList();
            }
            stopWatch.Stop();
            var result = stopWatch.ElapsedTicks.ToString() + " проц. тик.";

            return result;
        }
    }
}