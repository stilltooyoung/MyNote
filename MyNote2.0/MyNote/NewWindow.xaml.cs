﻿using System;
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
    /// NewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewWindow : Window
    {
        ModelNotes db = new ModelNotes();
        CalendarControl calen = new CalendarControl();
        public NewWindow()
        {
            InitializeComponent();

            this.Title = string.Empty;
            ShowInTaskbar = false;

            this.WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            //this.AllowsTransparency = true;

            ////窗口最大化
            //WindowState = WindowState.Maximized;
            ////最前
            //Topmost = true;

            root.Children.Add(calen);
            root.Loaded += LightDays;
            calen.AddHandler(CalendarControl.SelectedDayChangedEvent,new RoutedEventHandler(ShowThisDay));
            calen.AddHandler(CalendarControl.ShowYMChangedEvent,new RoutedEventHandler(LightDays));
        }
        public void ShowThisDay(object sender,RoutedEventArgs e)
        {
            showDay.Text = "第 " + calen.SelectedDay.DayOfYear.ToString() + " 天";

            ShowDiary();

            hour.Text = "";
            minute.Text = "";
            contentAttention.Text = "";

            diaryTitle.Text = "";
            contentDiary.Text = "";

            addAttention.Visibility = Visibility.Hidden;
            writeDiary.Visibility = Visibility.Hidden;

            //CollectionView aa=CollectionViewSource.GetDefaultView(db.Attentions.Local) as CollectionView;

            //aa.Filter = item =>
            //{
            //    Attention a = item as Attention;
            //    if (a.start.Date == selectedDay.Date)
            //    {
            //        return true;
            //    }
            //    else
            //        return false;
            //};

            //var sd = db.Attentions.Select(x => x.start.Date == selectedDay.Date);

            //var sddf = from s in db.Attentions
            //           where s.start.Date == selectedDay.Date
            //           select s;
            var d = calen.SelectedDay.AddDays(1);
            var sd = db.Attentions.Where(x => x.Deadline >= calen.SelectedDay.Date && x.Deadline < d.Date).OrderBy(y => y.Deadline).ToList();
            //var sd = db.Attentions.Local.Where(x=>x.start.Date==selectedDay.Date);

            //attentionList = db.Attentions.Where(x => x.start.Date == selectedDay.Date) as ObservableCollection<Attention>;
            //attentionDataGrid.ItemsSource = null;
            attentionDataGrid.ItemsSource = sd;

        }

        public void LightDays(object sender,RoutedEventArgs e)
        {
            DateTime ym = calen.YM.AddMonths(1);
            var LightDiaries = db.Diaries.Where(t => t.Time >= calen.YM && t.Time < ym);
            var LightAttentions = db.Attentions.Where(t => t.Deadline >= calen.YM && t.Deadline < ym);

            if (LightDiaries!=null&& LightDiaries.Count()!=0)
            {
                foreach (var item in LightDiaries)
                {
                    calen.LightDays(item.Time, System.Drawing.Color.LightBlue);
                }
            }
            if (LightAttentions!=null&& LightAttentions.Count()!=0)
            {
                foreach (var item in LightAttentions)
                {
                    calen.LightDays(item.Deadline, System.Drawing.Color.YellowGreen);
                }
            }
        }


        #region Calendar
        //显示当天日记
        public void ShowDiary()
        {
            DateTime d = calen.SelectedDay.Date.AddDays(1);
            var daydiay = db.Diaries.SingleOrDefault(x => x.Time >= calen.SelectedDay.Date && x.Time < d.Date);
            if (daydiay != null)
            { showDiary.Text = daydiay.Title + ":\n  " + daydiay.Content; }
            else if (calen.SelectedDay.AddDays(1) <= DateTime.Now)
            { showDiary.Text = "此日可待成追忆！"; }
            else if (calen.SelectedDay.Date == DateTime.Now.Date)
            {
                showDiary.Text = "今日还没有日记哦！";
            }
            else
            { showDiary.Text = "未来还未来，活在当下！"; }

        }

        
        //创建提醒
        private void creatAttention_Click(object sender, RoutedEventArgs e)
        {
            if (calen.SelectedDay.AddDays(1) < DateTime.Now)
            {
                MessageBox.Show("以往不谏，来者可追");
                return;
            }
            addAttention.Visibility = Visibility.Visible;
        }

        //创建日记
        private void Diary_Click(object sender, RoutedEventArgs e)
        {
            if (calen.SelectedDay.Date != DateTime.Now.Date)
            {
                MessageBox.Show("活在当下！");
                return;
            }

            DateTime d = calen.SelectedDay.Date.AddDays(1);
            var daydiay = db.Diaries.SingleOrDefault(x => x.Time >= calen.SelectedDay.Date && x.Time <= d.Date);
            if (daydiay != null)
            {
                MessageBox.Show("今日已记，明日再来吧！");
                return;
            }
            WriteDiaryTime = DateTime.Now;
            writeDiary.Visibility = Visibility.Visible;
        }

        //提交提醒
        private void submitAttention_Click(object sender, RoutedEventArgs e)
        {
            if (hour.Text == string.Empty)
            {
                MessageBox.Show("小时值不能为空！");
                return;
            }
            else if (Convert.ToInt32(hour.Text) > 23)
            {
                MessageBox.Show("小时值请输入0~23中任意字符！");
                return;
            }
            if (minute.Text == string.Empty)
            {
                MessageBox.Show("分钟值不能为空！");
                return;
            }
            else if (Convert.ToInt32(minute.Text) > 59)
            {
                MessageBox.Show("分钟值请输入0~59中任意字符！");
                return;
            }
            if (contentAttention.Text.Trim(' ') == string.Empty)
            {
                MessageBox.Show("提醒内容不能为空！");
                return;
            }
            if (contentAttention.Text.Count() > 25)
            {
                MessageBox.Show("请精简你的提醒内容在25字以内！");
                return;
            }
            if (calen.SelectedDay.Date == DateTime.Now.Date)
            {
                if (DateTime.Now.Hour > Convert.ToInt32(hour.Text))
                {
                    MessageBox.Show("请创建未来的提醒！");
                    return;
                }
                else if (DateTime.Now.Hour == Convert.ToInt32(hour.Text) && DateTime.Now.Minute > Convert.ToInt32(minute.Text))
                {
                    MessageBox.Show("请创建未来的提醒！");
                    return;
                }
            }
            else if (calen.SelectedDay < DateTime.Now)
            {
                MessageBox.Show("请创建未来的提醒！");
                return;
            }



            var newAttention = new Attention();
            newAttention.Start = DateTime.Now;
            newAttention.Deadline = calen.SelectedDay.Date.AddHours(Convert.ToInt32(hour.Text)).AddMinutes(Convert.ToInt32(minute.Text));
            switch (warning.SelectedIndex)
            {
                case -1: break;
                case 0: newAttention.Warning = null; break;
                case 1: newAttention.Warning = newAttention.Deadline; break;
                case 2: newAttention.Warning = newAttention.Deadline - TimeSpan.FromMinutes(10); break;
                case 3: newAttention.Warning = newAttention.Deadline - TimeSpan.FromMinutes(30); break;
                case 4: newAttention.Warning = (newAttention.Deadline - TimeSpan.FromDays(1)).Date.AddHours(22); break;
                case 5: newAttention.Warning = (newAttention.Deadline - TimeSpan.FromDays(3)).Date.AddHours(22); break;
                case 6: newAttention.Warning = (newAttention.Deadline - TimeSpan.FromDays(7)).Date.AddHours(22); break;
                default: break;
            }

            if (newAttention.Warning <= DateTime.Now)
            {
                MessageBox.Show(newAttention.Deadline.ToString() + "提醒时间" + newAttention.Warning.ToString() + "过时啦！");
                return;
            }

            newAttention.Content = contentAttention.Text;
            db.Attentions.Add(newAttention);



            var sd = db.Attentions.Local.Where(x => x.Start.Date == calen.SelectedDay.Date);
            attentionDataGrid.ItemsSource = sd;



            db.SaveChanges();

            hour.Clear();
            minute.Clear();
            contentAttention.Clear();

            addAttention.Visibility = Visibility.Hidden;
        }

        //提醒返回
        private void backAttention_Click(object sender, RoutedEventArgs e)
        {
            addAttention.Visibility = Visibility.Hidden;
        }

        DateTime WriteDiaryTime = DateTime.Now;

        //提交日记
        private void submitDiary_Click(object sender, RoutedEventArgs e)
        {

            if (diaryTitle.Text.Trim(' ') == string.Empty)
            {
                MessageBox.Show("日记标题不能为空！");
                return;
            }
            else if (contentDiary.Text.Trim(' ') == string.Empty)
            {
                MessageBox.Show("日记内容不能为空");
                return;
            }
            else if (diaryTitle.Text.Length > 20)
            {
                MessageBox.Show("标题字数限制在20个字符以内");
                return;
            }


            var newDiary = new Diary();
            newDiary.Title = diaryTitle.Text;
            newDiary.Content = contentDiary.Text;
            newDiary.Time = WriteDiaryTime;
            db.Diaries.Add(newDiary);
            db.SaveChanges();

            diaryTitle.Clear();
            contentDiary.Clear();
            writeDiary.Visibility = Visibility.Hidden;

            ShowDiary();

        }

        //日记返回
        private void backDiary_Click(object sender, RoutedEventArgs e)
        {
            writeDiary.Visibility = Visibility.Hidden;

        }



        //表头序号
        private void attentionDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        //加载当天数据
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowDiary();

            CollectionViewSource attentionViewSource = ((CollectionViewSource)(FindResource("attentionViewSource")));
            // 通过设置 CollectionViewSource.Source 属性加载数据: 
            // attentionViewSource.Source = [一般数据源]


            if (db.Attentions.Count()!=0)
            {
                db.Attentions.ToList();
                attentionViewSource.Source = db.Attentions.Local;

                attentionViewSource.View.Filter = item =>
                {
                    Attention a = item as Attention;
                    if (a.Start.Date == calen.SelectedDay.Date)
                    {
                        return true;
                    }
                    else
                        return false;
                };
            }
            else
            {
                attentionViewSource.Source = null;
            }
        }

        //限制数字
        private void LimitNum(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                double num = 0;
                if (!Double.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }

        }

        //小时检查
        private void hour_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (hour.Text.Length > 2)
            {
                hour.Text = hour.Text.Substring(0, 2);
                return;
            }
            LimitNum(sender, e);

        }

        //分钟检查
        private void minute_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (minute.Text.Length > 2)
            {
                minute.Text = minute.Text.Substring(0, 2);
                return;
            }
            LimitNum(sender, e);

        }

        #endregion

    }
}
