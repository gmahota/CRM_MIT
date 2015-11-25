using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using CRM.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    public class FuncionarioController : Controller
    {
        [FromServices]
        public ApplicationDbContext _applicationDbContext { get; set; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Create(string codigo, string nome, string departamento, string telemovel, string empresaId)
        {
            
            Funcionario funcionario = new Funcionario
            {
                codigo = codigo,
                nome = nome,
                telemovel = telemovel,
                empresaId = empresaId
            };
            var dep = _applicationDbContext.Departamentos.Where(d => d.departamento == departamento);

            if (dep != null)
                funcionario.departamentoId = dep.First().Id;
            
            try
            {
                var temp = _applicationDbContext.Funcionarios.Where(d => d.codigo == codigo && d.empresaId == empresaId);
                if (temp == null)
                {
                    _applicationDbContext.Funcionarios.Add(funcionario);
                    _applicationDbContext.SaveChanges();

                    return "ok";
                }
                else
                {
                    return "null";
                }
            }
            catch
            {
                

                return "null";


            }


        }
    }
}
