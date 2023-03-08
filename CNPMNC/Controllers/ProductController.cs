using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNPMNC.Models;

namespace CNPMNC.Controllers
{
    public class ProductController : Controller
    {
        CNPMNCEntities database = new CNPMNCEntities();
        // GET: Product
        public ActionResult Index()
        {
            var listSanPham = database.SANPHAMs.OrderByDescending(sp => sp.TENSP).ToList();
            return View(listSanPham);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SANPHAM sp)
        {
            try
            {
                if (sp.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(sp.UploadImage.FileName);
                    string extent = Path.GetExtension(sp.UploadImage.FileName);
                    filename = filename + extent;
                    sp.HINHSP = "~/Content/Images/" + filename;
                    sp.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), filename));
                }
                database.SANPHAMs.Add(sp);
                database.SaveChanges();
                return Redirect("/Management/ProductsManagement");
            }
            catch(Exception e)
            {
                ViewBag.Error = e.ToString();
                return View();
            }
        }
    }
}