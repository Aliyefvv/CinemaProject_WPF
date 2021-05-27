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
        public static List<Movie> DefaultMovies { get; set; } = new List<Movie>();
        public static List<Pushpin> Pushpins { get; set; } = new List<Pushpin>()
        {
            new Pushpin()
            {
                Name = "Cinema Plus 28 May",
                Location = new Microsoft.Maps.MapControl.WPF.Location(40.379460925461856,49.847255536029124),
                ImagePath = "../Assets/MainPage/Cinema/28may.jpg"
            },
            new Pushpin()
            {
                Name = "Park Cinema Park Bulvar",
                Location = new Microsoft.Maps.MapControl.WPF.Location(40.37125252192591, 49.850304372821135),
                ImagePath = "../Assets/MainPage/Cinema/parkcinema.jpg"
            },
            new Pushpin()
            {
                Name = "Cinema Plus",
                Location = new Microsoft.Maps.MapControl.WPF.Location(40.36887766462374, 49.83753732679958),
                ImagePath = "../Assets/MainPage/Cinema/cinemaplus.jpg"
            },
            new Pushpin()
            {
                Name = "Nizami Kinoteatrı",
                Location = new Microsoft.Maps.MapControl.WPF.Location(40.37559523983542, 49.84325234271758),
                ImagePath = "../Assets/MainPage/Cinema/nizami.jpg"
            },
        };
        public static List<Ticket> SoldTickets { get; set; } = new List<Ticket>();
        public static List<Product> Products { get; set; } = new List<Product>()
        {
            new Product()
            {
                ImagePath = "../Assets/BuyTicketPage/ProductImages/popcorn.png",
                Name = "Popcorn",
                Price = "1 ₼"
            },
            new Product()
            {
                ImagePath = "../Assets/BuyTicketPage/ProductImages/fanta.png",
                Name = "Fanta",
                Price = "2 ₼",
            },
            new Product()
            {
                ImagePath = "../Assets/BuyTicketPage/ProductImages/pepsi.png",
                Name = "Pepsi",
                Price = "2 ₼",
            },
            new Product()
            {
                ImagePath = "../Assets/BuyTicketPage/ProductImages/lays.gif",
                Name = "Lays",
                Price = "3 ₼",
            },
             new Product()
            {
                ImagePath = "../Assets/BuyTicketPage/ProductImages/water.png",
                Name = "Water",
                Price = "1 ₼",
            }
        };
    }
}
