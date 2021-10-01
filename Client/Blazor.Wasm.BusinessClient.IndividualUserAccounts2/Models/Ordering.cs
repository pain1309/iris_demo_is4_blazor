using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Wasm.BusinessClient.IndividualUserAccounts2.Models
{
    public class Ordering
    {
        [JsonProperty(PropertyName = "UserName")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "TotalPrice")]
        public decimal TotalPrice { get; set; }

        // BillingAddress
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "EmailAddress")]
        public string EmailAddress { get; set; }
        [JsonProperty(PropertyName = "Country")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "ZipCode")]
        public string ZipCode { get; set; }

        // Payment
        [JsonProperty(PropertyName = "CardName")]
        public string CardName { get; set; }
        [JsonProperty(PropertyName = "CardNumber")]
        public string CardNumber { get; set; }
        [JsonProperty(PropertyName = "Expiration")]
        public string Expiration { get; set; }
        [JsonProperty(PropertyName = "CVV")]
        public string CVV { get; set; }
        [JsonProperty(PropertyName = "PaymentMethod")]
        public int PaymentMethod { get; set; }
    }

    public class OrderingResponse
    {
        public IEnumerable<Ordering> Ordering { get; set; }
    }
}
