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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyNote
{
    /// <summary>
    /// ShowDiaries.xaml 的交互逻辑
    /// </summary>
    public partial class ShowDiaries : Window
    {
        ModelNotes db = new ModelNotes();

        DateTime ThisDay = DateTime.Now;

        public ShowDiaries()
        {
            InitializeComponent();

            //隐藏任务栏
            this.Title = string.Empty;
            ShowInTaskbar = false;

            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = true;

            //窗口最大化
            WindowState = WindowState.Maximized;
            //最前
            Topmost = true;

            var today = DateTime.Now.Date;
            var ad = today.AddDays(1);
            var d = db.Diaries.SingleOrDefault(x => x.Time >= today && x.Time < ad.Date);

            if (d != null)
            {
                title.Text = d.Title;
                diary.Text = d.Content;
            }
            else
            {
                title.Text = "今天还没有写日记哦！";
                diary.Text = "";
            }
        }

        private void selectDiary_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ThisDay = Convert.ToDateTime((sender as DatePicker).SelectedDate);
            var ad = ThisDay.AddDays(1);
            var sd = db.Diaries.SingleOrDefault(x=>x.Time>=ThisDay.Date&&x.Time<ad.Date);
            if (sd != null)
            {
                title.Text = sd.Title;
                diary.Text = sd.Content;
            }
            else
            {
                title.Text = "这天没有日记!";
                diary.Text = "";
            }

        }

        bool LeftEnd = false;
        bool RightEnd = false;
        
        private void leftDiary_Click(object sender, RoutedEventArgs e)
        {
            RightEnd = false;
            if (LeftEnd)
            {
                return;
            }
            var start = db.Diaries.OrderBy(x=>x.Time).FirstOrDefault();

            if (start!=null)
            {
                for (int i = 1; start.Time < ThisDay; i++)
                {
                    ThisDay = ThisDay.Date - TimeSpan.FromDays(1);
                    var ad = ThisDay.AddDays(1);
                    var d = db.Diaries.SingleOrDefault(x => x.Time >= ThisDay.Date && x.Time < ad.Date);
                    if (d != null)
                    {
                        title.Text = d.Title;
                        diary.Text = d.Content;
                        selectDiary.SelectedDate = ThisDay;
                        return;
                    }
                }
            }

           

            title.Text ="前面没有日记啦！";
            diary.Text = "";
            LeftEnd = true;
            
        }

        

        private void rightDiary_Click(object sender, RoutedEventArgs e)
        {
            LeftEnd = false;
            if (RightEnd)
            {
                return;
            }

            var end = db.Diaries.OrderByDescending(x=>x.Time).FirstOrDefault();

            if (end!=null)
            {
                for (int i = 1; end.Time > ThisDay; i++)
                {
                    ThisDay = ThisDay.Date.AddDays(1);
                    var ad = ThisDay.AddDays(1);
                    var d = db.Diaries.SingleOrDefault(x => x.Time >= ThisDay.Date && x.Time < ad.Date);
                    if (d != null)
                    {
                        title.Text = d.Title;
                        diary.Text = d.Content;
                        selectDiary.SelectedDate = ThisDay;
                        return;
                    }
                }
            }

            
            title.Text = "后面没有日记啦！";
            diary.Text = "";
            RightEnd = true;
        }

        private void ShowDiaries_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                leftDiary_Click(sender, e);
            }
            else if (e.Key == Key.Right)
            {
                rightDiary_Click(sender, e);
            }
            else if (e.Key==Key.Escape)
            {
                this.Visibility = Visibility.Hidden;
            }
        }
    }
}
