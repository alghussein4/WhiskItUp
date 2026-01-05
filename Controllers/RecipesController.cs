using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiskItUp.Data;
using WhiskItUp.Models;

namespace WhiskItUp.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index(string searchString, EDifficulty? difficulty)
        {
            var recipes = _context.tblRecipe.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(s => s.RecipeName!.Contains(searchString));
            }

            if (difficulty.HasValue)
            {
                recipes = recipes.Where(r => r.Difficulty == difficulty);
            }
            ViewBag.DifficultyList = new SelectList(Enum.GetValues(typeof(EDifficulty)));
            return View(await recipes.ToListAsync());
        }

        // ==========================================
        // UPDATED DETAILS METHOD START
        // ==========================================
        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // This logic uses Eager Loading to fetch the Users who liked this recipe
            var recipe = await _context.tblRecipe
                .Include(r => r.UserRecipeMappings)!        // Include the bridge table
                    .ThenInclude(ur => ur.User)             // Follow the bridge to the User table
                .FirstOrDefaultAsync(m => m.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }
        // ==========================================
        // UPDATED DETAILS METHOD END
        // ==========================================

        // GET: Recipes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeId,RecipeName,Description,Time,Servings,Difficulty,IsVegetarian,Calories")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.tblRecipe.FindAsync(id);
            if (recipe == null) return NotFound();

            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecipeId,RecipeName,Description,Time,Servings,Difficulty,IsVegetarian,Calories")] Recipe recipe)
        {
            if (id != recipe.RecipeId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecipeId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.tblRecipe
                .FirstOrDefaultAsync(m => m.RecipeId == id);

            if (recipe == null) return NotFound();

            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.tblRecipe.FindAsync(id);
            if (recipe != null)
            {
                _context.tblRecipe.Remove(recipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.tblRecipe.Any(e => e.RecipeId == id);
        }
    }
}