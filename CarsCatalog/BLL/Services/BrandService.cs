using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
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
    public class BrandService : IBrandService
    {
        private IGenericRepository<Brand> repo;
        private Brand brand;
        private IMapper mapper;

        public BrandService()
        {
            repo = new EFGenericRepository<Brand>();
            brand = new Brand();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap < BrandDTO, Brand>()).CreateMapper();
        }

        public void AddBrand(BrandDTO brandDTO)
        {
            if (brandDTO.Name == null)
            {
                throw new ValidationException("The brand must have a name!!!", "Brand");
            }
            brand = mapper.Map<BrandDTO, Brand>(brandDTO);

            var obj = repo.GetAll().FirstOrDefault(p => p.Name == brand.Name);

            if (obj == null)
            {
                repo.Create(brand);
            }
            else
            {
                throw new ValidationException("Such a brand <span style='color:red'>" + brand.Name + "</span> exist!!!", "Brand");
            }
        }

        public void DeleteBrand(BrandDTO brandDTO)
        {
            repo.Remove(mapper.Map<BrandDTO, Brand>(brandDTO));
        }

        public IList<BrandDTO> GetAllBrands()
        {
            return Mapper.Map<IEnumerable<Brand>, List<BrandDTO>>(repo.GetAll());
        }

        public BrandDTO GetBrand(int id)
        {
            return mapper.Map<Brand, BrandDTO>(repo.FindById(id));
        }

        public void UpdateBrand(BrandDTO brandDTO)
        {
            repo.Update(mapper.Map<BrandDTO, Brand>(brandDTO));
        }
    }
}
