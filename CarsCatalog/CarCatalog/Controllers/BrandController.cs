using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using CarCatalog.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCatalog.Controllers
{
    public class BrandController : Controller
    {
        private IBrandService brandService;

        public BrandController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IBrandService>().To<BrandService>();
            brandService = ninjectKernel.Get<IBrandService>();
        }

        public ActionResult Index()
        {
            var brands = Mapper.Map<IEnumerable<BrandDTO>, List<BrandViewModel>>(brandService.GetAllBrands());

            return View(brands);
        }

        public ActionResult RemoveBrand(int id, string brandName)
        {
            var brand = new BrandViewModel { Id = id, Name = brandName };

            return PartialView("RemoveBrand", brand);
        }

        [HttpPost]
        public ActionResult RemoveBrand(int id)
        {
            BrandDTO brand = new BrandDTO { Id = id };
            brandService.DeleteBrand(brand);

            return RedirectToAction("Index");
        }

        public ActionResult AddNewBrand()
        {
            return PartialView("AddNewBrand");
        }

        [HttpPost]
        public ActionResult AddNewBrand(BrandViewModel brand, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    brand.Photo = imageData;
                }

                try
                {
                    brandService.AddBrand(Mapper.Map<BrandViewModel, BrandDTO>(brand));
                }
                catch (ValidationException ex)
                {
                    ViewBag.PropertyException = ex.Property;
                    ViewBag.MessageException = ex.Message;

                    return View("ExceptionView");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditBrand(int id)
        {
            var brand = Mapper.Map<BrandDTO, BrandViewModel>(brandService.GetBrand(id));

            return PartialView("EditBrand", brand);
        }

        [HttpPost]
        public ActionResult EditBrand(BrandViewModel brand, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    brand.Photo = imageData;
                }
                else
                {
                    brand.Photo = (brandService.GetBrand(brand.Id)).Photo;
                }

                brandService.UpdateBrand(Mapper.Map<BrandViewModel, BrandDTO>(brand));
            }
            return RedirectToAction("Index");
        }
    }
}