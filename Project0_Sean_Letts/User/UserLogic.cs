using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace User
{
    public class UserLogic : IUserLogic
    {
        private const string filepath = "C:/Users/Owner/Desktop/Revature/Sean-Letts/Project0_Sean_Letts/User/UserDatabase/";
        private readonly string connectionString;

        public UserLogic(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void addNewUser(UserInfo newuser)
        {
            var allUsers = GetAllUsers();
            allUsers.Add(newuser);

            string commandString = "INSERT INTO Users (Username, Password, isAdmin) " +
                "VALUES (@Username, @Password, @isAdmin);";
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(commandString, connection);

            command.Parameters.AddWithValue("@Username", newuser.UserName);
            command.Parameters.AddWithValue("@Password", newuser.Password);
            command.Parameters.AddWithValue("@isAdmin", newuser.IsAdmin);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public bool validateUser(UserInfo loginUser)
        {
            var allUsers = GetAllUsers();
            var filteredUsers = allUsers.Where(p => p.UserName.Equals(loginUser.UserName)
            && p.Password.Equals(loginUser.Password)
            && p.IsAdmin.Equals(loginUser.IsAdmin)).ToList(); // Method Syntax
            if (filteredUsers.Count > 0)
                return true;
            return false;
        }

        public List<UserInfo> GetAllUsers()
        {
            string commandString = "SELECT * FROM Users;";

            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(commandString, connection);
            IDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataSet = new();
            connection.Open();
            adapter.Fill(dataSet); // this sends the query. DataAdapter uses a DataReader to read.
            connection.Close();

            var usersd = new List<UserInfo>();
            
            DataColumn levelColumn = dataSet.Tables[0].Columns[2];
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                usersd.Add(new UserInfo
                {
                    UserName = (string)row["Username"],
                    Password = (string)row["Password"],
                    IsAdmin = (bool)row["isAdmin"]
                });
            }
            return usersd;
        }

    }
}
