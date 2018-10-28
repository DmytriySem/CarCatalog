using AutoMapper;
using BLL.DTO;
using CarCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarCatalog.App_Start
{
    public class MappingConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BrandDTO, BrandViewModel>();
                cfg.CreateMap<BrandViewModel, BrandDTO>();
                cfg.CreateMap<ModelDTO, ModelViewModel>();
                cfg.CreateMap<CarDTO, CarViewModel>();
                cfg.CreateMap<CostDTO, CostViewModel>();
                cfg.CreateMap<CarViewModel, CarGridModel>();
                cfg.CreateMap<CarGridModel, CarViewModel>();
            });
        }
    }
}