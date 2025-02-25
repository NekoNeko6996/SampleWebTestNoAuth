using SampleWebTestNoAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SampleWebTestNoAuth.Controllers
{
    public class HomeController : Controller
    {
        private static List<Employee> _employees = new List<Employee>();
        public ActionResult Index()
        {
            int maxRows = 5;
            int page = Request.Params["page"] != null ? int.Parse(Request.Params["page"]) : 1;

            List<Employee> employees = _employees.OrderBy(e => e.EmployeeID)
                                                 .Skip((page - 1) * maxRows)
                                                 .Take(maxRows)
                                                 .ToList();

            ViewBag.Total = _employees.Count;
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

            // Tìm ID lớn nhất và tăng lên 1
            employee.EmployeeID = _employees.Any() ? _employees.Max(e => e.EmployeeID) + 1 : 1;
            _employees.Add(employee);

            TempData["Message"] = "Employee added successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Employee employee = _employees.FirstOrDefault(e => e.EmployeeID == id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            Employee existingEmployee = _employees.FirstOrDefault(e => e.EmployeeID == employee.EmployeeID);
            if (existingEmployee != null)
            {
                existingEmployee.EmployeeName = employee.EmployeeName;
                existingEmployee.EmployeeSalary = employee.EmployeeSalary;
            }

            TempData["Message"] = "Employee updated successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Employee employee = _employees.FirstOrDefault(e => e.EmployeeID == id);
            if (employee != null)
            {
                _employees.Remove(employee);
                TempData["Message"] = "Employee deleted successfully";
            }
            return RedirectToAction("Index");
        }
    }
}
