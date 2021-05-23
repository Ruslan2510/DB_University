using Microsoft.Data.Sqlite;
using SimpleLogger;
using SimpleLogger.Logging.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer
{
    public class Connection
    {
        private SqliteConnection connection;
        private string _connectionString = "Data Source=sqlitedata.db";

        public bool CheckConnection(string login, string password)
        {
            Logger.LoggerHandlerManager
                .AddHandler(new ConsoleLoggerHandler())
                .AddHandler(new FileLoggerHandler())
                .AddHandler(new DebugConsoleLoggerHandler());

            try
            {
                OpenConnection();
                Logger.Log($"Connection is opened.");
                CloseConnection();
                Logger.Log($"Connection is closed.");
            }
            catch (SqliteException ex)
            {
                //Logger.Log(ex);
                throw ex;
            }
            return true;
        }

        internal SqliteConnection OpenConnection()
        {
            try
            {
                connection = new SqliteConnection(_connectionString);
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
                Logger.Log(ex);
                throw ex;
            }
        }
    }
}