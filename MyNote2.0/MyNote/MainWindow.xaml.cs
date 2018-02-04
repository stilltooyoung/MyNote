using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows;
using System.Runtime.InteropServices;
using System;
using System.Linq;
using Microsoft.Win32;
using System.Windows.Media.Animation;

namespace MyNote
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        #region 去系统菜单
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, int revert);

        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        private static extern int GetMenuItemCount(IntPtr hmenu);

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        private static extern int RemoveMenu(IntPtr hmenu, int npos, int wflags);

        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        private static extern int DrawMenuBar(IntPtr hwnd);

        private const int MF_BYPOSITION = 0x0400;
        private const int MF_DISABLED = 0x0002;

        public static void RemoveWindowSystemMenu(Window window)
        {
            if (window == null)
            {
                return;
            }

            window.SourceInitialized += window_SourceInitialized;

        }

        static void window_SourceInitialized(object sender, EventArgs e)
        {
            var window = (Window)sender;

            var helper = new WindowInteropHelper(window);
            IntPtr windowHandle = helper.Handle; //Get the handle of this window
            IntPtr hmenu = GetSystemMenu(windowHandle, 0);
            int cnt = GetMenuItemCount(hmenu);

            for (int i = cnt - 1; i >= 0; i--)
            {
                RemoveMenu(hmenu, i, MF_DISABLED | MF_BYPOSITION);
            }
        }

        public static void RemoveWindowSystemMenuItem(Window window, int itemIndex)
        {
            if (window == null)
            {
                return;
            }

            window.SourceInitialized += delegate
            {
                var helper = new WindowInteropHelper(window);
                IntPtr windowHandle = helper.Handle; //Get the handle of this window
                IntPtr hmenu = GetSystemMenu(windowHandle, 0);

                //remove the menu item
                RemoveMenu(hmenu, itemIndex, MF_DISABLED | MF_BYPOSITION);

                DrawMenuBar(windowHandle); //Redraw the menu bar
            };



        }


        #endregion

        #region 系统托盘

        private System.Windows.Forms.NotifyIcon trayIcon;


        private System.Windows.Forms.NotifyIcon InitTrayIcon()
        {
            var trayIcon = new System.Windows.Forms.NotifyIcon();
            trayIcon.BalloonTipText = "MyNote";
            trayIcon.Text = "MyNote";
            trayIcon.Visible = false;
            string s = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "dreamico.ico";
            trayIcon.Icon = new System.Drawing.Icon(s);//读取程序图标，来作为托盘图标


            var conntextMenu = new System.Windows.Forms.ContextMenuStrip();

            var Shield = new System.Windows.Forms.ToolStripMenuItem("开启免打扰");
            Shield.Click += (object sender, EventArgs e) =>
            {
                DoShield();
            };
            conntextMenu.Items.Add(Shield);

            var CloseShield = new System.Windows.Forms.ToolStripMenuItem("关闭免打扰");
            CloseShield.Click += (object sender, EventArgs e) =>
            {
                Recovery();
            };
            conntextMenu.Items.Add(CloseShield);


            var Clean = new System.Windows.Forms.ToolStripMenuItem("清理过期提醒");
            Clean.Click += (object sender, EventArgs e) =>
            {
                CleanAttentions();
            };
            conntextMenu.Items.Add(Clean);

            var SeeDiaries = new System.Windows.Forms.ToolStripMenuItem("查看日记");
            SeeDiaries.Click += (object sender, EventArgs e) =>
            {
                SeeDiaries_Click();
            };
            conntextMenu.Items.Add(SeeDiaries);



            var powerboot = new System.Windows.Forms.ToolStripMenuItem("开机自启");


            if (IsPowerboot())
            {
                powerboot.CheckState = System.Windows.Forms.CheckState.Checked;
            }
            else
            {
                powerboot.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }

            powerboot.CheckOnClick = true;

            powerboot.Click += (object sender, EventArgs e) =>
            {
                if (powerboot.CheckState == System.Windows.Forms.CheckState.Checked)
                    open_Checked();
                else
                    close_Checked();
            };

            conntextMenu.Items.Add(powerboot);


            conntextMenu.ShowCheckMargin = true;
            var tsp = new System.Windows.Forms.ToolStripSeparator();
            conntextMenu.Items.Add(tsp);

            var exit = new System.Windows.Forms.ToolStripMenuItem("退出");
            exit.Click += (object sender, EventArgs e) =>
            {
                trayIcon.Dispose();
                sd.Close();
                Warn.Close();
                Close();
                w.Close();
             
                Application.Current.Shutdown();
                Environment.Exit(0);
            };
            conntextMenu.Items.Add(exit);

            trayIcon.ContextMenuStrip = conntextMenu;

            trayIcon.Visible = true;

            return trayIcon;
        }

        private void DoShield()
        {
            try
            {
                Warn.Close();
                nw.Visibility = Visibility.Hidden;
                dream.Visibility = Visibility.Hidden;
            }
            catch { }
        }

        private void Recovery()
        {
            try
            {
                Warn.Close();
            }
            catch { }
            Warn = new Warning();
            dream.Visibility = Visibility.Visible;
        }

        private void SeeDiaries_Click()
        {
            sd.Visibility = Visibility.Visible;    
        }

        private void CleanAttentions()
        {
            var outs = db.Attentions.Where(x=>x.Deadline<DateTime.Now&&x.State==true);
            db.Attentions.RemoveRange(outs);
            db.SaveChanges();
            nw.Hide();
            nw.Show();
            
        }

         //查询是否开机自启
        public bool IsPowerboot()
        {
            //获取程序执行路径..
            string starupPath = System.Windows.Forms.Application.ExecutablePath;
            //class Micosoft.Win32.RegistryKey. 表示Window注册表中项级节点,此类是注册表.
            RegistryKey loca = Registry.LocalMachine;
            RegistryKey run = loca.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            if (run == null)
            {
                return false;
            }
            try
            {
                string check = run.GetValue("MyNote", -1).ToString();
                loca.Close();
                if (check.Equals(starupPath))
                {
                    return true;  
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }

        }


        //开启开机自启
        public void open_Checked()
        {
            //获取程序执行路径..
            string starupPath = System.Windows.Forms.Application.ExecutablePath;
            //class Micosoft.Win32.RegistryKey. 表示Window注册表中项级节点,此类是注册表.
            RegistryKey loca = Registry.LocalMachine;
            RegistryKey run = loca.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (run == null)
            {
                run = loca.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            }
            try
            {
                //SetValue:存储值的名称
                run.SetValue("MyNote", starupPath); //写入子项
                                                    //run.DeleteValue("AppAutoRun", false);//取消开机启动
                loca.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误消息为：" + ex.Message);
            }

        }
        //关闭开机自启
        public void close_Checked()
        {

            //获取程序执行路径..
            string starupPath = System.Windows.Forms.Application.ExecutablePath;
            //class Micosoft.Win32.RegistryKey. 表示Window注册表中项级节点,此类是注册表.
            RegistryKey loca = Registry.LocalMachine;
            RegistryKey run = loca.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            try
            {

                run.DeleteValue("MyNote");
                loca.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误消息为：" + ex.Message);
            }
        }





        #endregion
        ModelNotes db = new ModelNotes();
        Window w = new Window();
        Warning Warn = new Warning();
        

        NewWindow nw = new NewWindow();

        ShowDiaries sd = new ShowDiaries();
        public MainWindow()
        {
            InitializeComponent();

        


            //将该窗口设置为工具窗口的子窗口
             // Create helper window

            w.ShowInTaskbar = false;
            
            w.WindowStyle = WindowStyle.ToolWindow; // Set window style as ToolWindow to avoid its icon in AltTab 
            
            w.Show(); // We need to show window before set is as owner to our main window
            
            Owner = w; // Okey, this will result to disappear icon for main window.

            w.Hide(); // Hide helper window just in case

            //隐藏任务栏
            this.Title = string.Empty;
            ShowInTaskbar = false;

            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = true;

            //窗口最大化
            WindowState = WindowState.Maximized;
            //最前
            Topmost = true;

            trayIcon = InitTrayIcon();
            
            Warn.WindowStyle = WindowStyle.None;
            Warn.Show();
            Warn.Owner = w;
            
            Warn.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            showAttention.Visibility = Visibility.Hidden;


          

            sd.WindowStyle = WindowStyle.None;

            sd.Visibility = Visibility.Hidden;
            Warn.Owner = w;

            

        }
        #region 事件处理
        private void dream_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                if (!nwReady)
                {
                    nw.Owner = this;
                    nw.Show();
                    nw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    //this.Visibility = Visibility.Hidden;
                    nwReady = true;

                    nw.Owner = this;
                    nw.Show();
                    nw.WindowStartupLocation = WindowStartupLocation.CenterScreen;


                    
                }
                else
                {
                    nw.Hide();
                    nwReady = false;

                    nw.Hide();
                   
                    
                }
            }
        }

        //MediaPlayer player = new MediaPlayer();

     
        private void dream_MouseEnter(object sender, MouseEventArgs e)
        {



            //player.Open(new Uri("music.mp3", UriKind.RelativeOrAbsolute));
            //player.Volume = 0.5;
            //player.Play();

            DoubleAnimation daV = new DoubleAnimation(0, 0.7, new Duration(TimeSpan.FromSeconds(1)));
            showAttention.BeginAnimation(UIElement.OpacityProperty, daV);

            showAttention.Items.Clear();
            var sa = db.Attentions.Where(y => y.State != true).OrderBy(z => z.Deadline);
            if (sa.Count()==0)
            {
                ListBoxItem nothing = new ListBoxItem();
                nothing.FontFamily = new System.Windows.Media.FontFamily("Verdana");
                nothing.FontSize = 12;
                nothing.FontWeight = FontWeights.Bold;
                
                TextBlock t = new TextBlock();
                t.Foreground = System.Windows.Media.Brushes.Green;
                t.Width = 200;
                t.TextAlignment = TextAlignment.Center;
                t.Text = "貌似没有待办耶，快去创建吧！";

                nothing.Content = t;
                showAttention.Items.Add(nothing);

            }
            else
            {
                ListBoxItem head = new ListBoxItem();
                head.FontFamily = new System.Windows.Media.FontFamily("Verdana");
                    head.FontSize = 12;
                head.FontWeight =FontWeights.Bold;
                StackPanel hsp = new StackPanel();
                hsp.VerticalAlignment = VerticalAlignment.Top;
                hsp.HorizontalAlignment = HorizontalAlignment.Center;
                hsp.Background = System.Windows.Media.Brushes.Black;
                hsp.Width = 278;
                hsp.Orientation = Orientation.Horizontal;
                hsp.Height = 22.583;
                TextBlock ho = new TextBlock();
                ho.Foreground = System.Windows.Media.Brushes.Green;
                ho.Width = 25;
                ho.TextAlignment = TextAlignment.Center;
                ho.Text = "序号";

                TextBlock hc = new TextBlock();
                hc.Foreground = System.Windows.Media.Brushes.Green;
                hc.Width = 145;
                hc.TextAlignment = TextAlignment.Center;
                hc.Text = "待办";

                TextBlock hd = new TextBlock();
                hd.Foreground = System.Windows.Media.Brushes.Green;
                hd.Width = 70;
                hd.TextAlignment = TextAlignment.Center;
                hd.Text = "截止时间";

                TextBlock hs = new TextBlock();
                hs.Foreground = System.Windows.Media.Brushes.Green;
                hs.Width = 40;
                hs.TextAlignment = TextAlignment.Center;
                hs.Text = "K.O.";

                hsp.Children.Add(ho);
                hsp.Children.Add(hc);
                hsp.Children.Add(hd);
                hsp.Children.Add(hs);
                head.Content = hsp;
                showAttention.Items.Add(head);
                int i = 0;
                foreach (var item in sa)
                {
                    i++;
                    ListBoxItem lbi = new ListBoxItem();
                    lbi.FontFamily = new System.Windows.Media.FontFamily("Verdana");
                    lbi.FontSize = 6.667;
                    lbi.FontWeight = FontWeights.Bold;
                    lbi.Height = 13.583;

                    StackPanel sp = new StackPanel();
                    sp.VerticalAlignment = VerticalAlignment.Top;
                    sp.HorizontalAlignment = HorizontalAlignment.Center;
                    sp.Background = Brushes.White;
                    sp.Width = 278;
                    sp.Orientation = Orientation.Horizontal;
                    sp.Height = 22.583;

                    TextBlock order = new TextBlock();
                    order.Text = i.ToString();
                    order.Foreground = System.Windows.Media.Brushes.Green;
                    order.Width = 25;
                    order.FontSize = 6.667;
                    order.TextAlignment = TextAlignment.Center;

                    TextBlock content = new TextBlock();
                    content.Text = item.Content;
                    content.Foreground = System.Windows.Media.Brushes.Green;
                    content.Width = 145;
                    content.TextAlignment = TextAlignment.Center;
                    content.FontSize = 6.667;
                    TextBlock dl = new TextBlock();
                    dl.Text = item.Deadline.ToString();
                    dl.Foreground = System.Windows.Media.Brushes.Green;
                    dl.Width = 90;
                    dl.TextAlignment = TextAlignment.Center;
                    dl.FontSize = 6.667;
                    CheckBox st = new CheckBox();
                    st.Name = "attention" + item.Id.ToString();
                    st.HorizontalContentAlignment = HorizontalAlignment.Center;
                    st.VerticalAlignment = VerticalAlignment.Top;
                    st.Foreground = System.Windows.Media.Brushes.Green;
                    st.MaxHeight = 10;
                    st.MaxWidth = 15;
                    

                    st.Checked += new RoutedEventHandler(st_Checked);

                    sp.Children.Add(order);
                    sp.Children.Add(content);
                    sp.Children.Add(dl);
                    sp.Children.Add(st);

                    lbi.Content = sp;
                    showAttention.Items.Add(lbi);
                }
            }
            showAttention.Visibility = Visibility.Visible;

        }

        bool nwReady = false;
        private void st_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int i = Convert.ToInt32(cb.Name.Substring(9));
            var d = db.Attentions.SingleOrDefault(x=>x.Id==i);
            if (d != null)
            {
                d.State = true;
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("未找到事件！");
            }
            
        }

        private void dream_MouseLeave(object sender, MouseEventArgs e)
        {

            if (ToHide)
            {
               
                showAttention.Visibility = Visibility.Hidden;

            }
            
        }

        bool ToHide = false;
        
        private void showAttention_MouseEnter(object sender, MouseEventArgs e)
        {
            ToHide = false;
            showAttention.Visibility = Visibility.Visible;
        }

        private void showAttention_MouseLeave(object sender, MouseEventArgs e)
        {
            ToHide = true;
            showAttention.Visibility = Visibility.Hidden;
        }
        #endregion
    }
}