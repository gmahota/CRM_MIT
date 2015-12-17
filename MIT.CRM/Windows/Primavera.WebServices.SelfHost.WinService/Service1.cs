using Primavera.WebServices.Services;
using Primavera.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Primavera.WebServices.SelfHost.WinService
{
    public partial class Service1 : ServiceBase
    {
        ServiceHost host;

        private static string sourceName = "Self Host Service";

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            host = new ServiceHost(typeof(Primavera_Service));

            var behavior = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            behavior.IncludeExceptionDetailInFaults = true;

            host.Open();

            PrimaveraWSLogger.escreveInformacao("Service Started", sourceName);
        }


        protected override void OnStop()
        {
            try
            {
                host.Close();

                PrimaveraWSLogger.escreveInformacao("Service Stopped",sourceName);
            }
            catch(Exception e) {
                PrimaveraWSLogger.escreveErro("Error ocorred on starting Service Started :" + e.Message, sourceName);
            }
        }

        /// <summary>
        /// Método para resolução das assemblies.
        /// </summary>
        /// <param name="sender">Application</param>
        /// <param name="args">Resolving Assembly Name</param>
        /// <returns>Assembly</returns>
        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyFullName;
            System.Reflection.AssemblyName assemblyName;
            const string PRIMAVERA_COMMON_FILES_FOLDER = "PRIMAVERA\\SG800"; //pasta dos ficheiros comuns especifica da versão do ERP PRIMAVERA utilizada.
            assemblyName = new System.Reflection.AssemblyName(args.Name);
            assemblyFullName = System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86), PRIMAVERA_COMMON_FILES_FOLDER), assemblyName.Name + ".dll");
            if (System.IO.File.Exists(assemblyFullName))
                return System.Reflection.Assembly.LoadFile(assemblyFullName);
            else
                return null;
        }
    }
}
