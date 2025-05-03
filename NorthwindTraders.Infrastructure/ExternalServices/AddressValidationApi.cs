using DotNetEnv;
using Newtonsoft.Json;
using NorthwindTraders.Application.DTOs;
using NorthwindTraders.Domain.Interfaces.ExternalServices;
using NorthwindTraders.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTraders.Infrastructure.ExternalServices
{
    public class AddressValidationApi(HttpClient httpClient) : IAddressValidationApi
    {
        public async Task<IResponse> validateAddress(string RegionCode, string Locality, string
                                        AdministrativeArea, string PostalCode, List<string> AddressLines)
        {

            var address = new AddressDto
            {
                regionCode = RegionCode,
                locality = Locality,
                administrativeArea = AdministrativeArea,
                postalCode = PostalCode,
                addressLines = AddressLines
            };

            var query = new AddressValidationRequestDto
            {
                address = address,
                enableUspsCass = (address.regionCode == "US")

            };

            var header = JsonConvert.SerializeObject(query, Formatting.Indented);

            //Console.WriteLine(header);

            Env.Load();

            var response =  await httpClient.PostAsync($"/v1:validateAddress?key={Environment.GetEnvironmentVariable("API_KEY")}", new StringContent(header, Encoding.UTF8, "application/json"));
            //response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(response +"\n\n\n" + content);
            return JsonConvert.DeserializeObject<IResponse>(content);
        }
    }
}
