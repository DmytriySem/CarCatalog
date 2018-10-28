using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarCatalog.Models
{
    public class ModelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public int BrandId { get; set; }
        public BrandDTO Brand { get; set; }
    }
}