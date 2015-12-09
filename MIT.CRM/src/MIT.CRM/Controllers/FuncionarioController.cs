using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

using Microsoft.AspNet.Identity;
using System.Security.Claims;
using MIT.CRM.Models;
using MIT.CRM.Services;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MIT.CRM.Controllers
{
    public class FuncionarioController : Controller
    {
        [FromServices]
        public ApplicationDbContext _context { get; set; }

        [FromServices]
        public UserManager<ApplicationUser> _userManager {get;set;}

        private readonly IEmailSender _emailSender;

        public FuncionarioController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

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
            

            var dep = _context.Departamentos.Where (d => d.descricao == departamento|| " " + d.descricao == departamento);

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

                    var callbackUrl = "http://192.168.3.16:5000/";

                    string mensaguem = " <h4>Caro Colaborador " + funcionario.nome + " </h4> <br/>" +
                        "<p>Foi criado com sucesso o seu utilizador para a marcação de ferias Online ainda em fase de Produção/Teste.</p>" +
                        "<p><b>Utilizador: </b> " + funcionario.email + "</p> <br/>" +
                        "<p><b>Password:   </b> Meridian123456!</p> <br/>" +
                        "<p><b>Aplicação:  </b> <a href=\"" + callbackUrl + "\">" + callbackUrl + "</a> </p> <br/>" +
                        "<br/> <p> <b> Faça Login e depois seleciona o menu RH e depois Ferias e em seguida o botão editar. </b></p>";


                    await _emailSender.SendAsync("GLOBAL@MIT.CO.MZ", "Não Responder", funcionario.email,"", "Aplicação de Marcação de Ferias -Em Produção / Teste",
                       mensaguem);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Funcionario");
                    await _userManager.AddClaimAsync(user, new Claim("Funcionario", "Allowed"));

                    funcionario.utilizadorId = user.Id;
                }

                var temp = _context.Funcionarios.Where(d => d.codigo == codigo && d.empresaId == empresaId);
                if (temp.Count ()== 0)
                {
                    _context.Funcionarios.Add(funcionario);
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
                Console.WriteLine(String.Format("Erro ao gravar funcionario {0} - {1}"), codigo, e.Message);

                return "null";


            }


        }

        [HttpPost]
        public async void sendEmailCriacaoFerias(int funcionarioId,string mensaguem, string titulo)
        {
            try
            {
                var funcionario = _context.Funcionarios.Include(f => f.utilizador).Single(f => f.id == funcionarioId);

                var dep = _context.Departamentos.Include(d => d.responsavel).Single(d => d.Id == funcionario.departamentoId);

                var user = funcionario.utilizador;

                var callbackUrl = "http://192.168.3.16:5000/RH/Edit/" + funcionarioId;

                string mensaguem1 = " <h4>Caro Responsavel do Departamento " + dep.descricao + " </h4> <br/>" +
                        "<p>O Funcionario " + funcionario.nome + " vem por meio deste email submeter o seu pedido de ferias nas datas abaixo:</p> <br/>" +
                         mensaguem + " <br/>" +
                        "<p>Para aprovação queira porfavor clicar neste linK: <a href=\"" + callbackUrl + "\">" + callbackUrl + "</a> </p> <br/>";

                if (user == null)
                {
                    user = _context.Users.Single(c => c.UserName == User.Identity.Name);

                    await _emailSender.SendAsync("global@mit.co.mz","Não Responder", dep.responsavel.Email, funcionario.email, titulo, mensaguem1);
                }
                else
                {
                    await _emailSender.SendAsync("global@mit.co.mz", "Não Responder", dep.responsavel.Email,user.Email, titulo, mensaguem1);
                }
            }
            catch { }
            

        }

        [HttpPost]
        public async void sendEmailAprovacaoFerias(string reponsavel, int funcionarioId, string mensaguem, string titulo)
        {
            try
            {
                var funcionario = _context.Funcionarios.Include(f => f.utilizador).Single(f => f.id == funcionarioId);

                var dep = _context.Departamentos.Include(d => d.responsavel).Single(d => d.Id == funcionario.departamentoId);

                var user = funcionario.utilizador;

                var callbackUrl = "http://192.168.3.16:5000/RH/Details/" + funcionarioId;

                string mensaguem1 = " <h4>Caro Colaborador " + funcionario.nome + " </h4> <br/>" +
                        "<p>Abaixo a lista de aprovações/repovações do pedido de ferias: </p> <br/>" +
                         mensaguem + " <br/>" +
                        "<p>Detalhes em : <a href=\"" + callbackUrl + "\">" + callbackUrl + "</a> </p> <br/>";

                if (user == null)
                {
                    user = _context.Users.Single(c => c.UserName == User.Identity.Name);

                    await _emailSender.SendAsync("global@mit.co.mz", "Não Responder",funcionario.email, dep.responsavel.Email, titulo, mensaguem1);
                }
                else
                {
                    await _emailSender.SendAsync("global@mit.co.mz", "Não Responder", funcionario.email, dep.responsavel.Email, titulo, mensaguem1);
                }
            }
            catch { }


        }

        [HttpGet]
        public JsonResult listaFuncionarios(string empresa,string departamento)
        {
            var listaFuncionarios = _context.Funcionarios.Include(f => f.departamento).Include(f=> f.utilizador).OrderBy(m => m.nome);
            
            return Json(listaFuncionarios, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        private void addUser()
        {
            
        }
    }
}
