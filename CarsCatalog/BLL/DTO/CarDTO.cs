using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AuxiliaryClass.Auxiliary;

namespace BLL.DTO
{
    public class CarDTO
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public BrandDTO Brand { get; set; }
        public int ModelId { get; set; }
        public ModelDTO Model { get; set; }
        public COLOR Color { get; set; }
        public double VolumeEngine { get; set; }
        public IList<CostDTO> Prices { get; set; }
        public string Description { get; set; }
    }
}
