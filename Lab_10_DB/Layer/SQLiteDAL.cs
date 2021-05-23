using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace Layer
{
    public class SQLiteDAL
    {
        Connection connect = new Connection();
        public void CreateClientsTable()
        {
            try
            {
                var connection = connect.OpenConnection();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText =
                "CREATE TABLE CLIENTS" +
                "(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
                "Name TEXT NOT NULL UNIQUE," +
                "Email TEXT NOT NULL UNIQUE," +
                "Phone TEXT NOT NULL UNIQUE," +
                "ImageId INTEGER," +
                "Balance DECIMAL NOT NULL);";
                command.ExecuteNonQuery();
                connect.CloseConnection();
            }
            catch (SqliteException ex)
            {
                throw ex;
            }
        }

        public void CreateFilesTable()
        {
            try
            {
                var connection = connect.OpenConnection();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText =
                "CREATE TABLE FILES" +
                "(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
                "Title TEXT NOT NULL," +
                "FileName TEXT NOT NULL," +
                "ImageData BLOB);";
                command.ExecuteNonQuery();
                connect.CloseConnection();
            }
            catch (SqliteException ex)
            {
                throw ex;
            }
        }

        public List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            string sqlExpression = "SELECT * FROM CLIENTS";
            try
            {
                var connection = connect.OpenConnection();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            clients.Add(new Client
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"]),
                                Email = Convert.ToString(reader["Email"]),
                                Phone = Convert.ToString(reader["Phone"]),
                                ImageUrl = GetImageById(Convert.ToInt32(reader["ImageId"])).FileName,
                                Balance = Convert.ToDecimal(reader["Balance"])
                            });
                        }
                    }
                }
                connect.CloseConnection();
            }
            catch (SqliteException ex)
            {
                throw ex;
            }
            return clients;
        }

        public void AddNewClient(Client client, File newImage)
        {
            try
            {
                client.ImageId = AddImage(newImage);

                var connection = connect.OpenConnection();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                //Получение id добавленного изображения


                command.CommandText = @"INSERT INTO Clients (Name, Email, Phone, ImageId, Balance)
                                        VALUES (@Name, @Email, @Phone, @ImageId, @Balance)";
                command.Parameters.Add(new SqliteParameter("@Name", client.Name));
                command.Parameters.Add(new SqliteParameter("@Email", client.Email));
                command.Parameters.Add(new SqliteParameter("@Phone", client.Phone));
                command.Parameters.Add(new SqliteParameter("@ImageId", client.ImageId));
                command.Parameters.Add(new SqliteParameter("@Balance", client.Balance));
                command.ExecuteNonQuery();
                connect.CloseConnection();
            }
            catch(SqliteException ex)
            {
                throw ex;
            }
        }

        private int ChainUserAndImage()
        {
            int id = 0;
            var connection = connect.OpenConnection();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;

            command.CommandText = "SELECT MAX(id) FROM FILES";

            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }

            connect.CloseConnection();
            return id;
        }

        public int AddImage(File newImage)
        {
            int fileId = 0;
            try
            {
                var connection = connect.OpenConnection();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;

                command.CommandText = @"INSERT INTO Files (Title, FileName, ImageData)
                                        VALUES (@Title, @FileName, @ImageData)";
                command.Parameters.Add(new SqliteParameter("@Title", newImage.Title));
                command.Parameters.Add(new SqliteParameter("@FileName", newImage.FileName));
                command.Parameters.Add(new SqliteParameter("@ImageData", newImage.ImageData));

                command.ExecuteNonQuery();
                connect.CloseConnection();

                fileId = ChainUserAndImage();
            }
            catch(SqliteException ex)
            {
                throw ex;
            }
            return fileId;
        }

        private File GetImageById(int imageId)
        {
            File file = null;
            try
            {
                var connection = connect.OpenConnection();
                string sqlExpression = string.Format($"SELECT * FROM FILES WHERE Id = {imageId}");

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        reader.Read();

                        file = new File
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = Convert.ToString(reader["Title"]),
                            FileName = Convert.ToString(reader["FileName"]),
                            ImageData = (byte[])reader["ImageData"]
                        };
                    }
                }

                connect.CloseConnection();
            }
            catch(SqliteException ex)
            {
                throw ex;
            }
            return file;
        }

        public void DeleteClientById(int id)
        {
            try
            {
                var connection = connect.OpenConnection();
                string sqlExpression = string.Format($"DELETE FROM Clients WHERE Id = {id}");

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                command.ExecuteNonQuery();
            }
            catch (SqliteException ex)
            {
                throw ex;
            }
        }

        public void UpdateClient(Client updatedClient)
        {
            try
            {
                var connection = connect.OpenConnection();
                string sqlExpression = string.Format($"UPDATE Clients " +
                    $"SET Name = '{updatedClient.Name}'," +
                    $"Phone = '{updatedClient.Phone}'," +
                    $"Email = '{updatedClient.Email}'," +
                    $"Balance = {updatedClient.Balance} " +
                    $"WHERE Id = {updatedClient.Id}");

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                command.ExecuteNonQuery();
            }
            catch (SqliteException ex)
            {
                throw ex;
            }
        }
    }
}
