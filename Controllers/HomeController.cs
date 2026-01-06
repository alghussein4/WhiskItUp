using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WhiskItUp.Data;
using WhiskItUp.Models;
using WhiskItUp.Models.ModelView;
using WhiskItUp.Models.ViewModels;
namespace WhiskItUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            var topRecipes = await _context.tblUserRecipeMapping
         .Include(x => x.Recipe)
         .GroupBy(x => x.Recipe)
         .Select(g => new TopRatedRecipeVM
         {
             RecipeName = g.Key.RecipeName,  
             AvgRating = g.Average(x => x.Rating) 
         })
         .OrderByDescending(x => x.AvgRating)
         .Take(3)
         .ToListAsync();

            return View(topRecipes); 
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

