using SampleWebTestNoAuth.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleWebTestNoAuth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int maxRows = 5;
            int page = Request.Params["page"] != null ? int.Parse(Request.Params["page"]) : 1;

            MVCContext dbContext = new MVCContext();
            List<Employee> employees = dbContext.Employees.OrderBy(e => e.EmployeeID).Skip((page - 1) * maxRows).Take(maxRows).ToList();

            ViewBag.Total = dbContext.Employees.Count();
            return View(employees);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            MVCContext dbContext = new MVCContext();
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();

            TempData["Message"] = "Employee added successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            MVCContext dbContext = new MVCContext();
            Employee employee = dbContext.Employees.Find(id);

            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            MVCContext dbContext = new MVCContext();
            dbContext.Entry(employee).State = EntityState.Modified;
            dbContext.SaveChanges();
            TempData["Message"] = "Employee updated successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            MVCContext dbContext = new MVCContext();
            Employee employee = dbContext.Employees.Find(id);
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
            TempData["Message"] = "Employee deleted successfully";
            return RedirectToAction("Index");
        }
    }
}