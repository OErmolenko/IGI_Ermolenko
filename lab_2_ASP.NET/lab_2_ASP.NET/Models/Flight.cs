using System;
using System.Collections.Generic;

namespace lab_2_ASP.NET.Models
{
    public class Flight
    {
        public DateTime Date { get; set; }
        public string PlaceDeparture { get; set; }
        public string PlaceArrival { get; set; }
        public int FlightTime { get; set; }

        public static List<Flight> Initializer(int flightNumber)
        {
            List<Flight> collection = new List<Flight>();
            Random randObj = new Random(1);

            string[] place = { "Минск", "Москва", "Париж", "Варшава", "Дубай", "Джакарта", "Лос-Анджелес", "Токио", "Лондон" };
            for (int flightID = 0; flightID < flightNumber; flightID++)
            {
                DateTime date = DateTime.Now.Date;
                date.AddDays(-flightID);
                string placeDeparture = place[randObj.Next(place.Length)];
                string placeArrival;
                do
                {
                    placeArrival = place[randObj.Next(place.Length)];
                } while (placeDeparture == placeArrival);
                int time = 10 * randObj.Next(1, 9);
                collection.Add(new Flight { Date = date, PlaceDeparture = placeDeparture, PlaceArrival = placeArrival, FlightTime = time });
            }

            return collection;
        }
    }
}
