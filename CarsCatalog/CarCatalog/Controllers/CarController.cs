using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using CarCatalog.Models;
using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuxiliaryClass;
using static AuxiliaryClass.Auxiliary;

namespace CarCatalog.Controllers
{
    public class CarController : Controller
    {
        private ICarService carService;

        public CarController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<ICarService>().To<CarService>();
            carService = ninjectKernel.Get<ICarService>();
        }

        public ActionResult Cars(int modelId)
        {
            var cars = Mapper.Map<IEnumerable<CarDTO>, IList<CarViewModel>>(carService.GetAllCars(modelId));
            var car = new CarViewModel();

            if (cars.Count() == 0)
            {
                IModelService modelService = new ModelService();
                var model = modelService.GetModel(modelId);
                ViewBag.BrandPhoto = model.Brand.Photo;
                ViewBag.ModelName = model.Name;
                ViewBag.ModelPhoto = model.PhotoUrl;
                car.BrandId = model.BrandId;
                TempData["BrandID"] = model.BrandId;
                TempData["modelID"] = model.Id;
            }
            else
            {
                car = cars.First();
                ViewBag.BrandPhoto = car.Brand.Photo;
                ViewBag.ModelName = car.Model.Name;
                ViewBag.ModelPhoto = car.Model.PhotoUrl;
                TempData["BrandID"] = car.BrandId;
                TempData["modelID"] = car.ModelId;
            }

            ViewBag.Colors = Auxiliary.GetStringMassOfColorEnums();
            ViewBag.VolEngines = Auxiliary.GetStringMassOfVolumeEngine();

            return View(car);
        }

        public string GetCars(int modelId, string sidx, int page, int rows, string sord, string searchString)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var cars = Mapper.Map<IEnumerable<CarDTO>, IList<CarViewModel>>(carService.GetAllCars(modelId));
            var carsGrid = Mapper.Map<IList<CarViewModel>, IList<CarGridModel>>(cars);

            for (int i = 0; i < cars.Count(); i++)
            {
                var costs = cars[i].Prices.ToList();
                carsGrid[i].LastDate = costs.Max(x => x.Date).ToShortDateString();
                carsGrid[i].LastPrice = costs.Where(x => x.Date.ToShortDateString() == carsGrid[i].LastDate).Select(x => x.Price).First();
                carsGrid[i].VolumeEngine = Math.Round(cars[i].VolumeEngine, 2);
            }

            int totalRecords = carsGrid.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                carsGrid = carsGrid.OrderByDescending(s => s.Id).ToList();
                carsGrid = carsGrid.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                carsGrid = carsGrid.OrderBy(s => s.Id).ToList();
                carsGrid = carsGrid.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }

            if (!string.IsNullOrEmpty(searchString))
            {                
                searchString = searchString.ToUpper();
                if (float.TryParse(searchString, out float volEngine))
                {
                    carsGrid = carsGrid.Where(m => m.VolumeEngine == Math.Round(volEngine, 2)).ToList();
                }
                else if ((Enum.IsDefined(typeof(COLOR), searchString)))
                {
                    carsGrid = carsGrid.Where(m => m.Color == (COLOR)Enum.Parse(typeof(COLOR), searchString)).ToList();
                }
            }

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = carsGrid
            };

            return JsonConvert.SerializeObject(jsonData);
        }

        [HttpPost]
        public void Delete(int id)
        {
            CarDTO car = new CarDTO { Id = id };
            carService.DeleteCar(car);
        }

        [HttpPost]
        public void Create(CarGridModel car)
        {
            decimal price = car.LastPrice;
            DateTime time = DateTime.Now;
            var carView = Mapper.Map<CarGridModel, CarViewModel>(car);
            carView.BrandId = (int)TempData["BrandID"];
            carView.ModelId = (int)TempData["ModelID"];
            carView.Prices.Add(new CostViewModel { Date = time, Price = price });
            carService.AddCar(Mapper.Map<CarViewModel, CarDTO>(carView));
        }

        [HttpPost]
        public void Edit(CarGridModel car)
        {
            decimal price = car.LastPrice;
            DateTime time = DateTime.Now;
            var carView = Mapper.Map<CarGridModel, CarViewModel>(car);
            carView.BrandId = (int)TempData["BrandID"];
            carView.ModelId = (int)TempData["ModelID"];

            //carView.Prices.Add(new CostViewModel { Date = time, Price = price });

            ICostService costService = new CostService();
            costService.UpdateLastCost(carView.Id, price);

            //carView.carService.GetCar(car.Id);

            carService.UpdateCar(Mapper.Map<CarViewModel, CarDTO>(carView));
        }
    }
}