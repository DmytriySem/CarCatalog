using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICostService
    {
        IList<CostDTO> GetAllCosts(int carId);
        CostDTO GetLastCost(int carId);
        void UpdateLastCost(int carId, decimal lastCost);
        void AddCost(CostDTO cost);
    }
}
