using Microsoft.AspNetCore.Mvc;
using Week3._3EmployeeApp.Models;

namespace Week3._3EmployeeApp.Controllers
{
    public class EmployeeController : Controller
    {
        public static List<EmployeeModel> employees = new List<EmployeeModel>(); //creating a list of employees using the EmployeeModel (inside I will have EmployeeModel objects)
        public IActionResult ListOfEmployees()
        { 
            //this ListOfEmployees View will be populated with the employees list
            return View(employees);
        }

        [HttpGet]
        public IActionResult CreateEmployee() //This CreateEmployee Action is only responsible for rendering ("GET"ting) my CreateEmployee View 
        {
            return View();
        }

        [HttpPost] //This action will be responsible for "POST"ting data to the server
        public IActionResult CreateEmployee(EmployeeModel employee) //This employee object is coming from my CreateEmployee View when I click submit
        {
            if (ModelState.IsValid) //Validating that I have a valid employee object
            {
                employees.Add(employee); //Adding the employee that I created in my CreateEmployee view to my employees list
                return RedirectToAction("ListOfEmployees"); //Return the ListOfEmployees View [WITH THE CHANGES <-- redirectToAction]
            }
            return View("CreateEmployee", employee); //returning the CreateEmployee View, with the employee object that the user is working on/creating
        }

        public IActionResult Details(int id) 
        {
            var employee = employees.FirstOrDefault(e => e.Id == id); //set first employee that has the Id equal to id coming in as an argument
            if(employee == null)
            {
                return NotFound();
            }
            return View(employee);

            //*----Long way---*//
            //foreach (EmployeeModel employee in employees)
            //{
            //    if (employee.Id == id)
            //    {
            //        return View(employee);
            //    }
            //}
            //return View();
        } 



    }
}
