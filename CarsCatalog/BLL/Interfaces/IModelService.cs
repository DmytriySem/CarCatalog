using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IModelService
    {
        IList<ModelDTO> GetAllModels();
        IList<ModelDTO> GetAllModels(int brandId);
        ModelDTO GetModel(int id);
        void AddModel(ModelDTO model);
        void DeleteModel(ModelDTO model);
        void UpdateModel(ModelDTO model);
    }
}
