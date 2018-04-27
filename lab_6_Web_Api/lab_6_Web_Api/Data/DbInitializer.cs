using System;
using System.Linq;

namespace lab_6_Web_Api.Models
{
    public static class DbInitializer
    {
        public static void Initialize(AirportContext db)
        {
            db.Database.EnsureCreated();

            if (db.Tickets.Any()) return;

            int aircraftNumber = 20;
            int typeNumber = 10;
            int flightNumber = 100;
            int ticketNumber = 200;
            
            Random randObj = new Random(1);

            string[] typesAircraft = { "Штрокофюзеляжные_", "Узкофюзеляжные_", "Региональные_" };
            string[] appointments = { "Для перелетов на средние и большие дистанции (до 11 000 км)", "Для маршрутов малой или средней протяженности", "На расстояние не превышающее 2-3 км" };
            string[] restrictions = { "До 100 пассажиров", "До 200 пассажиров", "До 500 пассажиров", "До 800 пассажиров" };
            for (int typeID = 0; typeID < typeNumber; typeID++)
            {
                string nameType = typesAircraft[randObj.Next(typesAircraft.Length)] + typeID.ToString();
                string appointment = appointments[randObj.Next(appointments.Length)];
                string restriction = restrictions[randObj.Next(restrictions.Length)];
                db.Types.Add(new Type { NameType = nameType, Appointment = appointment, Restrictions = restriction });
            }
            db.SaveChanges();

            string[] marks = { "Airbus-A310", "Airbus-A380", "Boeing-737", "Boeing-777", "ИЛ-62", "ТУ-154", "Adam A500 Adamjet", "Aerospatiale N262", "Spike S-512", "Aero Boero AB-95" };
            for (int aircraftID = 0; aircraftID < aircraftNumber; aircraftID++)
            {
                string mark = marks[randObj.Next(marks.Length)];
                int capasity = randObj.Next(300, 800);
                double carrying = 1000 * randObj.NextDouble();
                int typeID = randObj.Next(1, typeNumber - 1);
                db.Aircrafts.Add(new Aircraft { Mark = mark, Capasity = capasity, Carrying = carrying, TypeId = typeID });
            }
            db.SaveChanges();

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
                double time = 100 * randObj.NextDouble();
                int aircraftID = randObj.Next(1, aircraftNumber - 1);
                db.Flights.Add(new Flight { Date = date, PlaceDeparture = placeDeparture, PlaceArrival = placeArrival, FlightTime = time, AircraftId = aircraftID });
            }
            db.SaveChanges();


            string[] name = { "Ида", "Инга", "Инесса", "Ипполита", "Ирина", "Исидора", "Искра", "Иоанна", "Иона", "Илария" };
            string[] surname = { "Гусь", "Голубь", "Сорока", "Аист", "Ястреб", "Беркут", "Ласточка", "Снегирь", "Сеница", "Филин" };
            string[] middleName = { "Владимировна", "Вячеславовна", "Вадимовна", "Валентиновна", "Валерьевна", "Варламовна", "Васильевна", "Венедиктовна", "Вениаминовна", "Викторовна" };
            string[] placeNumber = { "A", "B", "C", "D", "E", "F", "G", "H" };
            for (int ticketID = 0; ticketID < ticketNumber; ticketID++)
            {
                string fullName = surname[randObj.Next(surname.Length)] + " "
                    + name[randObj.Next(name.Length)] + " "
                    + middleName[randObj.Next(middleName.Length)];
                string passportDate = "HB" + randObj.Next(100, 999).ToString() + randObj.Next(10, 99).ToString() + randObj.Next(10, 99).ToString();
                string number = placeNumber[randObj.Next(placeNumber.Length)] + (ticketID + 1).ToString();
                double price = 500 * randObj.NextDouble();
                int flightID = randObj.Next(1, flightNumber - 1);
                db.Tickets.Add(new Ticket { FullName = fullName, PassportData = passportDate, NumberPlace = number, Price = price, FlightId = flightID });
            }
            db.SaveChanges();
        }
    }
}
