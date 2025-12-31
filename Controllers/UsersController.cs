using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiskItUp.Data;
using WhiskItUp.Models;

namespace WhiskItUp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }




        // GET: Users
        // Features:
        // - Search (FullName/Email/Phone)
        // - Filter by Gender
        // - Sorting
        // - Pagination
        public async Task<IActionResult> Index(
            string? search,
            EGender? gender,
            string? sortOrder,
            int page = 1,
            int pageSize = 10)
        {
            var query = _context.tblUser.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(u =>
                    u.FullName.Contains(search) ||
                    (u.Email != null && u.Email.Contains(search)) ||
                    (u.PhoneNumber != null && u.PhoneNumber.Contains(search)));
            }

            // Filter Gender
            if (gender.HasValue)
            {
                query = query.Where(u => u.Gender == gender.Value);
            }

            // Sorting
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortByName = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SortById = sortOrder == "id_asc" ? "id_desc" : "id_asc";

            query = sortOrder switch
            {
                "name_desc" => query.OrderByDescending(u => u.FullName),
                "id_asc" => query.OrderBy(u => u.UserId),
                "id_desc" => query.OrderByDescending(u => u.UserId),
                _ => query.OrderBy(u => u.FullName)
            };

            // Pagination
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var users = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.Search = search;
            ViewBag.Gender = gender;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.TotalPages = totalPages;

            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.tblUser
                .Include(u => u.UserRecipeMappings) // لو موجودة ومرتبطة
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FullName,Email,Gender,YearsOfExp,PhoneNumber")] User user)
        {
            // Validation منطقي: منع تاريخ مستقبل
            if (user.YearsOfExp > DateTime.Today)
            {
                ModelState.AddModelError("YearsOfExp", "Years Of Experience date cannot be in the future.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.tblUser.FindAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FullName,Email,Gender,YearsOfExp,PhoneNumber")] User user)
        {
            if (id != user.UserId)
                return NotFound();


            if (user.YearsOfExp > DateTime.Today)
            {
                ModelState.AddModelError("YearsOfExp", "Years Of Experience date cannot be in the future.");
            }

            if (!ModelState.IsValid)
                return View(user);

            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserId))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }
        private bool UserExists(int id)
        {
            return _context.tblUser.Any(e => e.UserId == id);
        }

    }
}