using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Microsoft.AspNet.Hosting;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MIT.CRM.Controllers
{
    public class CobrancasController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public CobrancasController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult EnviaEmail(string empresa,string client,string to, string cc,string ficheiro)
        {
            try
            {
                //Email x = new Email();

                ////x.sendEmail("Cliente: " + client);

                //x.configSendGridasync(empresa,"avisos@accsys.co.mz",to, cc,ficheiro, _environment.WebRootPath);

                return Json(new
                {
                    Success = true,
                    Message = "Enviado"
                    //PartialViewHtml = RenderPartialViewToString("PersonList", new PersonListViewModel { PersonList = _personList })
                });
            }
            catch (Exception e)
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
