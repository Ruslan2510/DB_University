using SimpleLogger;
using SimpleLogger.Logging.Handlers;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class Connection
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
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
                // создание конструктора строк подключения 


                connectionStringBuilder.DataSource = @"192.168.56.2";   // используйте конструктор строк подключения для 
                connectionStringBuilder.InitialCatalog = @"Lab_9_2505";      // предотвращения изменения пользователем структуры строки подключения
                connectionStringBuilder.UserID = login;
                connectionStringBuilder.Password = password;

                OpenConnection(connectionStringBuilder.ConnectionString);
                Logger.Log($"Connection is opened.");
                CloseConnection();
                Logger.Log($"Connection is closed.");
            }
            catch (SqlException ex)
            {
                Logger.Log(ex);
                CloseConnection();
                throw ex;
            }
            return true;
        }

        private void OpenConnection(string connectionString)
        {
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
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                //Logger.Log(ex);
                throw ex;
            }
        }
    }
}
