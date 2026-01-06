using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiskItUp.Data;
using WhiskItUp.Models;
namespace WhiskItUp.Controllers
{
    [Authorize]
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
            var data = await _context.tblUserRecipeMapping
                                     .Include(ur => ur.Recipe)
                                     .Include(ur => ur.User)
                                     .ToListAsync();
            return View(data);
        }

        // GET: UserRecipeMappings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mapping = await _context.tblUserRecipeMapping
                                        .Include(ur => ur.Recipe)
                                        .Include(ur => ur.User)
                                        .FirstOrDefaultAsync(m => m.UserRecipeMappingId == id);

            if (mapping == null)
            {
                return NotFound();
            }

            return View(mapping);
        }
        [Authorize(Roles = "Admin")]
        // GET: UserRecipeMappings/Create
        public IActionResult Create()
        {
            ViewData["RecipeId"] = new SelectList(_context.tblRecipe, "RecipeId", "RecipeName");
            ViewData["UserId"] = new SelectList(_context.tblUser, "UserId", "FullName");
            return View();
        }

        // POST: UserRecipeMappings/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserRecipeMappingId,UserId,RecipeId,Rating,Comment,Favorite")] UserRecipeMapping mapping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mapping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipeId"] = new SelectList(_context.tblRecipe, "RecipeId", "RecipeName", mapping.RecipeId);
            ViewData["UserId"] = new SelectList(_context.tblUser, "UserId", "FullName", mapping.UserId);
            return View(mapping);
        }

        // GET: UserRecipeMappings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mapping = await _context.tblUserRecipeMapping.FindAsync(id);
            if (mapping == null)
            {
                return NotFound();
            }
            ViewData["RecipeId"] = new SelectList(_context.tblRecipe, "RecipeId", "RecipeName", mapping.RecipeId);
            ViewData["UserId"] = new SelectList(_context.tblUser, "UserId", "FullName", mapping.UserId);
            return View(mapping);
        }

        // POST: UserRecipeMappings/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("UserRecipeMappingId,UserId,RecipeId,Rating,Comment,Favorite")] UserRecipeMapping mapping)
        {
            if (id != mapping.UserRecipeMappingId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(mapping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.tblUserRecipeMapping.Any(e => e.UserRecipeMappingId == mapping.UserRecipeMappingId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["UserId"] = new SelectList(_context.tblUser, "UserId", "FullName", mapping.UserId);
            ViewData["RecipeId"] = new SelectList(_context.tblRecipe, "RecipeId", "RecipeName", mapping.RecipeId);
            return View(mapping);
        }
        // GET: UserRecipeMappings/Delete/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mapping = await _context.tblUserRecipeMapping
                                         .Include(ur => ur.Recipe)
                                         .Include(ur => ur.User)
                                         .FirstOrDefaultAsync(m => m.UserRecipeMappingId == id);

            if (mapping == null)
            {
                return NotFound();
            }

            return View(mapping);
        }

        // POST: UserRecipeMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mapping = await _context.tblUserRecipeMapping.FindAsync(id);
            if (mapping != null)
            {
                _context.tblUserRecipeMapping.Remove(mapping);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRecipeMappingExists(int id)
        {
            return _context.tblUserRecipeMapping.Any(e => e.UserRecipeMappingId == id);
        }
    }
}
