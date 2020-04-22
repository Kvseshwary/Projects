using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccessDetails;

namespace WebApi.Controllers
{
    public class EmployeesController : ApiController
    {

        //Go To The Employee Definition can see the model

            //Collection of employee IEnumerable
        public IEnumerable<Employee> Get()
        {
            //GO To EmployeeDataModelContext.cs that manages connection DbContext

            //Connection string needs to be added in webconfig file
            using (EmployeeEntities empEntity = new EmployeeEntities())
            {
                return empEntity.Employees.ToList();
            }
        }

        //Get a single employee based on the ID not use IEnumerable instead an employee object
        public Employee Get(int id)
        {
            //Connection object
            using (EmployeeEntities emp = new EmployeeEntities())
            {
                //Return Single employee

                return emp.Employees.FirstOrDefault(e => e.ID == id);
            }

        }

        // Update the existing user details 
        //User needs to have an existing id

        public HttpResponseMessage Put(int id,[FromBody]Employee emp)
        {
            //Connection object
            try
            {
                using (EmployeeEntities entities = new EmployeeEntities())
                {

                    //This checks the id in the identity column in database is equal to id passed
                    //The entities.Employees is the Dbset like Ado.net dataset
                    //The passed Id is checked with the Dbset Id  which comes from database
                    //The Dbset is assigned to the variable entity
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Id" + id + "Not Found in the database");
                    }
                    else
                    {
                        entity.FirstName = emp.FirstName;
                        entity.Lastname = emp.Lastname;
                        entity.Gender = emp.Gender;
                        entity.Salary = emp.Salary;
                        entities.SaveChanges();
                        return Request.CreateErrorResponse(HttpStatusCode.OK, entity.ToString());
                    }
                }
            }

            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
       
    
}
