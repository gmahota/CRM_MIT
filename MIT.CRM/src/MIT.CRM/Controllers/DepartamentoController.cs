using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MIT.CRM.Models;
using System;
using MIT.CRM.Services;
using System.Threading.Tasks;
using MIT.CRM.Models.Helper;
using System.Globalization;
using System.Text;
using MIT.Data;
using System.Threading;

namespace MIT.CRM.Controllers
{
    public class DepartamentoController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;



        public DepartamentoController(ApplicationDbContext context,IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _context = context;    
        }

        // GET: Departamento
        public IActionResult Index()
        {
            var applicationDbContext = _context.Empresas.ToList();

            for(int i = 0; i < applicationDbContext.Count(); i++)
            {
                applicationDbContext.ElementAt(i).departamentosList = _context.Departamentos.Include(e => e.responsavel).Where(e => e.empresaId == applicationDbContext.ElementAt(i).codigo).ToList();
            }
           
            return View(applicationDbContext.ToList());
        }

        // GET: Departamento/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Departamento departamento = _context.Departamentos.Single(m => m.Id == id);
            if (departamento == null)
            {
                return HttpNotFound();
            }

            return View(departamento);
        }

        // GET: Departamento/Create
        public IActionResult Create()
        {
            ViewData["empresaId"] = new SelectList(_context.Empresas, "codigo","nome");
            ViewData["responsavelId"] = new SelectList(_context.Users, "Id","UserName");
            return View();
        }
        
        public string Create_Json(string empresaId, string departamento, string descricao)
        {
            Departamento dep = new Departamento
            {
                departamento = departamento,
                descricao = descricao,
                empresaId = empresaId
            };
            try
            {
                var temp = _context.Departamentos.Where(d => d.departamento == departamento && d.empresaId == empresaId);
                if (temp.Count() == 0)
                {
                    _context.Departamentos.Add(dep);
                    _context.SaveChanges();

                    return "ok";
                }
                else
                {
                    return "null";
                }
            }
            catch (Exception e)
            {
                return "error " + e.Message;
            }


        }

        // POST: Departamento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Departamentos.Add(departamento);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["empresaId"] = new SelectList(_context.Empresas, "codigo", "empresa", departamento.empresaId);
            ViewData["responsavelId"] = new SelectList(_context.Users, "Id", "responsavel", departamento.responsavelId);
            return View(departamento);
        }

        // GET: Departamento/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Departamento departamento = _context.Departamentos.Single(m => m.Id == id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            ViewData["empresaId"] = new SelectList(_context.Empresas, "codigo", "nome", departamento.empresaId);
            ViewData["responsavelId"] = new SelectList(_context.Users, "Id", "UserName", departamento.responsavelId);
            return View(departamento);
        }

        // POST: Departamento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Departamento dep )
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");

            if (ModelState.IsValid)
            {
                _context.Update(dep);
                _context.SaveChanges();

                var callbackUrl = "http://ferias.mit.co.mz:5000/";

                ApplicationUser user = _context.Users.Include(m => m.funcionario).Single(m => m.Id == dep.responsavelId);
                Empresa emp = _context.Empresas.Single(m => m.codigo == dep.empresaId);
                if (dep != null && user != null)
                {
                    string mensaguem = " <h4>Caro (a) " + user.funcionario.nome + " </h4> <br/>" +
                        "<p>Foi adicionado como responsavel do departamento " + dep.descricao + " da empresa " + emp.nome + " para a gestão das ferias do colobaroados afectos ao respectivo departamento.</p> <br/>" +
                        "<p><b>Aplicação:  </b> <a href=\"" + callbackUrl + "\">" + callbackUrl + "</a> </p> <br/>";

                    string a =string.Format(CultureInfo.GetCultureInfo("pt-PT"), "Não Responder");

                    var b = @"Aplicação de Marcação de Ferias -Em Produção / Teste";
                    
                    var host = HttpContext.Request.Host.Value;

                    await _emailSender.SendAsync("do-not-reply@meridian32.com", a, user.Email,"", b, mensaguem, host );
                }


                

                return RedirectToAction("Index");
            }
            ViewData["empresaId"] = new SelectList(_context.Empresas, "codigo", "empresa", dep.empresaId);
            ViewData["responsavelId"] = new SelectList(_context.Users, "Id", "responsavel", dep.responsavelId);
            return View(dep);
        }

        // GET: Departamento/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Departamento departamento = _context.Departamentos.Single(m => m.Id == id);
            if (departamento == null)
            {
                return HttpNotFound();
            }

            return View(departamento);
        }

        // POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Departamento departamento = _context.Departamentos.Single(m => m.Id == id);
            _context.Departamentos.Remove(departamento);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}
