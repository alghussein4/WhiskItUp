using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiskItUp.Data;
using WhiskItUp.Models;
using WhiskItUp.Models.ModelView;

namespace WhiskItUp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        
        private readonly ApplicationDbContext _context = default!;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(EGender? gender, string? sortOrder, int page = 1, int pageSize = 10)
        {
            var query = _context.tblUser.AsQueryable();


            switch (sortOrder?.ToLower().Trim())
            {
                case "name_asc": query = query.OrderBy(u => u.FullName); break;
                case "name_desc": query = query.OrderByDescending(u => u.FullName); break;
                default: query = query.OrderBy(u => u.FullName); break;
            }

            var totalCount = await query.CountAsync();
            var users = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.tblUser
                .Include(u => u.UserRecipeMappings)
                    .ThenInclude(ur => ur.Recipe)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null) return NotFound();

            ViewBag.countrecipe = user.UserRecipeMappings?.Count() ?? 0;
            ViewBag.totaltime = user.UserRecipeMappings?.Sum(urm => urm.Recipe?.Time ?? 0) ?? 0;
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FullName,Email,Gender,YearsOfExp,PhoneNumber")] User user)
        {
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
            if (id == null) return NotFound();
            var user = await _context.tblUser.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,FullName,Email,Gender,YearsOfExp,PhoneNumber")] User user)
        {
            if (id != user.UserId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var user = await _context.tblUser.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
       
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.tblUser.FindAsync(id);
            if (user != null)
            {
                _context.tblUser.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id) => _context.tblUser.Any(e => e.UserId == id);

        // Report Logic
        public IActionResult GetuserrecipeReport() => View(new UserRecipesReport());

        [HttpPost]
        public IActionResult GetuserrecipeReport(UserRecipesReport model)
        {
            {

                if (model is null)
                    return NotFound();



                if (model.Email is not null && model.Email.Length > 0)
                    model.users = _context.tblUser
                                             .Include(s => s.UserRecipeMappings)!
                                             .ThenInclude(r => r.Recipe)
                                             .Where(s => s.Email.Contains(model.Email));
                else
                    model.users = _context.tblUser
                                            .Include(s => s.UserRecipeMappings)!
                                            .ThenInclude(r => r.Recipe)
                                            .Where(s => s.Gender == model.Gender);
                return View(model);
            }
        }
    }
}

