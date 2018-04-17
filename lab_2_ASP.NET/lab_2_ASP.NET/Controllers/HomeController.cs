using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lab_2_ASP.NET.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab_2_ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        List<Ticket> listTickets;
        List<Flight> listFlight;
        List<Aircraft> listAircrafts;

        public HomeController()
        {
            listTickets = Ticket.Initializer(10);
            listFlight = Flight.Initializer(10);
            listAircrafts = Aircraft.Initializer(10);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Tickets()
        {
            List<string> list = new List<string> { "10A","10B","10C","10D","10E","10F","11A","11B","11C","11F"};
            ViewBag.NumberPlace = list;
            ViewBag.ListTicket = listTickets;
            return View();
        }

        [HttpPost]
        public IActionResult Tickets(string FullName, string PassportData, string NumberPlace)
        {
            Ticket ticket = new Ticket() { FullName = FullName, PassportData = PassportData, NumberPlace = NumberPlace, Price = 100 };
            listTickets.Add(ticket);
            List<string> list = new List<string> { "10A", "10B", "10C", "10D", "10E", "10F", "11A", "11B", "11C", "11F" };
            ViewBag.NumberPlace = list;
            ViewBag.ListTicket = listTickets;
            return View();
        }

        [HttpGet]
        public IActionResult Flights()
        {
            ViewBag.PlaceDeparture = new string[] { "Минск", "Москва", "Париж", "Варшава", "Дубай", "Джакарта", "Лос-Анджелес", "Токио", "Лондон" };
            ViewBag.ListFlight = listFlight;
            return View();
        }

        [HttpPost]
        public IActionResult Flights(string Date, string PlaceDeparture, string PlaceArrival)
        {
            string[] tmp = Date.Split(new char[] { '.', '-', '\\', '/', ' ' });
            Flight flight = new Flight()
            {
                Date = new DateTime(Convert.ToInt32(tmp[2]), Convert.ToInt32(tmp[1]), Convert.ToInt32(tmp[0])),
                PlaceDeparture = PlaceDeparture,
                PlaceArrival = PlaceArrival,
                FlightTime = 100
            };
            listFlight.Add(flight);
            ViewBag.PlaceDeparture = new string[] { "Минск", "Москва", "Париж", "Варшава", "Дубай", "Джакарта", "Лос-Анджелес", "Токио", "Лондон" };
            ViewBag.ListFlight = listFlight;
            return View();
        }

        [HttpGet]
        public IActionResult Aircrafts()
        {
            ViewBag.Mark = new string[] { "Airbus-A310", "Airbus-A380", "Boeing-737", "Boeing-777", "ИЛ-62", "ТУ-154", "Adam A500 Adamjet", "Aerospatiale N262", "Spike S-512", "Aero Boero AB-95" };
            ViewBag.ListAircrafts = listAircrafts;
            return View();
        }

        [HttpPost]
        public IActionResult Aircrafts(string Mark, int Capasity, double Carrying)
        {
            Aircraft aircraft = new Aircraft()
            {
                Mark = Mark,
                Capasity = Capasity,
                Carrying = Carrying
            };
            listAircrafts.Add(aircraft);
            ViewBag.Mark = new string[] { "Airbus-A310", "Airbus-A380", "Boeing-737", "Boeing-777", "ИЛ-62", "ТУ-154", "Adam A500 Adamjet", "Aerospatiale N262", "Spike S-512", "Aero Boero AB-95" };
            ViewBag.listAircrafts = listAircrafts;
            return View();
        }
    }
}
