using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Domain.Entities
{
    public class OrderDetail
    {
        
        [Key] public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }

    }
}
