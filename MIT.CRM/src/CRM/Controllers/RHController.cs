using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Security.Claims;
using CRM.Models;
using System.Threading;

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

        public IActionResult Details()
        {
            ApplicationUser user = _applicationDbContext.Users.First(c => c.UserName == User.Identity.Name);

            var list = _applicationDbContext.Funcionarios.Where(f => f.utilizadorId == user.Id);


            if (list.Count() > 0)
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

        public IActionResult MarcacaoFerias(int id)
        {
            //ApplicationUser user = _applicationDbContext.Users.First(c => c.UserName == User.Identity.Name);

            //var list = _applicationDbContext.Funcionarios.Where(f => f.utilizadorId == user.Id);
            var list = _applicationDbContext.Funcionarios.Where(f => f.id == id);
            if (list.Count() > 0)
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

        [HttpPost]
        public async Task<string> MarcacaoFerias(Ferias_Itens feria)
        {
            

            //var listFerias = _applicationDbContext.Ferias.Where(f => f.funcionarioId == feria.funcionarioId);
            //bool existe = false;

            //if(listFerias.Count() == 0)
            //{
            //    Ferias ferias = new Ferias()
            //    {
            //        funcionarioId = feria.funcionarioId,
            //        dataInicio = feria.dataFeria,
            //        dataFim = feria.dataFeria
            //    };

            //    _applicationDbContext.Ferias.Add(ferias);
            //    _applicationDbContext.SaveChanges();

            //    feria.feriasId = ferias.id;
            //}
            //else
            //{

            //    foreach (var f in listFerias)
            //    {
            //        if (f.dataFim.AddDays(1).Date.Equals(feria.dataFeria.Date))
            //        {
            //            feria.feriasId = f.id;
            //            f.dataFim = feria.dataFeria;
            //            _applicationDbContext.Ferias.Update(f);
            //            existe = true;
            //            _applicationDbContext.SaveChanges();
            //            break;
            //        }
            //        if (f.dataInicio.AddDays(-1).Date.Equals(feria.dataFeria.Date))
            //        {
            //            feria.feriasId = f.id;
            //            f.dataInicio = feria.dataFeria;
            //            _applicationDbContext.Ferias.Update(f);
            //            existe = true;
            //            _applicationDbContext.SaveChanges();
            //            break;
                        
            //        }
            //    }

            //    if(existe == false)
            //    {
            //        Ferias ferias = new Ferias()
            //        {
            //            funcionarioId = feria.funcionarioId,
            //            dataInicio = feria.dataFeria,
            //            dataFim = feria.dataFeria
            //        };

            //        _applicationDbContext.Ferias.Add(ferias);
            //        _applicationDbContext.SaveChanges();
            //        feria.feriasId = ferias.id;
            //    }

            //}
             
            _applicationDbContext.Ferias_Itens.Add(feria);
            _applicationDbContext.SaveChanges();

             return "sucesso";
        }

        [HttpPost]
        public JsonResult ListaMarcacaoFerias(int funcionarioId, short ano)
        {
            var listaFerias = _applicationDbContext.Ferias_Itens.Where(f => f.funcionarioId == funcionarioId);
            return Json(listaFerias);
        }
    }
}
