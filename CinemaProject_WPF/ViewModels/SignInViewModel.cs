using CinemaProject_WPF.Command;
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

namespace CinemaProject_WPF.ViewModels
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }

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

        public SignInViewModel()
        {
            SignInCommand = new RelayCommand((e) =>
            {
                 MainWindow mainWindow = new MainWindow();
                 mainWindow.Show();
                 CloseAction();
                //if (Email == null) MessageBox.Show("Email can't be emtpy !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                //else if (Password == null) MessageBox.Show("Passowrd can't be emtpy !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                //bool find = false;
                //if (DB.Users != null)
                //{
                //    foreach (var user in DB.Users)
                //    {
                //        if (user.Email == Email && user.Password == Password)
                //        {
                //            find = true;
                //            MainWindow mainWindow = new MainWindow(user);
                //            mainWindow.Show();
                //            CloseAction();
                //        }
                //    }
                //    if (!find) MessageBox.Show("Email or Password is wrong !", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                //}
            });

            SignUpCommand = new RelayCommand((e) =>
            {
                SignUpWindow signUpWindow = new SignUpWindow();
                signUpWindow.Show();
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
