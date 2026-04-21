using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IEmployeeRepository
    {
        //this is a blueprint that classes can use to create methods
        //in this blueprint, we will declare the methods without their functionality,
        //and whichever class implements the interface, the class would be invalid without implementing all of the methods that are declared in the interface

        Task<IEnumerable<Employee>> GetEmployeesAsync(); //now this method, as well as any other method that is created here must be implemented in any class(s) that implement the interface
        Task AddEmployeeAsync(Employee employee);

    }
}
