using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarCatalog.Models
{
    public class CatalogModel
    {
        public List<BrandViewModel> Brands { get; set; }
        public CatalogModel()
        {
            Brands = new List<BrandViewModel>();
        }
    }
}