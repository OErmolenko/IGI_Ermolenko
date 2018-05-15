using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lab_4_Controllers_Filter.Models;
using lab_4_Controllers_Filter.Filters;
using lab_4_Controllers_Filter.ViewModels;
using lab_4_Controllers_Filter.Extensions;
using Microsoft.EntityFrameworkCore;

namespace lab_4_Controllers_Filter.Controllers
{
    [Logger]
    [ExceptionFilter]
    public class TypeController : Controller
    {
        private readonly AirportContext _context;

        public TypeController(AirportContext context)
        {
            _context = context;
        }

        [SaveSession]
        public async Task<IActionResult> Index(string nameType, int page = 1, TypeSortState sortOrder = TypeSortState.NameTypeAsc)
        {
            int pageSize = 10;

            if (HttpContext.Session.GetObject<string>("nameType") != null)
                nameType = HttpContext.Session.GetObject<string>("nameType");

            //Фильтрация
            IQueryable<Models.Type> types = _context.Types;

            if (nameType != null)
            {
                types = types.Where(t => t.NameType.StartsWith(nameType ?? ""));
            }

            //сортировка
            switch (sortOrder)
            {
                case TypeSortState.NameTypeAsc:
                    types = types.OrderBy(t => t.NameType);
                    break;
                case TypeSortState.NameTypeDesc:
                    types = types.OrderByDescending(t => t.NameType);
                    break;
                case TypeSortState.AppointmentAsc:
                    types = types.OrderBy(t => t.Appointment);
                    break;
                case TypeSortState.AppointmentDesc:
                    types = types.OrderByDescending(t => t.Appointment);
                    break;
                case TypeSortState.RestrictionsAsc:
                    types = types.OrderBy(t => t.Restrictions);
                    break;
                case TypeSortState.RestrictionsDesc:
                    types = types.OrderByDescending(t => t.Restrictions);
                    break;
                default:
                    types = types.OrderBy(t => t.NameType);
                    break;
            }

            //пагинация
            var count = await types.CountAsync();
            var items = await types.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            //формируем модель представления
            TypeIndexViewModel viewModel = new TypeIndexViewModel
            {
                Types = items,
                PageViewModel = new PageViewModel(count, page, pageSize),
                TypeSortViewModel = new TypeSortViewModel(sortOrder),
                TypeFilterViewModel = new TypeFilterViewModel(nameType)
            };

            return View(viewModel);
        }
    }
}