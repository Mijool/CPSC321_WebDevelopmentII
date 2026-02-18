using Microsoft.AspNetCore.Mvc;
using CPSC321_A03_MC.Models;


namespace CPSC321_A03_MC.Controllers;

public class UserController : Controller
{
    public static List<UserModel> userList = new List<UserModel>
    {
        new UserModel() { ID = 1, FirstName = "Derrick", LastName = "Rose", Department = "Bulls", Position = "Basketball Player", YearsOfExperience = 10, Salary = 200000}
    }; //entering a placeholder for testing

    //Add a total budget for this web application of $1,000,000
    //Add a budget for each employee of $250,000

    public static double salaryRunningTotal; //this variable is set at the class level so it will remain tracking as employees are added

        
    public bool checkSalary(UserModel currentUser)
    {
        const double totalSalaryBudget = 1_000_000f;
        const double ITBudget = 500_000f;
        const double BusinessBudget = 500_000f;
        double currentSalary = currentUser.Salary;
        
        
        if ((salaryRunningTotal + currentSalary) <= totalSalaryBudget)
        {
            switch (currentUser.Department) //checks whether their salary is in the constraint for their department
            {
                case "IT":
                    if (currentSalary > ITBudget)
                        return false;
                    break;
                case "Business":
                    if (currentSalary > BusinessBudget)
                        return false;
                    break;
            }
            return true; // only when the user salary is meets all constraints, it returns true 
        }
        return false;
            

    }

    // GET
    public IActionResult ListOfUsers()
    {
        salaryRunningTotal = 0;
        foreach (UserModel e in userList) //our total gets calulated everytime the page reloads so it doesn't keep accumulating
        {
            salaryRunningTotal += e.Salary;
        }

        ViewBag.TotalSalary = salaryRunningTotal; //this object allows up to display the total in our razor view
        
        return View(userList);
    }

    public IActionResult CreateUser()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CreateUser(UserModel NewUser)
    {
        if (ModelState.IsValid)
        {
            if (!checkSalary(NewUser)){ //if the salary violates our constraints, the user is redirected back into the same CreateUser view with the object they were already working on
                return View("CreateUser", NewUser);
            }
            if (userList.Count <= 10) { // ensures there aren't more than 10 users
                
                NewUser.ID = (userList[userList.Count].ID) + 1; //Gets the ID of the last object in the list and adds one to it - maintains uniqueness of IDs
                
                userList.Add(NewUser);
                RedirectToAction("ListOfUsers"); //sends the user back to the list, with the updates
            }
        }
        return View("CreateUser", NewUser); //if there's an issue with the model, it allows to user to reattempt to create it
    }
    
}