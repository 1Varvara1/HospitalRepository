using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class UserBLL
    {
        public string IdClientProfile { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public DateTime ? Birth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool NeedDoctor { get; set; }

        public UserBLL(string id, string name, string surname,
            string secondName, DateTime? birth, string address, string email, string role, string password="")
        {
            IdClientProfile = id;
            Name = name;
            Surname = surname;
            SecondName = secondName;
            Birth = birth;
            Address = address;
            Email = email;
            Password = password;
            Role = role;
            NeedDoctor = false;
        }
        public UserBLL( string name, string surname,
          string secondName, DateTime? birth, string address, string email, string role, string password = "")
        {
            
            Name = name;
            Surname = surname;
            SecondName = secondName;
            Birth = birth;
            Address = address;
            Email = email;
            Password = password;
            Role = role;
            NeedDoctor = false;
        }
        public UserBLL()
        {

        }
    }
}
