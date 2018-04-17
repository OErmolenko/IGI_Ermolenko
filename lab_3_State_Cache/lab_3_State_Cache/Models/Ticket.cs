﻿namespace lab_3_State_Cache.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string FullName { get; set; }
        public string PassportData { get; set; }
        public string NumberPlace { get; set; }
        public double Price { get; set; }

        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
