using Microsoft.AspNetCore.Hosting;
using MVClogin2.Models;
using MVClogin2.Services;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MVClogin2.Sql
{
    public class SqlWorker
    {
        string connetionString = string.Empty;
        SqlConnection sqlConnection;
        SqlModel sqlModel;
        public void initialize(IWebHostEnvironment _env)
        {
            JsonFileSqlConstService sqlConstService = new JsonFileSqlConstService(_env);
            sqlModel = sqlConstService.GetSqlConst();
        }
        public bool tryConnect()
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
            string queryString = $"SELECT COUNT (UserName) FROM [{sqlModel.Database}].[dbo].[AspNetUsers] WHERE UserName = '{username}' and PasswordHash = '{password}';";
            if (sqlConnection.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                if (command.ExecuteScalar().ToString() == "1")
                    return true;
            }
            return false;
        }
        public List<UserModel> getListOfMembers()
        {
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
            return list;
        }
        public List<CalibrationModel> getCalibrations(string id)
        {
            List<CalibrationModel> list = new List<CalibrationModel>();
            string queryString = $"SELECT [userId], [name], [description], [date] FROM [{sqlModel.Database}].[dbo].[Calibrations]";
            SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                CalibrationModel c = new CalibrationModel();
                c.UserId = (string)reader["userId"];
                if (c.UserId != id)
                    continue;
                c.Name = (string)reader["name"];
                c.Description = (string)reader["description"];
                c.dateTime = (string)reader["date"];
                list.Add(c);
            }
            return list;
        }
        public bool InsertCalibration(CalibrationModel model)
        {
            string queryString = $"INSERT INTO dbo.Calibrations (userId,name,description,date) VALUES " +
                $"('{model.UserId}','{model.Name}','{model.Description}', '{model.dateTime}')";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            if (command.ExecuteNonQuery() == 1)
                return true;
            return false;
        }
        public string getIdByUsername(string username)
        {
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
            if (list.Count == 1)
                return list[0];
            return null;
        }
        public void CloseConnection()
         {
            sqlConnection.Close();
         }
    }
}
