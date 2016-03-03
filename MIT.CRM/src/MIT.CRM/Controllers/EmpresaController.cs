using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MIT.CRM.Models;
using MIT.Repository;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MIT.CRM.Controllers
{
    public class EmpresaController : Controller
    {
        [FromServices]
        public ApplicationDbContext _applicationDbContext { get; set; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Create(string codigo, string nome)
        {
            Empresa empresa = new Empresa
            {
                codigo =codigo,
                codEmpresaPri =codigo,
                nome =nome,
                nomeEmpresa = nome,
                empresaPrimavera = true
            };

            var temp = _applicationDbContext.Empresas.Where(emp => emp.codigo == codigo);

            if (temp.Count() == 0)
            {
                _applicationDbContext.Empresas.Add(empresa);
                _applicationDbContext.SaveChanges();

                return "ok";
            }
            else
            {
                return "null";
            }
                

            
        }

        
    }
}
