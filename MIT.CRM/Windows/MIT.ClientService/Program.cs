using Microsoft.AspNet.SignalR.Client;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocket4Net;

namespace MIT.ClientService
{
    class Program
    {
        static WebSocket websocket;

        static void Main(string[] args)
        {
            //Set connection
            var _hubConnection = new HubConnection("http://localhost:5000/");

            _hubConnection.Received += _hubConnection_Received;
            _hubConnection.Reconnected += _hubConnection_Reconnected;
            _hubConnection.Reconnecting += _hubConnection_Reconnecting;
            _hubConnection.StateChanged += _hubConnection_StateChanged;
            _hubConnection.Error += _hubConnection_Error;
            _hubConnection.ConnectionSlow += _hubConnection_ConnectionSlow;
            _hubConnection.Closed += _hubConnection_Closed;

            //Make proxy to hub based on hub name on server
            var myHub = _hubConnection.CreateHubProxy("chat");

            myHub.On<string>("userConnected", (Id) => Console.WriteLine("Ola " + Id));

            

            //Start connection
            _hubConnection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();

            //connection.StateChanged += connection_StateChanged;



            //myHub.Invoke("Send", "HELLO World ").ContinueWith(task => {
            //    if (task.IsFaulted)
            //    {
            //        Console.WriteLine("There was an error calling send: {0}", task.Exception.GetBaseException());
            //    }
            //    else
            //    {
            //        Console.WriteLine("Send Complete.");
            //    }
            //});

            Console.ReadKey(true);


            //WebSocket websocket = new WebSocket("ws://localhost:2012/");
            //websocket.Opened += new EventHandler(websocket_Opened);
            //websocket.Error += new EventHandler<ErrorEventArgs>(websocket_Error);
            //websocket.Closed += new EventHandler(websocket_Closed);
            //websocket.MessageReceived += new EventHandler(websocket_MessageReceived);
            //websocket.Open();
        }

        private static void _hubConnection_Closed()
        {
            
        }

        private static void _hubConnection_ConnectionSlow()
        {
            
        }

        private static void _hubConnection_Error(Exception obj)
        {
            
        }

        private static void _hubConnection_StateChanged(StateChange obj)
        {
            
        }

        private static void _hubConnection_Reconnecting()
        {
            
        }

        private static void _hubConnection_Reconnected()
        {
            
        }

        private static void _hubConnection_Received(string obj)
        {
            
        }

        //private static void websocket_Closed(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private static void websocket_Error(object sender, ErrorEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private static void websocket_Opened(object sender, EventArgs e)
        //{
        //    websocket.Send("Hello World!");
        //}
    }
}
