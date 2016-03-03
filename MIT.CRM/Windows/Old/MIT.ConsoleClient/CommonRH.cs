using Microsoft.AspNet.SignalR.Client;
using MIT.Data.Model;
using MIT.MotoresPrimavera;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MIT.ConsoleClient
{
    public class CommonRH 
    {
        private TextWriter _traceWriter;

        HubConnection hubConnection;
        IHubProxy rhHubProxy;


        MotoresErp m = new MotoresErp();

        public CommonRH(string url,string serverName)
        {
            var querystringData = new Dictionary<string, string>();
            querystringData.Add("name", serverName);

            hubConnection = new HubConnection(url,querystringData);

            rhHubProxy = hubConnection.CreateHubProxy("rhHub");
            

            rhHubProxy.On<int,string,string,string>("daListaEmpresa", (tipoPlataforma, codUtilizador, password,categoria) =>
                daListaEmpresas(tipoPlataforma, codUtilizador, password, categoria)
             );
            

            hubConnection.Start().Wait();
            Console.WriteLine("transport.Name={0}", hubConnection.Transport.Name);
            rhHubProxy.Invoke("message", "ola");
            //hubConnection.TraceWriter.WriteLine("transport.Name={0}", hubConnection.Transport.Name);

            //hubConnection.TraceWriter.WriteLine("Invoking long running hub method with progress...");
            //var result = await hubProxy.Invoke<string, int>("ReportProgress",
            //    percent => hubConnection.TraceWriter.WriteLine("{0}% complete", percent),
            //    /* jobName */ "Long running job");
            //hubConnection.TraceWriter.WriteLine("{0}", result);

            //await hubProxy.Invoke("multipleCalls");
        }

        private void messageMethod(IList<Newtonsoft.Json.Linq.JToken> obj)
        {
            throw new NotImplementedException();
        }

        //private void inicializa(int tipoPlataforma, string codEmpresa, string codUtilizador, string password)
        //{
        //    m._empresaErp.AbreEmpresaPrimavera(tipoPlataforma, codEmpresa, codUtilizador, password);
        //    m.inicializaMotoresRH_EmpresaErp();
        //}

        public void daListaEmpresas(int tipoPlataforma, string codUtilizador, string password, string categoria = "")
        {
            try
            {
                rhHubProxy.Invoke("message", "ola");
                //inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);
                m._administradorErp = new MotoresPrimavera.Parametros.AdministradorErp(codUtilizador, password);
                List<Empresa> listEntidades = m._administradorErp.listaEmpresas(categoria);

                rhHubProxy.Invoke("ListaEmpresas", listEntidades);
                
                

                //hubConnection.Send(listEntidades);
                //Clients.Caller.daListaEmpresas(listEntidades);
                //return listEntidades;
            }
            catch (Exception e)
            {
                //return new List<Empresa>();

                //List<Empresa> listEmpresas = new List<Empresa>();
                //listEmpresas.Add(new Empresa() { codigo = "Demo" });

                //Clients.Caller.daListaEmpresas(listEmpresas);
                //return listEmpresas;
            }

        }

    }


}
