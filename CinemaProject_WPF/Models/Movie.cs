using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject_WPF.Models
{
    public class Movie : Entity
    {
        public string Title { get; set; }
        public string IMDB { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public string PosterPath { get; set; }
        public string TrailerLink { get; set; }

        public Movie() { }
        public Movie(dynamic title, dynamic imdb, dynamic year, dynamic genre, dynamic country, dynamic language, dynamic type, dynamic poster, dynamic trailer = null)
        {
            Title = title;
            IMDB = imdb;
            Year = year;
            Genre = genre;
            Country = country;
            Language = language;
            Type = type;
            PosterPath = poster;
            TrailerLink = trailer;
        }
    }
}
