using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lab_4_Controllers_Filter.Models;
using lab_4_Controllers_Filter.Filters;
using lab_4_Controllers_Filter.Extensions;
using Microsoft.EntityFrameworkCore;
using lab_4_Controllers_Filter.ViewModels;

namespace lab_4_Controllers_Filter.Controllers
{
    [Logger]
    [ExceptionFilter]
    public class TicketController : Controller
    {
        private readonly AirportContext _context;

        public TicketController(AirportContext context)
        {
            _context = context;
        }

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
            if(fullName != null)
            {
                tickets = tickets.Where(t => t.FullName.StartsWith(fullName ?? ""));
            }
            if(passportData != null)
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
    }
}