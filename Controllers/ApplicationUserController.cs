using CarWorkshop.Data;
using CarWorkshop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWorkshop.Controllers
{
    public class ApplicationUserController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IMemoryCache _cache;
        public ApplicationUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMemoryCache cache)
        {
            _context = context;
            _userManager = userManager;
            _cache = cache;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Cars()
        {
            var user = await _userManager.GetUserAsync(User);
            var cars = new List<Car>();
            string cacheKey = $"Cars:{user.Id}";
            if (!_cache.TryGetValue(cacheKey,out cars))
            {
                 cars = _context.Cars.Where(c => c.ApplicationUser.Id == user.Id).ToList();
                _cache.Set(cacheKey, cars, TimeSpan.FromMinutes(1));
            }
            int numberOfRepairedCars = cars.Where(c => c.Repaired == true).Count() * 100;
            ViewData["AmountOfRepairedCars"] = numberOfRepairedCars / cars.Count;
            return View(cars);
        }
    }
}
