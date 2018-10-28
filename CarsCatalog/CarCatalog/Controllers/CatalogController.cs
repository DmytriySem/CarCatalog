using AutoMapper;
using AuxiliaryClass;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using CarCatalog.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CarCatalog.Models.CatalogViewModel;
using static CarCatalog.Models.CatalogViewModel.BrandModel;
using static CarCatalog.Models.CatalogViewModel.BrandModel.ModelModel;
using static CarCatalog.Models.CatalogViewModel.BrandModel.ModelModel.CarModel;

namespace CarCatalog.Controllers
{
    public class CatalogController : Controller
    {
        private IBrandService brandService;
        private IModelService modelService;
        private ICarService carService;

        private readonly CatalogViewModel catalogTree;
        private List<CarViewModel> cars;

        public CatalogController()
        {
            brandService = new BrandService();
            modelService = new ModelService();
            carService = new CarService();

            catalogTree = GetCatalogTree();
        }

        public string GetCars(string treeNodeName, int color, int volEngine, int colsNum,
            string minPrice, string maxPrice, string dateTime)
        {
            decimal min = decimal.Parse(minPrice);
            decimal max = decimal.Parse(maxPrice);

            var searchDate = dateTime.Split('/');
            DateTime date = new DateTime(Convert.ToInt32(searchDate[0]), Convert.ToInt32(searchDate[1]), Convert.ToInt32(searchDate[2]));

            if (treeNodeName == "ALL")
            {
                cars = Mapper.Map<IEnumerable<CarDTO>, List<CarViewModel>>(carService.GetAllCars());
            }
            else
            {
                BrandViewModel brand = Mapper.Map<BrandDTO, BrandViewModel>(brandService.GetAllBrands().FirstOrDefault(x => x.Name == treeNodeName));
                if (brand != null)
                {
                    cars = Mapper.Map<IEnumerable<CarDTO>, List<CarViewModel>>(carService.GetAllCars().Where(x => x.BrandId == brand.Id));

                }
                else
                {
                    ModelViewModel model = Mapper.Map<ModelDTO, ModelViewModel>(modelService.GetAllModels().FirstOrDefault(x => x.Name == treeNodeName));
                    if (model != null)
                    {
                        cars = Mapper.Map<IEnumerable<CarDTO>, List<CarViewModel>>(carService.GetAllCars().Where(x => x.BrandId == model.BrandId && x.ModelId == model.Id));
                    }
                }
            }

            if (color - 1 >= 0)
            {
                cars = cars.FindAll(x => x.Color == (Auxiliary.COLOR)(color - 1));
            }

            if (volEngine - 1 >= 0)
            {
                cars = cars.FindAll(x => x.VolumeEngine == Auxiliary.VolumeEngine[volEngine - 1]);
            }
            List<CarTileModel> tiles = new List<CarTileModel>();
            foreach (var item in cars)
            {
                var time = item.Prices.Where(x => DateTime.Compare(x.Date, date) < 0).ToList();

                if (time.Count == 0)
                    continue;

                var price = time.OrderByDescending(x => x.Date).First();

                if (price.Price < min || price.Price > max)
                    continue;

                CarTileModel model = new CarTileModel()
                {
                    PhotoUrl = item.Model.PhotoUrl,
                    Photo = item.Brand.Photo,
                    Name = item.Model.Name,
                    Color = item.Color,
                    VolumeEngine = item.VolumeEngine,
                    Description = item.Description
                    ,
                    Date = price.Date,
                    Price = price.Price
                };

                tiles.Add(model);
            }

            return CarCatalog.Helpers.CarsListHelper.CreateCarsList(tiles, colsNum).ToString();
        }

        public string GetMinMaxPrices()
        {
            cars = Mapper.Map<IEnumerable<CarDTO>, List<CarViewModel>>(carService.GetAllCars());
            
            var prices = from car in cars
                         from price in car.Prices
                         select price.Price;

            return prices.Min().ToString() + " " + prices.Max().ToString();
        }

        private CatalogViewModel GetCatalogTree()
        {
            CatalogViewModel catalog = new CatalogViewModel();

            var brands = brandService.GetAllBrands();
            for (int i = 0; i < brands.Count(); i++)
            {
                BrandModel brand = new BrandModel
                {
                    Id = brands[i].Id,
                    Name = brands[i].Name,
                    Photo = brands[i].Photo
                };

                var models = modelService.GetAllModels(brands[i].Id);
                for (int j = 0; j < models.Count(); j++)
                {
                    ModelModel model = new ModelModel
                    {
                        Id = models[j].Id,
                        Name = models[j].Name,
                        Photo = models[j].PhotoUrl
                    };
                    brand.Models.Add(model);
                }
                catalog.Brands.Add(brand);
            }
            return catalog;
        }

        private CatalogViewModel Catalog()
        {
            CatalogViewModel catalog = new CatalogViewModel();

            var brands = brandService.GetAllBrands();
            for (int i = 0; i < brands.Count(); i++)
            {
                BrandModel brand = new BrandModel
                {
                    Id = brands[i].Id,
                    Name = brands[i].Name,
                    Photo = brands[i].Photo
                };

                var models = modelService.GetAllModels(brands[i].Id);
                for (int j = 0; j < models.Count(); j++)
                {
                    ModelModel model = new ModelModel
                    {
                        Id = models[j].Id,
                        Name = models[j].Name,
                        Photo = models[j].PhotoUrl
                    };

                    var cars = carService.GetAllCars(models[j].Id);
                    for (int k = 0; k < cars.Count(); k++)
                    {
                        CarModel car = new CarModel
                        {
                            Id = cars[k].Id,
                            Color = cars[k].Color,
                            VolumeEngine = cars[k].VolumeEngine,
                            Description = cars[k].Description
                        };

                        var prices = cars[k].Prices;
                        for (int l = 0; l < prices.Count(); l++)
                        {
                            CostModel cost = new CostModel
                            {
                                Id = prices[l].Id,
                                Date = prices[l].Date,
                                Price = prices[l].Price
                            };

                            car.Prices.Add(cost);
                        }
                        model.Cars.Add(car);
                    }
                    brand.Models.Add(model);
                }
                catalog.Brands.Add(brand);
            }

            return catalog;
        }

        // GET: Catalog
        public ActionResult Index()
        {
            ViewBag.ListColors = listColors();
            ViewBag.VolEngines = listEngines();

            return View(catalogTree);
        }
        private List<SelectListItem> listColors()
        {
            string[] colors = Auxiliary.GetStringMassOfColorEnums();
            int sizeColors = colors.Length + 1;
            string[] COLORS = new string[sizeColors];
            for (int i = 0, j = 0; i < sizeColors; i++)
            {
                COLORS[i] = i == 0 ? "ALL" : colors[j++];
            }

            List<SelectListItem> listColors = new List<SelectListItem>();
            for (int i = 0; i < COLORS.Length; i++)
            {
                listColors.Add(new SelectListItem
                {
                    Text = COLORS[i],
                    Value = i.ToString()
                });
            }
            listColors.First().Selected = true;

            return listColors;
        }

        private List<SelectListItem> listEngines()
        {
            string[] volEngines = Auxiliary.GetStringMassOfVolumeEngine();
            int sizeEngines = volEngines.Length + 1;
            string[] VOL_ENGINES = new string[sizeEngines];
            for (int i = 0, j = 0; i < sizeEngines; i++)
            {
                VOL_ENGINES[i] = i == 0 ? "ALL" : volEngines[j++];
            }

            List<SelectListItem> listEngines = new List<SelectListItem>();
            for (int i = 0; i < VOL_ENGINES.Length; i++)
            {
                listEngines.Add(new SelectListItem
                {
                    Text = VOL_ENGINES[i],
                    Value = i.ToString()
                });
            }
            listEngines.First().Selected = true;

            return listEngines;
        }
    }
}