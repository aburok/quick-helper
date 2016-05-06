using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MahApps.Metro.Controls;
using NHotkey;
using NHotkey.Wpf;
using QuickHelper.ViewModels;
using Application = System.Windows.Application;
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

        protected MainViewModel ViewModel
        {
            get { return this.DataContext as MainViewModel; }
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (string.IsNullOrWhiteSpace(this.ViewModel.FilterText))
                {
                    this.Hide();
                }
                else
                {
                    this.ViewModel.ClearFilter();
                }
            }
        }

        private NotifyIcon _notifyIcon;

        private void InitTrayIcon()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = new Icon(SystemIcons.Question, 40, 40);
            _notifyIcon.Visible = true;
            _notifyIcon.DoubleClick += (sender, args) =>
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
            _notifyIcon.MouseClick += (sender, args) =>
            {
                if (args.Button == MouseButtons.Right)
                {
                    var menu = this.FindResource("TrayContextMenu") as System.Windows.Controls.ContextMenu;
                    menu.IsOpen = true;
                }
            };
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        protected void Menu_Exit(object sender, RoutedEventArgs e)
        {
            _notifyIcon.Visible = false;
            Application.Current.Shutdown();
        }
    }
}
