using AutoMapper;
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

namespace BLL.Services
{
    public class CostService : ICostService
    {
        private IGenericRepository<Cost> repo;
        private Cost cost;
        private IMapper mapper;
        private IMapper mapper1;

        public CostService()
        {
            repo = new EFGenericRepository<Cost>();
            cost = new Cost();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<Cost, CostDTO>()).CreateMapper();
            mapper1 = new MapperConfiguration(cfg => cfg.CreateMap<CostDTO, Cost>()).CreateMapper();
        }

        public void AddCost(CostDTO cost)
        {
            throw new NotImplementedException();
        }

        public IList<CostDTO> GetAllCosts(int carId)
        {
            return mapper.Map<IEnumerable<Cost>, List<CostDTO>>(repo.GetAll());
        }

        public CostDTO GetLastCost(int carId)
        {
            return mapper.Map<Cost, CostDTO>(repo.GetAll().Where(x => x.CarId == carId).OrderByDescending(x => x.Date).First());
        }

        public void UpdateLastCost(int carId, decimal lastCost)
        {
            CostDTO cost = mapper.Map<Cost, CostDTO>(repo.GetAll().Where(x => x.CarId == carId).OrderByDescending(x => x.Date).First());
            cost.Price = lastCost;
            repo.Update(mapper1.Map<CostDTO, Cost>(cost));
        }
    }
}
