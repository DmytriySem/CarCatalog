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
    public class ModelService : IModelService
    {
        private IGenericRepository<Model> repo;
        private Model model;
        private IMapper mapper;

        public ModelService()
        {
            repo = new EFGenericRepository<Model>();
            model = new Model();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<ModelDTO, Model>()).CreateMapper();
        }

        public void AddModel(ModelDTO modelDTO)
        {
            if (modelDTO.Name == null)
            {
                throw new ValidationException("The model must have a name!!!", "Model");
            }
            model = mapper.Map<ModelDTO, Model>(modelDTO);

            var obj = repo.GetAll().Where(x => x.BrandId == modelDTO.BrandId).FirstOrDefault(p => p.Name == model.Name);

            if (obj == null)
            {
                repo.Create(model);
            }
            else
            {
                throw new ValidationException("Such a model <span style='color:red'>" + model.Name + "</span> exist!!!", "Model");
            }
        }

        public void DeleteModel(ModelDTO modelDTO)
        {
            repo.Remove(mapper.Map<ModelDTO, Model>(modelDTO));
        }

        public IList<ModelDTO> GetAllModels()
        {
            return Mapper.Map<IEnumerable<Model>, List<ModelDTO>>(repo.GetAll());
        }

        public IList<ModelDTO> GetAllModels(int brandId)
        {
            return Mapper.Map<IEnumerable<Model>, List<ModelDTO>>(repo.GetAll().Where(x => x.BrandId == brandId));
        }

        public ModelDTO GetModel(int id)
        {
            return mapper.Map<Model, ModelDTO>(repo.FindById(id));
        }

        public void UpdateModel(ModelDTO modelDTO)
        {
            repo.Update(Mapper.Map<ModelDTO, Model>(modelDTO));
        }
    }
}
