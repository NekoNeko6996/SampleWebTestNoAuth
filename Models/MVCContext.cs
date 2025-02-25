using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SampleWebTestNoAuth.Models
{
	public class MVCContext : DbContext
    {
        public MVCContext() : base("EPDatabase")
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}