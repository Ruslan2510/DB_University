using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectedLayer
{
    public class Layer
    {
        Connection connection = new Connection();


        private string GetConnectionString()
        {
            var connectionDetails = ConfigurationManager.ConnectionStrings["ConnectionString"];
            var providerName = connectionDetails.ProviderName;

            return connectionDetails.ConnectionString;
        }

        public DataTable ShowTable(string table)
        {
            DataTable dataTable = null;
            try
            {
                string sqlCommand = string.Format($"SELECT * FROM {table}");

                var connectionString = GetConnectionString();
                var sqlConnect = connection.OpenConnection(connectionString);

                using (var command = new SqlCommand(sqlCommand, sqlConnect))
                {
                    var dataReader = command.ExecuteReader();

                    if (dataReader.HasRows) // если есть данные
                    {
                        dataTable = new DataTable(string.Format($"{table}"));
                        dataTable.Load(dataReader);
                    }
                    connection.CloseConnection();
                };
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return dataTable;
        }

        //Stored procedure
        public void SellOrBuyPaper(int biddingId, int paperId, int paperAmount, string deal, char customer)
        {
            try
            {
                var connectionString = GetConnectionString();
                var sqlConnect = connection.OpenConnection(connectionString);

                SqlCommand cmd = new SqlCommand("SecuritiesOperations", sqlConnect)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                cmd.Parameters.AddRange(new[] {
                    new SqlParameter("@biddingId", biddingId),
                    new SqlParameter("@securitiesId", paperId),
                    new SqlParameter("@securitiesAmount", paperAmount),
                    new SqlParameter("@dealDate", DateTime.Now),
                    new SqlParameter("@dealCustomer", customer),
                    new SqlParameter("@dealType", deal)
                });

                SqlDataReader reader = cmd.ExecuteReader();

                connection.CloseConnection();
            }
            catch (SqlException ex)
            {
                //Logger.Log(ex);
                throw ex;
            }
        }


        public DataTable UserFunction(string lowLimit, string upperLimit)
        {
            DataTable dataTable = null;
            var connectionString = GetConnectionString();
            var sqlConnection = connection.OpenConnection(connectionString);

            string sqlCommand = string.Format($"SELECT * FROM dbo.GetBurseIncome('{lowLimit}', '{upperLimit}')");

            using (var command = new SqlCommand(sqlCommand, sqlConnection))
            {
                try
                {
                    var dataReader = command.ExecuteReader();

                    if (dataReader.HasRows) // если есть данные
                    {
                        dataTable = new DataTable();
                        dataTable.Load(dataReader);
                    }

                    connection.CloseConnection();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dataTable;
        }
    }
}
