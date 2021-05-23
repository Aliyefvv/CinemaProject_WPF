using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject_WPF.Models
{
    public class Pushpin
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public string ImagePath { get; set; }
    }
}
