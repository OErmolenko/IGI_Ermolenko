using System;
using System.Collections.Generic;

namespace lab_3_State_Cache.Models
{
    public class Aircraft
    {
        public int AircraftId { get; set; }
        public string Mark { get; set; }
        public int Capasity { get; set; }
        public double Carrying { get; set; }
        //public string TechnicalParameters { get; set; }
        //public DateTime DateIssue { get; set; }

        public int? TypeId { get; set; }
        public virtual Type Type { get; set; }
        public virtual List<Flight> Flights { get; set; }
    }
}
