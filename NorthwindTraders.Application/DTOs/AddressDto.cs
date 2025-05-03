using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Application.DTOs
{
    public class AddressDto
    {
            public string regionCode { get; set; }
            public string locality { get; set; }
            public string administrativeArea { get; set; }
            public string postalCode { get; set; }
            public List<string> addressLines { get; set; }
    }
}
