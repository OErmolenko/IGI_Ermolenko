using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using lab_1_Entity_Framework.Models;
using System.Collections.Generic;


namespace lab_1_Entity_Framework
{
    class Program
    {
        static void Main(string[] args)
        {
            using(AirportContext db = new AirportContext())
            {
                DbInitializer.Initialize(db);
                Select(db); 
                Add(db); 
                Delete(db); 
                Update(db);
            }
        }

        static void Print<T>(string sqltext, IEnumerable<T> items)
        {
            Console.WriteLine(sqltext);
            Console.WriteLine("Записи: ");
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        static void Select(AirportContext airport)
        {
            var queryLINQ1 = from o in airport.Types
                             select o;
            Print("1. Выборку всех данных из таблицы, стоящей в схеме базы данных нас стороне отношения «один» – 1 шт.", queryLINQ1.ToList());

            var queryLINQ2 = from o in airport.Types
                             where (o.NameType.Length > 10 && o.NameType.Length < 15)
                             select o;
            Print("2. Выборку данных из таблицы, стоящей в схеме базы данных нас стороне отношения «один», отфильтрованные по определенному условию, налагающему ограничения на одно или несколько полей – 1 шт.", queryLINQ2.ToList()); 

            var queryLINQ3 = from o in airport.Tickets
                             group o.Price by o.FlightId into gr
                             select new
                             {
                                 Код_цены = gr.Key,
                                 Средняя_цена = gr.Average()
                             };
            Print("3. Выборку данных, сгруппированных по любому из полей данных с выводом какого-либо итогового результата (min, max, avg, сount или др.) по выбранному полю из таблицы, стоящей в схеме базы данных нас стороне отношения «многие» – 1 шт.", queryLINQ3.ToList()); 

            var queryLINQ4 = from o in airport.Tickets
                             join t in airport.Flights
                             on o.FlightId equals t.FlightId
                             select new
                             {
                                 Дата = t.Date,
                                 ФИО = o.FullName,
                                 Паспортные_данные = o.PassportData,
                                 Цена = o.Price,
                                 Впемя_перелета = t.FlightTime
                             };
            Print("4. Выборку данных из двух полей двух таблиц, связанных между собой отношением «один-ко-многим» – 1 шт.", queryLINQ4.ToList()); 

            var queryLINQ5 = from o in airport.Tickets
                             join t in airport.Flights
                            on o.FlightId equals t.FlightId
                             where o.Price > 100 && t.FlightTime < 50
                             select new
                             {
                                 Дата = t.Date,
                                 ФИО = o.FullName,
                                 Паспортные_данные = o.PassportData,
                                 Цена = o.Price,
                                 Впемя_перелета = t.FlightTime
                             };
            Print("5. Выборку данных из двух таблиц, связанных между собой отношением «один-ко-многим» и отфильтрованным по некоторому условию, налагающему ограничения на значения одного или нескольких полей – 1 шт.", queryLINQ5.ToList()); 

        }

        static void Add(AirportContext airport)
        {
            lab_1_Entity_Framework.Models.Type type = new lab_1_Entity_Framework.Models.Type
            {
                NameType = "Штрокофюзеляжные_21",
                Appointment = "Для перелетов на средние и большие дистанции (до 11 000 км)",
                Restrictions = "До 500 пассажиров"
            };
            airport.Types.Add(type);
            airport.SaveChanges();

            Aircraft aircraft = new Aircraft
            {
                Mark = "Airbus-A310",
                Capasity = 700,
                Carrying = 800,
                TypeId = type.TypeId
            };
            airport.Aircrafts.Add(aircraft);
            airport.SaveChanges();
        }

        static void Delete(AirportContext airport)
        {
            string place = "Москва";
            var flight = airport.Flights.Where(t => t.PlaceDeparture == place);
            var ticket = airport.Tickets.Include("Flight").Where(t => t.Flight.PlaceDeparture == place);
            airport.Tickets.RemoveRange(ticket);
            airport.SaveChanges();
            airport.Flights.RemoveRange(flight);
            airport.SaveChanges();
        }

        static void Update(AirportContext airport)
        {
            string name = "Аист Виктория Владимировна";
            var ticket = airport.Tickets.Where(t => t.FullName == name).FirstOrDefault();
            if (ticket != null)
            {
                ticket.Price = 50;
            }
            airport.SaveChanges();
        }
    }
}
