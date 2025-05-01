using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Domain.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [StringLength(5)]
        public string? CustomerID { get; set; } // Make nullable

        public int? EmployeeID { get; set; } // Make nullable

        public DateTime OrderDate { get; set; }

        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int ShipVia { get; set; }

        [Column(TypeName = "money")]
        public decimal Freight { get; set; }

        [StringLength(40)]
        public string? ShipName { get; set; } // Strings may be nullable

        [StringLength(60)]
        public string? ShipAddress { get; set; }

        [StringLength(15)]
        public string? ShipCity { get; set; }

        [StringLength(15)]
        public string? ShipRegion { get; set; }

        [StringLength(10)]
        public string? ShipPostalCode { get; set; }

        [StringLength(15)]
        public string? ShipCountry { get; set; }

        // Navigation properties
        public Customer? Customer { get; set; } // Nullable
        public Employee? Employee { get; set; } // Nullable
                                                // public Shipper Shipper { get; set; }
    }
}
