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
    /// Sincronização com o modulo de RH
    /// </summary>
    [HubName("rhHub")]
    public class RhHub : Hub
    {
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

        public void daListaFuncionarios(int tipoPlataforma, string codEmpresa, string codUtilizador, 
            string password,string resticoes="")
        {
            try
            {
                inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

                List<Funcionario> listaFuncionarios = m._rh.daListaFuncionarios(resticoes);

                Clients.Caller.daListaFuncionarios(listaFuncionarios);

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

                FuncInfFerias infFerias = m._rh.daInfFeriasFuncionario(codigo,ano);

                Clients.Caller.daInfFeriasFuncionario(infFerias);

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

                List<Departamento> lista = m._rh.daListaDepartamentos(resticoes);

                Clients.Caller.daListaDepartamentos(lista);

            }
            catch
            {

            }

        }

        public void fazMarcacaoFerias(int tipoPlataforma, string codEmpresa, string codUtilizador,
            string password, string funcionario_Codigo, short ano, DateTime dataFeria )
        {
            inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

            Ferias_Itens feria = new Ferias_Itens()
            {
                ano = ano,
                dataFeria = dataFeria,
                funcionario_Codigo = funcionario_Codigo
            };

            m._rh.fazMacacaoFerias(feria);
        }

        public void fazMarcacaoFeriasColecao(int tipoPlataforma, string codEmpresa, string codUtilizador,
            string password, List<Ferias_Itens> listFerias)
        {
            inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

            foreach (var feria in listFerias)
                m._rh.fazMacacaoFerias(feria);
        }

        private void inicializa(int tipoPlataforma, string codEmpresa, string codUtilizador, string password)
        {
            m._empresaErp.AbreEmpresaPrimavera(tipoPlataforma, codEmpresa, codUtilizador, password);
            m.inicializaMotoresRH_EmpresaErp();
        }


    }
}
