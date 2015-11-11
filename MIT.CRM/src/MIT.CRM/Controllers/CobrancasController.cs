using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MIT.CRM.Services;
using System.Configuration;
using System.Net;
using System.Diagnostics;
using SendGrid;

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

        public JsonResult EnviaEmail(string client,string ficheiro)
        {
            try
            {
                Email x = new Email();

                //x.sendEmail("Cliente: " + client);

                x.configSendGridasync("gmahota@mit.co.mz", ficheiro);

                return Json(new
                {
                    Success = true,
                    Message = "Enviado"
                    //PartialViewHtml = RenderPartialViewToString("PersonList", new PersonListViewModel { PersonList = _personList })
                });
            }
            catch(Exception e)
            {
                return Json(new
                {
                    Success = true,
                    Message = "Erro" + e.Message
                    //PartialViewHtml = RenderPartialViewToString("PersonList", new PersonListViewModel { PersonList = _personList })
                });
            }
            
            

        }


    }
}
