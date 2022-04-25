using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace User
{
    public class UserInfo : IUserInfo
    {
        private string filepath = "C:/Users/Owner/Desktop/Revature/Visual Studio/Project0_Sean_Letts/User/UserDatabase/";
        private string jsonString;
        public void addNewUser(UserDetails newuser)
        {
            var allUsers = GetAllUsers();
            allUsers.Add(newuser);
            var usersString = JsonSerializer.Serialize<List<UserDetails>>(allUsers, new JsonSerializerOptions { WriteIndented = true });
            try
            {
                File.WriteAllText(filepath + "users.json", usersString);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Please check the path, " + ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Please check the file name, " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool validateUser(UserDetails loginUser)
        {
            var allUsers = GetAllUsers();
            var filteredUsers = allUsers.Where(p => p.UserName.Equals(loginUser.UserName)
            && p.Password.Equals(loginUser.Password)
            && p.IsAdmin.Equals(loginUser.IsAdmin)).ToList(); // Method Syntax
            if (filteredUsers.Count > 0)
                return true;
            return false;
        }

        public List<UserDetails> GetAllUsers()
        {
            try
            {
                jsonString = File.ReadAllText(filepath + "users.json");
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Please check the path, " + ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Please check the file name, " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (!string.IsNullOrEmpty(jsonString))
                return JsonSerializer.Deserialize<List<UserDetails>>(jsonString);
            else
                return null;
        }
    }
}
