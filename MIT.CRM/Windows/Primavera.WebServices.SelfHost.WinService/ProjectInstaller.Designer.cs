namespace Primavera.WebServices.SelfHost.WinService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.primaveraWebServiceSelfHostService = new System.ServiceProcess.ServiceProcessInstaller();
            this.primaveraWebServiceSelfHostServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // primaveraWebServiceSelfHostService
            // 
            this.primaveraWebServiceSelfHostService.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.primaveraWebServiceSelfHostService.Password = null;
            this.primaveraWebServiceSelfHostService.Username = null;
            // 
            // primaveraWebServiceSelfHostServiceInstaller
            // 
            this.primaveraWebServiceSelfHostServiceInstaller.DisplayName = "Primavera Self Host Service";
            this.primaveraWebServiceSelfHostServiceInstaller.ServiceName = "Primavera Self Host Service";
            this.primaveraWebServiceSelfHostServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.primaveraWebServiceSelfHostServiceInstaller_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.primaveraWebServiceSelfHostService,
            this.primaveraWebServiceSelfHostServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller primaveraWebServiceSelfHostService;
        private System.ServiceProcess.ServiceInstaller primaveraWebServiceSelfHostServiceInstaller;
    }
}