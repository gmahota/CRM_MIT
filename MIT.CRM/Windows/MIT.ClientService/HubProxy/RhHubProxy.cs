using Microsoft.AspNet.SignalR.Client;
using MIT.Data.Model;
using MIT.MotoresPrimavera;
using System;
using System.Collections.Generic;
using System.IO;


namespace MIT.ClientService.HubProxy
{
    public class RhHubProxy :BaseHubProxy
    {
        private TextWriter _traceWriter;
        
        MotoresErp m = new MotoresErp();

        public RhHubProxy(string url,string serverName)
        {
            var querystringData = new Dictionary<string, string>();
            querystringData.Add("Name", serverName);
            querystringData.Add("Type", "Primavera Server");

            hubConnection = new HubConnection(url,querystringData);

            _HubProxy = hubConnection.CreateHubProxy("rhHub");

            _HubProxy.On<string,int,string,string>("daListaEmpresa", daListaEmpresas );

            _HubProxy.On<string>("userConnected", (Id) => Console.WriteLine("Ola " + Id));

            hubConnection.Start().Wait();

            Console.WriteLine("transport.Name={0}", hubConnection.Transport.Name);
            
            //hubConnection.TraceWriter.WriteLine("transport.Name={0}", hubConnection.Transport.Name);

            //hubConnection.TraceWriter.WriteLine("Invoking long running hub method with progress...");
            //var result = await hubProxy.Invoke<string, int>("ReportProgress",
            //    percent => hubConnection.TraceWriter.WriteLine("{0}% complete", percent),
            //    /* jobName */ "Long running job");
            //hubConnection.TraceWriter.WriteLine("{0}", result);

            //await hubProxy.Invoke("multipleCalls");
        }

        

    }


}
