namespace Week3._3EmployeeApp.Models
{
    public class EmployeeModel
    {
        public EmployeeModel(int id, string firstname, string lastname, string position, int salary)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
            Position = position;
            Salary = salary;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
    }
}
