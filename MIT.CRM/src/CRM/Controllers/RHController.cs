using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Security.Claims;
using CRM.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    [Authorize]
    public class RHController : Controller
    {
        [FromServices]
        public ApplicationDbContext _applicationDbContext { get; set; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ApplicationUser user = _applicationDbContext.Users.First(c => c.UserName == User.Identity.Name);
            //Funcionario funcionario = user.funcionario;

            //if (funcionario == null)
            //{
            //    ViewData["Message"] = String.Format("Não existe nenhum funcionario associado ao Utilizador {0}, por favor contacte o administrador do sistema", User.Identity.Name);
            //    return View("Error");
            //}
            //else
            //{
            //    ViewData["modulo_value"] = funcionario.codigo + " - " + funcionario.nome;
            //}

            return View();
        }
        
        public IActionResult MarcacaoFerias()
        {
            ApplicationUser user = _applicationDbContext.Users.First(c => c.UserName == User.Identity.Name);

            var list = _applicationDbContext.Funcionarios.Where(f => f.utilizadorId == user.Id);
            
            if(list.Count() > 0)
            {
                Funcionario funcionario = list.First();
                ViewData["modulo_value"] = funcionario.codigo + " - " + funcionario.nome;
                return View(funcionario);
            }
            else
            {
                ViewData["Message"] = String.Format("Não existe nenhum funcionario associado ao Utilizador {0}, por favor contacte o administrador do sistema", User.Identity.Name);
                return View("Error");
            }
                
           
            
            
        }
    }
}
