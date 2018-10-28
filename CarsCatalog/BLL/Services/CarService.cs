using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services
{
    public class CarService : ICarService
    {
        private IGenericRepository<Car> repo;
        private Car car;

        public CarService()
        {
            repo = new EFGenericRepository<Car>();
            car = new Car();
        }

        public void AddCar(CarDTO carDTO)
        {
            repo.Create(Mapper.Map<CarDTO, Car>(carDTO));
        }

        public void DeleteCar(CarDTO carDTO)
        {
            repo.Remove(Mapper.Map<CarDTO, Car>(carDTO));
        }

        public IList<CarDTO> GetAllCars()
        {
            return Mapper.Map<IEnumerable<Car>, List<CarDTO>>(repo.GetAll());
        }

        public IList<CarDTO> GetAllCars(int modelId)
        {
            return Mapper.Map<IEnumerable<Car>, List<CarDTO>>(repo.GetAll().Where(x => x.ModelId == modelId));
        }

        public CarDTO GetCar(int id)
        {
            return Mapper.Map<Car, CarDTO>(repo.FindById(id));
        }

        public void UpdateCar(CarDTO carDTO)
        {
            repo.Update(Mapper.Map<CarDTO, Car>(carDTO));
        }
    }
}
