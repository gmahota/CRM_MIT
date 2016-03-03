using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MIT.CRM.Models;
using MIT.Repository;
using Microsoft.Extensions.OptionsModel;
using MIT.CRM.Models.Helper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;


namespace MIT.CRM.Controllers
{
    public class HomeController : Controller
    {
        [FromServices]
        public ApplicationDbContext _context { get; set; }

        private IOptions<AppSettings> _config;

        public HomeController(IOptions<AppSettings> config)
        {
            _config = config;

        }

        public IActionResult Index()
        {
            if(User.Identity.Name != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            try
            {
                var funcionarios = _context.Funcionarios.Include(f => f.utilizador)
                    .Include(f=> f.departamento)
                    .Where(f=> f.activo == true).ToList();

                ViewBag.empresas = _context.Empresas.ToList();
                @ViewBag.totalFuncionarios = funcionarios.Count();
                return View(funcionarios);
            }
            catch
            {
                ViewBag.funcionarios = new List<Funcionario>();
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
            ViewData["Message"] = "Your contact page. " + _config.Value.SiteTitle ;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
