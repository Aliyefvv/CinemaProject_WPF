using CinemaProject_WPF.Models;
using CinemaProject_WPF.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject_WPF.Repository
{
    public static class Repo
    {
        public static List<User> GetUsers()
        {
            return File.ReadJSON_Users("users.json") as List<User>;
        }
    }
}
