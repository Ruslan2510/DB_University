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
                Logger.Log($"Database connection {DateTime.Now}");
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
                connectionStringBuilder["Data Source"] = "192.168.56.2";
                connectionStringBuilder["Initial Catalog"] = "Module_2_2105";
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
