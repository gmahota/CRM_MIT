using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using Microsoft.Extensions.Logging;
using MIT.CRM.Models;
using Microsoft.Data.Entity;
using System.Linq;
using MIT.Repository;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using MIT.CRM.Models.Helper;

namespace MIT.CRM.Hubs
{
    /// <summary>
    /// Sincronização com o modulo de RH
    /// </summary>
    [HubName("rhHub")]
    public class RhHub : Hub
    {
        /// <summary>
        /// Logger
        /// </summary>
        public ILogger Logger { get; private set; }

        private  ApplicationDbContext _context;

        private  UserManager<ApplicationUser> _userManager;

        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public RhHub(UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILoggerFactory loggerFactory) : base()
        {
            Logger = loggerFactory.CreateLogger<RhHub>();
            _context = context;
            _userManager = userManager;
        }

        public override async Task OnConnected()
        {
            
            string name = Context.User.Identity.Name;
            string type = "";

            if (Context.User.Identity.Name == null )
            {
                name = Context.QueryString["Name"];
                type = Context.QueryString["Type"];
                Clients.All.userConnected(String.Format("Connected : {0} - {1}", Context.ConnectionId, _connections.Count));
            }
            else
            {
                type = "User";
            }

            _connections.Add(name, Context.ConnectionId);
            
            Logger.LogInformation(String.Format("Connected : {0} - {1}", name, type));

            Clients.Caller.userConnected(String.Format("Connected : {0} - {1}", name, type));

            Clients.All.userConnected(String.Format("Connected : {0} - {1}", Context.ConnectionId, _connections.Count));
            await base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;
            string type = "";

            if (Context.User.Identity.IsAuthenticated == false)
            {
                name = Context.QueryString["Name"];
                type = Context.QueryString["Type"];
            }
            else
            {
                type = "User";
            }

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Remove(name, Context.ConnectionId);
            }

            Logger.LogInformation(String.Format("Disconeted : {0} - {1}", name, type));

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;
            string type = "";

            if (Context.User.Identity.IsAuthenticated == false)
            {
                name = Context.QueryString["Name"];
                type = Context.QueryString["Type"];
            }
            else
            {
                type = "User";
            }

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }
            Logger.LogInformation(String.Format("OnReconnected : {0} - {1}", name, type));

            return base.OnReconnected();
        }







        public void daListaEmpresas(int tipoPlataforma, string codUtilizador, string password, string categoria = "")
        {
            try
            {
                string name = Context.User.Identity.Name;
                string type = "";

                if (Context.User.Identity.IsAuthenticated == false)
                {
                    name = Context.QueryString["Name"];
                    type = Context.QueryString["Type"];
                }
                else
                {
                    type = "User";
                }
                foreach (var connectionId in _connections.GetConnections("Primavera ACC"))
                {
                    Clients.Client(connectionId).daListaEmpresa(connectionId,tipoPlataforma, codUtilizador,password);
                }

                //Clients.Caller.daListaEmpresas(listEntidades);
                //return listEntidades;
            }
            catch (Exception e)
            {
                //List<Empresa> listEmpresas = new List<Empresa>();
                //listEmpresas.Add(new Empresa() { codigo = "Demo" });

                //Clients.Caller.daListaEmpresas(listEmpresas);
                //return listEmpresas;
            }

        }

        public void listaEmpresas(string connectionId, List<Empresa> lista)
        {
            Clients.Client(connectionId).daListaEmpresa(lista);
        }

        public void daDadosEmpresa(int tipoPlataforma, string codEmpresa, string codUtilizador, string password)
        {
            try
            {
                //inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);
                //m._administradorErp = new MotoresPrimavera.Parametros.AdministradorErp(codUtilizador, password);
                //List<Empresa> listEmpresas = m._administradorErp.listaEmpresas();
                //Empresa temp = new Empresa();
                //foreach (var emp in listEmpresas)
                //{
                //    if (emp.codigo == codEmpresa)
                //    {
                //        temp = emp;


                //    }
                //}
                //Clients.Caller.daDadosEmpresa(temp);

                //return listEntidades;
            }
            catch (Exception e)
            {
                //List<Empresa> listEmpresas = new List<Empresa>();
                //listEmpresas.Add(new Empresa() { codigo = "Demo" });

                //Clients.Caller.daListaEmpresas(listEmpresas);
                //return listEmpresas;
            }


        }

        public void daListaFuncionarios(int tipoPlataforma, string codEmpresa, string codUtilizador, 
            string password,string resticoes="")
        {
            try
            {
                inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

                //List<Funcionario> listaFuncionarios = m._rh.daListaFuncionarios(resticoes);

                //Clients.Caller.daListaFuncionarios(listaFuncionarios);

            }
            catch
            {

            }

        }

        public void daInfFeriasFuncionario(int tipoPlataforma, string codEmpresa, string codUtilizador,
            string password,string codigo,short ano)
        {
            try
            {
                inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

                //FuncInfFerias infFerias = m._rh.daInfFeriasFuncionario(codigo,ano);

                //Clients.Caller.daInfFeriasFuncionario(infFerias);

            }
            catch
            {

            }
        }

        public void daListaDepartamentos(int tipoPlataforma, string codEmpresa, string codUtilizador, 
            string password,string resticoes = "")
        {
            try
            {
                inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

                //List<Departamento> lista = m._rh.daListaDepartamentos(resticoes);

                //Clients.Caller.daListaDepartamentos(lista);

            }
            catch
            {

            }

        }

        public void fazMarcacaoFerias(int tipoPlataforma, string codEmpresa, string codUtilizador,
            string password, string funcionario_Codigo, short ano, DateTime dataFeria )
        {
            inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

            //Ferias_Itens feria = new Ferias_Itens()
            //{
            //    ano = ano,
            //    dataFeria = dataFeria,
            //    funcionario_Codigo = funcionario_Codigo
            //};

            //m._rh.fazMacacaoFerias(feria);
        }

        //public void fazMarcacaoFeriasColecao(int tipoPlataforma, string codEmpresa, string codUtilizador,
        //    string password, List<Ferias_Itens> listFerias)
        //{
        //    inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

        //    foreach (var feria in listFerias)
        //        m._rh.fazMacacaoFerias(feria);
        //}

        private void inicializa(int tipoPlataforma, string codEmpresa, string codUtilizador, string password)
        {
            //m._empresaErp.AbreEmpresaPrimavera(tipoPlataforma, codEmpresa, codUtilizador, password);
            //m.inicializaMotoresRH_EmpresaErp();
        }


    }
}
