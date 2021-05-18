using CinemaProject_WPF.Command;
using CinemaProject_WPF.Repository;
using CinemaProject_WPF.Database;
using CinemaProject_WPF.Models;
using CinemaProject_WPF.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace CinemaProject_WPF.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }

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

        public RelayCommand SignUpCommand { get; set; }
        public RelayCommand SignInCommand { get; set; }

        public SignUpViewModel()
        {
            if (Helper.File.ReadJSON_Users("users.json") != null) DB.Users = Helper.File.ReadJSON_Users("users.json");
            SignUpCommand = new RelayCommand((e) =>
            {
                Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"); // Email regex
                bool find = false;
                if (Name == null) MessageBox.Show("Name can't be emtpy !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (Surname == null) MessageBox.Show("Surname can't be emtpy !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (Email == null) MessageBox.Show("Email can't be emtpy !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (!regex.IsMatch(Email)) MessageBox.Show("Email is not valid !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (Password == null) MessageBox.Show("Passowrd can't be emtpy !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (Password.Length < 8) MessageBox.Show("Your password must be longer than 8 characters !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                if (Repo.GetUsers() != null)
                {
                    foreach (var user in Repo.GetUsers())
                    {
                        if (user.Email == Email)
                        {
                            find = true;
                            MessageBox.Show("Email is already used", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }

                if (!find)
                {
                    if (Name != null && Surname != null && Email != null && regex.IsMatch(Email) && Password != null && Password.Length > 8)
                    {
                        User temp_user = new User(Name, Surname, Email, Password);
                        DB.Users.Add(temp_user);
                        Helper.File.WriteJSON(DB.Users, "users.json");
                        SignInWindow signInWindow = new SignInWindow();
                        signInWindow.Show();
                        CloseAction();
                    }
                }
            });

            SignInCommand = new RelayCommand((e) =>
            {
                SignInWindow signInWindow = new SignInWindow();
                signInWindow.Show();
                CloseAction();
            });
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
