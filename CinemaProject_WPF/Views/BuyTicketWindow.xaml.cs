using CinemaProject_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaProject_WPF.Views
{
    /// <summary>
    /// Interaction logic for BuyTicketWindow.xaml
    /// </summary>
    public partial class BuyTicketWindow : Window
    {
        public BuyTicketWindow(string movieTitle)
        {
            InitializeComponent();
            BuyTicketViewModel vm = new BuyTicketViewModel(movieTitle);
            DataContext = vm;
            if (vm.CloseAction == null) vm.CloseAction = new Action(this.Close);
        }
    }
}
