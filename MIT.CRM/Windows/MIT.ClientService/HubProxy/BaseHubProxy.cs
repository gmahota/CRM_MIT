using Microsoft.AspNet.SignalR.Client;
using MIT.Data.Model;
using MIT.MotoresPrimavera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIT.ClientService.HubProxy
{
    public class BaseHubProxy:IDisposable
    {
        public HubConnection hubConnection { get; set; }
        public IHubProxy _HubProxy { get; set; }
        public MotoresErp m { get; set; }

        public BaseHubProxy()
        {
            m = new MotoresErp();

        }

        public BaseHubProxy(string url, string serverName)
        {
            m = new MotoresErp();

        }

        public void daListaEmpresas(string connectionId,int tipoPlataforma, string codUtilizador, string password)
        {
            try
            {
                m._administradorErp = new MotoresPrimavera.Parametros.AdministradorErp(codUtilizador, password);
                List<Empresa> listEntidades = m._administradorErp.listaEmpresas("");

                _HubProxy.Invoke("listaEmpresas", connectionId, listEntidades);

            }
            catch (Exception e)
            {
                _HubProxy.Invoke("Erro", "Ocorreu um erro durante a pesquisas das empresas no primavera");

            }
        }

        public void Dispose()
        {
            m = null;
            
        }
    }
}
