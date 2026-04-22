using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using ThreeLayerBizApp.Models;

namespace ThreeLayerBizApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await employeeService.GetEmployeesAsync();  //this makes sure the business logic gets processed before returning the view
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee() //create a new employee using the Employee from the DAL and populating it with the data from the employee object that came from the View
                {
                    FirstName = employeeModel.FirstName,
                    LastName = employeeModel.LastName,
                    Position = employeeModel.Position,
                    Salary = employeeModel.Salary
                };
                await employeeService.AddEmployeeAsync(employee); //call the AddEMployeesAsync method from BLL, and pass in the new employee that was created
                return RedirectToAction("Index");
            }
            return View(employeeModel);
        }
    }
}
