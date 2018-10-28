using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using CarCatalog.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarCatalog.Controllers
{
    public class ModelController : Controller
    {
        private IModelService modelService;

        public ModelController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IModelService>().To<ModelService>();
            modelService = ninjectKernel.Get<IModelService>();
        }

        public ActionResult Models(int brandId)
        {
            TempData["BrandId"] = brandId;

            var models = Mapper.Map<IEnumerable<ModelDTO>, List<ModelViewModel>>(modelService.GetAllModels(brandId));

            if (models.Count() == 0)
            {
                IBrandService brandService = new BrandService();
                var brand = brandService.GetBrand(brandId);
                ViewBag.BrandName = brand.Name;
                ViewBag.BrandPhoto = brand.Photo;
            }

            return View(models);
        }

        public ActionResult RemoveModel(int id, string modelName)
        {
            var model = new ModelViewModel { Id = id, Name = modelName };

            return PartialView("RemoveModel", model);
        }

        [HttpPost]
        public ActionResult RemoveModel(int id)
        {
            ModelDTO model = new ModelDTO { Id = id };
            modelService.DeleteModel(model);

            return RedirectToAction("Models", new RouteValueDictionary(new { controller = "Model", action = "Models", brandId = TempData["BrandId"] }));
        }

        public ActionResult AddNewModel()
        {
            return PartialView("AddNewModel");
        }

        [HttpPost]
        public ActionResult AddNewModel(ModelViewModel model, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    string writePath = Server.MapPath(@"~/Content/Images/Models/") + model.Name + ".jpg";

                    Image img = Image.FromStream(uploadImage.InputStream);

                    img.Save(writePath);

                    model.PhotoUrl = model.Name + ".jpg";
                }

                model.BrandId = (int)TempData.Peek("BrandId");

                try
                {
                    modelService.AddModel(Mapper.Map<ModelViewModel, ModelDTO>(model));
                }
                catch (ValidationException ex)
                {
                    ViewBag.PropertyException = ex.Property;
                    ViewBag.MessageException = ex.Message;
                    ViewBag.BrandId = model.BrandId;

                    return View("ExceptionView");
                }
            }
            return RedirectToAction("Models", new RouteValueDictionary(new { controller = "Model", action = "Models", brandId = TempData["BrandId"] }));
        }

        public ActionResult EditModel(int id)
        {
            var model = Mapper.Map<ModelDTO, ModelViewModel>(modelService.GetModel(id));

            return PartialView("EditModel", model);
        }

        [HttpPost]
        public ActionResult EditModel(ModelViewModel model, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    string writePath = Server.MapPath(@"~/Content/Images/Models/") + model.Name + ".jpg";

                    Image img = Image.FromStream(uploadImage.InputStream);

                    img.Save(writePath);

                    model.PhotoUrl = model.Name + ".jpg";
                }
                else
                {
                    model.PhotoUrl = (modelService.GetModel(model.Id)).PhotoUrl;
                }

                model.BrandId = (int)TempData.Peek("BrandId");

                modelService.UpdateModel(Mapper.Map<ModelViewModel, ModelDTO>(model));
            }

            return RedirectToAction("Models", new RouteValueDictionary(new { controller = "Model", action = "Models", brandId = TempData["BrandId"] }));
        }
    }
}