using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static AuxiliaryClass.Auxiliary;

namespace CarCatalog.Models
{
    public class CarTileModel
    {
        public byte[] Photo { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public COLOR Color { get; set; }
        public double VolumeEngine { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}