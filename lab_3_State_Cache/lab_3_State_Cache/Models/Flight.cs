using System;
using System.Collections.Generic;

namespace lab_3_State_Cache.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public DateTime Date { get; set; }
        public string PlaceDeparture { get; set; }
        public string PlaceArrival { get; set; }
        public double FlightTime { get; set; }

        public int? AircraftId { get; set; }
        public virtual Aircraft Aircraft { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
    }
}
