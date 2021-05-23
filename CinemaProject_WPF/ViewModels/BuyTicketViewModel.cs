using CinemaProject_WPF.Command;
using CinemaProject_WPF.Models;
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
    public class BuyTicketViewModel : INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }
        private DateTime _displayDateEnd;
        public DateTime DisplayDateEnd
        {
            get { return _displayDateEnd; }
            set { _displayDateEnd = value; OnPropertyChanged(); }
        }
        private decimal _total;
        public decimal Total
        {
            get { return _total; }
            set { _total = value; OnPropertyChanged(); }
        }
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(); }
        }
        private string _selectedTime;
        public string SelectedTime
        {
            get { return _selectedTime; }
            set { _selectedTime = value; OnPropertyChanged(); }
        }
        private bool _rbtn1;
        public bool RadioButton1
        {
            get { return _rbtn1; }
            set { _rbtn1 = value; OnPropertyChanged(); }
        }
        private bool _rbtn2;
        public bool RadioButton2
        {
            get { return _rbtn2; }
            set { _rbtn2 = value; OnPropertyChanged(); }
        }
        private bool _rbtn3;
        public bool RadioButton3
        {
            get { return _rbtn3; }
            set { _rbtn3 = value; OnPropertyChanged(); }
        }

        public int TicketCount { get; set; } = 0;
        public List<int> TicketNumbers { get; set; } = new List<int>();

        public RelayCommand CheckedCheapSeatCommand { get; set; }
        public RelayCommand UnCheckedCheapSeatCommand { get; set; }
        public RelayCommand CheckedExpensiveSeatCommand { get; set; }
        public RelayCommand UnCheckedExpensiveSeatCommand { get; set; }
        public RelayCommand BuyTicketCommand { get; set; }

        public void ClickExecute_CheckedCheapSeat(object param)
        {
            string name = param as string;
            Total += 4;
            TicketCount++;
            TicketNumbers.Add(Convert.ToInt32(name));
        }
        public void ClickExecute_UnCheckedCheapSeat(object param)
        {
            string name = param as string;
            Total -= 4;
            TicketCount--;
            TicketNumbers.Remove(Convert.ToInt32(name));
        }
        public void ClickExecute_CheckedExpensiveSeat(object param)
        {
            string name = param as string;
            Total += 7;
            TicketCount++;
            TicketNumbers.Add(Convert.ToInt32(name));
        }
        public void ClickExecute_UnCheckedExpensiveSeat(object param)
        {
            string name = param as string;
            Total -= 7;
            TicketCount--;
            TicketNumbers.Remove(Convert.ToInt32(name));
        }

        public BuyTicketViewModel(string movieTitle)
        {
            DisplayDateEnd = DateTime.Now.AddMonths(2);
            SelectedDate = DateTime.Now;
            RadioButton3 = true;
            Total = 0;
            CheckedCheapSeatCommand = new RelayCommand(ClickExecute_CheckedCheapSeat);
            UnCheckedCheapSeatCommand = new RelayCommand(ClickExecute_UnCheckedCheapSeat);
            CheckedExpensiveSeatCommand = new RelayCommand(ClickExecute_CheckedExpensiveSeat);
            UnCheckedExpensiveSeatCommand = new RelayCommand(ClickExecute_UnCheckedExpensiveSeat);
            BuyTicketCommand = new RelayCommand((e) =>
            {
                Ticket ticket = new Ticket();
                ticket.MovieName = movieTitle;
                ticket.Date = SelectedDate;
                if (RadioButton1) ticket.Time = "13:00-15:00";
                else if (RadioButton2) ticket.Time = "16:00-18:00";
                else if (RadioButton3) ticket.Time = "20:00-22:00";
                ticket.TicketCount = TicketCount;
                ticket.Total = Total;
                ticket.TicketNumbers = TicketNumbers;
                Helper.File.PrintVaucher(ticket);
                MessageBox.Show("Thank you for buying tickets. Enjoy it !", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
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
