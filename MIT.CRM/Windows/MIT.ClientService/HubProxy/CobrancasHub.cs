using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MIT.Data.Model;
using MIT.MotoresPrimavera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MIT.WPF_Server.Model
{
    /// <summary>
    /// Hub para Sincronização com o modulo de cobranças
    /// </summary>
    [HubName("cobrancasHub")]
    public class CobrancasHub : Hub
    {
        MotoresErp m = new MotoresErp();


        public override Task OnConnected()
        {
            //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client connected: " + Context.ConnectionId));

            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            //Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client disconnected: " + Context.ConnectionId));

            return base.OnDisconnected(stopCalled);
        }

        public void daListaEmpresas(int tipoPlataforma, string codUtilizador, string password, string categoria = "")
        {
            try
            {
                //inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);
                m._administradorErp = new MotoresPrimavera.Parametros.AdministradorErp(codUtilizador, password);
                List<Empresa> listEntidades = m._administradorErp.listaEmpresas(categoria);

                Clients.Caller.daListaEmpresas(listEntidades);
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

        public void daDadosEmpresa(int tipoPlataforma, string codEmpresa, string codUtilizador, string password)
        {
            try
            {
                //inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);
                m._administradorErp = new MotoresPrimavera.Parametros.AdministradorErp(codUtilizador, password);
                List<Empresa> listEmpresas = m._administradorErp.listaEmpresas();
                Empresa temp = new Empresa();
                foreach (var emp in listEmpresas)
                {
                    if (emp.codigo == codEmpresa)
                    {
                        temp = emp;


                    }
                }
                Clients.Caller.daDadosEmpresa(temp);

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

        public void daListaClientes(int tipoPlataforma, string codEmpresa, string codUtilizador, string password)
        {
            try
            {
                inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

                List<Entidade> listEntidades = m._comercial.daListaClientes();

                Clients.Caller.daListaClientes(listEntidades);

            }
            catch
            {

            }

        }

        public void daExtratoPDFCliente(int tipoPlataforma, string codEmpresa, string codUtilizador, string password, string cliente)
        {

            try
            {
                inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

                //string caminhoPdf = m._comercial.DocExtactoContasToPDF(cliente);

                //string caminhoPdf = m._comercial.imprimirPdf();
                string email_to = "";
                string email_cc = "";

                var contactos = m._comercial.daContactosEntidade("C", cliente);
                foreach(var cont in contactos)
                {
                    email_to += cont.Email +" ; ";
                    email_cc += cont.EmailAssist + " ; ";
                }

                var report = new Report()
                {
                    caminho = m._comercial.DocExtactoContasToPDF(cliente),
                    empresa = codEmpresa,
                    tipoEntidade = "C",
                    entidade = cliente,
                    to = email_to,
                    cc = email_cc
                };


                Clients.Caller.daExtratoPDFCliente(report);

            }
            catch (Exception e)
            {
                Clients.Caller.daExtratoPDFCliente("Erro do tipo: " + e.Message);
            }

        }

        
        public void daEntidade(int tipoPlataforma, string codEmpresa, string codUtilizador, string password, string tipoEntidade, string entidade)
        {
            inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

            Entidade ent = m._comercial.daCliente(entidade);
            Clients.Caller.daEntidade(ent);

        }

        public void daPendentesCliente(int tipoPlataforma, string codEmpresa, string codUtilizador, string password, string tipoEntidade, string entidade)
        {
            inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

            List<Pendente> listPendentes = m._comercial.ConsultaConta(entidade);
            Clients.Caller.daPendentesCliente(listPendentes);
            //Clients.Caller.daPendentesCliente(listPendentes);
        }

        private void inicializa(int tipoPlataforma, string codEmpresa, string codUtilizador, string password)
        {
            m._empresaErp.AbreEmpresaPrimavera(tipoPlataforma, codEmpresa, codUtilizador, password);
            m.inicializaMotores_EmpresaErp();
        }
    }
}
