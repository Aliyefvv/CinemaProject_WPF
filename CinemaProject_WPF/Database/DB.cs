using CinemaProject_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject_WPF.Database
{
    public static class DB
    {
        public static List<User> Users { get; set; } = new List<User>();
        public static List<Admin> Admins { get; set; } = new List<Admin>();
        public static List<Movie> DefaultMovies { get; set; } = new List<Movie>();
    }
}
