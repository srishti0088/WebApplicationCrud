using AspWebApi_Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspWebApi_Crud.Controllers
{
    public class CrudApiController : ApiController
    {
        web_api_crud_dbEntities db = new web_api_crud_dbEntities();

      [HttpGet]
      public  IHttpActionResult GetEmployees()
        {
          List<Employee> list = db.Employees.ToList();
            return Ok(list);
        }

        // To Get Employees By ID

        [HttpGet]
        public IHttpActionResult GetEmployeesById(int id)
        {
            var emp = db.Employees.Where(model => model.id == id).FirstOrDefault();
            return Ok(emp);
        }


        [HttpPost]
        public IHttpActionResult EmpInsert(Employee e)
        {
            db.Employees.Add(e);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult EmpUpdate(Employee e)
        {
            // here emp object intially will fetch the old record for employee
            var emp = db.Employees.Where(x => x.id == e.id).FirstOrDefault();
            if (emp != null)
            {
                emp.id = e.id;
                emp.name = e.name;
                emp.designation = e.designation;
                emp.age = e.age;
                emp.gender = e.gender;
                emp.salary = e.salary;
                db.SaveChanges();
            }
            else {
                return NotFound();
            }

            //db.Entry(e).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult EmpDelete(int id)
        {
            var emp = db.Employees.Where(model => model.id == id).FirstOrDefault();
            db.Entry(emp).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }
    }
}
