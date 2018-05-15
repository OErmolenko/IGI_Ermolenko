using lab_4_Controllers_Filter.Extensions;
using lab_4_Controllers_Filter.Filters;
using lab_4_Controllers_Filter.Models;
using lab_4_Controllers_Filter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_4_Controllers_Filter.Controllers
{
    [Logger]
    [ExceptionFilter]
    public class AircraftController : Controller
    {
        private readonly AirportContext _context;

        public AircraftController(AirportContext context)
        {
            _context = context;
        }

        [SaveSession]
        public async Task<IActionResult> Index(string mark, int capasity, double carrying, int page = 1, AircraftSortState sortOrder = AircraftSortState.MarkAsc)
        {
            int pageSize = 10;

            if (HttpContext.Session.GetObject<string>("mark") != null)
                mark = HttpContext.Session.GetObject<string>("mark");
            if (HttpContext.Session.GetObject<string>("capasity") != null)
                capasity = Convert.ToInt32(HttpContext.Session.GetObject<string>("capasity"));
            if (HttpContext.Session.GetObject<string>("carrying") != null)
                carrying = Convert.ToDouble(HttpContext.Session.GetObject<string>("carrying"));

            //Фильтрация
            IQueryable<Aircraft> aircrafts = _context.Aircrafts;

            if (mark != null)
            {
                aircrafts = aircrafts.Where(t => t.Mark.StartsWith(mark ?? ""));
            }
            if (capasity != 0)
            {
                aircrafts = aircrafts.Where(t => t.Capasity == capasity);
            }
            if (carrying != 0)
            {
                aircrafts = aircrafts.Where(t => t.Carrying == carrying);
            }

            //сортировка
            switch (sortOrder)
            {
                case AircraftSortState.MarkAsc:
                    aircrafts = aircrafts.OrderBy(t => t.Mark);
                    break;
                case AircraftSortState.MarkDesc:
                    aircrafts = aircrafts.OrderByDescending(t => t.Mark);
                    break;
                case AircraftSortState.CapasityAsc:
                    aircrafts = aircrafts.OrderBy(t => t.Capasity);
                    break;
                case AircraftSortState.CapasityDesc:
                    aircrafts = aircrafts.OrderByDescending(t => t.Capasity);
                    break;
                case AircraftSortState.CarryingAsc:
                    aircrafts = aircrafts.OrderBy(t => t.Carrying);
                    break;
                case AircraftSortState.CarryingDesc:
                    aircrafts = aircrafts.OrderByDescending(t => t.Carrying);
                    break;
                default:
                    aircrafts = aircrafts.OrderBy(t => t.Mark);
                    break;
            }

            //пагинация
            var count = await aircrafts.CountAsync();
            var items = await aircrafts.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            //формируем модель представления
            AircraftIndexViewModel viewModel = new AircraftIndexViewModel
            {
                Aircraft = items,
                PageViewModel = new PageViewModel(count, page, pageSize),
                AircraftSortViewModel = new AircraftSortViewModel(sortOrder),
                AirportFilterViewModel = new AirportFilterViewModel(new List<string> { "Airbus-A310", "Airbus-A380", "Boeing-737", "Boeing-777", "ИЛ-62", "ТУ-154", "Adam A500 Adamjet", "Aerospatiale N262", "Spike S-512", "Aero Boero AB-95" }, mark, capasity, carrying)
            };

            return View(viewModel);
        }
    }
}