using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MIT.CRM.Models;

namespace MIT.CRM.Controllers
{
    public class HomeController : Controller
    {
        [FromServices]
        public ApplicationDbContext _context { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Dashboard()
        {
            try
            {
                ViewBag.empresas = _context.Empresas.ToList();
            }
            catch
            {
                ViewBag.empresas = new List<Empresa>();
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
