using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac.Core;
using Autofac.Extras.CommonServiceLocator;
using MahApps.Metro.Controls;
using Microsoft.Practices.ServiceLocation;
using NHotkey;
using NHotkey.Wpf;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace QuickHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
            this.InitTrayIcon();

            HotkeyManager.Current.AddOrReplace(
                "Bring to focus", 
                Key.OemQuestion,
                ModifierKeys.Control | ModifierKeys.Windows,
                OnBringToFocus);
        }

        public void OnBringToFocus(object sender, HotkeyEventArgs eventArgs)
        {
            if (!this.IsVisible)
            {
                this.Show();
            }

            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Maximized;
            }

            this.Activate();
            this.Topmost = true;  // important
            this.Topmost = false; // important
            this.Focus();         // important        
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Hide();
            }
        }

        private void InitTrayIcon()
        {
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon(SystemIcons.Question, 40, 40);
            //ni.Icon = new System.Drawing.Icon("..\\..\\Content\\icon.png");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if(WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }
    }
}
