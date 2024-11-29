using AspWebApi_Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AspWebApi_Crud.Controllers
{
    public class CrudMvcController : Controller
    {
        // GET: CrudMvc

        // To receive the requests of WEBAPI
        HttpClient client = new HttpClient();

        // MVC Controller action methods will provide data to API Controller Action Methods and api action methods will
        // perform the operations on the db

        // ASP.NET WEB API (data returned while hitting api) => consume => ASP.NET MVC App => HTML(Show data with help of html)
        public ActionResult Index()
        {
            List<Employee> emp_list = new List<Employee>();
            client.BaseAddress = new Uri("https://localhost:44328/api/CrudApi");
            var response = client.GetAsync("CrudApi");
            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
             // data inside test is of type list of employee that we have stored in display variable
                var display = test.Content.ReadAsAsync<List<Employee>>();
                display.Wait();
                emp_list = display.Result;
            }

            return View(emp_list);
        }

        // This action method will just return the create View
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            // pass this emp to our web api for post

            client.BaseAddress = new Uri("https://localhost:44328/api/CrudApi");
            // We are passing the data to the CrudApi and than from there data will be stored in db
            var response = client.PostAsJsonAsync<Employee>("CrudApi",emp);
            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Create");
        }

        public ActionResult Details(int id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44328/api/CrudApi");
            // In query string we will have the id which is in the parameter
            var response = client.GetAsync("CrudApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Employee>();
                display.Wait();
                e = display.Result;
            }

            return View(e);

        }

        public ActionResult Edit(int id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44328/api/CrudApi");
            // In query string we will have the id which is in the parameter
            var response = client.GetAsync("CrudApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Employee>();
                display.Wait();
                e = display.Result;
            }
            return View(e);
        }

        // This Action is for Updating the Data after it's populated
        [HttpPost]
        public ActionResult Edit(Employee e)
        {
            client.BaseAddress = new Uri("https://localhost:44328/api/CrudApi");
            // We are passing the data to the CrudApi and than from there data will be stored in db
            var response = client.PutAsJsonAsync<Employee>("CrudApi", e);
            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Edit");
        }

        public ActionResult Delete(int id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44328/api/CrudApi");
            // In query string we will have the id which is in the parameter
            var response = client.GetAsync("CrudApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Employee>();
                display.Wait();
                e = display.Result;
            }
            return View(e);
        }

        // When user clicks on delete button to delete
        [HttpPost , ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            client.BaseAddress = new Uri("https://localhost:44328/api/CrudApi");
            // We are passing the data to the CrudApi and than from there data will be stored in db
            var response = client.DeleteAsync("CrudApi/" + id.ToString());
            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Delete");
            
        }
    }
}