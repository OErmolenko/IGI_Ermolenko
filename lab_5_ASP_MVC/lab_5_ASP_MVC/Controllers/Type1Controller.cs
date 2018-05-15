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
    public class Type1Controller : Controller
    {
        private readonly AirportContext _context;

        public Type1Controller(AirportContext context)
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

        // GET: Type1/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Type1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("TypeId,NameType,Appointment,Restrictions")] Models.Type @type)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@type);
        }

        // GET: Type1/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @type = await _context.Types.SingleOrDefaultAsync(m => m.TypeId == id);
            if (@type == null)
            {
                return NotFound();
            }
            return View(@type);
        }

        // POST: Type1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("TypeId,NameType,Appointment,Restrictions")] Models.Type @type)
        {
            if (id != @type.TypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeExists(@type.TypeId))
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
            return View(@type);
        }

        // GET: Type1/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @type = await _context.Types
                .SingleOrDefaultAsync(m => m.TypeId == id);
            if (@type == null)
            {
                return NotFound();
            }

            return View(@type);
        }

        // POST: Type1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @type = await _context.Types.SingleOrDefaultAsync(m => m.TypeId == id);
            _context.Types.Remove(@type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeExists(int id)
        {
            return _context.Types.Any(e => e.TypeId == id);
        }
    }
}
