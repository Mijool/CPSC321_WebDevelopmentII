using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CPSC321_A06_MC.Data;
using CPSC321_A06_MC.Models;
using BootstrapBlazor.Components;
using System.Runtime.InteropServices;

namespace CPSC321_A06_MC.Controllers
{
    public class UserController : Controller
    {
        private readonly CPSC321_A06_MCContext employees;

        public UserController(CPSC321_A06_MCContext context)
        {
            employees = context;
        }

        public static double salaryRunningTotal; //this variable is set at the class level so it will remain tracking as employees are added
        public static double salaryITTotal; //they must be static so they exist even when this controller gets destroyed
        public static double salaryBusinessTotal;


        public bool checkSalary(UserModel currentUser)
        {
            const double totalSalaryBudget = 1_000_000;
            const double ITBudget = 500_000;
            const double BusinessBudget = 500_000;
            double currentSalary = currentUser.Salary;


            if ((salaryRunningTotal + currentSalary) > totalSalaryBudget)
            {
                return false;
            }

            switch (currentUser.Department) //checks whether their salary is in the constraint for their department
            {
                case "IT":
                    if ((salaryITTotal + currentSalary) > ITBudget)
                    { return false; }

                    
                    break;
                case "Business":
                    if ((salaryBusinessTotal + currentSalary) > BusinessBudget)
                    {
                        return false;
                    }

                    
                    break;
            }

            return true; // only when the user salary is meets all constraints, it returns true 



        }

        public async Task<IActionResult> ListOfUsers()
        {
            var listOfEmployees = await employees.Users.ToListAsync();

            if (listOfEmployees.Count == 0) { return BadRequest(); }

            //used LINQ method chains to sum up the salaries
            ViewBag.TotalSalary = salaryRunningTotal = listOfEmployees.Sum(total => total.Salary); 
            ViewBag.ITTotal = salaryITTotal = listOfEmployees.Where(dpt => dpt.Department == "IT").Sum(total => total.Salary);
            ViewBag.BusinessTotal = salaryBusinessTotal =listOfEmployees.Where(dpt => dpt.Department == "Business").Sum(total => total.Salary);

            return View(listOfEmployees);
        }

        public IActionResult CreateUser() => View();

        [HttpPost]
        public async Task<IActionResult> CreateUser( UserModel userModel)
        {
            int userListSize = await employees.Users.CountAsync(); //check the size of the list before adding

            if (userModel != null)
            {
                if (checkSalary(userModel) == false)
                { //if the salary violates our constraints, the user is redirected back into the same CreateUser view with the object they were already working on
                    return View("CreateUser", userModel);
                }
                if (userListSize > 10)
                { // ensures there aren't more than 10 users
                    return View("CreateUser", userModel);
                }

                employees.Add(new UserModel
                {
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Salary = userModel.Salary,
                    Department = userModel.Department,
                    Position = userModel.Position,
                    YearsOfExperience = userModel.YearsOfExperience
                });

                await employees.SaveChangesAsync();

                return RedirectToAction("ListOfUsers"); //sends the user back to the list, with the updates
            }
            return View(userModel);
        }

//        public async Task<IActionResult> Edit(int? id)
//        {
//            var userModel = await employees.Users.FirstOrDefaultAsync(x => x.ID == id);

//            if (userModel == null) return NotFound();

//            return View(userModel);
//        }


//        [HttpPost]

//        public async Task<IActionResult> Edit(UserModel userModel)
//        {
//            var existingModel = await employees.Users.FirstOrDefaultAsync(x => x.ID == userModel.ID);

//            if (existingModel != null && ModelState.IsValid)
//            {

//            }
//            return View(userModel);
//        }



//        public async Task<IActionResult> Delete(int? id)
//        {
//            var userModel = await employees.Users.FirstOrDefaultAsync(x => x.ID == id);

//            if (userModel == null) return NotFound();

//            return View(userModel);
//        }



//        [HttpPost]
//        public async Task<IActionResult> Delete(int id)
//        {
//            return null;
//        }


 }
}
