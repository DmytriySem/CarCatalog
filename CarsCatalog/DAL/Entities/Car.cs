using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AuxiliaryClass.Auxiliary;

namespace DAL.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        public int ModelId { get; set; }
        public virtual Model Model { get; set; }

        public COLOR Color { get; set; }
        public double VolumeEngine { get; set; }
        public virtual ICollection<Cost> Prices { get; set; }
        public string Description { get; set; }

        public Car()
        {
            Prices = new List<Cost>();
        }
    }
}
