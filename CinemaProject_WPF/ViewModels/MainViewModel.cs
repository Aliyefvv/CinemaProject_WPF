using CinemaProject_WPF.Command;
using CinemaProject_WPF.Database;
using CinemaProject_WPF.Helper;
using CinemaProject_WPF.Models;
using CinemaProject_WPF.Repository;
using CinemaProject_WPF.Views;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

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
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(); }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        private string _instagram;
        public string Instagram
        {
            get { return _instagram; }
            set { _instagram = value; OnPropertyChanged(); }
        }
        private string _facebook;
        public string Facebook
        {
            get { return _facebook; }
            set { _facebook = value; OnPropertyChanged(); }
        }
        private string _twitter;
        public string Twitter
        {
            get { return _twitter; }
            set { _twitter = value; OnPropertyChanged(); }
        }
        private string _snapchat;
        public string Snapchat
        {
            get { return _snapchat; }
            set { _snapchat = value; OnPropertyChanged(); }
        }
        private string _profilePhoto;
        public string ProfilePhoto
        {
            get { return _profilePhoto; }
            set { _profilePhoto = value; OnPropertyChanged(); }
        }
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; OnPropertyChanged(); }
        }
        private MapMode mapMode;
        public MapMode MapMode
        {
            get { return mapMode; }
            set { mapMode = value; OnPropertyChanged(); }
        }
        private string selectedMode;
        public string SelectedMode
        {
            get { return selectedMode; }
            set { selectedMode = value; OnPropertyChanged(); }
        }
        private string _about;
        public string About
        {
            get { return _about; }
            set { _about = value; OnPropertyChanged(); }
        }

        public string AboutAZ = @"
CinemaPlus şəbəkəsinə 8 kinoteatr, 38 ekran və 3802 oturacaq daxildir.
Yüksək ölçülü 3D-kontentini nümayiş etdirmək imkanı olan rəqəmsal proyeksiya sistemi və 
yüksək keyfiyyətli kinoekranlar ilə təchiz olunub. Həmçinin, gücləndirilmiş parlaqlıq və 
“Enhanced 4K Barco” dəqiq təsvirinə malikdir. Bütün bunlar və başqa amillər kinofilmləri 
ən yaxşı keyfiyyətdə nümayiş etdirmək imkanı yaradır.

“CinemaPlus” kinoteatrlar şəbəkəsinin tətbiq etdiyi “Platinum Movie Suites” konsepsiyası 
tamaşaçılara yüksək komfortlu, dəbdəbəli və dəridən hazırlanmış, söykənəcəyi tam arxaya açılan
italyan kreslolarda film izləmək və kinoseans zamanı qida və içkilərin sifariş etmək imkanı yaradır.

Bundan əlavə “CinemaPlus” Azərbaycanda ilk dəfə Dolby Atmos texnologiyası tətbiq edib.

Səs müşayiətini kinoteatrın istənilən yerinə yerləşdirmək və yerini dəyişmək imkanı 
hesabına Dolby Digital Atmos film yaradıcılarına kinoda səsi yeni bir mərhələyə çıxarmaq imkan yaradır.
Nəticədə tamaşaçı ekranda baş verənləri sadəcə izləmir, hadisənin mərkəzinə daxil olur.";
        public string AboutEN = @"
The CinemaPlus network includes 8 cinemas, 38 screens and 3802 seats.
Digital projection system and the ability to display high-dimensional 3D content
Equipped with high quality movie screens. Also, enhanced brightness and
Enhanced 4K Barco has an accurate description. All these and other factors make movies
allows you to demonstrate the best quality.

Platinum Movie Suites concept implemented by CinemaPlus cinema network
Highly comfortable, luxurious and made of leather, the back of which is fully open
allows you to watch a movie in Italian seats and order food and drinks during the movie.

In addition, CinemaPlus has introduced Dolby Atmos technology for the first time in Azerbaijan.

Ability to place and change the soundtrack anywhere in the cinema
Dolby Digital Atmos allows filmmakers to take their voice in cinema to a new level.
As a result, the viewer not only watches what is happening on the screen, but enters the center of the event.";
        public string AboutRU = @"
Сеть CinemaPlus включает 8 кинотеатров, 38 экранов и 3802 места.
Цифровая проекционная система и возможность отображать объемный 3D-контент
Оборудован высококачественными киноэкранами. Также повышенная яркость и
У Enhanced 4K Barco есть точное описание. Все эти и другие факторы делают фильмы
позволяет продемонстрировать лучшее качество.

Концепция Platinum Movie Suites реализована сетью кинотеатров CinemaPlus
Очень удобная, роскошная, кожаная, задняя часть которой полностью открыта.
позволяет смотреть фильм на итальянских сиденьях и заказывать еду и напитки во время фильма.

Кроме того, CinemaPlus впервые в Азербайджане представила технологию Dolby Atmos.

Возможность разместить и изменить саундтрек в любом месте кинотеатра
Dolby Digital Atmos позволяет кинематографистам поднять свой голос в кино на новый уровень.
В результате зритель не только наблюдает за происходящим на экране, но и попадает в центр события.";

        public RelayCommand SelectedItemChangedCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand VideoLoadCommand { get; set; }
        public RelayCommand BuyTicketCommand { get; set; }
        public RelayCommand SaveChangesCommand { get; set; }
        public RelayCommand ChangeProfilePhotoCommand { get; set; }
        public RelayCommand InstagramCommand { get; set; }
        public RelayCommand FacebookCommand { get; set; }
        public RelayCommand YoutubeCommand { get; set; }
        public RelayCommand TwitterCommand { get; set; }
        public RelayCommand SelectedIndexChangedCommand { get; set; }
        public RelayCommand ChangeMapModeCommand { get; set; }
        public RelayCommand SocialMediaCommand { get; set; }

        public void OpenSocialMediaPage(object param)
        {
            if (param != null)
            {
                string url = param as string;
                System.Diagnostics.Process.Start(url);
            }
        }

        private async void Search(string movieName)
        {
            var searchListRequest = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAP3A7I4GkT-5JRG2J6RPQC4c3nKCge9ls",
                ApplicationName = this.GetType().ToString()
            }).Search.List("snippet");
            searchListRequest.Q = $"{movieName} trailer";
            searchListRequest.MaxResults = 1;
            string _videoId = (await searchListRequest.ExecuteAsync()).Items[0].Id.VideoId;
            MovieVideoURL = $@"https://www.youtube.com/embed/{_videoId}?&start=5&autoplay=1&controls=0&showinfo=0&rel=0&disablekb=1&iv_load_policy=3&loop=1&modestbranding=1";
        }

        public void GetMovies()
        {
            string[] movies = { "Thor", "Avengers Endgame", "The Godfather", "Interstellar", "Game Of Thrones", "Venom", "The Maze Runner", "Joker" };
            foreach (var item in movies)
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response = http.GetAsync($@"http://www.omdbapi.com/?apikey=a91a5037&t={item}&plot=full").Result;
                var str = response.Content.ReadAsStringAsync().Result;
                Data = JsonConvert.DeserializeObject(str);
                Movie movie = new Movie(Data.imdbID, Data.Title, Data.imdbRating, Data.Released, Data.Genre, Data.Country, Data.Language, Data.Type, Data.Poster);
                Movies.Add(movie);
            }
        }

        public string Key { get; set; } = "grLxkFvx8zUmfU6sts1Z~U9NagxEUPjUt0VxN0T3EYg~Ak7wcNEmy_sJeP3S12za3kh8OsOyGyRR1sWpkLFFnrGS_qelcVs3knkQ56yyjMje";
        public ApplicationIdCredentialsProvider Provider { get; set; }
        public List<Models.Pushpin> Pushpins { get; set; }

        public dynamic Data { get; set; }
        readonly HttpClient http = new HttpClient();

        public MainViewModel(User user)
        {
            About = AboutAZ;
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
            Password = user.Password;
            ProfilePhoto = user.ProfilePhoto;
            Instagram = user.Instagram;
            Facebook = user.Facebook;
            Twitter = user.Twitter;
            Snapchat = user.Snapchat;

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
                Search(MovieTitle);
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

            SaveChangesCommand = new RelayCommand((e) =>
            {
                bool haveWarning = false;
                foreach (var item in DB.Users)
                {
                    if (user.ID == item.ID)
                    {
                        bool find = false;
                        Regex email_regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                        Regex instagram_regex = new Regex(@"(https?)?:?(www)?instagram\.com/[a-z].{3}");
                        Regex facebook_regex = new Regex(@"(?:https?:\/\/)?(?:www\.)?(mbasic.facebook|m\.facebook|facebook|fb)\.(com|me)\/(?:(?:\w\.)*#!\/)?(?:pages\/)?(?:[\w\-\.]*\/)*([\w\-\.]*)");
                        Regex twitter_regex = new Regex(@"/(?:http:\/\/)?(?:www\.)?twitter\.com\/(?:(?:\w)*#!\/)?(?:pages\/)?(?:[\w\-]*\/)*([\w\-]*)/");
                        Regex snapchat_regex = new Regex(@"^(?!.*\.\.|.*__|.*\-\-)(?!.*\.$|.*_$|.*\-$)(?!.*\.\-|.*\-\.|.*\-_|.*_\-|.*\._|.*_\.)[a-zA-Z]+[\w.-][0-9A-z]{0,15}$");
                        if (Name == null) MessageBox.Show("Name can't be emtpy !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else if (Surname == null) MessageBox.Show("Surname can't be emtpy !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else if (Email == null) MessageBox.Show("Email can't be emtpy !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else if (!email_regex.IsMatch(Email)) MessageBox.Show("Email is not valid !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else if (Password == null) MessageBox.Show("Passowrd can't be emtpy !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else if (Password.Length < 8) MessageBox.Show("Your password must be longer than 8 characters !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        if (Instagram != null)
                        {
                            if (!instagram_regex.IsMatch(Instagram) && Instagram != "")
                            { MessageBox.Show("Instagram is not valid !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); haveWarning = true; }
                        }
                        if (Facebook != null)
                        {
                            if (!facebook_regex.IsMatch(Facebook) && Facebook != "")
                            { MessageBox.Show("Facebook is not valid !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); haveWarning = true; }
                        }
                        if (Twitter != null)
                        {
                            if (!twitter_regex.IsMatch(Twitter) && Twitter != "")
                            { MessageBox.Show("Twitter is not valid !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); haveWarning = true; }
                        }
                        if (Snapchat != null)
                        {
                            if (!snapchat_regex.IsMatch(Snapchat) && Snapchat != "")
                            { MessageBox.Show("Snapchat is not valid !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); haveWarning = true; }
                        }
                        if (Repo.GetUsers() != null)
                        {
                            foreach (var repo_user in Repo.GetUsers())
                                if (repo_user.Email == Email && Email != user.Email)
                                {
                                    MessageBox.Show("Email is already used", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    find = true; haveWarning = true;
                                }
                        }
                        if (!find)
                        {
                            if (Name != null && Surname != null && Email != null && email_regex.IsMatch(Email) && Password != null && Password.Length > 8 && !haveWarning)
                            {
                                item.Name = Name;
                                item.Surname = Surname;
                                item.Email = Email;
                                item.Password = Password;
                                item.Instagram = Instagram;
                                item.Facebook = Facebook;
                                item.Twitter = Twitter;
                                item.Snapchat = Snapchat;
                                item.ProfilePhoto = ProfilePhoto;
                                File.UpdateUser(DB.Users);
                                MessageBox.Show("All Changes Saved !", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                }
            });

            ChangeProfilePhotoCommand = new RelayCommand((e) =>
            {
                OpenFileDialog op = new OpenFileDialog
                {
                    Title = "Select a picture",
                    Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                             "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                             "Portable Network Graphic (*.png)|*.png"
                };
                if (op.ShowDialog() == true)
                {
                    ProfilePhoto = op.FileName;
                }
            });

            InstagramCommand = new RelayCommand((e) =>
            {
                System.Diagnostics.Process.Start("http://www.instagram.com");
            });

            FacebookCommand = new RelayCommand((e) =>
            {
                System.Diagnostics.Process.Start("https://www.facebook.com/CINEMAPLUS.az/");
            });

            YoutubeCommand = new RelayCommand((e) =>
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/user/The28Cinema?feature=watch");
            });

            TwitterCommand = new RelayCommand((e) =>
            {
                System.Diagnostics.Process.Start("https://twitter.com/CinemaPlusAz");
            });

            SelectedIndexChangedCommand = new RelayCommand((e) =>
            {
                if (SelectedIndex == 0) About = AboutAZ;
                else if (SelectedIndex == 1) About = AboutEN;
                else if (SelectedIndex == 2) About = AboutRU;
            });

            ChangeMapModeCommand = new RelayCommand((e) =>
            {
                if (SelectedMode == "System.Windows.Controls.ComboBoxItem: Aerial") MapMode = new AerialMode();
                else if (SelectedMode == "System.Windows.Controls.ComboBoxItem: Aerial with labels") MapMode = new AerialMode(true);
                else if (SelectedMode == "System.Windows.Controls.ComboBoxItem: Road") MapMode = new RoadMode();
            });

            SocialMediaCommand = new RelayCommand(OpenSocialMediaPage);

            Provider = new ApplicationIdCredentialsProvider(Key);
            Pushpins = DB.Pushpins;
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
