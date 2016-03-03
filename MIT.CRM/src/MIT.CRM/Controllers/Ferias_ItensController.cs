using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MIT.CRM.Models;
using MIT.Repository;

namespace MIT.CRM.Controllers
{
    public class Ferias_ItensController : Controller
    {
        private ApplicationDbContext _context;

        public Ferias_ItensController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Ferias_Itens
        public IActionResult Index()
        {
            var applicationDbContext = _context.Ferias_Itens.Include(f => f.ferias).Include(f => f.funcionario);
            return View(applicationDbContext.ToList());
        }

        // GET: Ferias_Itens/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Ferias_Itens ferias_Itens = _context.Ferias_Itens.Single(m => m.id == id);
            if (ferias_Itens == null)
            {
                return HttpNotFound();
            }

            return View(ferias_Itens);
        }

        // GET: Ferias_Itens/Create
        public IActionResult Create()
        {
            ViewData["feriasId"] = new SelectList(_context.Ferias, "id", "ferias");
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "id", "nome");
            return View();
        }

        // POST: Ferias_Itens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ferias_Itens ferias_Itens)
        {
            if (ModelState.IsValid)
            {
                _context.Ferias_Itens.Add(ferias_Itens);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["feriasId"] = new SelectList(_context.Ferias, "id", "ferias", ferias_Itens.feriasId);
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "id", "funcionario", ferias_Itens.funcionarioId);
            return View(ferias_Itens);
        }

        // GET: Ferias_Itens/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Ferias_Itens ferias_Itens = _context.Ferias_Itens.Single(m => m.id == id);
            if (ferias_Itens == null)
            {
                return HttpNotFound();
            }
            ViewData["feriasId"] = new SelectList(_context.Ferias, "id", "ferias", ferias_Itens.feriasId);
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "id", "funcionario", ferias_Itens.funcionarioId);
            return View(ferias_Itens);
        }

        // POST: Ferias_Itens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ferias_Itens ferias_Itens)
        {
            if (ModelState.IsValid)
            {
                _context.Update(ferias_Itens);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["feriasId"] = new SelectList(_context.Ferias, "id", "ferias", ferias_Itens.feriasId);
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "id", "funcionario", ferias_Itens.funcionarioId);
            return View(ferias_Itens);
        }

        // GET: Ferias_Itens/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Ferias_Itens ferias_Itens = _context.Ferias_Itens.Single(m => m.id == id);
            if (ferias_Itens == null)
            {
                return HttpNotFound();
            }

            return View(ferias_Itens);
        }

        // POST: Ferias_Itens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Ferias_Itens ferias_Itens = _context.Ferias_Itens.Single(m => m.id == id);
            _context.Ferias_Itens.Remove(ferias_Itens);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
