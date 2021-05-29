using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CinemaProject_WPF.Models
{
    public class Product : UserControl
    {
        public string ImagePath { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(Product), new PropertyMetadata(0));

        public Product()
        {
            Value = 0;
        }
    }
}
