﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;
using System.Security.Claims;

using System.Threading;
using MIT.CRM.Models;
using MIT.CRM.Services;
using Newtonsoft.Json;
using MIT.Repository;
using Microsoft.Extensions.OptionsModel;
using MIT.CRM.Models.Helper;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MIT.CRM.Controllers
{
    [Authorize]
    public class RHController : Controller
    {
        [FromServices]
        public ApplicationDbContext _context { get; set; }

        private readonly IEmailSender _emailSender;

        private IOptions<AppSettings> _config;

        public RHController(IEmailSender emailSender, IOptions<AppSettings> config)
        {
            _emailSender = emailSender;
            _config = config;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            try
            {
                Funcionario funcionario = _context.Funcionarios
                    .Include(m => m.utilizador)
                    .Include(m => m.departamento)
                    .Include(m => m.empresa)
                    .Single( m=> m.utilizador.UserName == User.Identity.Name);
                
                if (funcionario != null)
                {
                    //ViewData["ResponsaveId"] = user;
                    ViewBag.departamento = funcionario.departamento.descricao;
                    ViewBag.empresa = funcionario.empresa.codigo;
                }
                else { ViewBag.departamento = ""; }

            }
            catch { ViewBag.departamento = ""; }


            return View();
        }

        [HttpGet]
        public IActionResult ListaFuncionarios()
        {
            var list = _context.Funcionarios.OrderBy(m => m.nome);

            List<int> id = new List<int>();
            foreach (var i in list)
                id.Add(i.id);

            return Json(id);
        }

        public async Task<IActionResult> Details()
        {
            ApplicationUser user = _context.Users.First(c => c.UserName == User.Identity.Name);

            var list = _context.Funcionarios.Where(f => f.utilizadorId == user.Id);

            ViewData["conexao"] = _config.Value.signalRConfig.server_Url;
            ViewData["url"] = _config.Value.signalRConfig.javaScript_Url;

            if (list.Count() > 0)
            {
                Funcionario funcionario = list.First();
                ViewData["modulo_value"] = funcionario.codigo + " - " + funcionario.nome;
                return View(funcionario);
            }
            else
            {
                ViewData["Message"] = String.Format("Não existe o respectivo funcionario ou não tem permissões de acesso, por favor contacte o administrador do sistema");
                return View("Error");
            }

        }

        public IActionResult Edit(int id)
        {
            ViewData["conexao"] = _config.Value.signalRConfig.server_Url;
            ViewData["url"] = _config.Value.signalRConfig.javaScript_Url;

            ApplicationUser user = _context.Users.First(c => c.UserName == User.Identity.Name);

            Funcionario funcionario = _context.Funcionarios.Include(f => f.departamento).Single(f => f.id == id);
            
            if (funcionario != null)
            {

                if(user.Id == funcionario.departamento.responsavelId)
                {
                    ViewData["modulo_value"] = funcionario.codigo + " - " + funcionario.nome;
                    return View(funcionario);
                }
                else
                {
                    ViewData["Message"] = String.Format("O utilizador {0}, não é reponsavel pelo departamento {1} ", User.Identity.Name,funcionario.departamento.descricao);

                }

            }
            else
            {
                ViewData["Message"] = String.Format("Não existe nenhum funcionario associado ao Utilizador {0}, por favor contacte o administrador do sistema", User.Identity.Name);
                
            }
            return View("Error");
        }

        public IActionResult Create(int id)
        {
            ViewData["conexao"] = _config.Value.signalRConfig.server_Url;
            ViewData["url"] = _config.Value.signalRConfig.javaScript_Url;

            //ApplicationUser user = _applicationDbContext.Users.First(c => c.UserName == User.Identity.Name);

            //var list = _applicationDbContext.Funcionarios.Where(f => f.utilizadorId == user.Id);
            var list = _context.Funcionarios.Where(f => f.id == id);
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

        
        public async void MarcacaoFerias(short ano, DateTime dataFeria, int funcionarioId, string tipo, string estado,int tipoMarcacao)
        {
            Ferias_Itens feria = new Ferias_Itens()
            {
                ano = ano,
                dataFeria = dataFeria,
                funcionarioId = funcionarioId,
                tipo = tipo,
                estado = estado,
                tipoMarcacao = tipoMarcacao

            };
            
            _context.Ferias_Itens.Add(feria);
            _context.SaveChanges();

            ApplicationUser user = _context.Users.First(c => c.UserName == User.Identity.Name);

            Historio_Ferias_Item historico = new Historio_Ferias_Item
            {
                estado = feria.estado,
                ferias_item_id = feria.id,
                utilizadorId = user.Id,
                data = DateTime.Now
            };

            _context.Historio_Ferias_Item.Add(historico);
            _context.SaveChanges();

            //return Json("sucesso");
        }

        // POST: Ferias/Delete/5
        [HttpPost]
        public async void Delete_Ferias(int id)
        {
            try
            {
                Ferias_Itens ferias = _context.Ferias_Itens.Single(m => m.id == id);
                _context.Ferias_Itens.Remove(ferias);
                _context.SaveChanges();
            }catch(Exception e) { }
            
        }

        [HttpPost]
        public JsonResult ListaMarcacaoFerias(int funcionarioId, short ano)
        {
            var listaFerias = _context.Ferias_Itens.Include(f=> f.funcionario)
                .Where(f => f.funcionarioId == funcionarioId && f.ano == ano && f.estado != "Recusado").OrderBy(x => x.dataFeria);

            return Json(listaFerias,new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        [HttpPost]
        public JsonResult listaFuncionarios(string empresa, string departamento)
        {
            var listaFuncionarios = _context.Funcionarios.Include(m=> m.departamento)
                .Where(f=> f.empresaId == empresa && f.departamento.departamento== departamento)
                .OrderBy(m => m.nome);

            return Json(listaFuncionarios, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        [HttpGet]
        public JsonResult DataMarcacaoFerias(int funcionarioId, int ano,int mes)
        {
            // &&
            //
            var ferias = _context.Ferias_Itens.Include(f => f.funcionario).Where(m => m.funcionarioId == funcionarioId &&
                m.dataFeria.Year == ano && m.dataFeria.Month == mes).OrderBy(m=> m.dataFeria);

            return Json(ferias, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        [HttpPost]
        public JsonResult AprovacaoFerias(Ferias_Itens _feria)
        {
            Ferias_Itens feria = new Ferias_Itens();
            var list = _context.Ferias_Itens.Where(f => 
                f.dataFeria.Date.Equals(_feria.dataFeria.Date) &&
                f.funcionarioId == _feria.funcionarioId &&
                f.estado == "Por Aprovar"
                //&& f.tipo == _feria.tipo
            );

            if(list.Count() > 0)
            {
                feria = list.First();

                feria.estado = _feria.estado;

                ApplicationUser user = _context.Users.First(c => c.UserName == User.Identity.Name);

                Historio_Ferias_Item historico = new Historio_Ferias_Item
                {
                    estado = feria.estado,
                    ferias_item_id = feria.id,
                    utilizadorId = user.Id,
                    data = DateTime.Now
                };
                _context.Ferias_Itens.Update(feria);
                _context.Historio_Ferias_Item.Add(historico);
                _context.SaveChanges();
            }

            return Json(feria, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
