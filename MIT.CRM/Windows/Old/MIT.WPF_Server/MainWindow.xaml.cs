using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Host.HttpListener;
using Microsoft.Owin;
using Owin;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Cors;
using MIT.MotoresPrimavera;
using System.Collections.Generic;
using MIT.Data.Model;
using Microsoft.AspNet.SignalR.Hubs;

namespace MIT.WPF_Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IDisposable SignalR { get; set; }
        string ServerURI = "http://localhost:8989";
        MotoresErp m = new MotoresErp();
        private void inicializa(int tipoPlataforma, string codEmpresa, string codUtilizador, string password)
        {
            m._empresaErp.AbreEmpresaPrimavera(tipoPlataforma, codEmpresa, codUtilizador, password);
            m.inicializaMotores_EmpresaErp();
        }

        public void daExtratoPDFCliente(int tipoPlataforma, string codEmpresa, string codUtilizador, string password, string cliente)
        {

            try
            {
                inicializa(tipoPlataforma, codEmpresa, codUtilizador, password);

                //string caminhoPdf = m._comercial.DocFacturaToPDF(cliente, "2015", "FA", 1);
                string caminhoPdf = m._comercial.DocExtactoContasToPDF(cliente);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public MainWindow()
        {
            InitializeComponent();
            
            daExtratoPDFCliente(0, "acc", "gmahota", "Accsys2011!","1004");
        }

        /// <summary>
        /// Calls the StartServer method with Task.Run to not
        /// block the UI thread. 
        /// </summary>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            WriteToConsole("Starting server...");
            ButtonStart.IsEnabled = false;
            Task.Run(() => StartServer());
        }

        /// <summary>
        /// Stops the server and closes the form. Restart functionality omitted
        /// for clarity.
        /// </summary>
        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            SignalR.Dispose();
            Close();
        }

        /// <summary>
        /// Starts the server and checks for error thrown when another server is already 
        /// running. This method is called asynchronously from Button_Start.
        /// </summary>
        private void StartServer()
        {
            try
            {
                
                ServerURI = @"http://+:8989/";
                SignalR =  WebApp.Start  (ServerURI);
            }
            catch (TargetInvocationException )
            {
                WriteToConsole("A server is already running at " + ServerURI   );
                this.Dispatcher.Invoke(() => ButtonStart.IsEnabled = true);
                return;
            }
            catch(Exception e)
            {
                WriteToConsole("Error" + e.Message);
                this.Dispatcher.Invoke(() => ButtonStart.IsEnabled = true);
                return;
            }
            this.Dispatcher.Invoke(() => ButtonStop.IsEnabled = true);
            WriteToConsole("Server started at " + ServerURI);
        }

        ///This method adds a line to the RichTextBoxConsole control, using Dispatcher.Invoke if used
        /// from a SignalR hub thread rather than the UI thread.
        public void WriteToConsole(String message)
        {
            if (!(RichTextBoxConsole.CheckAccess()))
            {
                this.Dispatcher.Invoke(() =>
                    WriteToConsole(message)
                );
                return;
            }
            RichTextBoxConsole.AppendText(message + "\r");
        }

    }

    /// <summary>
    /// Used by OWIN's startup process. 
    /// </summary>
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }

    
}
