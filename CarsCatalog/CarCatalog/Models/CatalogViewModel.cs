using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static AuxiliaryClass.Auxiliary;

namespace CarCatalog.Models
{
    public class CatalogViewModel
    {
        public List<BrandModel> Brands { get; set; }

        public CatalogViewModel()
        {
            Brands = new List<BrandModel>();
        }

        public class BrandModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public byte[] Photo { get; set; }
            public List<ModelModel> Models { get; set; }

            public BrandModel()
            {
                Models = new List<ModelModel>();
            }

            public class ModelModel
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Photo { get; set; }
                public List<CarModel> Cars { get; set; }

                public ModelModel()
                {
                    Cars = new List<CarModel>();
                }

                public class CarModel
                {
                    public int Id { get; set; }                    
                    [JsonConverter(typeof(StringEnumConverter))]
                    public COLOR Color { get; set; }
                    public double VolumeEngine { get; set; }
                    public string Description { get; set; }
                    public List<CostModel> Prices { get; set; }

                    public CarModel()
                    {
                        Prices = new List<CostModel>();
                    }

                    public class CostModel
                    {
                        public int Id { get; set; }
                        public DateTime Date { get; set; }
                        public decimal Price { get; set; }
                    }
                }
            }
        }
    }
}