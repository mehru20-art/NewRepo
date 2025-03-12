using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Library_Management_System
{
    internal class User
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static int StudentID { get; set; }
        public User(string Username, string Password, int StudentID)
        {
            User.Username = Username;
            User.Password = Password;
            User.StudentID = StudentID;
        }

        
    }
}
