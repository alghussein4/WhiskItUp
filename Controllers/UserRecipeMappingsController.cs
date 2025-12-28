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
    public class UserRecipeMappingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserRecipeMappingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserRecipeMappings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserRecipeMapping.Include(u => u.Recipe).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserRecipeMappings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRecipeMapping = await _context.UserRecipeMapping
                .Include(u => u.Recipe)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserRecipeMappingId == id);
            if (userRecipeMapping == null)
            {
                return NotFound();
            }

            return View(userRecipeMapping);
        }

        // GET: UserRecipeMappings/Create
        public IActionResult Create()
        {
            ViewData["RecipeId"] = new SelectList(_context.Recipe, "RecipeId", "RecipeId");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FullName");
            return View();
        }

        // POST: UserRecipeMappings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserRecipeMappingId,UserId,RecipeId,Comment,Rating,Favorite")] UserRecipeMapping userRecipeMapping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRecipeMapping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipe, "RecipeId", "RecipeId", userRecipeMapping.RecipeId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FullName", userRecipeMapping.UserId);
            return View(userRecipeMapping);
        }

        // GET: UserRecipeMappings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRecipeMapping = await _context.UserRecipeMapping.FindAsync(id);
            if (userRecipeMapping == null)
            {
                return NotFound();
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipe, "RecipeId", "RecipeId", userRecipeMapping.RecipeId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FullName", userRecipeMapping.UserId);
            return View(userRecipeMapping);
        }

        // POST: UserRecipeMappings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserRecipeMappingId,UserId,RecipeId,Comment,Rating,Favorite")] UserRecipeMapping userRecipeMapping)
        {
            if (id != userRecipeMapping.UserRecipeMappingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRecipeMapping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRecipeMappingExists(userRecipeMapping.UserRecipeMappingId))
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
            ViewData["RecipeId"] = new SelectList(_context.Recipe, "RecipeId", "RecipeId", userRecipeMapping.RecipeId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FullName", userRecipeMapping.UserId);
            return View(userRecipeMapping);
        }

        // GET: UserRecipeMappings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRecipeMapping = await _context.UserRecipeMapping
                .Include(u => u.Recipe)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserRecipeMappingId == id);
            if (userRecipeMapping == null)
            {
                return NotFound();
            }

            return View(userRecipeMapping);
        }

        // POST: UserRecipeMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRecipeMapping = await _context.UserRecipeMapping.FindAsync(id);
            if (userRecipeMapping != null)
            {
                _context.UserRecipeMapping.Remove(userRecipeMapping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRecipeMappingExists(int id)
        {
            return _context.UserRecipeMapping.Any(e => e.UserRecipeMappingId == id);
        }
    }
}
