using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAppMigration.Data;
using StudentAppMigration.Models;

namespace StudentAppMigration.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentAppDbContext studentAppDbContext;

        public StudentController(StudentAppDbContext studentAppDbContext)
        {
            this.studentAppDbContext = studentAppDbContext;    
        }
        public async Task<IActionResult> Index()
        {
            var students = await studentAppDbContext.Students.ToListAsync();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentModel studentModel)
        {
            //we do this here because our view doesn't inheritently return the same type of Student model we need
            var student = new StudentModel()
            {
                FirstName = studentModel.FirstName,
                LastName = studentModel.LastName,
                Major = studentModel.Major
            };

            //after creating the student object, we want to add this student to out Students DbSet so it can be added to the database
            await studentAppDbContext.Students.AddAsync(student);
            await studentAppDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await studentAppDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await studentAppDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentModel editedStudent)
        {
            //the only difference with a dbo is needing to access our students list through our DbContext object, and adding Aync to the end of our LINQ 
            var existingStudent = await studentAppDbContext.Students.FirstOrDefaultAsync(x => x.Id == editedStudent.Id);

            //this logic is the exact same as before, we dont want to edit the id as it can break our database preset for unique PKs
            if (existingStudent != null && ModelState.IsValid)
            {
                //existingStudent.Id = editedStudent.Id;
                existingStudent.FirstName = editedStudent.FirstName;
                existingStudent.LastName = editedStudent.LastName;
                existingStudent.Major = editedStudent.Major;
                //make sure to save our changes to the database
                await studentAppDbContext.SaveChangesAsync();
            
                return RedirectToAction("Index");
            }

            return View(editedStudent);

            
        }
        public async Task<IActionResult> Delete(int id)
        {
            //ensure any timeyou are getting information from the dbo, you put an await prefix on it
            var student = await studentAppDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(StudentModel editedStudent)
        {
            var existingStudent = await studentAppDbContext.Students.FirstOrDefaultAsync(x => x.Id == editedStudent.Id);

            if(existingStudent != null)
            {
                studentAppDbContext.Students.Remove(existingStudent);
            }
             

            await studentAppDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
