using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab_5_ASP_MVC.Models;
using lab_5_ASP_MVC.Filters;
using Microsoft.AspNetCore.Authorization;
using lab_5_ASP_MVC.ViewModels;
using lab_5_ASP_MVC.Extensions;

namespace lab_5_ASP_MVC.Controllers
{
    [Logger]
    [ExceptionFilter]
    public class Aircraft1Controller : Controller
    {
        private readonly AirportContext _context;

        public Aircraft1Controller(AirportContext context)
        {
            _context = context;
        }

        // GET: Aircraft1
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

        // GET: Aircraft1/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.Types, "TypeId", "TypeId");
            return View();
        }

        // POST: Aircraft1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("AircraftId,Mark,Capasity,Carrying,TypeId")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aircraft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.Types, "TypeId", "TypeId", aircraft.TypeId);
            return View(aircraft);
        }

        // GET: Aircraft1/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts.SingleOrDefaultAsync(m => m.AircraftId == id);
            if (aircraft == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.Types, "TypeId", "TypeId", aircraft.TypeId);
            return View(aircraft);
        }

        // POST: Aircraft1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("AircraftId,Mark,Capasity,Carrying,TypeId")] Aircraft aircraft)
        {
            if (id != aircraft.AircraftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aircraft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AircraftExists(aircraft.AircraftId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.Types, "TypeId", "TypeId", aircraft.TypeId);
            return View(aircraft);
        }

        // GET: Aircraft1/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts
                .Include(a => a.Type)
                .SingleOrDefaultAsync(m => m.AircraftId == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // POST: Aircraft1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aircraft = await _context.Aircrafts.SingleOrDefaultAsync(m => m.AircraftId == id);
            _context.Aircrafts.Remove(aircraft);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AircraftExists(int id)
        {
            return _context.Aircrafts.Any(e => e.AircraftId == id);
        }

        public IActionResult View(int? id)
        {
            return View();
        }
    }
}
