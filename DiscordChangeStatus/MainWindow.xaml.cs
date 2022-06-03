using DiscordRPC;
using DiscordRPC.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Button = DiscordRPC.Button;
using WinForms = System.Windows.Forms;
using System.Windows.Forms;

namespace DiscordChangeStatus
{
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon m_notifyIcon;
        public MainWindow()
        {
            InitializeComponent();
            m_notifyIcon = new System.Windows.Forms.NotifyIcon();
            m_notifyIcon.Text = "RPC by LilGreen";
            //var icon = System.Drawing.Icon.ExtractAssociatedIcon(@".\Resources\DiscordIcon.ico");
            var icon = Properties.Resources.DiscordIcon;
            m_notifyIcon.Icon = icon;
            m_notifyIcon.Click += new EventHandler(m_notifyIcon_Click);
        }

        private WindowState m_storedWindowState = WindowState.Normal;
        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = m_storedWindowState;
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_notifyIcon.Dispose();
            m_notifyIcon = null;
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();
            else
                m_storedWindowState = WindowState;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CheckTrayIcon();
        }

        void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        void ShowTrayIcon(bool show)
        {
            if (m_notifyIcon != null)
                m_notifyIcon.Visible = show;
        }
        //------------------------------------------------------
        public DiscordRpcClient client;
        RichPresence RichPresence = new RichPresence();
        bool initiliaze = false;
        public Timestamps time = Timestamps.Now;

        private void InitializeClient()
        {
            initiliaze = true;
            client = new DiscordRpcClient(ApplicationId.Text);
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            client.Initialize();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        private void ButtonInitialize_Click(object sender, RoutedEventArgs e)
        {
            InitializeClient();
        }

        private void ButtonUpgrade_Click(object sender, RoutedEventArgs e)
        {
            if (initiliaze == false)
            {
                initiliaze = false;
            }
            else if (CheckBoxButton1.IsChecked == true && UrlButton1.Text.Length > 2 && LabelButton1.Text.Length > 2 && CheckBoxButton2.IsChecked == true && UrlButton2.Text.Length > 2 && LabelButton2.Text.Length > 2)
            {
                client.SetPresence(new RichPresence()
                {
                    Details = $"{Detailtext.Text}",
                    State = $"{Statetext.Text}",
                    Timestamps = time,
                    Buttons = new Button[]
                    {
                    new Button() { Label = LabelButton1.Text, Url = UrlButton1.Text },
                    new Button() { Label = LabelButton2.Text, Url = UrlButton2.Text },
                    },
                    Assets = new Assets()
                    {
                        LargeImageKey = $"{LargeImgKey.Text}",
                        LargeImageText = $"{LargeImgText.Text}",
                        SmallImageKey = $"{SmallImgKey.Text}",
                        SmallImageText = $"{SmallImgText.Text}",
                    },
                });
            }
            else if (CheckBoxButton1.IsChecked == true && UrlButton1.Text.Length > 2 && LabelButton1.Text.Length > 2)
            {
                client.SetPresence(new RichPresence()
                {
                    Details = $"{Detailtext.Text}",
                    State = $"{Statetext.Text}",
                    Timestamps = time,
                    Buttons = new Button[]
                    {
                    new Button() { Label = LabelButton1.Text, Url = UrlButton1.Text },
                    },
                    Assets = new Assets()
                    {
                        LargeImageKey = $"{LargeImgKey.Text}",
                        LargeImageText = $"{LargeImgText.Text}",
                        SmallImageKey = $"{SmallImgKey.Text}",
                        SmallImageText = $"{SmallImgText.Text}",
                    },
                });
            }
            else if (CheckBoxButton2.IsChecked == true && UrlButton2.Text.Length > 2 && LabelButton2.Text.Length > 2)
            {
                client.SetPresence(new RichPresence()
                {
                    Details = $"{Detailtext.Text}",
                    State = $"{Statetext.Text}",
                    Timestamps = time,
                    Buttons = new Button[]
                    {
                    new Button() { Label = LabelButton2.Text, Url = UrlButton2.Text },
                    },
                    Assets = new Assets()
                    {
                        LargeImageKey = $"{LargeImgKey.Text}",
                        LargeImageText = $"{LargeImgText.Text}",
                        SmallImageKey = $"{SmallImgKey.Text}",
                        SmallImageText = $"{SmallImgText.Text}",
                    },
                });
            }
            else
            {
                client.SetPresence(new RichPresence()
                {
                    Details = $"{Detailtext.Text}",
                    State = $"{Statetext.Text}",
                    Timestamps = time,
                    Assets = new Assets()
                    {
                        LargeImageKey = $"{LargeImgKey.Text}",
                        LargeImageText = $"{LargeImgText.Text}",
                        SmallImageKey = $"{SmallImgKey.Text}",
                        SmallImageText = $"{SmallImgText.Text}",
                    },
                });
            }
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Detailtext.Text = Properties.Settings.Default.Detail;
            Statetext.Text = Properties.Settings.Default.State;
            LargeImgKey.Text = Properties.Settings.Default.LargeImageKey;
            SmallImgKey.Text = Properties.Settings.Default.SmallImageKey;
            LargeImgText.Text = Properties.Settings.Default.LargeImageText;
            SmallImgText.Text = Properties.Settings.Default.SmallImageText;
            ApplicationId.Text = Properties.Settings.Default.ApplicationId;
            LabelButton1.Text = Properties.Settings.Default.Button1Label;
            LabelButton2.Text = Properties.Settings.Default.Button2Label;
            UrlButton1.Text = Properties.Settings.Default.Button1Url;
            UrlButton2.Text = Properties.Settings.Default.Button2Url;
        }

        private void MyWindow_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Detail = Detailtext.Text;
            Properties.Settings.Default.State = Statetext.Text;
            Properties.Settings.Default.LargeImageKey = LargeImgKey.Text;
            Properties.Settings.Default.SmallImageKey = SmallImgKey.Text;
            Properties.Settings.Default.LargeImageText = LargeImgText.Text;
            Properties.Settings.Default.SmallImageText = SmallImgText.Text;
            Properties.Settings.Default.ApplicationId = ApplicationId.Text;
            Properties.Settings.Default.Button1Label = LabelButton1.Text;
            Properties.Settings.Default.Button2Label = LabelButton2.Text;
            Properties.Settings.Default.Button1Url = UrlButton1.Text;
            Properties.Settings.Default.Button2Url = UrlButton2.Text;
            Properties.Settings.Default.Save();

        }
    }
}
