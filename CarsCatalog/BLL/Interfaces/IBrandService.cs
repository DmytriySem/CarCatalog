using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBrandService
    {
        IList<BrandDTO> GetAllBrands();
        BrandDTO GetBrand(int id);
        void AddBrand(BrandDTO brand);
        void DeleteBrand(BrandDTO brand);
        void UpdateBrand(BrandDTO brand);
    }
}
