using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MIT.CRM.Models;
using MIT.Data;

namespace MIT.CRM.Controllers
{
    public class ContactsController : Controller
    {
        private ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Contacts
        public IActionResult Index()
        {
            return View(_context.Contact.ToList());
        }

        // GET: Contacts/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Contact contact = _context.Contact.Single(m => m.id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contact.Add(contact);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Contact contact = _context.Contact.Single(m => m.id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Update(contact);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Contact contact = _context.Contact.Single(m => m.id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Contact contact = _context.Contact.Single(m => m.id == id);
            _context.Contact.Remove(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
