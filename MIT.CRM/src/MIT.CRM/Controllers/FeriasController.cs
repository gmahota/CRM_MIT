using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MIT.CRM.Models;
using MIT.Repository;

namespace MIT.CRM.Controllers
{
    public class FeriasController : Controller
    {
        private ApplicationDbContext _context;

        public FeriasController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Ferias
        public IActionResult Index()
        {
            var applicationDbContext = _context.Ferias.Include(f => f.funcionario);
            return View(applicationDbContext.ToList());
        }

        // GET: Ferias/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Ferias ferias = _context.Ferias.Single(m => m.id == id);
            if (ferias == null)
            {
                return HttpNotFound();
            }

            return View(ferias);
        }

        // GET: Ferias/Create
        public IActionResult Create()
        {
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "id", "funcionario");
            return View();
        }

        // POST: Ferias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ferias ferias)
        {
            if (ModelState.IsValid)
            {
                _context.Ferias.Add(ferias);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "id", "funcionario", ferias.funcionarioId);
            return View(ferias);
        }

        // GET: Ferias/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Ferias ferias = _context.Ferias.Single(m => m.id == id);
            if (ferias == null)
            {
                return HttpNotFound();
            }
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "id", "funcionario", ferias.funcionarioId);
            return View(ferias);
        }

        // POST: Ferias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ferias ferias)
        {
            if (ModelState.IsValid)
            {
                _context.Update(ferias);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "id", "funcionario", ferias.funcionarioId);
            return View(ferias);
        }

        // GET: Ferias/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Ferias ferias = _context.Ferias.Single(m => m.id == id);
            if (ferias == null)
            {
                return HttpNotFound();
            }

            return View(ferias);
        }

        // POST: Ferias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Ferias ferias = _context.Ferias.Single(m => m.id == id);
            _context.Ferias.Remove(ferias);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}
