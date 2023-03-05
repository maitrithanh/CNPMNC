using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNPMNC.Models;

namespace CNPMNC.Controllers
{
    public class ManagementController : Controller
    {
        CNPMNCEntities database = new CNPMNCEntities();
        // GET: Management
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductsManagement()
        {
            var listProductsManagement = database.SANPHAMs.OrderByDescending(sp => sp.TENSP).ToList();
            return View(listProductsManagement);
        }
    }
}