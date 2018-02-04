using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyNote
{
    /// <summary>
    /// Warning.xaml 的交互逻辑
    /// </summary>
    public partial class Warning : Window
    {
        ModelNotes db = new ModelNotes();
        private DispatcherTimer timer;

        public Warning()
        {
            InitializeComponent();
            //隐藏任务栏
            this.Title = string.Empty;
            ShowInTaskbar = false;

          
            this.AllowsTransparency = true;

            //窗口最大化
            WindowState = WindowState.Maximized;
            //最前
            Topmost = true;

            warnShow.Visibility = Visibility.Hidden;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        
        //时间检测
        private void timer_Tick(object sender, EventArgs e)
        {

            DateTime nowtime = DateTime.Now.AddMilliseconds(1000);
            var sw = db.Attentions.Where(x =>x.Warning!=null &&x.State==false && x.Warning > DateTime.Now && x.Warning <nowtime);
           
            

            if (sw.Count()!=0)
            {
        
                warnShow.Visibility = Visibility.Visible;

                ListBoxItem head = new ListBoxItem();
                head.FontFamily = new System.Windows.Media.FontFamily("Verdana");
                head.FontSize = 12;
                head.FontWeight = FontWeights.Bold;
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
                hd.Width = 90;
                hd.TextAlignment = TextAlignment.Center;
                hd.Text = "截止时间";

                hsp.Children.Add(ho);
                hsp.Children.Add(hc);
                hsp.Children.Add(hd);
                
                head.Content = hsp;
                showWarning.Items.Add(head);


                int i = 0;
                foreach (var item in sw)
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
                    sp.Background = System.Windows.Media.Brushes.White;
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
                    sp.Children.Add(order);
                    sp.Children.Add(content);
                    sp.Children.Add(dl);


                    lbi.Content = sp;
                    showWarning.Items.Add(lbi);

                    timer.Stop();
                }
            }
        }

        
        private void know_Click(object sender, RoutedEventArgs e)
        {
            showWarning.Items.Clear();
            DoubleAnimation daV = new DoubleAnimation(0.7, 0, new Duration(TimeSpan.FromSeconds(1)));
            warnShow.BeginAnimation(UIElement.OpacityProperty, daV);
            warnShow.Visibility = Visibility.Hidden;
            timer.Start();

        }
    }
}
