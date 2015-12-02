using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Security.Claims;

using System.Threading;
using MIT.CRM.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MIT.CRM.Controllers
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

        public IActionResult Edit(int id)
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

        public IActionResult Create(int id)
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
            
            _applicationDbContext.Ferias_Itens.Add(feria);
            _applicationDbContext.SaveChanges();

            ApplicationUser user = _applicationDbContext.Users.First(c => c.UserName == User.Identity.Name);

            Historio_Ferias_Item historico = new Historio_Ferias_Item
            {
                estado = feria.estado,
                ferias_item_id = feria.id,
                utilizadorId = user.Id,
                data = DateTime.Now
            };

            _applicationDbContext.Historio_Ferias_Item.Add(historico);
            _applicationDbContext.SaveChanges();

            return "sucesso";
        }

        [HttpPost]
        public JsonResult ListaMarcacaoFerias(int funcionarioId, short ano)
        {
            var listaFerias = _applicationDbContext.Ferias_Itens.Where(f => f.funcionarioId == funcionarioId).OrderBy(x => x.dataFeria);
            return Json(listaFerias);
        }

        [HttpPost]
        public void AprovacaoFerias(Ferias_Itens _feria)
        {
            var list = _applicationDbContext.Ferias_Itens.Where(f => 
                f.dataFeria.Date.Equals(_feria.dataFeria.Date) &&
                f.funcionarioId == _feria.funcionarioId && 
                f.tipo == _feria.tipo
            );

            if(list.Count() > 0)
            {
                Ferias_Itens feria = list.First();

                feria.estado = _feria.estado;

                ApplicationUser user = _applicationDbContext.Users.First(c => c.UserName == User.Identity.Name);

                Historio_Ferias_Item historico = new Historio_Ferias_Item
                {
                    estado = feria.estado,
                    ferias_item_id = feria.id,
                    utilizadorId = user.Id,
                    data = DateTime.Now
                };
                _applicationDbContext.Ferias_Itens.Update(feria);
                _applicationDbContext.Historio_Ferias_Item.Add(historico);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
