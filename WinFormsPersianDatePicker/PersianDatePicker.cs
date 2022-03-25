using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbrAfzarGostaran;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AbrAfzarGostaran.Windows.Forms
{
    [DefaultBindingProperty("SelectedDateString")]
    [DefaultEvent("SelectedDateChange")]
    public class WinFormsPersianDatePicker : UserControl
    {
        private IContainer components = null;

        private WinFormsLinearList WinFormsLinearListDate;

        private DateTime selectedDateTime;

        private PersianCalendar pc;

        private WinFormsPersianCalendar WinFormsPersianCalendar;

        public int EndOfYears
        {
            get
            {
                return this.WinFormsPersianCalendar.EndOfYears;
            }
            set
            {
                this.WinFormsPersianCalendar.EndOfYears = value;
            }
        } 

        public Color InnerBorderColor
        {
            get
            {
                return this.WinFormsPersianCalendar.InnerBorderColor;
            }
            set
            {
                this.WinFormsPersianCalendar.InnerBorderColor = value;
            }
        }

        public bool isBorder3D
        {
            get
            {
                return this.WinFormsPersianCalendar.isBorder3D;
            }
            set
            {
                this.WinFormsPersianCalendar.isBorder3D = value;
            }
        }

        public Font ItemsFont
        {
            get
            {
                return this.WinFormsPersianCalendar.ItemsFont;
            }
            set
            {
                this.WinFormsPersianCalendar.ItemsFont = value;
            }
        }

        public Font LinearListFont
        {
            get
            {
                return this.WinFormsPersianCalendar.LinearListFont;
            }
            set
            {
                this.WinFormsPersianCalendar.LinearListFont = value;
                this.WinFormsLinearListDate.ItemFont = value;
            }
        }

        public Color OuterBorderColor
        {
            get
            {
                return this.WinFormsPersianCalendar.OuterBorderColor;
            }
            set
            {
                this.WinFormsPersianCalendar.OuterBorderColor = value;
            }
        }

        public Color PanelEndBackColor
        {
            get
            {
                return this.WinFormsPersianCalendar.PanelEndBackColor;
            }
            set
            {
                this.WinFormsPersianCalendar.PanelEndBackColor = value;
            }
        }

        public Color PanelStartBackColor
        {
            get
            {
                return this.WinFormsPersianCalendar.PanelStartBackColor;
            }
            set
            {
                this.WinFormsPersianCalendar.PanelStartBackColor = value;
            }
        }

        [Bindable(true)]
        public DateTime SelectedDate
        {
            get
            {
                return this.GetDateTime(this.SelectedDateString);
            }
            set
            {
                this.SelectedDateString = this.getPersianDate(value);
                this.WinFormsPersianCalendar_AbrCalendarSelectedDateChange(this, new WinFormsPersianCalendar.AbrSelectedDateChangesEventArgs(this.getPersianDate(value)));
                if (this.SelectedDateChange != null)
                {
                    this.SelectedDateChange(this, new WinFormsPersianDatePicker.AbrSelectedDateChangesEventArgs(value));
                }
            }
        }

        [Bindable(true)]
        [RefreshProperties(RefreshProperties.All)]
        public string SelectedDateString
        {
            get
            {
                string str;
                str = (!(this.selectedDateTime != DateTime.MinValue.AddDays(1)) ? "" : Utils.getPersianDate(this.selectedDateTime));
                return str;
            }
            set
            {
                if (!(value != ""))
                {
                    this.WinFormsLinearListDate.Enabled = false;
                    this.selectedDateTime = DateTime.MinValue.AddDays(1);
                    this.WinFormsPersianCalendar_AbrCalendarSelectedDateChange(this, new WinFormsPersianCalendar.AbrSelectedDateChangesEventArgs(value));
                }
                else
                {
                    this.WinFormsLinearListDate.Enabled = true;
                    this.selectedDateTime = this.GetDateTime(value);
                    this.WinFormsPersianCalendar_AbrCalendarSelectedDateChange(this, new WinFormsPersianCalendar.AbrSelectedDateChangesEventArgs(value));
                }
            }
        }

        public WinFormsPersianCalendar.DaysOfWeek[] SelectedHolidays
        {
            get
            {
                return this.WinFormsPersianCalendar.SelectedHolidays;
            }
            set
            {
                this.WinFormsPersianCalendar.SelectedHolidays = value;
            }
        }

        public string SelectedMonthName
        {
            get
            {
                return this.WinFormsPersianCalendar.SelectedMonthName;
            }
            set
            {
                this.WinFormsPersianCalendar.SelectedMonthName = value;
            }
        }

        public int StartOfYears
        {
            get
            {
                return this.WinFormsPersianCalendar.StartOfYears;
            }
            set
            {
                this.WinFormsPersianCalendar.StartOfYears = value;
            }
        }

        public WinFormsPersianDatePicker(int endYears=1450)
        {
            this.InitializeComponent();
            this.WinFormsPersianCalendar = new WinFormsPersianCalendar();
            this.WinFormsPersianCalendar.AbrCalendarSelectedDateChange += new WinFormsPersianCalendar.AbrCalendarSelectedDateChangeEventHandler(this.WinFormsPersianCalendar_AbrCalendarSelectedDateChange);
            this.WinFormsPersianCalendar.Visible = false;
            this.selectedDateTime = DateTime.Today;
            this.WinFormsLinearListDate.Items.Add(this.getPersianDate(this.selectedDateTime.AddDays(-1)));
            this.WinFormsLinearListDate.Items.Add(this.getPersianDate(this.selectedDateTime));
            this.WinFormsLinearListDate.Items.Add(this.getPersianDate(this.selectedDateTime.AddDays(1)));
            this.WinFormsLinearListDate.SelectedIndex = 1;
            this.SelectedDateString = this.WinFormsLinearListDate.Items[1].Text;
        }

        private void WinFormsLinearListDate_WinFormsLinearListIndexChange(object sender, WinFormsLinearList.WinFormsLinearListEventArgs e)
        {
            if (this.WinFormsLinearListDate.SelectedIndex == 2)
            {
                this.selectedDateTime = this.selectedDateTime.AddDays(1);
                this.setDateListItems();
                this.WinFormsLinearListDate.SelectedIndex = 1;
                this.SelectedDateString = this.WinFormsLinearListDate.Items[1].Text;
            }
            else if (this.WinFormsLinearListDate.SelectedIndex == 0)
            {
                this.selectedDateTime = this.selectedDateTime.AddDays(-1);
                this.setDateListItems();
                this.WinFormsLinearListDate.SelectedIndex = 1;
                this.SelectedDateString = this.WinFormsLinearListDate.Items[1].Text;
            }
            if (this.SelectedDateChange != null)
            {
                this.SelectedDateChange(this, new WinFormsPersianDatePicker.AbrSelectedDateChangesEventArgs(this.selectedDateTime));
            }
        }

        private void WinFormsLinearListDate_WinFormsLinearListMiddleMouseClick(object sender, MouseEventArgs e)
        {
            Point location;
            this.WinFormsPersianCalendar.Focus();
            if (!this.WinFormsPersianCalendar.Visible)
            {
                if (this.WinFormsLinearListDate.PointToScreen(new Point(this.WinFormsLinearListDate.Location.X, this.WinFormsLinearListDate.Location.Y)).Y + this.WinFormsPersianCalendar.Height >= Screen.PrimaryScreen.WorkingArea.Height)
                {
                    var screen = this.WinFormsPersianCalendar;
                    WinFormsLinearList WinFormsLinearList = this.WinFormsLinearListDate;
                    location = this.WinFormsLinearListDate.Location;
                    int x = location.X - (this.WinFormsPersianCalendar.Width - this.WinFormsLinearListDate.Width) / 2;
                    location = this.WinFormsLinearListDate.Location;
                    screen.Location = WinFormsLinearList.PointToScreen(new Point(x, location.Y - this.WinFormsPersianCalendar.Height));
                }
                else
                {
                    var WinFormsPersianCalendar = this.WinFormsPersianCalendar;
                    WinFormsLinearList WinFormsLinearList1 = this.WinFormsLinearListDate;
                    location = this.WinFormsLinearListDate.Location;
                    int num = location.X - (this.WinFormsPersianCalendar.Width - this.WinFormsLinearListDate.Width) / 2;
                    location = this.WinFormsLinearListDate.Location;
                    WinFormsPersianCalendar.Location = WinFormsLinearList1.PointToScreen(new Point(num, location.Y + this.WinFormsLinearListDate.Height));
                }
                this.WinFormsPersianCalendar.SelectedDate = this.SelectedDateString;
                this.WinFormsPersianCalendar.Visible = true;
            }
            if (this.SelectedDateChange != null)
            {
                this.SelectedDateChange(this, new WinFormsPersianDatePicker.AbrSelectedDateChangesEventArgs(this.selectedDateTime));
            }
        }

        private void WinFormsPersianCalendar_AbrCalendarSelectedDateChange(object sender, WinFormsPersianCalendar.AbrSelectedDateChangesEventArgs e)
        {
            if (!(e.SelectedDate != ""))
            {
                this.selectedDateTime = DateTime.MinValue.AddDays(1);
            }
            else
            {
                this.selectedDateTime = this.GetDateTime(e.SelectedDate);
            }
            this.setDateListItems();
            if (this.SelectedDateChange != null)
            {
                this.SelectedDateChange(this, new WinFormsPersianDatePicker.AbrSelectedDateChangesEventArgs(this.selectedDateTime));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if ((!disposing ? false : this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private DateTime GetDateTime(string pdate)
        {
            DateTime dateTime;
            DateTime dateTime1;
            try
            {
                string str = pdate.Substring(5);
                int num = int.Parse(pdate.Substring(0, 4));
                int num1 = int.Parse(str.Substring(0, str.IndexOf("/")));
                str = str.Substring(0);
                str = str.Substring(str.IndexOf("/") + 1);
                int num2 = int.Parse(str);
                dateTime = this.pc.ToDateTime(num, num1, num2, 1, 1, 1, 1);
            }
            catch
            {
                dateTime = DateTime.MinValue.AddDays(1);
                dateTime1 = dateTime;
                return dateTime1;
            }
            dateTime1 = dateTime;
            return dateTime1;
        }

        private string getPersianDate(DateTime dt)
        {
            string str;
            bool flag;
            this.pc = new PersianCalendar();
            if (!(dt != DateTime.MinValue.AddDays(1)) || !(dt != DateTime.MinValue))
            {
                flag = true;
            }
            else
            {
                DateTime minValue = DateTime.MinValue;
                flag = !(dt != minValue.AddDays(2));
            }
            if (flag)
            {
                str = "";
            }
            else
            {
                object[] year = new object[] { this.pc.GetYear(dt), "/", null, null, null };
                int month = this.pc.GetMonth(dt);
                year[2] = month.ToString("D2");
                year[3] = "/";
                month = this.pc.GetDayOfMonth(dt);
                year[4] = month.ToString("D2");
                str = string.Concat(year);
            }
            return str;
        }

        private void InitializeComponent()
        {
            this.WinFormsLinearListDate = new WinFormsLinearList();
            base.SuspendLayout();
            this.WinFormsLinearListDate.ArrowColor = Color.DimGray;
            this.WinFormsLinearListDate.BackColor = Color.Transparent;
            this.WinFormsLinearListDate.CheckedEndColor = Color.LightGray;
            this.WinFormsLinearListDate.CheckedStartColor = Color.WhiteSmoke;
            this.WinFormsLinearListDate.DisabledTextColor = Color.LightGray;
            this.WinFormsLinearListDate.EnabledTextColor = Color.Black;
            this.WinFormsLinearListDate.EndGradiantColor = Color.LightGray;
            this.WinFormsLinearListDate.HoverBorderColor = Color.SkyBlue;
            this.WinFormsLinearListDate.IsFocused = false;
            this.WinFormsLinearListDate.ItemFont = new Font("B Nazanin", 15f, FontStyle.Bold);
            this.WinFormsLinearListDate.Location = new Point(0, 0);
            this.WinFormsLinearListDate.Name = "WinFormsLinearListDate";
            this.WinFormsLinearListDate.Selectable = true;
            this.WinFormsLinearListDate.Selected = false;
            this.WinFormsLinearListDate.SelectedArrowColor = Color.SkyBlue;
            this.WinFormsLinearListDate.SelectedBorderColor = Color.DimGray;
            this.WinFormsLinearListDate.SelectedEndColor = Color.FromArgb(50, 173, 216, 230);
            this.WinFormsLinearListDate.SelectedIndex = 0;
            this.WinFormsLinearListDate.SelectedStartColor = Color.FromArgb(50, 224, 255, 255);
            this.WinFormsLinearListDate.Size = new Size(150, 25);
            this.WinFormsLinearListDate.StartGradiantColor = Color.WhiteSmoke;
            this.WinFormsLinearListDate.TabIndex = 1;
            this.WinFormsLinearListDate.WinFormsLinearListIndexChange += new WinFormsLinearList.WinFormsLinearListIndexChangeHandler(this.WinFormsLinearListDate_WinFormsLinearListIndexChange);
            this.WinFormsLinearListDate.WinFormsLinearListMiddleMouseClick += new WinFormsLinearList.WinFormsLinearListMiddleMouseClickHandler(this.WinFormsLinearListDate_WinFormsLinearListMiddleMouseClick);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.WinFormsLinearListDate);
            this.MaximumSize = new Size(150, 25);
            this.MinimumSize = new Size(150, 25);
            base.Name = "WinFormsPersianDatePicker";
            base.Size = new Size(150, 25);
            base.ResumeLayout(false);
        }

        public virtual void OnDroppedDown(object sender, EventArgs e)
        {
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        private void setDateListItems()
        {
            this.WinFormsLinearListDate.Items[0].Text = this.getPersianDate(this.selectedDateTime.AddDays(-1));
            this.WinFormsLinearListDate.Items[1].Text = this.getPersianDate(this.selectedDateTime);
            this.WinFormsLinearListDate.Items[2].Text = this.getPersianDate(this.selectedDateTime.AddDays(1));
        }

        public event WinFormsPersianDatePicker.AbrCalendarSelectedDateChangeEventHandler SelectedDateChange;

        public delegate void AbrCalendarSelectedDateChangeEventHandler(object sender, WinFormsPersianDatePicker.AbrSelectedDateChangesEventArgs e);

        public class AbrSelectedDateChangesEventArgs : EventArgs
        {
            private DateTime selectedDate;

            public DateTime SelectedDate
            {
                get
                {
                    return this.selectedDate;
                }
            }

            public AbrSelectedDateChangesEventArgs(DateTime date)
            {
                this.selectedDate = date;
            }
        }
    }

    internal class Utils
    {
        public Utils()
        {
        }

        public static bool comparePersianDates(string date, string dateA, string dateB)
        {
            DateTime miladiFromPersian = Utils.getMiladiFromPersian(date);
            DateTime dateTime = Utils.getMiladiFromPersian(dateA);
            DateTime miladiFromPersian1 = Utils.getMiladiFromPersian(dateB);
            return ((miladiFromPersian < dateTime ? true : !(miladiFromPersian <= miladiFromPersian1)) ? false : true);
        }

        public static string correctDate(string date)
        {
            string str;
            string str1 = date;
            str = (str1.Length != 2 ? string.Concat("0", str1) : str1);
            return str;
        }

        public static string getFullTime()
        {
            DateTime now = DateTime.Now;
            object[] hour = new object[] { now.Hour, ":", Utils.correctDate(string.Concat(now.Minute)), ":", Utils.correctDate(string.Concat(now.Second)) };
            return string.Concat(hour);
        }

        public static DateTime getMiladiFromPersian(string perDate)
        {
            int num = int.Parse(perDate.Substring(0, perDate.IndexOf("/")));
            perDate = perDate.Substring(perDate.IndexOf("/") + 1);
            int num1 = int.Parse(perDate.Substring(0, perDate.IndexOf("/")));
            perDate = perDate.Substring(perDate.IndexOf("/") + 1);
            return new DateTime(num, num1, int.Parse(perDate));
        }

        public static string getPersianDate()
        {
            DateTime now = DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            object[] year = new object[] { persianCalendar.GetYear(now), "/", Utils.correctDate(string.Concat(persianCalendar.GetMonth(now))), "/", Utils.correctDate(string.Concat(persianCalendar.GetDayOfMonth(now))) };
            return string.Concat(year);
        }

        public static string getPersianDate(DateTime dt)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            object[] year = new object[] { persianCalendar.GetYear(dt), "/", Utils.correctDate(string.Concat(persianCalendar.GetMonth(dt))), "/", Utils.correctDate(string.Concat(persianCalendar.GetDayOfMonth(dt))) };
            return string.Concat(year);
        }

        public static string getPersianDateWithoutSlash()
        {
            DateTime now = DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            string str = string.Concat(persianCalendar.GetYear(now), Utils.correctDate(string.Concat(persianCalendar.GetMonth(now))), Utils.correctDate(string.Concat(persianCalendar.GetDayOfMonth(now))));
            return str;
        }

        public static string getTime()
        {
            DateTime now = DateTime.Now;
            string str = string.Concat(now.Hour, ":", now.Minute);
            return str;
        }

        public static string ToEnglishNumber(string input)
        {
            input = input.Replace("/", "");
            input = input.Replace("۰", "0");
            input = input.Replace("۱", "1");
            input = input.Replace("۲", "2");
            input = input.Replace("۳", "3");
            input = input.Replace("۴", "4");
            input = input.Replace("۵", "5");
            input = input.Replace("۶", "6");
            input = input.Replace("۷", "7");
            input = input.Replace("۸", "8");
            input = input.Replace("۹", "9");
            return input;
        }

        public static string ToPersianNumber(string input)
        {
            input = input.Replace("0", "۰");
            input = input.Replace("1", "۱");
            input = input.Replace("2", "۲");
            input = input.Replace("3", "۳");
            input = input.Replace("4", "۴");
            input = input.Replace("5", "۵");
            input = input.Replace("6", "۶");
            input = input.Replace("7", "۷");
            input = input.Replace("8", "۸");
            input = input.Replace("9", "۹");
            return input;
        }
    }
  

}