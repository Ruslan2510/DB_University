//using SimpleLogger;
//using SimpleLogger.Logging.Handlers;
using SimpleLogger;
using SimpleLogger.Logging.Handlers;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ConnectedLayer
{
    public class Connection
    {
        public SqlConnection connection;

        public bool CheckConnection(string login, string password)
        {
            Logger.LoggerHandlerManager
                .AddHandler(new ConsoleLoggerHandler())
                .AddHandler(new FileLoggerHandler())
                .AddHandler(new DebugConsoleLoggerHandler());

            try
            {
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
                connectionStringBuilder["Data Source"] = "192.168.56.2";
                connectionStringBuilder["Initial Catalog"] = "Lab_9_2505";
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
                Logger.Log($"connection is closed.");
            }
            catch (SqlException ex)
            {
                Logger.Log(ex);
                CloseConnection();
                throw ex;
            }
            return true;
        }

        internal SqlConnection OpenConnection(string connectionString)
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

            return connection;
        }

        internal void CloseConnection()
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
