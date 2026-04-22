using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CPSC321_A07_MC.Data;
using CPSC321_A07_MC.Models;
using BootstrapBlazor.Components;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;

namespace CPSC321_A07_MC.Controllers
{
    public class UserController : Controller
    {
        private readonly CPSC321_A07_MCContext employees;

        //Must add a new migration and update our databaase to allow for the extra deletion comment property to be saved in the dbo

        public UserController(CPSC321_A07_MCContext context)
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
                    if ((salaryITTotal + currentSalary) > ITBudget) return false; 
                    break;
                case "Business":
                    if ((salaryBusinessTotal + currentSalary) > BusinessBudget) return false;
                    break;
            }
            return true; // only when the user salary is meets all constraints, it returns true 
        }

        public async Task<IActionResult> ListOfUsers()
        {
            IEnumerable<UserModel> listOfEmployees = await employees.Users.ToListAsync();

            //used LINQ method chains to sum up the salaries
            ViewBag.TotalSalary = salaryRunningTotal = listOfEmployees.Sum(total => total.Salary);
            ViewBag.ITTotal = salaryITTotal = listOfEmployees.Where(dpt => dpt.Department == "IT").Sum(total => total.Salary);
            ViewBag.BusinessTotal = salaryBusinessTotal = listOfEmployees.Where(dpt => dpt.Department == "Business").Sum(total => total.Salary);

            //To get around creating multiple tables to hold deleted employees, instead we just check to see if they have a deletion comment and group them into a deleted category
            //im sure theres a better way of doing this, but since we never truly delete the employee in the first place, I think this works well enough for our usecase
            return View(listOfEmployees.Where(deletionComment => deletionComment.DeletionComment == null));
        }

        //all of our list view getters, using Where LINQ queries to quickly sort through the lists
        public async Task<IActionResult> ListOfIT()
        {
            var listOfIT = await employees.Users.ToListAsync();
            return View(listOfIT.Where(emp => emp.Department.Equals("IT") && emp.DeletionComment == null));
        }

        public async Task<IActionResult> ListOfBusiness()
        {
            var listOfBusiness = await employees.Users.ToListAsync();

            return View(listOfBusiness.Where(emp => emp.Department.Equals("Business") && emp.DeletionComment == null));
        }

        public async Task<IActionResult> DeletedUsers() {
            List<UserModel> DeletedUsers = await employees.Users.ToListAsync();
            
            return View(DeletedUsers.Where(emp => emp.DeletionComment != null));
        }

        public IActionResult CreateUser() => View();

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserModel userModel)
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
        public async Task<IActionResult> Details(int? id)
        {
            var userModel = await employees.Users.FirstOrDefaultAsync(x => x.ID == id);
            //if (userModel == null) return NotFound();

            //return View(userModel);
            //we can turn the above code into a shorthand if else statement
            return (userModel != null) ? View(userModel) : NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var userModel = await employees.Users.FirstOrDefaultAsync(x => x.ID == id);
            return (userModel != null) ? View(userModel) : NotFound();
        }

        public async Task<IActionResult> EditTenure(int? id)
        {
            var userModel = await employees.Users.FirstOrDefaultAsync(x => x.ID == id);
            return (userModel != null) ? View(userModel) : NotFound();
        }
        public async Task<IActionResult> EditSalary(int? id)
        {
            var userModel = await employees.Users.FirstOrDefaultAsync(x => x.ID == id);
            return (userModel != null) ? View(userModel) : NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> EditTenure(UserModel userModel)
        {
            var existingUserModel = await employees.Users.FirstOrDefaultAsync(x => x.ID == userModel.ID);
            if (existingUserModel != null)
            {
                //same structure as before, except now we have to tell the our DB context when to save, followed by a switch case to return to the proper list

                existingUserModel.YearsOfExperience = userModel.YearsOfExperience;
                await employees.SaveChangesAsync();

                switch (userModel.Department)
                {
                    case "IT":
                        return RedirectToAction(nameof(ListOfIT));
                    case "Business":
                        return RedirectToAction(nameof(ListOfBusiness));
                    default:
                        return RedirectToAction(nameof(ListOfUsers));
                }
            }
            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditSalary(UserModel userModel)
        {
            var existingUserModel = await employees.Users.FirstOrDefaultAsync(x => x.ID == userModel.ID);
            if (existingUserModel != null)
            {

                existingUserModel.Salary = userModel.Salary;
                await employees.SaveChangesAsync();

                switch (userModel.Department)
                {
                    case "IT":
                        return RedirectToAction(nameof(ListOfIT));
                    case "Business":
                        return RedirectToAction(nameof(ListOfBusiness));
                    default:
                        return RedirectToAction(nameof(ListOfUsers));
                }
            }
            return View(userModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var userModel = await employees.Users.FirstOrDefaultAsync(x => x.ID == id);

            return (userModel != null) ? View(userModel) : NotFound();
        }



        [HttpPost]
        public async Task<IActionResult> Delete(UserModel user)
        {
            var userModel = await employees.Users.FirstOrDefaultAsync(x => x.ID == user.ID);
            if (userModel != null)
            {
                //all we do here is add the deletion comment, as explained above theres no real point in creating two dbSets that link to the same model, instead I just filter our list based on who has a deletion comment and who doesnt.

                userModel.DeletionComment = user.DeletionComment;
            
                await employees.SaveChangesAsync();
                return RedirectToAction(nameof(DeletedUsers));
            }
            return View(user);


        }
    }
}
