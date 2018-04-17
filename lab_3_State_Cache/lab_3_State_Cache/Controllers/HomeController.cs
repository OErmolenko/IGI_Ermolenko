using lab_3_State_Cache.Extensions;
using lab_3_State_Cache.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace lab_3_State_Cache.NET.Controllers
{
    public class HomeController : Controller
    {
        readonly AirportContext _airportContext;
        readonly IMemoryCache _cache;

        public HomeController(AirportContext airportContext, IMemoryCache cache)
        {
            _airportContext = airportContext;
            _cache = cache;
        }

        [ResponseCache(CacheProfileName = "Default")]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(CacheProfileName = "Default")]
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(CacheProfileName = "Default")]
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(CacheProfileName = "Default")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Tickets()
        {
            List<Ticket> list = new List<Ticket>
            {
                _cache.Get<Ticket>("Ticket")
            };

            ViewBag.FullName = Request.Cookies["FullName"] ?? "";
            ViewBag.PassportData = Request.Cookies["PassportData"] ?? "";

            if (HttpContext.Session.GetObject<List<Ticket>>("Tickets") != null)
                ViewBag.ListTicket = HttpContext.Session.GetObject<List<Ticket>>("Tickets");
            else ViewBag.ListTicket = list;

            return View();
        }

        [HttpPost]
        public IActionResult Tickets(string FullName, string PassportData)
        {
            List<Ticket> list = _airportContext
                .Tickets
                .Where(t => t.FullName.StartsWith(FullName ?? ""))
                .Where(t => t.PassportData.StartsWith(PassportData ?? ""))
                .ToList();
            Response.Cookies.Append("FullName", FullName ?? "");
            Response.Cookies.Append("PassportData", PassportData ?? "");

            ViewBag.ListTicket = list;

            HttpContext.Session.SetObject("Tickets", list.ToList());

            return View();
        }

        [HttpGet]
        public IActionResult Flights()
        {
            ViewBag.PlaceDeparture = new string[] { "", "Минск", "Москва", "Париж", "Варшава", "Дубай", "Джакарта", "Лос-Анджелес", "Токио", "Лондон" };

            List<Flight> list = new List<Flight>
            {
                _cache.Get<Flight>("Flight")
            };
            ViewBag.Date = Request.Cookies["Date"] ?? "";
            ViewBag.Departure = Request.Cookies["PlaceDeparture"] ?? "";
            ViewBag.Arrival = Request.Cookies["PlaceArrival"] ?? "";

            if (HttpContext.Session.GetObject<List<Flight>>("Flights") != null)
                ViewBag.ListFlight = HttpContext.Session.GetObject<List<Flight>>("Flights");
            else ViewBag.ListFlight = list;

            return View();
        }

        [HttpPost]
        public IActionResult Flights(string Date, string PlaceDeparture, string PlaceArrival)
        {
            ViewBag.PlaceDeparture = new string[] { "", "Минск", "Москва", "Париж", "Варшава", "Дубай", "Джакарта", "Лос-Анджелес", "Токио", "Лондон" };

            IQueryable<Flight> collection = _airportContext
                    .Flights
                    .Where(t => t.PlaceDeparture.StartsWith(PlaceDeparture ?? ""))
                    .Where(t => t.PlaceArrival.StartsWith(PlaceArrival ?? ""));
            if (Date != null)
            {
                string[] tmp = Date.Split(new char[] { '.', '-', '\\', '/', ' ' });
                DateTime date = new DateTime(Convert.ToInt32(tmp[2]), Convert.ToInt32(tmp[1]), Convert.ToInt32(tmp[0]));
                collection = collection.Where(t => t.Date == date);
            }
            ViewBag.ListFlight = collection;
            
            Response.Cookies.Append("Date", Date ?? "");
            Response.Cookies.Append("PlaceDeparture", PlaceDeparture ?? "");
            Response.Cookies.Append("PlaceArrival", PlaceArrival ?? "");
            HttpContext.Session.SetObject("Flights", collection.ToList());

            return View();
        }

        [HttpGet]
        public IActionResult Aircrafts()
        {
            ViewBag.Mark = new string[] { "", "Airbus-A310", "Airbus-A380", "Boeing-737", "Boeing-777", "ИЛ-62", "ТУ-154", "Adam A500 Adamjet", "Aerospatiale N262", "Spike S-512", "Aero Boero AB-95" };
            List<Aircraft> list = new List<Aircraft>
            {
                _cache.Get<Aircraft>("Aircraft")
            };

            ViewBag.SelectMark = Request.Cookies["SelectMark"] ?? "";
            ViewBag.Capasity = Request.Cookies["Capasity"] ?? "";
            ViewBag.Carrying = Request.Cookies["Carrying"] ?? "";

            if (HttpContext.Session.GetObject<List<Aircraft>>("Aircrafts") != null)
                ViewBag.ListAircrafts = HttpContext.Session.GetObject<List<Aircraft>>("Aircrafts");
            else ViewBag.ListAircrafts = list;

            return View();
        }

        [HttpPost]
        public IActionResult Aircrafts(string Mark, int Capasity, double Carrying)
        {
            ViewBag.Mark = new string[] { "", "Airbus-A310", "Airbus-A380", "Boeing-737", "Boeing-777", "ИЛ-62", "ТУ-154", "Adam A500 Adamjet", "Aerospatiale N262", "Spike S-512", "Aero Boero AB-95" };

            IQueryable<Aircraft> collection = _airportContext.Aircrafts.Where(t => t.Mark.StartsWith(Mark ?? ""));
            if (Capasity != 0) collection = collection.Where(t => t.Capasity == Capasity);
            if (Carrying != 0) collection = collection.Where(t => t.Carrying >= Carrying);
            ViewBag.listAircrafts = collection;

            Response.Cookies.Append("SelectMark", Mark ?? "");
            Response.Cookies.Append("Capasity", Capasity == 0 ? "" : Capasity.ToString());
            Response.Cookies.Append("Carrying", Carrying == 0 ? "" : Carrying.ToString());

            HttpContext.Session.SetObject("Aircrafts", collection.ToList());

            return View();
        }
    }
}
