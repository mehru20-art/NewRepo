using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Library_Management_System
{
    internal class User
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static int StudentID { get; set; }
        public User(string Username, string password, int StudentID)
        {
            User.Username = Username;
            User.Password = password;
            User.StudentID = StudentID;
        }

        public static string HashingPassword(string password)
        {
            var sha256 = SHA256.Create();
            byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            var sb = new StringBuilder();
            foreach (byte b in data)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
            //data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            //return System.Text.Encoding.ASCII.GetString(data);
        }
    }
}
