//using SimpleLogger;
//using SimpleLogger.Logging.Handlers;
using SimpleLogger;
using SimpleLogger.Logging.Handlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Layer
{
    [Table(Name = "Client")]
    public class Client
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "client_id")]
        public int ClientId { get; set; }
        [Column(Name = "client_surname")]
        public string ClientSurname { get; set; }
        [Column(Name = "client_name")]
        public string ClientName { get; set; }
        [Column(Name = "client_patronymic")]
        public string ClientPatronymic { get; set; }
        [Column(Name = "client_tel")]
        public string ClientPhone { get; set; }
        [Column(Name = "client_email")]
        public string ClientEmail { get; set; }
        [Column(Name = "client_fortune")]
        public decimal ClientFortune { get; set; }
    }

    public class LinqLayer
    {
        private SqlConnection connection;

        public bool CheckConnection(string login, string password)
        {
            Logger.LoggerHandlerManager
    .AddHandler(new ConsoleLoggerHandler())
    .AddHandler(new FileLoggerHandler())
    .AddHandler(new DebugConsoleLoggerHandler());

            try
            {

                Logger.Log($"Database connection {DateTime.Now}");
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
                connectionStringBuilder["Data Source"] = "192.168.56.2";
                connectionStringBuilder["Initial Catalog"] = "HomeTaskDB";
                connectionStringBuilder["User ID"] = login;
                connectionStringBuilder["Password"] = password;

                string connectionString = connectionStringBuilder.ToString();

                var setting = new ConnectionStringSettings
                {
                    Name = "ConnectionString",     //имя строки подключения в конфигурационном файле
                    ConnectionString = connectionString,
                    ProviderName = "System.Data.SqlClient"
                };


                //Объект Config представляет собой конфигурационный файл
                Configuration config;


                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings.Remove("ConnectionStrings");
                config.ConnectionStrings.ConnectionStrings.Remove(setting);
                config.ConnectionStrings.ConnectionStrings.Add(setting);
                config.Save();


                OpenConnection(connectionString);
                Logger.Log($"Connection is opened.");
                CloseConnection();
                Logger.Log($"Connection is closed.");
            }
            catch (SqlException ex)
            {
                Logger.Log(ex);
                throw ex;
            }
            return true;
        }

        private void OpenConnection(string connectionString)
        {
            Logger.LoggerHandlerManager
    .AddHandler(new ConsoleLoggerHandler())
    .AddHandler(new FileLoggerHandler())
    .AddHandler(new DebugConsoleLoggerHandler());
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw ex;
            }
        }

        public void CloseConnection()
        {
            Logger.LoggerHandlerManager
    .AddHandler(new ConsoleLoggerHandler())
    .AddHandler(new FileLoggerHandler())
    .AddHandler(new DebugConsoleLoggerHandler());
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw ex;
            }
        }

        public string GetConnectionString()
        {
            var connectionDetails = ConfigurationManager.ConnectionStrings["ConnectionString"];
            var providerName = connectionDetails.ProviderName;

            return connectionDetails.ConnectionString;
        }

        public DataTable GetClientTable()
        {
            DataTable dataTable = null;
            try
            {
                var connectionString = GetConnectionString();

                DataContext dataContext = new DataContext(connectionString);
                var clients = dataContext.GetTable<Client>();
                dataTable = LINQResultToDataTable(clients);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();
            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                               == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                           (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        public void DeleteClientById(int clientId)
        {
            try
            {
                var connectionString = GetConnectionString();
                var dataContext = new DataContext(connectionString);
                var client = dataContext.GetTable<Client>().FirstOrDefault(x => x.ClientId == clientId);

                if (client != null)
                {
                    dataContext.GetTable<Client>().DeleteOnSubmit(client);
                    dataContext.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddClient(string surname, string name, string patronymic, string email, string phone, decimal balance)
        {
            Client newClinet = null;
            int userId = 0;
            try
            {
                var connectionString = GetConnectionString();
                var dataContext = new DataContext(connectionString);

                newClinet = new Client()
                {
                    ClientSurname = surname,
                    ClientName = name,
                    ClientPatronymic = patronymic,
                    ClientEmail = email,
                    ClientPhone = phone,
                    ClientFortune = balance
                };

                dataContext.GetTable<Client>().InsertOnSubmit(newClinet);
                dataContext.SubmitChanges();

                var clientsCount = dataContext.GetTable<Client>().Count();

                var query = dataContext.GetTable<Client>().Skip(clientsCount - 1).Take(1).ToList();

                userId = query[0].ClientId;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return userId;
        }

        public void UpdateClient(Client newClient)
        {
            try
            {
                var connectionString = GetConnectionString();
                var dataContext = new DataContext(connectionString);

                var client = dataContext.GetTable<Client>().FirstOrDefault(x => x.ClientId == newClient.ClientId);

                client.ClientSurname = newClient.ClientSurname;
                client.ClientName = newClient.ClientName;
                client.ClientPatronymic = newClient.ClientPatronymic;
                client.ClientEmail = newClient.ClientEmail;
                client.ClientPhone = newClient.ClientPhone;
                client.ClientFortune = newClient.ClientFortune;

                dataContext.SubmitChanges();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable StoredProcedure(string date, int paperAmount, int paperId, int biddingId, string customer, string deal)
        {
            var connectionString = GetConnectionString();
            var dataContext = new DataContext(connectionString);

            int modifedRowsNumber = dataContext.ExecuteCommand
                (string.Format($"Execute SecuritiesOperations {biddingId}, {paperId}, {paperAmount}, '{date}'," +
                $"'{customer}', '{deal}'"));

            return GetClientTable();
        }
    }
}
