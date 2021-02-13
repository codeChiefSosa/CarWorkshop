using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarWorkshop.Data;
using CarWorkshop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace CarWorkshop.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemoryCache _cache;
        public CarController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMemoryCache cache)
        {
            _context = context;
            _userManager = userManager;
            _cache = cache;
        }

        // GET: Car
        [Authorize(Roles = "Mechanic")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cars.Where(c => c.Repaired == false).ToListAsync());
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Plates,RepairList")] Car car)
        {
            car.ApplicationUser = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("Cars", "ApplicationUser");
            }
            return View(car);
        }

        [Authorize(Roles = "Mechanic")]
        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            var user = await _userManager.GetUserAsync(User);
            string cacheKey = $"Cars:{user.Id}";
            if (_cache.Get(cacheKey) != null)
            {
                _cache.Remove(cacheKey);
            }
            return RedirectToAction("Cars", "ApplicationUser");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFault(string carId, string fault)
        {

            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == Int32.Parse(carId));

            var faultsList = car.RepairList.Split(',').ToList();
            faultsList.Remove(fault);

            car.RepairList = String.Join(",", faultsList);

            if(!faultsList.Any())
            {
                car.Repaired = true;
            }

            try
            {
                _context.Update(car);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return new EmptyResult();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
