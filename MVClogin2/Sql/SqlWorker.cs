using Microsoft.AspNetCore.Hosting;
using MVClogin2.Models;
using MVClogin2.Services;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            List <UserModel> list = new List<UserModel>();
            string queryString =  $"SELECT [UserName], [firstName], [lastName] FROM [{sqlModel.Database}].[dbo].[AspNetUsers]";
            SqlCommand sqlCommand = new SqlCommand(queryString,sqlConnection);
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
         public void CloseConnection()
         {
            sqlConnection.Close();
         }
    }
}
