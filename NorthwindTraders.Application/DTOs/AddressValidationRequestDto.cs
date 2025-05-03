using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Application.DTOs
{
    public class AddressValidationRequestDto
    {
        public AddressDto address { get; set; }
        public bool enableUspsCass { get; set; }
    }
}
