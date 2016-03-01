using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MIT.CRM.Models;
using MIT.Data;

namespace MIT.CRM.Controllers
{
    public class FuncInfFeriasController : Controller
    {
        private ApplicationDbContext _context;

        public FuncInfFeriasController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: FuncInfFerias
        public IActionResult Index()
        {
            var applicationDbContext = _context.FuncInfFerias.Include(f => f.funcionario);
            return View(applicationDbContext.ToList());
        }

        // GET: FuncInfFerias/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FuncInfFerias funcInfFerias = _context.FuncInfFerias.Single(m => m.id == id);
            if (funcInfFerias == null)
            {
                return HttpNotFound();
            }

            return View(funcInfFerias);
        }


        public IActionResult Details(int?id, short ano)
        {
            var ferias = _context.FuncInfFerias.Single(m => m.funcionarioId == id && m.ano == ano);

            return PartialView("_Details", ferias);
        }
        // GET: FuncInfFerias/Create
        public IActionResult Create()
        {
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "id", "funcionario");
            return View();
        }

        // POST: FuncInfFerias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FuncInfFerias funcInfFerias)
        {
            if (ModelState.IsValid)
            {
                _context.FuncInfFerias.Add(funcInfFerias);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "id", "funcionario", funcInfFerias.funcionarioId);
            return View(funcInfFerias);
        }

        // GET: FuncInfFerias/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FuncInfFerias funcInfFerias = _context.FuncInfFerias.Single(m => m.id == id);
            if (funcInfFerias == null)
            {
                return HttpNotFound();
            }
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "id", "funcionario", funcInfFerias.funcionarioId);
            return View(funcInfFerias);
        }

        // POST: FuncInfFerias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FuncInfFerias funcInfFerias)
        {
            if (ModelState.IsValid)
            {
                _context.Update(funcInfFerias);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["funcionarioId"] = new SelectList(_context.Funcionarios, "id", "funcionario", funcInfFerias.funcionarioId);
            return View(funcInfFerias);
        }

        // GET: FuncInfFerias/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FuncInfFerias funcInfFerias = _context.FuncInfFerias.Single(m => m.id == id);
            if (funcInfFerias == null)
            {
                return HttpNotFound();
            }

            return View(funcInfFerias);
        }

        // POST: FuncInfFerias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            FuncInfFerias funcInfFerias = _context.FuncInfFerias.Single(m => m.id == id);
            _context.FuncInfFerias.Remove(funcInfFerias);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
