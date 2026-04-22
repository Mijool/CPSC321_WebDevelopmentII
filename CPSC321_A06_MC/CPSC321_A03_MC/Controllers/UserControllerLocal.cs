using Microsoft.AspNetCore.Mvc;
using CPSC321_A06_MC.Models;


namespace CPSC321_A06_MC.Controllers;

public class UserControllerLocal : Controller
{
    public static List<UserModel> userList = new List<UserModel>
    {
        new UserModel() { ID = 1, FirstName = "Joey", LastName = "Smith",
            Department = "IT", Position = "Software Developer", YearsOfExperience = 10, Salary = 100_000},
        new UserModel() { ID = 3, FirstName = "Moe", LastName = "Crown",
            Department = "IT", Position = "Data Analyst", YearsOfExperience = 5, Salary = 399_000},
        new UserModel() { ID = 4, FirstName = "Wyatt", LastName = "Cooper",
            Department = "Business", Position = "Business Analyst", YearsOfExperience = 17, Salary = 499_000}
    }; //entering a placeholder for testing

    //Add a total budget for this web application of $1,000,000
    //Add a budget for each employee of $250,000

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

                //salaryITTotal += currentSalary;
                break;
            case "Business":
                if ((salaryBusinessTotal + currentSalary) > BusinessBudget)
                {
                    return false;
                }
                    
                //salaryBusinessTotal += currentSalary;
                break;
        }

        return true; // only when the user salary is meets all constraints, it returns true 



    }

    
    public IActionResult ListOfUsers()
    {
        salaryRunningTotal = 0;
        salaryITTotal = 0;
        salaryBusinessTotal = 0;
        foreach (UserModel e in userList) //our total gets calulated everytime the page reloads so it doesn't keep accumulating
        {
            salaryRunningTotal += e.Salary;

            if (e.Department.Equals("IT"))
            {
                salaryITTotal += e.Salary;
            }
            if (e.Department.Equals("Business"))
            {
                salaryBusinessTotal += e.Salary;
            }

        }

        ViewBag.TotalSalary = salaryRunningTotal; //this object allows up to display the total in our razor view
        ViewBag.ITTotal = salaryITTotal;
        ViewBag.BusinessTotal = salaryBusinessTotal;
        
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

            if (checkSalary(NewUser) == false){ //if the salary violates our constraints, the user is redirected back into the same CreateUser view with the object they were already working on
                return View("CreateUser", NewUser);
            }
            if (userList.Count > 10) { // ensures there aren't more than 10 users
                return View("CreateUser", NewUser);
            }

            NewUser.ID = ((userList[userList.Count - 1].ID) + 1); //Gets the ID of the last object in the list and adds one to it - maintains uniqueness of IDs 

            userList.Add(NewUser);

            return RedirectToAction("ListOfUsers"); //sends the user back to the list, with the updates

        }
        return View("CreateUser", NewUser); //if there's an issue with the model, it allows to user to reattempt to create it
    }
    
}