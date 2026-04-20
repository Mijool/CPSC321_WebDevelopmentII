using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CPSC321_A05_MC.Data;
using CPSC321_A05_MC.Models;

namespace CPSC321_A05_MC.Controllers
{
    public class CarModelsController : Controller
    {
        //creating a scaffolded "controller with entity framework" sets up 90% of the extra files needed to access our database

        //we only need to open up the nuget terminal and enter 'add-migration "first-migration"', then 'update-database'
        // now our app is fully set up to run out localdb

        private readonly CPSC321_A05_MCContext Dealership;

        public CarModelsController(CPSC321_A05_MCContext context)
        {
            this.Dealership = context;
        }

       

        // GET: CarModels
        public async Task<IActionResult> Index()
        {
            List<CarModel> carsList = await Dealership.Cars.ToListAsync();
            return View(carsList);

            
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            var carModel = await Dealership.Cars.FirstOrDefaultAsync(c => c.Id == id);
            return View(carModel);

        }

        // GET: CarModels/Create
        public IActionResult Create() => View();

     
        [HttpPost]
      
        public async Task<IActionResult> Create(CarModel carModel)
        {
            

            if(carModel != null) 
            {
                var car = new CarModel()
                {
                Make = carModel.Make,
                Mileage = carModel.Mileage,
                Model = carModel.Model,
                Year = carModel.Year,
                Color = carModel.Color,
                BodyStyle = carModel.BodyStyle
                };

                await Dealership.Cars.AddAsync(car);

                await Dealership.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(carModel);

        }

        // GET: CarModels
        public async Task<IActionResult> Edit(int? id)
        {
            var carModel = await Dealership.Cars.FirstOrDefaultAsync(c => c.Id == id);

            if (carModel != null) return View(carModel);

            return NotFound();

        }


        [HttpPost]

        public async Task<IActionResult> Edit(CarModel newcarModel)
        {
            var existingcarModel = await Dealership.Cars.FirstOrDefaultAsync(c => c.Id == newcarModel.Id);
            if (existingcarModel != null)
            {
                existingcarModel.Mileage = newcarModel.Mileage;
                existingcarModel.Color = newcarModel.Color;

                await Dealership.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(newcarModel);

        }

        // GET: CarModels/Delete/
        public async Task<IActionResult> Delete(int id)
        {
            //ensure any timeyou are getting information from the dbo, you put an await prefix on it
            var existingcarModel = await Dealership.Cars.FirstOrDefaultAsync(x => x.Id == id);
            return View(existingcarModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CarModel car)
        {
            var existingcarModel = await Dealership.Cars.FirstOrDefaultAsync(x => x.Id == car.Id);

            if (existingcarModel != null)
            {
                Dealership.Cars.Remove(existingcarModel);

                await Dealership.SaveChangesAsync();
            }


            
            return RedirectToAction("Index");
        }

    }
}
