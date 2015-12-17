using System.Windows.Forms;
using System.ServiceProcess;
using System.Security.Principal;
namespace WindowsFormsApplication1
{
    partial class SelfHostingServiceForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelfHostingServiceForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contexMenuuu_ItemClicked);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.CheckOnClick = true;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.toolStripMenuItem1.Text = "Exit";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(116, 0);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.Text = "Self Hosting Service";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public static bool runAsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();

            var principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }  

        public void updateIconByServiseStatus()
        {
            ServiceController sc = new ServiceController();

            sc.ServiceName = Properties.Resources.ResourceManager.GetString("ServiceName");

            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                changeSystemTrayIcon("stopped");
            }
            else if (sc.Status == ServiceControllerStatus.Running)
            {
                changeSystemTrayIcon("started");
            }
            else
            {
                changeSystemTrayIcon("peding");
            }
            
            if (!runAsAdministrator())
            {
                showBalloonTipText("Must run as Administratrator!!!");

                //Sleep enough to user see and understand the message
                System.Threading.Thread.Sleep(7000);

                this.Dispose();
            }
           
        }

        public void contexMenuuu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;

            ServiceController sc = new ServiceController();

            sc.ServiceName = Properties.Resources.ResourceManager.GetString("ServiceName");

            if (item.Text == "Start" && sc.Status == ServiceControllerStatus.Stopped)
            {
                tryStartService(sc);
  
            }
            else if (item.Text == "Stop" && sc.Status == ServiceControllerStatus.Running)
            {
                tryStopService(sc);
            }

            else if (item.Text == "Exit")
            {
                tryExitApp(sc);
            }
        }

        private void tryExitApp(ServiceController sc)
        {
            try
            {
                // Stop the service, and wait until its status is "Running".
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    stopService(sc);
                }

                //Exit app
                this.Dispose();
            }
            catch (System.InvalidOperationException)
            {
                changeSystemTrayIcon("peding");

                showBalloonTipText("Ocorreu um erro!");
            }
        }

        private void tryStopService(ServiceController sc)
        {
            try
            {
                // Stop the service, and wait until its status is "Running".
                stopService(sc);

                // Change System Tray Icon
                changeSystemTrayIcon("stopped");

                showBalloonTipText("Service Stopped successfully!");
            }
            catch (System.InvalidOperationException)
            {
                changeSystemTrayIcon("peding");
            }
        }

        private void tryStartService(ServiceController sc)
        {
            try
            {
                // Start the service
                startService(sc);

                //cahange App SystemTrayIcon
                changeSystemTrayIcon("started");

                showBalloonTipText("Self Hosting Service Running!");
            }
            catch (System.InvalidOperationException)
            {
                changeSystemTrayIcon("stopped");
            }
        }

        private void changeSystemTrayIcon(string iconName)
        {
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(Properties.Resources.ResourceManager.GetObject(iconName)));
        }

        private void showBalloonTipText(string message)
        {
            this.notifyIcon1.BalloonTipText = message;
            this.notifyIcon1.ShowBalloonTip(1000);
        }

        private static void startService(ServiceController sc)
        {
            sc.Start();
            sc.WaitForStatus(ServiceControllerStatus.Running);
        }

        private static void stopService(ServiceController sc)
        {
            sc.Stop();
            sc.WaitForStatus(ServiceControllerStatus.Stopped);
        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;



    }
}

