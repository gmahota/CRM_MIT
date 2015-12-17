using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MIT.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //var writer = Console.Out;
            //var client = new CommonClient(writer);
            //client.RunAsync("http://f:5000/").Wait();

            //Console.ReadKey();
            //CommonRH r = new CommonRH("http://ferias.mit.co.mz:5000/", "primaveraServer");
            CommonRH r = new CommonRH("http://localhost:5000/", "primaveraServer");

            try
            {
                while (1 == 1)
                {
                    
                    Console.ReadKey();
                }
                

                //IHubProxy stockTickerHubProxy = connection.CreateHubProxy("rhHub");
                //stockTickerHubProxy.On("UpdateStockPrice", stock => Console.WriteLine("Stock update for {0} new price {1}", stock.Symbol, stock.Price));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }

        private async Task RunDemo(string url)
        {
            //var hubConnection = new HubConnection(url);

            //var querystringData = new Dictionary<string, string>();
            //querystringData.Add("name", "primaveraServer");

            //var connection = new HubConnection(url, querystringData);

            ////hubConnection.TraceWriter = _traceWriter;

            //var hubProxy = hubConnection.CreateHubProxy("rhHub");
            //hubProxy.On<int>("invoke", (i) =>
            //{
            //    int n = hubProxy.GetValue<int>("index");
            //    hubConnection.TraceWriter.WriteLine("{0} client state index -> {1}", i, n);
            //});
            
            //await hubConnection.Start();

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



