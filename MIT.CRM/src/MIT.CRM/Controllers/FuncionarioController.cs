using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Microsoft.AspNet.Identity;
using System.Security.Claims;
using MIT.CRM.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MIT.CRM.Controllers
{
    public class FuncionarioController : Controller
    {



        [FromServices]
        public ApplicationDbContext _applicationDbContext { get; set; }

        [FromServices]
        public UserManager<ApplicationUser> _userManager {get;set;}


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> Create(string codigo, string nome, string departamento, string email, string telemovel,  string empresaId)
        {
            
            Funcionario funcionario = new Funcionario
            {
                codigo = codigo,
                nome = nome,
                telemovel = telemovel,
                email = email.Trim(),
                empresaId = empresaId
            };
            

            var dep = _applicationDbContext.Departamentos.Where (d => d.descricao == departamento|| " " + d.descricao == departamento);

            if (dep.Count() > 0)
                funcionario.departamentoId = dep.First().Id;
            else
                funcionario.departamentoId = 17;

            try
            {

                var user = await _userManager.FindByNameAsync(funcionario.email);

                if (user == null)
                {
                    user = new ApplicationUser { UserName = funcionario.email, Email = funcionario.email, PhoneNumber=funcionario.telemovel };
                    await _userManager.CreateAsync(user, "Meridian123456!");
                    await _userManager.AddToRoleAsync(user, "Funcionario");
                    await _userManager.AddClaimAsync(user, new Claim("Funcionario", "Allowed"));

                    funcionario.utilizadorId = user.Id;
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Funcionario");
                    await _userManager.AddClaimAsync(user, new Claim("Funcionario", "Allowed"));

                    funcionario.utilizadorId = user.Id;
                }

                var temp = _applicationDbContext.Funcionarios.Where(d => d.codigo == codigo && d.empresaId == empresaId);
                if (temp.Count ()== 0)
                {
                    _applicationDbContext.Funcionarios.Add(funcionario);
                    _applicationDbContext.SaveChanges();

                    return "ok";
                }
                else
                {
                    return "null";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("Erro ao gravar funcionario {0} - {1}"), codigo, e.Message);

                return "null";


            }


        }

        private void addUser()
        {
            
        }
    }
}
