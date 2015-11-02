using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MIT.CRM.Controllers
{
    public class CobrancasController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult EnviaEmail(string client)
        {
            
            return Json(new
            {
                Success = true,
                Message = "The person has been added!"
                //PartialViewHtml = RenderPartialViewToString("PersonList", new PersonListViewModel { PersonList = _personList })
            });

        }

    }
}
