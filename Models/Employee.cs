using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SampleWebTestNoAuth.Models
{
	public class Employee
	{
		[Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Please enter employee name")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Please enter employee position")]
        public string EmployeePosition { get; set; }

        [Required(ErrorMessage = "Please enter employee salary")]
        [Range(10000, 10000000, ErrorMessage = "Salary must be between 10000 and 100000")]
        public double EmployeeSalary { get; set; }

    }
}