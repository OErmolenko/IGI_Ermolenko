using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_6_Web_Api.ViewModels
{
    public class AircraftViewModels
    {
        public int AircraftId { get; set; }
        public string Mark { get; set; }
        public int Capasity { get; set; }
        public double Carrying { get; set; }
        public int? TypeId { get; set; }
        public string NameType { get; set; }
    }
}
