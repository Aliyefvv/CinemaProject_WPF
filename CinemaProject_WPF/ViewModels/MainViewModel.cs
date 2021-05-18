using CinemaProject_WPF.Command;
using CinemaProject_WPF.Models;
using CinemaProject_WPF.Views;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YouTubeSearch;

namespace CinemaProject_WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public List<Movie> Movies { get; set; } = new List<Movie>();
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; OnPropertyChanged(); }
        }
        private string _moviePosterPath;
        public string MoviePosterPath
        {
            get { return _moviePosterPath; }
            set { _moviePosterPath = value; OnPropertyChanged(); }
        }
        private string _movieTitle;
        public string MovieTitle
        {
            get { return _movieTitle; }
            set { _movieTitle = value; OnPropertyChanged(); }
        }
        private string _movieImdb;
        public string MovieIMDB
        {
            get { return _movieImdb; }
            set { _movieImdb = value; OnPropertyChanged(); }
        }
        private string _movieYear;
        public string MovieYear
        {
            get { return _movieYear; }
            set { _movieYear = value; OnPropertyChanged(); }
        }
        private string _movieGenre;
        public string MovieGenre
        {
            get { return _movieGenre; }
            set { _movieGenre = value; OnPropertyChanged(); }
        }
        private string _moiveCountry;
        public string MovieCountry
        {
            get { return _moiveCountry; }
            set { _moiveCountry = value; OnPropertyChanged(); }
        }
        private string _movieLanguage;
        public string MovieLanguage
        {
            get { return _movieLanguage; }
            set { _movieLanguage = value; OnPropertyChanged(); }
        }
        private string _movieVideoURL;
        public string MovieVideoURL
        {
            get { return _movieVideoURL; }
            set { _movieVideoURL = value; OnPropertyChanged(); }
        }
        private string _movieType;
        public string MovieType
        {
            get { return _movieType; }
            set { _movieType = value; OnPropertyChanged(); }
        }
        private Movie _selectedItem;
        public Movie SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public RelayCommand SelectedItemChangedCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand VideoLoadCommand { get; set; }
        public RelayCommand BuyTicketCommand { get; set; }

        private string _videoId;
        private async Task Search(string movieName)
        {
            var searchListRequest = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCj6CNDKnOJEMFXm3Dt5CkBSmkk1cPbt5I",
                ApplicationName = this.GetType().ToString()
            }).Search.List("snippet");
            searchListRequest.Q = $"{movieName} trailer";
            searchListRequest.MaxResults = 1;
            _videoId = (await searchListRequest.ExecuteAsync()).Items[0].Id.VideoId;
        }

        public dynamic Data { get; set; }
        HttpClient http = new HttpClient();
        public MainViewModel()
        {
            GetMovies();
            SearchCommand = new RelayCommand((e) =>
            {
                HttpResponseMessage response = new HttpResponseMessage();
                var name = SearchText;
                response = http.GetAsync($@"http://www.omdbapi.com/?apikey=a91a5037&t={name}&plot=full").Result;
                var str = response.Content.ReadAsStringAsync().Result;
                Data = JsonConvert.DeserializeObject(str);
                if (name.Length > 1)
                {
                    try
                    {
                        MoviePosterPath = Data.Poster;
                        MovieTitle = Data.Title;
                        MovieIMDB = Data.imdbRating;
                        MovieYear = Data.Released;
                        MovieGenre = Data.Genre;
                        MovieCountry = Data.Country;
                        MovieLanguage = Data.Language;
                        MovieType = Data.Type;
                        //VideoSearch items = new VideoSearch();
                        //var video = items.GetVideos($"{MovieTitle} trailer", 1).Result[0];
                        //MovieVideoURL = video.getUrl();
                        Search(MovieTitle);
                    }
                    catch (Exception) { }
                }
            });

            SelectedItemChangedCommand = new RelayCommand((e) =>
            {
                MoviePosterPath = SelectedItem.PosterPath;
                MovieTitle = SelectedItem.Title;
                MovieIMDB = SelectedItem.IMDB;
                MovieYear = SelectedItem.Year;
                MovieGenre = SelectedItem.Genre;
                MovieCountry = SelectedItem.Country;
                MovieLanguage = SelectedItem.Language;
                MovieType = SelectedItem.Type;
            });

            BuyTicketCommand = new RelayCommand((e) =>
            {
                if (MovieTitle != null)
                {
                    BuyTicketWindow buyTicketWindow = new BuyTicketWindow(MovieTitle);
                    buyTicketWindow.ShowDialog();
                }
                else MessageBox.Show("You must select a movie !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            });
        }

        public void GetMovies()
        {
            string[] movies = { "Thor", "Avengers Endgame", "The Godfather", "Interstellar", "Game Of Thrones", "Venom", "The Maze Runner" };
            foreach (var item in movies)
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = http.GetAsync($@"http://www.omdbapi.com/?apikey=a91a5037&t={item}&plot=full").Result;
                var str = response.Content.ReadAsStringAsync().Result;
                Data = JsonConvert.DeserializeObject(str);
                Movie movie = new Movie(Data.Title, Data.imdbRating, Data.Released, Data.Genre, Data.Country, Data.Language, Data.Type, Data.Poster);
                Movies.Add(movie);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
