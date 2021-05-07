using SimpleLogger;
using SimpleLogger.Logging.Handlers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer
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
                string connectionString = string.Format($"Data Source = 192.168.56.2; Initial Catalog = Lab_8_DB;Persist Security Info=False;User={login};" +
                    $"PWD = {password}; Pooling = True; MultipleActiveResultSets = False; Encrypt = False; TrustServerCertificate = False");


                OpenConnection(connectionString);
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
                Logger.Log(ex);
                throw ex;
            }
        }
    }
}
