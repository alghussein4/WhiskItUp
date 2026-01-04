using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WhiskItUp.Data;
using WhiskItUp.Models;

namespace WhiskItUp.Controllers
{
    [Authorize] // Protects the whole controller
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

        // ==========================================
        // FIXED: ADDED DETAILS METHOD
        // ==========================================
        // GET: UserRecipeMappings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // We MUST use .Include() to fetch the linked Recipe and User data
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

        // GET: UserRecipeMappings/Create
        public async Task<IActionResult> Create(int? recipeId)
        {
            // 1. Get the Identity Email of the logged-in user
            string userEmail = User.Identity?.Name!;

            // 2. Find their profile in your custom User table
            var currentUser = await _context.tblUser.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (currentUser == null)
            {
                // Safety check: if they have a login but no "User Profile" yet, send them to create one
                return RedirectToAction("Create", "Users");
            }

            // 3. Prepare data for the View
            ViewBag.UserFullName = currentUser.FullName;
            ViewBag.UserId = currentUser.UserId;

            // Pre-select the recipe if we came from the Details page
            ViewData["RecipeId"] = new SelectList(_context.tblRecipe, "RecipeId", "RecipeName", recipeId);

            return View();
        }

        // POST: UserRecipeMappings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserRecipeMappingId,UserId,RecipeId,Rating,Comment,Favorite")] UserRecipeMapping mapping)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mapping);
                await _context.SaveChangesAsync();
                // After reviewing, go back to the recipe they were looking at
                return RedirectToAction("Details", "Recipes", new { id = mapping.RecipeId });
            }

            // If error, reload the identification data
            var currentUser = await _context.tblUser.FindAsync(mapping.UserId);
            ViewBag.UserFullName = currentUser?.FullName;
            ViewBag.UserId = mapping.UserId;
            ViewData["RecipeId"] = new SelectList(_context.tblRecipe, "RecipeId", "RecipeName", mapping.RecipeId);
            return View(mapping);
        }

        // GET: UserRecipeMappings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var mapping = await _context.tblUserRecipeMapping.FindAsync(id);
            if (mapping == null) return NotFound();

            ViewData["RecipeId"] = new SelectList(_context.tblRecipe, "RecipeId", "RecipeName", mapping.RecipeId);
            ViewData["UserId"] = new SelectList(_context.tblUser, "UserId", "FullName", mapping.UserId);
            return View(mapping);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserRecipeMappingId,UserId,RecipeId,Rating,Comment,Favorite")] UserRecipeMapping mapping)
        {
            if (id != mapping.UserRecipeMappingId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mapping);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRecipeMappingExists(mapping.UserRecipeMappingId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mapping);
        }

        // GET: UserRecipeMappings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var mapping = await _context.tblUserRecipeMapping
                                         .Include(ur => ur.Recipe)
                                         .Include(ur => ur.User)
                                         .FirstOrDefaultAsync(m => m.UserRecipeMappingId == id);

            if (mapping == null) return NotFound();

            return View(mapping);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mapping = await _context.tblUserRecipeMapping.FindAsync(id);
            if (mapping != null)
            {
                _context.tblUserRecipeMapping.Remove(mapping);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserRecipeMappingExists(int id)
        {
            return _context.tblUserRecipeMapping.Any(e => e.UserRecipeMappingId == id);
        }
    }
}