using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; } // Changed from Username to Email
        public string Password { get; set; }
        public string Role { get; set; } // Finder or Owner
    }
}
