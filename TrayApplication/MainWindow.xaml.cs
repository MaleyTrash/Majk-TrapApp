using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace TrayApplication
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DateTime TimeStart;
        public NotifyIcon icon = new NotifyIcon();
        public Timer timer = new Timer();
        public MainWindow()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            icon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(@"./megu_fvK_icon.ico");
            icon.DoubleClick += (s, args) => ShowMainWindow();
            icon.Visible = true;
            CreateContextMenu();
            TimeStart = DateTime.Now;
            timer.Tick += SetRuntimeText;
            timer.Interval = 1;
            timer.Start();
        }
        private void CreateContextMenu()
        {
            icon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            icon.ContextMenuStrip.Items.Add("App runTime").Click += (s, e) => ShowMainWindow();
            icon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }
        private void SetRuntimeText(object sender, EventArgs e)
        {
            Runtime.Content = "RunTime : " + (DateTime.Now - TimeStart).ToString();
        }
        private void ExitApplication()
        {
            System.Windows.Application.Current.Shutdown();
        }

        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;
            ShowInTaskbar = true;
            Visibility = Visibility.Hidden;
            icon.Visible = true;
        }
        private void ShowMainWindow()
        {
            if (this.IsVisible)
            {
                if (this.WindowState == WindowState.Minimized)
                {
                    this.WindowState = WindowState.Normal;
                }
                this.Activate();
            }
            else
            {
                this.Show();
            }
        }
    }
}
