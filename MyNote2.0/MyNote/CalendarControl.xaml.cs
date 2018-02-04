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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace MyNote
{
    /// <summary>
    /// CalendarControl.xaml 的交互逻辑
    /// </summary>
    public partial class CalendarControl : System.Windows.Controls.UserControl
    {
        private System.Windows.Forms.DataGridView calendarDataGrid;
        DateTime ShowYM = DateTime.Parse(DateTime.Now.ToString("yyyy年MM月01日"));

        public CalendarControl()
        {
            InitializeComponent();

            SelectedDay = DateTime.Now.Date;
            YM = ShowYM;
            ym.Text = DateTime.Now.ToString("yyyy-MM");


            calendarDataGrid = windowsFormsHost.Child as System.Windows.Forms.DataGridView;

            calendarDataGrid.RowHeadersVisible = false;
            calendarDataGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;


            string[] week = new string[] { "周日", "周一", "周二", "周三", "周四", "周五", "周六" };
            foreach (var item in week)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = item;
                column.Width = 50;
                calendarDataGrid.Columns.Add(column);
            }

            calendarDataGrid.Rows.Add(6);

            for (int i = 0; i < 6; i++)
            {
                calendarDataGrid.Rows[i].Height = 50;
            }
            for (int i = 0; i < calendarDataGrid.Columns.Count; i++)
            {
                calendarDataGrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                calendarDataGrid.Columns[i].Width = 50;
            }

            int count = 0;
            int weekValue = Convert.ToInt16(DateTime.Parse(DateTime.Now.ToString("yyyy年MM月01日")).DayOfWeek);
            int monthDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            calendarDataGrid.AllowUserToResizeRows = false;
            calendarDataGrid.AllowUserToResizeColumns = false;
            calendarDataGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Green;
            calendarDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            calendarDataGrid.RowTemplate.Height = 50;

            calendarDataGrid.Update();
            calendarDataGrid.Update();
            for (int row = 0; row < calendarDataGrid.RowCount; row++)
            {
                if (row > 0)
                    weekValue = 0;
                for (int column = weekValue; column < calendarDataGrid.ColumnCount; column++)
                {
                    count++;
                    if (count > monthDays)
                        break;
                    calendarDataGrid.Rows[row].Cells[column].Value = count.ToString("00");
                    if (count == DateTime.Now.Day)
                    {
                        calendarDataGrid.Rows[row].Cells[column].Selected = true;
                        calendarDataGrid.Rows[row].Cells[column].Style.ForeColor = System.Drawing.Color.Red;
                        calendarDataGrid.Rows[row].Cells[column].Value = "今";
                        calendarDataGrid.CurrentCell = calendarDataGrid.Rows[row].Cells[column];
                    }
                }

            }

            calendarDataGrid.CurrentCellChanged += new EventHandler(calendarDataGrid_CurrentCellChanged);
     
        }





        private void ShowCalendar()
        {
            int count = 0;
            int weekValue = Convert.ToInt16(DateTime.Parse(ShowYM.ToString("yyyy年MM月01日")).DayOfWeek);
            int monthDays = DateTime.DaysInMonth(ShowYM.Year, ShowYM.Month);
            ym.Text = ShowYM.ToString("yyyy-MM");

            for (int row = 0; row < calendarDataGrid.RowCount; row++)
            {
                for (int column = 0; column < calendarDataGrid.ColumnCount; column++)
                {
                    calendarDataGrid.Rows[row].Cells[column].Value = "";
                    calendarDataGrid.Rows[row].Cells[column].Style.BackColor = System.Drawing.Color.Gray;
                    calendarDataGrid.Rows[row].Cells[column].Style.ForeColor = System.Drawing.Color.Black;
                    
                }
            }

            for (int row = 0; row < calendarDataGrid.RowCount; row++)
            {
                if (row > 0)
                    weekValue = 0;
                for (int column = weekValue; column < calendarDataGrid.ColumnCount; column++)
                {
                    count++;
                    if (count > monthDays)
                        break;
                    calendarDataGrid.Rows[row].Cells[column].Value = count.ToString("00");
                    calendarDataGrid.Rows[row].Cells[column].Style.BackColor = System.Drawing.Color.White;
                    if (ShowYM.Date == DateTime.Parse(DateTime.Now.ToString("yyyy年MM月01日")).Date && count == DateTime.Now.Day)
                    {
                        calendarDataGrid.Rows[row].Cells[column].Selected = true;
                        calendarDataGrid.Rows[row].Cells[column].Style.ForeColor = System.Drawing.Color.Red;
                        calendarDataGrid.Rows[row].Cells[column].Value = "今";
                        calendarDataGrid.CurrentCell = calendarDataGrid.Rows[row].Cells[column];
                    }

                }

            }
        }

        public static readonly DependencyProperty SelectedDayProperty =
           DependencyProperty.Register("SelectedDay", typeof(DateTime),
           typeof(CalendarControl));
        public DateTime SelectedDay
        {
            get { return (DateTime)GetValue(SelectedDayProperty); }

            set { SetValue(SelectedDayProperty, value); }
        }

        public static readonly DependencyProperty YMProperty =
           DependencyProperty.Register("YM", typeof(DateTime),
           typeof(CalendarControl));
        public DateTime YM
        {
            get { return (DateTime)GetValue(YMProperty); }

            set { SetValue(YMProperty, value); }
        }



        public static readonly RoutedEvent ShowYMChangedEvent =
            EventManager.RegisterRoutedEvent("ShowYMChanged",RoutingStrategy.Bubble,typeof(RoutedPropertyChangedEventHandler<object>),typeof(CalendarControl));
        public event ColumnClickEventHandler ShowYMChanged
        {
            add
            {
                this.AddHandler(ShowYMChangedEvent,value);
            }

            remove
            {
                this.RemoveHandler(ShowYMChangedEvent, value);
            }
        }

        public static readonly RoutedEvent SelectedDayChangedEvent =
            EventManager.RegisterRoutedEvent("SelectedDayChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(CalendarControl));

        public event RoutedPropertyChangedEventHandler<object> SelectedDayChanged
        {
            add
            {
                this.AddHandler(SelectedDayChangedEvent, value);
            }

            remove
            {
                this.RemoveHandler(SelectedDayChangedEvent, value);
            }
        }

        public void calendarDataGrid_CurrentCellChanged(object oldValue, object newValue)
        {
            RoutedPropertyChangedEventArgs<object> arg =
                new RoutedPropertyChangedEventArgs<object>(oldValue, newValue, SelectedDayChangedEvent);

            DateTime thisMonth = DateTime.Parse(ShowYM.ToString("yyyy年MM月01日"));
            int weekValue = Convert.ToInt16(thisMonth.DayOfWeek);

            SelectedDay = thisMonth.AddDays((calendarDataGrid.CurrentCell.RowIndex) * 7 - weekValue + calendarDataGrid.CurrentCell.ColumnIndex);

            this.RaiseEvent(arg);
            
            
        }

        
        private void left_Click(object sender, RoutedEventArgs e)
        {
            ShowYM = ShowYM.AddMonths(-1);
            YM = ShowYM;
            
            calendarDataGrid_CurrentCellChanged(SelectedDay,SelectedDay);

            ShowCalendar();

            e.RoutedEvent = ShowYMChangedEvent;
            e.Source = this;
            this.RaiseEvent(e);

        }

        private void right_Click(object sender, RoutedEventArgs e)
        {
            ShowYM = ShowYM.AddMonths(1);
            YM = ShowYM;

            calendarDataGrid_CurrentCellChanged(SelectedDay, SelectedDay);

            ShowCalendar();

            e.RoutedEvent = ShowYMChangedEvent;
            e.Source = this;
            this.RaiseEvent(e);
        }

        public void LightDays(DateTime day,System.Drawing.Color color)
        {
            DateTime thisMonth = DateTime.Parse(ShowYM.ToString("yyyy年MM月01日"));
            int weekValue = Convert.ToInt16(thisMonth.DayOfWeek);

            int num =weekValue+day.Day-1;

            calendarDataGrid.Rows[num / 7].Cells[num % 7].Style.ForeColor = color;
        }
    }
}
