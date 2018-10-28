using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICarService
    {
        IList<CarDTO> GetAllCars();
        IList<CarDTO> GetAllCars(int modelId);
        CarDTO GetCar(int id);
        void AddCar(CarDTO car);
        void DeleteCar(CarDTO car);
        void UpdateCar(CarDTO car);
    }
}
