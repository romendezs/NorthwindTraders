using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Domain.Entities
{
    [Table("Employees")]
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string TitleOfCourtesy { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string Country { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public byte[]? Photo { get; set; } // Stored as byte array
        public string Notes { get; set; } = string.Empty;
        public int? ReportsTo { get; set; } // Nullable for top-level employee
        public string PhotoPath { get; set; } = string.Empty;

        // Optional: navigation property if using EF Core
        //public Employee? Manager { get; set; }
    }
}
