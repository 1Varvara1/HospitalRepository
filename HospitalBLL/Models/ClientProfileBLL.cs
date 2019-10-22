using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class ClientProfileBLL
    {
        public string IdClientProfile { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public ClientProfileBLL(string id , string name, string surname, string secondName,
            DateTime birth, string email, string role)
        {
            IdClientProfile = id;
            Name = name;
            Surname = surname;
            SecondName = secondName;
            Birth = birth;
            Email = email;
            Role = role;
        }
       
    }
}
