using Microsoft.AspNetCore.Hosting;
using MVClogin2.Models;
using MVClogin2.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MVClogin2.Sql
{
    public class SqlWorker :ISqlWorker
    {
        private IWebHostEnvironment _env;
        string connetionString = string.Empty;
        SqlConnection sqlConnection;
        SqlModel sqlModel;
        public SqlWorker(IWebHostEnvironment env)
        {
            _env = env;
            JsonFileSqlConstService sqlConstService = new JsonFileSqlConstService(_env);
            sqlModel = sqlConstService.GetSqlConst();
        }
        private bool tryConnect()
        {
            connetionString = "Server=" + sqlModel.Server + ";Database=" + sqlModel.Database +
                ";user id=" + sqlModel.userId + ";password=" + sqlModel.password;
            sqlConnection = new SqlConnection(connetionString);
            try
            {
                sqlConnection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool tryAuthenticate(string username, string password)
        {
            tryConnect();
            string queryString = $"SELECT COUNT (UserName) FROM [{sqlModel.Database}].[dbo].[AspNetUsers] WHERE UserName = '{username}' and PasswordHash = '{password}';";
            if (sqlConnection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                if (command.ExecuteScalar().ToString() == "1")
                {
                    CloseConnection();
                    return true;
                }
            }
            CloseConnection();
            return false;
        }
        public List<UserModel> getListOfMembers()
        {
            tryConnect();
            List<UserModel> list = new List<UserModel>();
            string queryString = $"SELECT [UserName], [firstName], [lastName] FROM [{sqlModel.Database}].[dbo].[AspNetUsers]";
            SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                UserModel u = new UserModel();
                u.username = (string)reader["username"];
                u.firstname = (string)reader["firstname"];
                u.lastname = (string)reader["lastname"];
                list.Add(u);
            }
            CloseConnection();
            return list;
        }
        public List<CalibrationModel> getCalibrations(string id)
        {
            tryConnect();
            List<CalibrationModel> list = new List<CalibrationModel>();
            string queryString = $"SELECT [UserId], [Name], [Description], [dateTime] FROM [{sqlModel.Database}].[dbo].[Calibrations]";
            SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    CalibrationModel c = new CalibrationModel();
                    c.UserId = (string)reader["UserId"];
                    if (c.UserId != id)
                        continue;
                    c.Name = (string)reader["Name"];
                    c.Description = (string)reader["Description"];
                    c.dateTime = (string)reader["dateTime"];
                    list.Add(c);
                }
                CloseConnection();
                return list;
            }
            catch
            {
                CloseConnection();
                return list;
            }
        }
        public bool InsertCalibration(CalibrationModel model, string username)
        {
            tryConnect();
            string queryString = $"INSERT INTO dbo.Calibrations (UserId,Name,Description,dateTime) VALUES " +
                $"('{getIdByUsername(username)}','{model.Name}','{model.Description}', '{DateTime.Now.ToString()}')";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            if (command.ExecuteNonQuery() == 1)
            {
                CloseConnection();
                return true;
            }
            CloseConnection();
            return false;
        }
        private string getIdByUsername(string username)
        {
            tryConnect();
            List<string> list = new List<string>();
            string queryString = $"SELECT [Id] FROM [{sqlModel.Database}].[dbo].[AspNetUsers] WHERE UserName = '{username}'";
            SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                string i = (string)reader["Id"];
                list.Add(i);
            }
            reader.Close();
            CloseConnection();
            if (list.Count == 1)
                return list[0];
            return null;
        }
        private void CloseConnection()
        {
            sqlConnection.Close();
        }
        public void AddDefault()
        {
            throw new NotImplementedException();
        }

        string ISqlWorker.getIdByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void SaveMessageToDb(UserMessage message)
        {
            throw new NotImplementedException();
        }

        public void LoadMessageFromDb()
        {
            throw new NotImplementedException();
        }

        public List<UserMessage> LoadMessageFromDb(string id)
        {
            throw new NotImplementedException();
        }
    }
}
