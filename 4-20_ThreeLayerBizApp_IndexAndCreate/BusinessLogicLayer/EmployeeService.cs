using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        //implement the method from the service interface; and here, I can add business logic to my method
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            //business logic would be written here
            return await _employeeRepository.GetEmployeesAsync();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            //write your logic
            //..for example, we want the budget for each employee to be $250,000
            if (employee.Salary <= 250000)
            {
                await _employeeRepository.AddEmployeeAsync(employee);
            }
            else
            {
                //
            }
        }
    }
}
