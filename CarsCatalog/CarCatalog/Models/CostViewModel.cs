using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarCatalog.Models
{
    public class CostViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        //public int CarId { get; set; }
    }
}