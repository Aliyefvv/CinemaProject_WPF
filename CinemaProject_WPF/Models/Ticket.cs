using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject_WPF.Models
{
    public class Ticket
    {
        public string MovieName { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int TicketCount { get; set; }
        public List<int> TicketNumbers { get; set; }
        public Dictionary<string,int> Snacks { get; set; }
        public decimal Total { get; set; }
    }
}
