using Microsoft.AspNetCore.Mvc;
using Week3._3EmployeeApp.Models;

namespace Week3._3EmployeeApp.Controllers
{
    public class EmployeeController : Controller
    {
        public static List<EmployeeModel> employees = new List<EmployeeModel>
        {
            new EmployeeModel(1, "firstName","lastName","employed",10000), //made a constructor for testing purposes
            new EmployeeModel(2, "firstName","lastName","employed",10001)
        }; 
        //creating a list of employees using the EmployeeModel (inside I will have EmployeeModel objects)
        
        

        //-------------------EXERCISE-------------------
        //Add a total budget for this web application of $1,000,000
        //Add a budget for each employee of $250,000

        private decimal salaryRunningTotal = 0; //this variable is set at the class level so it will remain tracking as employees are added

        EmployeeModel employee;
        
        public bool checkSalary(EmployeeModel currentEmployee)
        {
            const decimal totalSalaryBudget = 1_000_000;
            const decimal maxSalaryPerEmployee = 250_000;
            decimal currentSalary = currentEmployee.Salary;

            if ((currentSalary < maxSalaryPerEmployee) && ((salaryRunningTotal + currentSalary) < totalSalaryBudget))
            {
                salaryRunningTotal += currentSalary;
                return true;
            }
            return false;
            

        }

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
                if (!checkSalary(employee)) //if the salary violates our constraints, the user is redirected back into the same createEmployee view with the object they were already working on
                {
                    return View("CreateEmployee", employee);

                }
                employees.Add(employee); //Adding the employee that I created in my CreateEmployee view to my employees list
                return RedirectToAction("ListOfEmployees"); //Return the ListOfEmployees View [WITH THE CHANGES <-- redirectToAction]
            }
            return View("CreateEmployee", employee); //returning the CreateEmployee View, with the employee object that the user is working on/creating
        }

        public IActionResult Details(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id); //set first employee that has the Id equal to id coming in as an argument
            if (employee == null)
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

        [HttpGet] //This is the default; however, we'll label it here so we know the difference
        public IActionResult Edit(int id) //Edit action is to return the Edit View, the ID is needed for to return the view of the specific employee that matches the ID being passed in
        {
            //we want to use the ID being passed in to search for the employee in the employees list that has the matching ID
            var employee = employees.FirstOrDefault(e => e.Id == id);
            
            if (employee != null)
            {
                return View(employee);
                
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(EmployeeModel editedEmployee)
        {
            var existingEmployee = employees.FirstOrDefault(e => e.Id == editedEmployee.Id); //I am setting existingEmployee variable to be the employee that has the matching ID from the employee that's being edited from the view
            
            if (ModelState.IsValid) //if the employee is valid and not equal to null
            {

                if (existingEmployee != null)
                {
                    if (!checkSalary(editedEmployee)) //same as before, if the edited employee's salary violates our contraints, it redirects the user back into editing the same employee they were working on
                    {
                        return View("Edit", editedEmployee);
                    }

                    //update the information for the existing employee based on the employee that got edited from my View
                    existingEmployee.FirstName = editedEmployee.FirstName;
                    existingEmployee.LastName = editedEmployee.LastName;
                    existingEmployee.Position = editedEmployee.Position;
                    existingEmployee.Salary = editedEmployee.Salary;
                    
                }
                return RedirectToAction("ListOfEmployees"); //return to the list view with the updated information
            }
            return View(editedEmployee);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(EmployeeModel employeeToDelete)
        {
            var existingEmployee = employees.FirstOrDefault(e => e.Id == employeeToDelete.Id); //I am setting existingEmployee variable to be the employee that has the matching ID from the employee that's being edited from the view

            if (existingEmployee == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid) //if the employee is valid
            {
                    //update the information for the existing employee based on the employee that got edited from my View
                    employees.Remove(existingEmployee);
                    
            }
            return RedirectToAction("ListOfEmployees"); //return to the list view with the updated information
        }

    }
}
