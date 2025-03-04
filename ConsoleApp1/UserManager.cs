using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class UserManager
    {
        private List<User> users = new List<User>();

        public UserManager()
        {
            users.Add(new User { Username = "dilini", Email = "dilini@gmail.com", Password = "123", Role = "owner" });
            users.Add(new User { Username = "buddhi", Email = "buddhi@gmail.com", Password = "234", Role = "owner" });
            users.Add(new User { Username = "deumini", Email = "deumini@gmail.com", Password = "345", Role = "finder" });
            users.Add(new User { Username = "hasangi", Email = "hasangi@gmail.com", Password = "456", Role = "finder" });
        }

        public User Signup(string username, string email, string password, string role)
        {
            if (users.Exists(u => u.Email == email)) // Checking email for uniqueness
            {
                Console.WriteLine("Email already exists.");
                return null;
            }
            User newUser = new User { Username = username, Email = email, Password = password, Role = role };
            users.Add(newUser);
            return newUser;
        }

        public User Login(string email, string password, string role)
        {
            return users.Find(u => u.Email == email && u.Password == password && u.Role == role); // Check if email, password, and role match
        }
    }
}

