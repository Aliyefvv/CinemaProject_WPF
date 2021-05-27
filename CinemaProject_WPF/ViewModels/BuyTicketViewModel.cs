using CinemaProject_WPF.Command;
using CinemaProject_WPF.Database;
using CinemaProject_WPF.Helper;
using CinemaProject_WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

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
        private int _value;
        public int Valuee
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged(); }
        }

        public List<ToggleButton> CheapSeats { get; set; } = new List<ToggleButton>();
        public List<ToggleButton> ExpensiveSeats { get; set; } = new List<ToggleButton>();
        public List<Product> Products { get; set; } = new List<Product>();

        public int TicketCount { get; set; } = 0;
        public List<int> TicketNumbers { get; set; } = new List<int>();

        public RelayCommand CheckedCheapSeatCommand { get; set; }
        public RelayCommand UnCheckedCheapSeatCommand { get; set; }
        public RelayCommand CheckedExpensiveSeatCommand { get; set; }
        public RelayCommand UnCheckedExpensiveSeatCommand { get; set; }
        public RelayCommand BuyTicketCommand { get; set; }
        public RelayCommand RadioButtonCheckedCommand { get; set; }
        public RelayCommand SelectedDateChangedDateCommand { get; set; }
        public RelayCommand SelectProductCommand { get; set; }

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
        public void ProductValueChanged(object param)
        {
            decimal price = decimal.Parse((param as string).Split()[0]);
            MessageBox.Show((Valuee * price).ToString());
        }

        private void DisableSeats(Ticket ticket)
        {
            foreach (var item in ticket.TicketNumbers)
            {
                if (CheapSeats.Exists(s => s.Content.ToString() == item.ToString()))
                {
                    var btn = CheapSeats.FirstOrDefault(s => s.Content.ToString() == item.ToString());
                    btn.IsEnabled = false;
                }
                if (ExpensiveSeats.Exists(s => s.Content.ToString() == item.ToString()))
                {
                    var btn = ExpensiveSeats.FirstOrDefault(s => s.Content.ToString() == item.ToString());
                    btn.IsEnabled = false;
                }
            }
        }
        private void CheckTickets(string movieTitle)
        {
            foreach (var t in DB.SoldTickets)
            {
                if (movieTitle == t.MovieName && SelectedDate.Month == t.Date.Month && SelectedDate.Day == t.Date.Day)
                {
                    if (t.Time == "13:00-15:00" && RadioButton1) DisableSeats(t);
                    else if (t.Time == "16:00-18:00" && RadioButton2) DisableSeats(t);
                    else if (t.Time == "20:00-22:00" && RadioButton3) DisableSeats(t);
                }
            }
        }
        public void EnableSeats()
        {
            foreach (var item in CheapSeats) item.IsEnabled = true;
            foreach (var item in ExpensiveSeats) item.IsEnabled = true;
        }
        public BuyTicketViewModel(string movieTitle)
        {
            Valuee = 0;
            Products = DB.Products;
            if (Helper.File.ReadJSON_Tickets("soldtickets.json") != null) DB.SoldTickets = Helper.File.ReadJSON_Tickets("soldtickets.json");
            for (int i = 1; i <= 12; i++)
            {
                ExpensiveSeats.Add(new ToggleButton()
                {
                    Name = "button" + i.ToString(),
                    Content = i.ToString()
                });
            }
            for (int i = 13; i < 37; i++)
            {
                CheapSeats.Add(new ToggleButton()
                {
                    Name = "button" + i.ToString(),
                    Content = i.ToString()
                });
            }
            DisplayDateEnd = DateTime.Now.AddMonths(2);
            SelectedDate = DateTime.Now;
            RadioButton3 = true;
            Total = 0;
            CheckedCheapSeatCommand = new RelayCommand(ClickExecute_CheckedCheapSeat);
            UnCheckedCheapSeatCommand = new RelayCommand(ClickExecute_UnCheckedCheapSeat);
            CheckedExpensiveSeatCommand = new RelayCommand(ClickExecute_CheckedExpensiveSeat);
            UnCheckedExpensiveSeatCommand = new RelayCommand(ClickExecute_UnCheckedExpensiveSeat);
            CheckTickets(movieTitle);
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
                DB.SoldTickets.Add(ticket);
                MessageBox.Show("Thank you for buying tickets. Enjoy it !", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                File.WriteJSON(DB.SoldTickets, "soldtickets.json");
                CloseAction();
            });

            RadioButtonCheckedCommand = new RelayCommand((e) =>
            {
                EnableSeats();
                CheckTickets(movieTitle);
            });

            SelectedDateChangedDateCommand = new RelayCommand((e) =>
            {
                EnableSeats();
                CheckTickets(movieTitle);
            });

            SelectProductCommand = new RelayCommand(ProductValueChanged);
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
