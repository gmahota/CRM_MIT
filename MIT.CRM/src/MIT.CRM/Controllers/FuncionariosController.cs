using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MIT.CRM.Models;
using MIT.Repository;
using Microsoft.AspNet.Hosting;
using System.Collections.Generic;
using Microsoft.AspNet.Http;
using System.IO;
using Microsoft.Net.Http.Headers;
using MIT.CRM.Services;
using System.Threading.Tasks;

namespace MIT.CRM.Controllers
{
    public class FuncionariosController : Controller
    {
        private ApplicationDbContext _context;

        private IHostingEnvironment _environment;

        private AppServices _appServices;

        public FuncionariosController(ApplicationDbContext context, IHostingEnvironment environment, AppServices appServices)
        {
            _context = context;
            _environment = environment;
            _appServices = appServices;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {

            if ((await _appServices.getResponsavelDepartamento(User.Identity.Name)))
            {
                //ViewData["modulo_value"] = funcionario.codigo + " - " + funcionario.nome;
                return View(await _appServices.getFuncionariosFromResponsavelDepartamento(User.Identity.Name));
            }


           
            return View(new List<Funcionario>());
        }

        // GET: Funcionarios/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Funcionario funcionario = _context.Funcionarios.Single(m => m.id == id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            ViewData["departamentoId"] = new SelectList(_context.Departamentos, "Id", "departamento");
            ViewData["empresaId"] = new SelectList(_context.Empresas, "codigo", "empresa");
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "utilizador");
            return View();
        }

        // POST: Funcionarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Funcionarios.Add(funcionario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["departamentoId"] = new SelectList(_context.Departamentos, "Id", "departamento", funcionario.departamentoId);
            ViewData["empresaId"] = new SelectList(_context.Empresas, "codigo", "empresa", funcionario.empresaId);
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "utilizador", funcionario.utilizadorId);
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public  IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            
            Funcionario funcionario = _context.Funcionarios.Single(m => m.id == id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            ViewData["departamentoId"] = new SelectList(_context.Departamentos, "Id", "departamento", funcionario.departamentoId);
            ViewData["empresaId"] = new SelectList(_context.Empresas, "codigo", "nome", funcionario.empresaId);
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "UserName", funcionario.utilizadorId);
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Funcionario funcionario, ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            uploads = Path.Combine(uploads, "funcionarios");
            uploads = Path.Combine(uploads, funcionario.codigo);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {

                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    file.SaveAsAsync(Path.Combine(uploads, fileName));

                    
                }

            }

            if (ModelState.IsValid)
            {
                _context.Update(funcionario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            

            ViewData["departamentoId"] = new SelectList(_context.Departamentos, "id", "departamento", funcionario.departamentoId);
            ViewData["empresaId"] = new SelectList(_context.Empresas, "codigo", "nome", funcionario.empresaId);
            ViewData["utilizadorId"] = new SelectList(_context.Users, "Id", "UserName", funcionario.utilizadorId);
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Funcionario funcionario = _context.Funcionarios.Single(m => m.id == id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            
            Funcionario funcionario = _context.Funcionarios.Single(m => m.id == id);
            _context.Funcionarios.Remove(funcionario);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
