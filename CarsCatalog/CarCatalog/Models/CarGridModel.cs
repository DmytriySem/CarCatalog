using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static AuxiliaryClass.Auxiliary;

namespace CarCatalog.Models
{
    public class CarGridModel
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public COLOR Color { get; set; }
        public double VolumeEngine { get; set; }
        public string Description { get; set; }
        public string LastDate { get; set; }
        public decimal LastPrice { get; set; }
    }
}