using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab_5_ASP_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using lab_5_ASP_MVC.Filters;
using lab_5_ASP_MVC.ViewModels;
using lab_5_ASP_MVC.Extensions;

namespace lab_5_ASP_MVC.Controllers
{
    [Logger]
    [ExceptionFilter]
    public class Ticket1Controller : Controller
    {
        private readonly AirportContext _context;

        public Ticket1Controller(AirportContext context)
        {
            _context = context;
        }

        // GET: Ticket1
        [SaveSession]
        public async Task<IActionResult> Index(string fullName, string passportData, int page = 1, TicketSortState sortOrder = TicketSortState.FullNameAsc)
        {
            int pageSize = 10;

            if (HttpContext.Session.GetObject<string>("fullName") != null)
                fullName = HttpContext.Session.GetObject<string>("fullName");
            if (HttpContext.Session.GetObject<string>("passportData") != null)
                passportData = HttpContext.Session.GetObject<string>("passportData");

            //Фильтрация
            IQueryable<Ticket> tickets = _context.Tickets;
            if (fullName != null)
            {
                tickets = tickets.Where(t => t.FullName.StartsWith(fullName ?? ""));
            }
            if (passportData != null)
            {
                tickets = tickets.Where(t => t.PassportData.StartsWith(passportData ?? ""));
            }

            //сортировка
            switch (sortOrder)
            {
                case TicketSortState.FullNameAsc:
                    tickets = tickets.OrderBy(t => t.FullName);
                    break;
                case TicketSortState.FullNameDesc:
                    tickets = tickets.OrderByDescending(t => t.FullName);
                    break;
                case TicketSortState.PassportDataAsc:
                    tickets = tickets.OrderBy(t => t.PassportData);
                    break;
                case TicketSortState.PassportDataDesc:
                    tickets = tickets.OrderByDescending(t => t.PassportData);
                    break;
                case TicketSortState.NumberPlaceAsc:
                    tickets = tickets.OrderBy(t => t.NumberPlace);
                    break;
                case TicketSortState.NumberPlaceDesc:
                    tickets = tickets.OrderByDescending(t => t.NumberPlace);
                    break;
                case TicketSortState.PriceAsc:
                    tickets = tickets.OrderBy(t => t.Price);
                    break;
                case TicketSortState.PriceDesc:
                    tickets = tickets.OrderByDescending(t => t.Price);
                    break;
                default:
                    tickets = tickets.OrderBy(t => t.FullName);
                    break;
            }

            //пагинация
            var count = await tickets.CountAsync();
            var items = await tickets.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            TicketIndexViewModel viewModel = new TicketIndexViewModel
            {
                Ticket = tickets,
                PageViewModel = new PageViewModel(count, page, pageSize),
                TicketSortViewModel = new TicketSortViewModel(sortOrder),
                TicketFilterViewModel = new TicketFilterViewModel(fullName, passportData)
            };

            return View(viewModel);
        }

        // GET: Ticket1/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId");
            return View();
        }

        // POST: Ticket1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("TicketId,FullName,PassportData,NumberPlace,Price,FlightId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", ticket.FlightId);
            return View(ticket);
        }

        // GET: Ticket1/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.SingleOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", ticket.FlightId);
            return View(ticket);
        }

        // POST: Ticket1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,FullName,PassportData,NumberPlace,Price,FlightId")] Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketId))
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
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", ticket.FlightId);
            return View(ticket);
        }

        // GET: Ticket1/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Flight)
                .SingleOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Ticket1/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.SingleOrDefaultAsync(m => m.TicketId == id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketId == id);
        }
    }
}
