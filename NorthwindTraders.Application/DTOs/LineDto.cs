using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Application.DTOs
{
    public class LineDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }    // Could be UnitPrice after discount
        public decimal Amount { get; set; }
    }
}
