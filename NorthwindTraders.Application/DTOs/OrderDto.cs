using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Application.DTOs
{
    public class OrderDto
    {
        [Key]
        public int OrderID { get; set; }

        [StringLength(5)]
        public string CustomerID { get; set; }

        public int? EmployeeID { get; set; }


        public DateTime OrderDate { get; set; }


        [StringLength(60)]
        public string ShipAddress { get; set; }

        [StringLength(15)]
        public string? ShipCity { get; set; }

        [StringLength(15)]
        public string? ShipRegion { get; set; }

        [StringLength(10)]
        public string? ShipPostalCode { get; set; }

        [StringLength(15)]
        public string? ShipCountry { get; set; }

        // Navigation properties (optional, depending on relationships)
        // public CustomerDto Customer { get; set; }
        //public EmployeeDto Employee { get; set; }
        // public Shipper Shipper { get; set; }
    }
}
