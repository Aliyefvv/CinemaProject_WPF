using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject_WPF.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilePhoto { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Snapchat { get; set; }

        public User() 
        {
            ProfilePhoto = "../Assets/MainPage/Profile/profile.jpg";
        }

        public User(string name,string surname,string email,string password)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Instagram = "";
            Facebook = "";
            Twitter = "";
            Snapchat = "";
            ProfilePhoto = "../Assets/MainPage/Profile/profile.jpg";
        }
    }
}
