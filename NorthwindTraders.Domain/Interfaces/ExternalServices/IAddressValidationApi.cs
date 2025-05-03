using NorthwindTraders.Domain.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthwindTraders.Domain.Interfaces.ExternalServices
{
    public interface IAddressValidationApi
    {
        Task<IResponse> validateAddress(string RegionCode, string Locality, string
                                        AdministrativeArea, string PostalCode, List<string> AddressLines);
    }
}
