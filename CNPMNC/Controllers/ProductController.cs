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

        public ActionResult Create()
        {
            ViewBag.IDDANHMUC = new SelectList(database.DANHMUCs, "IDDANHMUC", "TENDANHMUC");
            ViewBag.NhanVien = new SelectList(database.NHANVIENs, "MANV", "HOTENNV");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSP,TENSP,SOLUONG,DONGIA,MOTA,HINHSP,IDDANHMUC,IDNV")] SANPHAM sp, HttpPostedFileBase UploadImage)
        {

            if (ModelState.IsValid)
            {
                if (UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(UploadImage.FileName);
                    string extent = Path.GetExtension(UploadImage.FileName);
                    filename = filename + extent;
                    sp.HINHSP = "~/Content/Images/" + filename;
                    UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), filename));
                }
                database.SANPHAMs.Add(sp);
                database.SaveChanges();
                return Redirect("/Management/ProductsManagement");
            }

            ViewBag.IDDANHMUC = new SelectList(database.DANHMUCs, "IDDANHMUC", "TENDANHMUC", sp.IDDANHMUC);
            ViewBag.NhanVien = new SelectList(database.NHANVIENs, "MANV", "HOTENNV", sp.NHANVIEN);
            return View();
        }
        public ActionResult DSProduct(string SearchString ="")
        {
            if (SearchString !="")
            {
                var list = database.SANPHAMs.OrderByDescending(s => s.DANHMUC).Where(x => x.TENSP.ToUpper().Contains(SearchString.ToUpper()));
                return View(list.ToList());
            }
            else
            {
                var listProducts = database.SANPHAMs.OrderByDescending(sp => sp.TENSP).ToList();
                return View(listProducts);
            }
        }

    }
}