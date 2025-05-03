using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace NorthwindTraders.Domain.Models
{
    public partial class IResponse
    {
        [JsonProperty("result")]
        public Result Result { get; set; }

        [JsonProperty("responseId")]
        public Guid ResponseId { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("verdict")]
        public Verdict Verdict { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("geocode")]
        public Geocode Geocode { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("uspsData")]
        public UspsData UspsData { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("formattedAddress")]
        public string FormattedAddress { get; set; }

        [JsonProperty("postalAddress")]
        public PostalAddress PostalAddress { get; set; }

        [JsonProperty("addressComponents")]
        public AddressComponent[] AddressComponents { get; set; }
    }

    public partial class AddressComponent
    {
        [JsonProperty("componentName")]
        public ComponentName ComponentName { get; set; }

        [JsonProperty("componentType")]
        public string ComponentType { get; set; }

        [JsonProperty("confirmationLevel")]
        public string ConfirmationLevel { get; set; }

        [JsonProperty("inferred", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Inferred { get; set; }
    }

    public partial class ComponentName
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("languageCode", NullValueHandling = NullValueHandling.Ignore)]
        public string LanguageCode { get; set; }
    }

    public partial class PostalAddress
    {
        [JsonProperty("regionCode")]
        public string RegionCode { get; set; }

        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("administrativeArea")]
        public string AdministrativeArea { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("addressLines")]
        public string[] AddressLines { get; set; }
    }

    public partial class Geocode
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("plusCode")]
        public PlusCode PlusCode { get; set; }

        [JsonProperty("bounds")]
        public Bounds Bounds { get; set; }

        [JsonProperty("placeId")]
        public string PlaceId { get; set; }

        [JsonProperty("placeTypes")]
        public string[] PlaceTypes { get; set; }
    }

    public partial class Bounds
    {
        [JsonProperty("low")]
        public Location Low { get; set; }

        [JsonProperty("high")]
        public Location High { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    public partial class PlusCode
    {
        [JsonProperty("globalCode")]
        public string GlobalCode { get; set; }
    }

    public partial class Metadata
    {
        [JsonProperty("business")]
        public bool Business { get; set; }

        [JsonProperty("residential")]
        public bool Residential { get; set; }
    }

    public partial class UspsData
    {
        [JsonProperty("standardizedAddress")]
        public StandardizedAddress StandardizedAddress { get; set; }

        [JsonProperty("deliveryPointCode")]
        public string DeliveryPointCode { get; set; }

        [JsonProperty("deliveryPointCheckDigit")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long DeliveryPointCheckDigit { get; set; }

        [JsonProperty("dpvConfirmation")]
        public string DpvConfirmation { get; set; }

        [JsonProperty("dpvFootnote")]
        public string DpvFootnote { get; set; }

        [JsonProperty("dpvCmra")]
        public string DpvCmra { get; set; }

        [JsonProperty("dpvVacant")]
        public string DpvVacant { get; set; }

        [JsonProperty("dpvNoStat")]
        public string DpvNoStat { get; set; }

        [JsonProperty("carrierRoute")]
        public string CarrierRoute { get; set; }

        [JsonProperty("carrierRouteIndicator")]
        public string CarrierRouteIndicator { get; set; }

        [JsonProperty("postOfficeCity")]
        public string PostOfficeCity { get; set; }

        [JsonProperty("postOfficeState")]
        public string PostOfficeState { get; set; }

        [JsonProperty("fipsCountyCode")]
        public string FipsCountyCode { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("elotNumber")]
        public string ElotNumber { get; set; }

        [JsonProperty("elotFlag")]
        public string ElotFlag { get; set; }

        [JsonProperty("addressRecordType")]
        public string AddressRecordType { get; set; }

        [JsonProperty("cassProcessed")]
        public bool CassProcessed { get; set; }

        [JsonProperty("dpvNoStatReasonCode")]
        public long DpvNoStatReasonCode { get; set; }

        [JsonProperty("dpvDrop")]
        public string DpvDrop { get; set; }

        [JsonProperty("dpvThrowback")]
        public string DpvThrowback { get; set; }

        [JsonProperty("dpvNonDeliveryDays")]
        public string DpvNonDeliveryDays { get; set; }

        [JsonProperty("dpvNoSecureLocation")]
        public string DpvNoSecureLocation { get; set; }

        [JsonProperty("dpvPbsa")]
        public string DpvPbsa { get; set; }

        [JsonProperty("dpvDoorNotAccessible")]
        public string DpvDoorNotAccessible { get; set; }

        [JsonProperty("dpvEnhancedDeliveryCode")]
        public string DpvEnhancedDeliveryCode { get; set; }
    }

    public partial class StandardizedAddress
    {
        [JsonProperty("firstAddressLine")]
        public string FirstAddressLine { get; set; }

        [JsonProperty("cityStateZipAddressLine")]
        public string CityStateZipAddressLine { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zipCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ZipCode { get; set; }

        [JsonProperty("zipCodeExtension")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ZipCodeExtension { get; set; }
    }

    public partial class Verdict
    {
        [JsonProperty("inputGranularity")]
        public string InputGranularity { get; set; }

        [JsonProperty("validationGranularity")]
        public string ValidationGranularity { get; set; }

        [JsonProperty("geocodeGranularity")]
        public string GeocodeGranularity { get; set; }

        [JsonProperty("addressComplete")]
        public bool AddressComplete { get; set; }

        [JsonProperty("hasInferredComponents")]
        public bool HasInferredComponents { get; set; }
    }
    // Add the missing ParseStringConverter class
    public class ParseStringConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String && long.TryParse((string)reader.Value, out var result))
            {
                return result;
            }

            throw new JsonSerializationException("Invalid value for ParseStringConverter.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long) || objectType == typeof(long?);
        }
    }
}
