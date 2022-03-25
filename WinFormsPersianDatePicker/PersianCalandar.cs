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
[DefaultEvent("AbrCalendarSelectedDateChange")]
public class WinFormsPersianCalendar : Form
{
    private string CalendarSelectedMonthName;

    private string CalendarSelectedDate;

    private int CalendarSelectedIndex;

    private int StartYear = 1350;

    private int EndYear = 1450;

    private int startMonthDay = 0;

    private Color PanelBackStartColor = Color.FromArgb(239, 239, 239);

    private Color PanelBackEndColor = Color.FromArgb(202, 202, 202);

    private Color DateSelectorInneerBorderColor = Color.FromArgb(254, 254, 254);

    private Color DateSelectorOuterBorderColor = Color.FromArgb(192, 192, 192);

    private Font WinFormsLinearListFont = new Font("B Nazanin", 15f, FontStyle.Bold);

    private Font AbrItemsFont = new Font("B Nazanin", 15f, FontStyle.Bold);

    private bool isOpen = false;

    private bool is3DBorder = true;

    private bool isVisible = false;

    private PersianCalendar persianCalendar;

    private WinFormsPersianDatePickerDayItem[] days;

    private WinFormsPersianCalendar.DaysOfWeek[] CalendarHolidays;

    private IContainer components = null;

    private WinFormsLinearList WinFormsLinearListYear;

    private WinFormsLinearList WinFormsLinearListMonth;

    private WinFormsPersianDatePickerDayItem WinFormsPersianDatePickerDayItemSH;

    private WinFormsPersianDatePickerDayItem WinFormsPersianDatePickerDayItemYE;

    private WinFormsPersianDatePickerDayItem WinFormsPersianDatePickerDayItemDO;

    private WinFormsPersianDatePickerDayItem WinFormsPersianDatePickerDayItemSE;

    private WinFormsPersianDatePickerDayItem WinFormsPersianDatePickerDayItemCH;

    private WinFormsPersianDatePickerDayItem WinFormsPersianDatePickerDayItemPA;

    private WinFormsPersianDatePickerDayItem WinFormsPersianDatePickerDayItemJO;

    private WinFormsPersianDatePickerDayItem WinFormsPersianDatePickerDayItemToday;

    public int EndOfYears
    {
        get
        {
            return this.EndYear;
        }
        set
        {
            this.EndYear = value;
            this.AddMonthsAndYearsToLists();
            base.Invalidate();
        }
    }

    public Color InnerBorderColor
    {
        get
        {
            return this.DateSelectorInneerBorderColor;
        }
        set
        {
            this.DateSelectorInneerBorderColor = value;
            base.Invalidate();
        }
    }

    public bool isBorder3D
    {
        get
        {
            return this.is3DBorder;
        }
        set
        {
            this.is3DBorder = value;
            base.Invalidate();
        }
    }

    public bool isPanelOpen
    {
        get
        {
            return this.isOpen;
        }
        set
        {
            this.isOpen = value;
            base.Invalidate();
        }
    }

    public Font ItemsFont
    {
        get
        {
            return this.AbrItemsFont;
        }
        set
        {
            this.AbrItemsFont = value;
            this.WinFormsPersianDatePickerDayItemCH.ItemFont = this.ItemsFont;
            this.WinFormsPersianDatePickerDayItemDO.ItemFont = this.ItemsFont;
            this.WinFormsPersianDatePickerDayItemJO.ItemFont = this.ItemsFont;
            this.WinFormsPersianDatePickerDayItemPA.ItemFont = this.ItemsFont;
            this.WinFormsPersianDatePickerDayItemSE.ItemFont = this.ItemsFont;
            this.WinFormsPersianDatePickerDayItemSH.ItemFont = this.ItemsFont;
            this.WinFormsPersianDatePickerDayItemYE.ItemFont = this.ItemsFont;
            this.WinFormsPersianDatePickerDayItemToday.ItemFont = this.ItemsFont;
            for (int i = 0; i < 42; i++)
            {
                this.days[i].ItemFont = this.ItemsFont;
            }
            base.Invalidate();
        }
    }

    public Font LinearListFont
    {
        get
        {
            return this.WinFormsLinearListFont;
        }
        set
        {
            this.WinFormsLinearListFont = value;
            this.WinFormsLinearListMonth.ItemFont = this.LinearListFont;
            this.WinFormsLinearListYear.ItemFont = this.LinearListFont;
            base.Invalidate();
        }
    }

    public Color OuterBorderColor
    {
        get
        {
            return this.DateSelectorOuterBorderColor;
        }
        set
        {
            this.DateSelectorOuterBorderColor = value;
            base.Invalidate();
        }
    }

    public Color PanelEndBackColor
    {
        get
        {
            return this.PanelBackEndColor;
        }
        set
        {
            this.PanelBackEndColor = value;
            base.Invalidate();
        }
    }

    public Color PanelStartBackColor
    {
        get
        {
            return this.PanelBackStartColor;
        }
        set
        {
            this.PanelBackStartColor = value;
            base.Invalidate();
        }
    }

    public string SelectedDate
    {
        get
        {
            return this.CalendarSelectedDate;
        }
        set
        {
            this.CalendarSelectedDate = value;
            if (this.isVisible)
            {
                if (this.AbrCalendarSelectedDateChange != null)
                {
                    this.AbrCalendarSelectedDateChange(this, new WinFormsPersianCalendar.AbrSelectedDateChangesEventArgs(value));
                }
            }
        }
    }

    public WinFormsPersianCalendar.DaysOfWeek[] SelectedHolidays
    {
        get
        {
            return this.CalendarHolidays;
        }
        set
        {
            this.CalendarHolidays = value;
            base.Invalidate();
        }
    }

    public string SelectedMonthName
    {
        get
        {
            return this.CalendarSelectedMonthName;
        }
        set
        {
            this.CalendarSelectedMonthName = value;
            base.Invalidate();
        }
    }

    public int StartOfYears
    {
        get
        {
            return this.StartYear;
        }
        set
        {
            this.StartYear = value;
            this.AddMonthsAndYearsToLists();
            base.Invalidate();
        }
    }

    public WinFormsPersianCalendar(int endYear=1450)
    {
        this.InitializeComponent();
        base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
        this.initializeAbrDatePickerContents(DateTime.Today);
        this.EndOfYears = endYear;
    }

    public WinFormsPersianCalendar(DateTime date)
    {
        this.InitializeComponent();
        base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
        this.initializeAbrDatePickerContents(date);
    }

    private void abrDayItem_AbrDayItemChecked(object sender, MouseEventArgs e)
    {
        this.days[this.CalendarSelectedIndex].Selected = false;
        this.CalendarSelectedIndex = int.Parse(((WinFormsPersianDatePickerDayItem)sender).Name);
        this.days[this.CalendarSelectedIndex].Selected = true;
        object[] selectedText = new object[] { this.WinFormsLinearListYear.SelectedText, "/", this.WinFormsLinearListMonth.SelectedIndex + 1, "/", ((WinFormsPersianDatePickerDayItem)sender).Caption };
        this.SelectedDate = string.Concat(selectedText);
    }

    private void WinFormsLinearListMonth_WinFormsLinearListIndexChange(object sender, WinFormsLinearList.WinFormsLinearListEventArgs e)
    {
        DateTime dateTime = this.persianCalendar.ToDateTime(int.Parse(this.WinFormsLinearListYear.SelectedText), this.WinFormsLinearListMonth.SelectedIndex + 1, 1, 1, 1, 1, 1);
        this.setDays(dateTime);
        object[] selectedText = new object[] { this.WinFormsLinearListYear.SelectedText, "/", this.WinFormsLinearListMonth.SelectedIndex + 1, "/", this.days[this.CalendarSelectedIndex].Caption };
        this.SelectedDate = string.Concat(selectedText);
    }

    private void WinFormsPersianCalendar_Deactivate(object sender, EventArgs e)
    {
        base.Visible = false;
    }

    private void WinFormsPersianDatePickerDayItem1_MouseDown(object sender, MouseEventArgs e)
    {
        this.WinFormsPersianDatePickerDayItemToday.Selected = false;
    }

    private void WinFormsPersianDatePickerDayItem1_MouseUp(object sender, MouseEventArgs e)
    {
        this.WinFormsPersianDatePickerDayItemToday.Selected = true;
    }

    private void WinFormsPersianDatePickerDayItemToday_AbrDayItemChecked(object sender, MouseEventArgs e)
    {
        string selectedDate = this.SelectedDate;
        object[] year = new object[] { this.persianCalendar.GetYear(DateTime.Today), "/", this.persianCalendar.GetMonth(DateTime.Today), "/", this.persianCalendar.GetDayOfMonth(DateTime.Today) };
        if (selectedDate != string.Concat(year))
        {
            this.SelectDate(DateTime.Today);
            year = new object[] { this.persianCalendar.GetYear(DateTime.Today), "/", this.persianCalendar.GetMonth(DateTime.Today), "/", this.persianCalendar.GetDayOfMonth(DateTime.Today) };
            this.SelectedDate = string.Concat(year);
        }
    }

    private void AddMonthsAndYearsToLists()
    {
        this.WinFormsLinearListMonth.Items.Clear();
        this.WinFormsLinearListYear.Items.Clear();
        this.WinFormsLinearListMonth.Items.Add("فروردین");
        this.WinFormsLinearListMonth.Items.Add("اردیبهشت");
        this.WinFormsLinearListMonth.Items.Add("خرداد");
        this.WinFormsLinearListMonth.Items.Add("تیر");
        this.WinFormsLinearListMonth.Items.Add("مرداد");
        this.WinFormsLinearListMonth.Items.Add("شهریور");
        this.WinFormsLinearListMonth.Items.Add("مهر");
        this.WinFormsLinearListMonth.Items.Add("آبان");
        this.WinFormsLinearListMonth.Items.Add("آذر");
        this.WinFormsLinearListMonth.Items.Add("دی");
        this.WinFormsLinearListMonth.Items.Add("بهمن");
        this.WinFormsLinearListMonth.Items.Add("اسفند");
        for (int i = this.StartYear; i <= this.EndYear; i++)
        {
            this.WinFormsLinearListYear.Items.Add(i.ToString());
        }
    }

    private void CalculateComponentSizeAndLocation()
    {
        int width = (base.Width - 7 * this.WinFormsPersianDatePickerDayItemSH.Width) / 8;
        int num = this.WinFormsPersianDatePickerDayItemCH.Width;
        this.WinFormsLinearListMonth.Size = new Size(2 * (base.Width / 3) - base.Width / 10, 23);
        this.WinFormsLinearListYear.Size = new Size(base.Width / 3 - base.Width / 30, 23);
        this.WinFormsLinearListMonth.Location = new Point((base.Width - this.WinFormsLinearListMonth.Width - this.WinFormsLinearListYear.Width) / 2, 10);
        this.WinFormsLinearListYear.Location = new Point((base.Width - this.WinFormsLinearListMonth.Width - this.WinFormsLinearListYear.Width) / 2 + this.WinFormsLinearListMonth.Width, 10);
        WinFormsPersianDatePickerDayItem point = this.WinFormsPersianDatePickerDayItemJO;
        Point location = this.WinFormsLinearListMonth.Location;
        point.Location = new Point(width, location.Y + this.WinFormsLinearListMonth.Height + 5);
        WinFormsPersianDatePickerDayItem WinFormsPersianDatePickerDayItem = this.WinFormsPersianDatePickerDayItemPA;
        location = this.WinFormsLinearListMonth.Location;
        WinFormsPersianDatePickerDayItem.Location = new Point(2 * width + num, location.Y + this.WinFormsLinearListMonth.Height + 5);
        WinFormsPersianDatePickerDayItem point1 = this.WinFormsPersianDatePickerDayItemCH;
        location = this.WinFormsLinearListMonth.Location;
        point1.Location = new Point(3 * width + 2 * num, location.Y + this.WinFormsLinearListMonth.Height + 5);
        WinFormsPersianDatePickerDayItem WinFormsPersianDatePickerDayItem1 = this.WinFormsPersianDatePickerDayItemSE;
        location = this.WinFormsLinearListMonth.Location;
        WinFormsPersianDatePickerDayItem1.Location = new Point(4 * width + 3 * num, location.Y + this.WinFormsLinearListMonth.Height + 5);
        WinFormsPersianDatePickerDayItem point2 = this.WinFormsPersianDatePickerDayItemDO;
        location = this.WinFormsLinearListMonth.Location;
        point2.Location = new Point(5 * width + 4 * num, location.Y + this.WinFormsLinearListMonth.Height + 5);
        WinFormsPersianDatePickerDayItem WinFormsPersianDatePickerDayItem2 = this.WinFormsPersianDatePickerDayItemYE;
        location = this.WinFormsLinearListMonth.Location;
        WinFormsPersianDatePickerDayItem2.Location = new Point(6 * width + 5 * num, location.Y + this.WinFormsLinearListMonth.Height + 5);
        WinFormsPersianDatePickerDayItem point3 = this.WinFormsPersianDatePickerDayItemSH;
        location = this.WinFormsLinearListMonth.Location;
        point3.Location = new Point(7 * width + 6 * num, location.Y + this.WinFormsLinearListMonth.Height + 5);
        for (int i = 0; i < 42; i++)
        {
            if (this.days != null)
            {
                WinFormsPersianDatePickerDayItem WinFormsPersianDatePickerDayItem3 = this.days[i];
                int num1 = (1 + (6 - i % 7)) * width + num * (6 - i % 7);
                location = this.WinFormsPersianDatePickerDayItemSH.Location;
                WinFormsPersianDatePickerDayItem3.Location = new Point(num1, location.Y + this.WinFormsPersianDatePickerDayItemSH.Height + 20 + (this.WinFormsPersianDatePickerDayItemSH.Height + 5) * (i / 7));
            }
        }
        if (this.days != null)
        {
            WinFormsPersianDatePickerDayItem point4 = this.WinFormsPersianDatePickerDayItemToday;
            int width1 = (base.Width - this.WinFormsPersianDatePickerDayItemToday.Width) / 2;
            location = this.days[40].Location;
            point4.Location = new Point(width1, location.Y + this.days[40].Height + 20);
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

    private void DrawInnerBorder(Graphics gfx, Rectangle rc, Color clr, int dia)
    {
        rc.Inflate(-2, -2);
        GraphicsPath graphicsPath = this.RoundRect(rc, (float)dia, (float)dia, (float)dia, (float)dia);
        try
        {
            Pen pen = new Pen(clr);
            try
            {
                gfx.SmoothingMode = SmoothingMode.HighQuality;
                gfx.CompositingQuality = CompositingQuality.HighQuality;
                gfx.DrawPath(pen, graphicsPath);
            }
            finally
            {
                if (pen != null)
                {
                    ((IDisposable)pen).Dispose();
                }
            }
        }
        finally
        {
            if (graphicsPath != null)
            {
                ((IDisposable)graphicsPath).Dispose();
            }
        }
    }

    private void DrawOuterBorder(Graphics gfx, Rectangle rc, Color clr, int dia)
    {
        rc.Inflate(-1, -1);
        GraphicsPath graphicsPath = this.RoundRect(rc, (float)dia, (float)dia, (float)dia, (float)dia);
        try
        {
            Pen pen = new Pen(clr);
            try
            {
                gfx.SmoothingMode = SmoothingMode.HighQuality;
                gfx.CompositingQuality = CompositingQuality.HighQuality;
                gfx.DrawPath(pen, graphicsPath);
            }
            finally
            {
                if (pen != null)
                {
                    ((IDisposable)pen).Dispose();
                }
            }
        }
        finally
        {
            if (graphicsPath != null)
            {
                ((IDisposable)graphicsPath).Dispose();
            }
        }
    }

    private DateTime GetDateTime(string pdate)
    {
        PersianCalendar persianCalendar = new PersianCalendar();
        string str = pdate.Substring(5);
        int num = int.Parse(pdate.Substring(0, 4));
        int num1 = int.Parse(str.Substring(0, str.IndexOf("/")));
        str = str.Substring(0);
        str = str.Substring(str.IndexOf("/") + 1);
        int num2 = int.Parse(str);
        DateTime dateTime = persianCalendar.ToDateTime(num, num1, num2, 1, 1, 1, 1);
        return dateTime;
    }

    private void initializeAbrDatePickerContents(DateTime date)
    {
        this.days = new WinFormsPersianDatePickerDayItem[42];
        int width = (base.Width - 7 * this.WinFormsPersianDatePickerDayItemSH.Width) / 8;
        int num = this.WinFormsPersianDatePickerDayItemCH.Width;
        this.CalendarHolidays = new WinFormsPersianCalendar.DaysOfWeek[1];
        this.CalendarHolidays.SetValue(WinFormsPersianCalendar.DaysOfWeek.JOME, 0);
        for (int i = 0; i < 42; i++)
        {
            this.days[i] = new WinFormsPersianDatePickerDayItem();
            this.days[i].ItemFont = this.ItemsFont;
            this.days[i].BackColor = Color.Transparent;
            this.days[i].Name = string.Concat(i);
            WinFormsPersianDatePickerDayItem point = this.days[i];
            int num1 = (1 + (6 - i % 7)) * width + num * (6 - i % 7);
            Point location = this.WinFormsPersianDatePickerDayItemSH.Location;
            point.Location = new Point(num1, location.Y + this.WinFormsPersianDatePickerDayItemSH.Height + 20 + (this.WinFormsPersianDatePickerDayItemSH.Height + 5) * (i / 7));
            base.Controls.Add(this.days[i]);
            this.days[i].AbrDayItemChecked += new WinFormsPersianDatePickerDayItem.AbrDayItemCheckedHandler(this.abrDayItem_AbrDayItemChecked);
        }
        this.persianCalendar = new PersianCalendar();
        this.AddMonthsAndYearsToLists();
        this.CalculateComponentSizeAndLocation();
        this.SelectDate(date);
    }

    private void InitializeComponent()
    {
        this.WinFormsPersianDatePickerDayItemToday = new WinFormsPersianDatePickerDayItem();
        this.WinFormsPersianDatePickerDayItemJO = new WinFormsPersianDatePickerDayItem();
        this.WinFormsPersianDatePickerDayItemPA = new WinFormsPersianDatePickerDayItem();
        this.WinFormsPersianDatePickerDayItemCH = new WinFormsPersianDatePickerDayItem();
        this.WinFormsPersianDatePickerDayItemSE = new WinFormsPersianDatePickerDayItem();
        this.WinFormsPersianDatePickerDayItemDO = new WinFormsPersianDatePickerDayItem();
        this.WinFormsPersianDatePickerDayItemYE = new WinFormsPersianDatePickerDayItem();
        this.WinFormsPersianDatePickerDayItemSH = new WinFormsPersianDatePickerDayItem();
        this.WinFormsLinearListMonth = new WinFormsLinearList();
        this.WinFormsLinearListYear = new WinFormsLinearList();
        base.SuspendLayout();
        this.WinFormsPersianDatePickerDayItemToday.Caption = "امروز";
        this.WinFormsPersianDatePickerDayItemToday.CheckedEndColor = Color.WhiteSmoke;
        this.WinFormsPersianDatePickerDayItemToday.CheckedStartColor = Color.LightBlue;
        this.WinFormsPersianDatePickerDayItemToday.DisabledTextColor = Color.Silver;
        this.WinFormsPersianDatePickerDayItemToday.EnabledTextColor = Color.Black;
        this.WinFormsPersianDatePickerDayItemToday.EndGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemToday.HoverBorderColor = Color.SkyBlue;
        this.WinFormsPersianDatePickerDayItemToday.ItemFont = new Font("B Nazanin", 15f, FontStyle.Bold);
        this.WinFormsPersianDatePickerDayItemToday.Location = new Point(43, 236);
        this.WinFormsPersianDatePickerDayItemToday.Name = "WinFormsPersianDatePickerDayItemToday";
        this.WinFormsPersianDatePickerDayItemToday.Selectable = true;
        this.WinFormsPersianDatePickerDayItemToday.Selected = true;
        this.WinFormsPersianDatePickerDayItemToday.SelectedBorderColor = Color.SteelBlue;
        this.WinFormsPersianDatePickerDayItemToday.SelectedEndColor = Color.LightBlue;
        this.WinFormsPersianDatePickerDayItemToday.SelectedStartColor = Color.LightCyan;
        this.WinFormsPersianDatePickerDayItemToday.Size = new Size(114, 23);
        this.WinFormsPersianDatePickerDayItemToday.StartGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemToday.TabIndex = 10;
        this.WinFormsPersianDatePickerDayItemToday.AbrDayItemChecked += new WinFormsPersianDatePickerDayItem.AbrDayItemCheckedHandler(this.WinFormsPersianDatePickerDayItemToday_AbrDayItemChecked);
        this.WinFormsPersianDatePickerDayItemToday.MouseDown += new MouseEventHandler(this.WinFormsPersianDatePickerDayItem1_MouseDown);
        this.WinFormsPersianDatePickerDayItemToday.MouseUp += new MouseEventHandler(this.WinFormsPersianDatePickerDayItem1_MouseUp);
        this.WinFormsPersianDatePickerDayItemJO.BackColor = Color.Transparent;
        this.WinFormsPersianDatePickerDayItemJO.Caption = "ج";
        this.WinFormsPersianDatePickerDayItemJO.CheckedEndColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemJO.CheckedStartColor = Color.WhiteSmoke;
        this.WinFormsPersianDatePickerDayItemJO.DisabledTextColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemJO.EnabledTextColor = Color.Black;
        this.WinFormsPersianDatePickerDayItemJO.EndGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemJO.HoverBorderColor = Color.SkyBlue;
        this.WinFormsPersianDatePickerDayItemJO.ItemFont = new Font("B Nazanin", 15f, FontStyle.Bold);
        this.WinFormsPersianDatePickerDayItemJO.Location = new Point(16, 65);
        this.WinFormsPersianDatePickerDayItemJO.Name = "WinFormsPersianDatePickerDayItemJO";
        this.WinFormsPersianDatePickerDayItemJO.Selectable = false;
        this.WinFormsPersianDatePickerDayItemJO.Selected = false;
        this.WinFormsPersianDatePickerDayItemJO.SelectedBorderColor = Color.DimGray;
        this.WinFormsPersianDatePickerDayItemJO.SelectedEndColor = Color.LightCyan;
        this.WinFormsPersianDatePickerDayItemJO.SelectedStartColor = Color.LightBlue;
        this.WinFormsPersianDatePickerDayItemJO.Size = new Size(23, 18);
        this.WinFormsPersianDatePickerDayItemJO.StartGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemJO.TabIndex = 9;
        this.WinFormsPersianDatePickerDayItemPA.BackColor = Color.Transparent;
        this.WinFormsPersianDatePickerDayItemPA.Caption = "پ";
        this.WinFormsPersianDatePickerDayItemPA.CheckedEndColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemPA.CheckedStartColor = Color.WhiteSmoke;
        this.WinFormsPersianDatePickerDayItemPA.DisabledTextColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemPA.EnabledTextColor = Color.Black;
        this.WinFormsPersianDatePickerDayItemPA.EndGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemPA.HoverBorderColor = Color.SkyBlue;
        this.WinFormsPersianDatePickerDayItemPA.ItemFont = new Font("B Nazanin", 15f, FontStyle.Bold);
        this.WinFormsPersianDatePickerDayItemPA.Location = new Point(41, 65);
        this.WinFormsPersianDatePickerDayItemPA.Name = "WinFormsPersianDatePickerDayItemPA";
        this.WinFormsPersianDatePickerDayItemPA.Selectable = false;
        this.WinFormsPersianDatePickerDayItemPA.Selected = false;
        this.WinFormsPersianDatePickerDayItemPA.SelectedBorderColor = Color.DimGray;
        this.WinFormsPersianDatePickerDayItemPA.SelectedEndColor = Color.LightCyan;
        this.WinFormsPersianDatePickerDayItemPA.SelectedStartColor = Color.LightBlue;
        this.WinFormsPersianDatePickerDayItemPA.Size = new Size(23, 18);
        this.WinFormsPersianDatePickerDayItemPA.StartGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemPA.TabIndex = 8;
        this.WinFormsPersianDatePickerDayItemCH.BackColor = Color.Transparent;
        this.WinFormsPersianDatePickerDayItemCH.Caption = "چ";
        this.WinFormsPersianDatePickerDayItemCH.CheckedEndColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemCH.CheckedStartColor = Color.WhiteSmoke;
        this.WinFormsPersianDatePickerDayItemCH.DisabledTextColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemCH.EnabledTextColor = Color.Black;
        this.WinFormsPersianDatePickerDayItemCH.EndGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemCH.HoverBorderColor = Color.SkyBlue;
        this.WinFormsPersianDatePickerDayItemCH.ItemFont = new Font("B Nazanin", 15f, FontStyle.Bold);
        this.WinFormsPersianDatePickerDayItemCH.Location = new Point(66, 65);
        this.WinFormsPersianDatePickerDayItemCH.Name = "WinFormsPersianDatePickerDayItemCH";
        this.WinFormsPersianDatePickerDayItemCH.Selectable = false;
        this.WinFormsPersianDatePickerDayItemCH.Selected = false;
        this.WinFormsPersianDatePickerDayItemCH.SelectedBorderColor = Color.DimGray;
        this.WinFormsPersianDatePickerDayItemCH.SelectedEndColor = Color.LightCyan;
        this.WinFormsPersianDatePickerDayItemCH.SelectedStartColor = Color.LightBlue;
        this.WinFormsPersianDatePickerDayItemCH.Size = new Size(23, 18);
        this.WinFormsPersianDatePickerDayItemCH.StartGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemCH.TabIndex = 7;
        this.WinFormsPersianDatePickerDayItemSE.BackColor = Color.Transparent;
        this.WinFormsPersianDatePickerDayItemSE.Caption = "س";
        this.WinFormsPersianDatePickerDayItemSE.CheckedEndColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemSE.CheckedStartColor = Color.WhiteSmoke;
        this.WinFormsPersianDatePickerDayItemSE.DisabledTextColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemSE.EnabledTextColor = Color.Black;
        this.WinFormsPersianDatePickerDayItemSE.EndGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemSE.HoverBorderColor = Color.SkyBlue;
        this.WinFormsPersianDatePickerDayItemSE.ItemFont = new Font("B Nazanin", 15f, FontStyle.Bold);
        this.WinFormsPersianDatePickerDayItemSE.Location = new Point(91, 65);
        this.WinFormsPersianDatePickerDayItemSE.Name = "WinFormsPersianDatePickerDayItemSE";
        this.WinFormsPersianDatePickerDayItemSE.Selectable = false;
        this.WinFormsPersianDatePickerDayItemSE.Selected = false;
        this.WinFormsPersianDatePickerDayItemSE.SelectedBorderColor = Color.DimGray;
        this.WinFormsPersianDatePickerDayItemSE.SelectedEndColor = Color.LightCyan;
        this.WinFormsPersianDatePickerDayItemSE.SelectedStartColor = Color.LightBlue;
        this.WinFormsPersianDatePickerDayItemSE.Size = new Size(23, 18);
        this.WinFormsPersianDatePickerDayItemSE.StartGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemSE.TabIndex = 6;
        this.WinFormsPersianDatePickerDayItemDO.BackColor = Color.Transparent;
        this.WinFormsPersianDatePickerDayItemDO.Caption = "د";
        this.WinFormsPersianDatePickerDayItemDO.CheckedEndColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemDO.CheckedStartColor = Color.WhiteSmoke;
        this.WinFormsPersianDatePickerDayItemDO.DisabledTextColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemDO.EnabledTextColor = Color.Black;
        this.WinFormsPersianDatePickerDayItemDO.EndGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemDO.HoverBorderColor = Color.SkyBlue;
        this.WinFormsPersianDatePickerDayItemDO.ItemFont = new Font("B Nazanin", 15f, FontStyle.Bold);
        this.WinFormsPersianDatePickerDayItemDO.Location = new Point(116, 65);
        this.WinFormsPersianDatePickerDayItemDO.Name = "WinFormsPersianDatePickerDayItemDO";
        this.WinFormsPersianDatePickerDayItemDO.Selectable = false;
        this.WinFormsPersianDatePickerDayItemDO.Selected = false;
        this.WinFormsPersianDatePickerDayItemDO.SelectedBorderColor = Color.DimGray;
        this.WinFormsPersianDatePickerDayItemDO.SelectedEndColor = Color.LightCyan;
        this.WinFormsPersianDatePickerDayItemDO.SelectedStartColor = Color.LightBlue;
        this.WinFormsPersianDatePickerDayItemDO.Size = new Size(23, 18);
        this.WinFormsPersianDatePickerDayItemDO.StartGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemDO.TabIndex = 5;
        this.WinFormsPersianDatePickerDayItemYE.BackColor = Color.Transparent;
        this.WinFormsPersianDatePickerDayItemYE.Caption = "ی";
        this.WinFormsPersianDatePickerDayItemYE.CheckedEndColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemYE.CheckedStartColor = Color.WhiteSmoke;
        this.WinFormsPersianDatePickerDayItemYE.DisabledTextColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemYE.EnabledTextColor = Color.Black;
        this.WinFormsPersianDatePickerDayItemYE.EndGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemYE.HoverBorderColor = Color.SkyBlue;
        this.WinFormsPersianDatePickerDayItemYE.ItemFont = new Font("B Nazanin", 15f, FontStyle.Bold);
        this.WinFormsPersianDatePickerDayItemYE.Location = new Point(141, 65);
        this.WinFormsPersianDatePickerDayItemYE.Name = "WinFormsPersianDatePickerDayItemYE";
        this.WinFormsPersianDatePickerDayItemYE.Selectable = false;
        this.WinFormsPersianDatePickerDayItemYE.Selected = false;
        this.WinFormsPersianDatePickerDayItemYE.SelectedBorderColor = Color.DimGray;
        this.WinFormsPersianDatePickerDayItemYE.SelectedEndColor = Color.LightCyan;
        this.WinFormsPersianDatePickerDayItemYE.SelectedStartColor = Color.LightBlue;
        this.WinFormsPersianDatePickerDayItemYE.Size = new Size(23, 18);
        this.WinFormsPersianDatePickerDayItemYE.StartGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemYE.TabIndex = 4;
        this.WinFormsPersianDatePickerDayItemSH.BackColor = Color.Transparent;
        this.WinFormsPersianDatePickerDayItemSH.Caption = "ش";
        this.WinFormsPersianDatePickerDayItemSH.CheckedEndColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemSH.CheckedStartColor = Color.WhiteSmoke;
        this.WinFormsPersianDatePickerDayItemSH.DisabledTextColor = Color.LightGray;
        this.WinFormsPersianDatePickerDayItemSH.EnabledTextColor = Color.Black;
        this.WinFormsPersianDatePickerDayItemSH.EndGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemSH.HoverBorderColor = Color.SkyBlue;
        this.WinFormsPersianDatePickerDayItemSH.ItemFont = new Font("B Nazanin", 15f, FontStyle.Bold);
        this.WinFormsPersianDatePickerDayItemSH.Location = new Point(166, 65);
        this.WinFormsPersianDatePickerDayItemSH.Name = "WinFormsPersianDatePickerDayItemSH";
        this.WinFormsPersianDatePickerDayItemSH.Selectable = false;
        this.WinFormsPersianDatePickerDayItemSH.Selected = false;
        this.WinFormsPersianDatePickerDayItemSH.SelectedBorderColor = Color.DimGray;
        this.WinFormsPersianDatePickerDayItemSH.SelectedEndColor = Color.LightCyan;
        this.WinFormsPersianDatePickerDayItemSH.SelectedStartColor = Color.LightBlue;
        this.WinFormsPersianDatePickerDayItemSH.Size = new Size(23, 18);
        this.WinFormsPersianDatePickerDayItemSH.StartGradiantColor = Color.White;
        this.WinFormsPersianDatePickerDayItemSH.TabIndex = 3;
        this.WinFormsLinearListMonth.ArrowColor = Color.DimGray;
        this.WinFormsLinearListMonth.BackColor = Color.Transparent;
        this.WinFormsLinearListMonth.CheckedEndColor = Color.LightGray;
        this.WinFormsLinearListMonth.CheckedStartColor = Color.WhiteSmoke;
        this.WinFormsLinearListMonth.DisabledTextColor = Color.LightGray;
        this.WinFormsLinearListMonth.EnabledTextColor = Color.Black;
        this.WinFormsLinearListMonth.EndGradiantColor = Color.LightGray;
        this.WinFormsLinearListMonth.HoverBorderColor = Color.SkyBlue;
        this.WinFormsLinearListMonth.ItemFont = new Font("B Nazanin", 15f, FontStyle.Bold, GraphicsUnit.Point, 178);
        this.WinFormsLinearListMonth.Location = new Point(3, 35);
        this.WinFormsLinearListMonth.Name = "WinFormsLinearListMonth";
        this.WinFormsLinearListMonth.Selectable = true;
        this.WinFormsLinearListMonth.Selected = false;
        this.WinFormsLinearListMonth.SelectedArrowColor = Color.SkyBlue;
        this.WinFormsLinearListMonth.SelectedBorderColor = Color.DimGray;
        this.WinFormsLinearListMonth.SelectedEndColor = Color.FromArgb(50, 173, 216, 230);
        this.WinFormsLinearListMonth.SelectedIndex = 0;
        this.WinFormsLinearListMonth.SelectedStartColor = Color.FromArgb(50, 224, 255, 255);
        this.WinFormsLinearListMonth.Size = new Size(115, 23);
        this.WinFormsLinearListMonth.StartGradiantColor = Color.WhiteSmoke;
        this.WinFormsLinearListMonth.TabIndex = 2;
        this.WinFormsLinearListMonth.WinFormsLinearListIndexChange += new WinFormsLinearList.WinFormsLinearListIndexChangeHandler(this.WinFormsLinearListMonth_WinFormsLinearListIndexChange);
        this.WinFormsLinearListYear.ArrowColor = Color.DimGray;
        this.WinFormsLinearListYear.BackColor = Color.Transparent;
        this.WinFormsLinearListYear.CheckedEndColor = Color.LightGray;
        this.WinFormsLinearListYear.CheckedStartColor = Color.WhiteSmoke;
        this.WinFormsLinearListYear.DisabledTextColor = Color.LightGray;
        this.WinFormsLinearListYear.EnabledTextColor = Color.Black;
        this.WinFormsLinearListYear.EndGradiantColor = Color.LightGray;
        this.WinFormsLinearListYear.HoverBorderColor = Color.SkyBlue;
        this.WinFormsLinearListYear.ItemFont = new Font("B Nazanin", 15f, FontStyle.Bold, GraphicsUnit.Point, 178);
        this.WinFormsLinearListYear.Location = new Point(119, 35);
        this.WinFormsLinearListYear.MinimumSize = new Size(75, 15);
        this.WinFormsLinearListYear.Name = "WinFormsLinearListYear";
        this.WinFormsLinearListYear.Selectable = true;
        this.WinFormsLinearListYear.Selected = false;
        this.WinFormsLinearListYear.SelectedArrowColor = Color.SkyBlue;
        this.WinFormsLinearListYear.SelectedBorderColor = Color.DimGray;
        this.WinFormsLinearListYear.SelectedEndColor = Color.FromArgb(50, 173, 216, 230);
        this.WinFormsLinearListYear.SelectedIndex = 0;
        this.WinFormsLinearListYear.SelectedStartColor = Color.FromArgb(50, 224, 255, 255);
        this.WinFormsLinearListYear.Size = new Size(75, 23);
        this.WinFormsLinearListYear.StartGradiantColor = Color.WhiteSmoke;
        this.WinFormsLinearListYear.TabIndex = 1;
        this.WinFormsLinearListYear.WinFormsLinearListIndexChange += new WinFormsLinearList.WinFormsLinearListIndexChangeHandler(this.WinFormsLinearListMonth_WinFormsLinearListIndexChange);
        base.AutoScaleDimensions = new SizeF(6f, 13f);
        base.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.Snow;
        base.ClientSize = new Size(200, 270);
        base.Controls.Add(this.WinFormsPersianDatePickerDayItemToday);
        base.Controls.Add(this.WinFormsPersianDatePickerDayItemJO);
        base.Controls.Add(this.WinFormsPersianDatePickerDayItemPA);
        base.Controls.Add(this.WinFormsPersianDatePickerDayItemCH);
        base.Controls.Add(this.WinFormsPersianDatePickerDayItemSE);
        base.Controls.Add(this.WinFormsPersianDatePickerDayItemDO);
        base.Controls.Add(this.WinFormsPersianDatePickerDayItemYE);
        base.Controls.Add(this.WinFormsPersianDatePickerDayItemSH);
        base.Controls.Add(this.WinFormsLinearListMonth);
        base.Controls.Add(this.WinFormsLinearListYear);
        base.FormBorderStyle = FormBorderStyle.None;
        this.MaximumSize = new Size(400, 420);
        this.MinimumSize = new Size(200, 270);
        base.Name = "WinFormsPersianCalendar";
        base.ShowIcon = false;
        base.ShowInTaskbar = false;
        base.StartPosition = FormStartPosition.Manual;
        base.TransparencyKey = Color.Snow;
        base.Deactivate += new EventHandler(this.WinFormsPersianCalendar_Deactivate);
        base.ResumeLayout(false);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics graphics = e.Graphics;
        float x = (float)base.AutoScrollPosition.X;
        Point autoScrollPosition = base.AutoScrollPosition;
        graphics.TranslateTransform(x, (float)autoScrollPosition.Y);
        Rectangle rectangle = new Rectangle(0, 0, base.Width, base.Height);
        LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, this.PanelBackStartColor, this.PanelBackEndColor, LinearGradientMode.Vertical);
        this.PaintRoundedRectangle(graphics, linearGradientBrush, this.is3DBorder, 0, 1, base.Width, base.Height - 1, 50);
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        Pen pen = new Pen(this.DateSelectorOuterBorderColor);
        int num = this.WinFormsPersianDatePickerDayItemJO.Location.X;
        autoScrollPosition = this.WinFormsPersianDatePickerDayItemCH.Location;
        Point point = new Point(num, autoScrollPosition.Y + this.WinFormsPersianDatePickerDayItemCH.Height + 5);
        autoScrollPosition = this.WinFormsPersianDatePickerDayItemSH.Location;
        int x1 = autoScrollPosition.X + this.WinFormsPersianDatePickerDayItemSH.Width;
        autoScrollPosition = this.WinFormsPersianDatePickerDayItemCH.Location;
        graphics.DrawLine(pen, point, new Point(x1, autoScrollPosition.Y + this.WinFormsPersianDatePickerDayItemCH.Height + 5));
        Pen pen1 = new Pen(this.DateSelectorInneerBorderColor);
        int num1 = this.WinFormsPersianDatePickerDayItemJO.Location.X;
        autoScrollPosition = this.WinFormsPersianDatePickerDayItemCH.Location;
        Point point1 = new Point(num1, 1 + autoScrollPosition.Y + this.WinFormsPersianDatePickerDayItemCH.Height + 5);
        autoScrollPosition = this.WinFormsPersianDatePickerDayItemSH.Location;
        int x2 = autoScrollPosition.X + this.WinFormsPersianDatePickerDayItemSH.Width;
        autoScrollPosition = this.WinFormsPersianDatePickerDayItemCH.Location;
        graphics.DrawLine(pen1, point1, new Point(x2, 1 + autoScrollPosition.Y + this.WinFormsPersianDatePickerDayItemCH.Height + 5));
        Pen pen2 = new Pen(this.DateSelectorOuterBorderColor);
        int num2 = this.WinFormsPersianDatePickerDayItemJO.Location.X;
        autoScrollPosition = this.days[36].Location;
        Point point2 = new Point(num2, autoScrollPosition.Y + this.days[36].Height + 10);
        autoScrollPosition = this.WinFormsPersianDatePickerDayItemSH.Location;
        int x3 = autoScrollPosition.X + this.WinFormsPersianDatePickerDayItemSH.Width;
        autoScrollPosition = this.days[36].Location;
        graphics.DrawLine(pen2, point2, new Point(x3, autoScrollPosition.Y + this.days[36].Height + 10));
        Pen pen3 = new Pen(this.DateSelectorInneerBorderColor);
        int num3 = this.WinFormsPersianDatePickerDayItemJO.Location.X;
        autoScrollPosition = this.days[36].Location;
        Point point3 = new Point(num3, 1 + autoScrollPosition.Y + this.days[36].Height + 10);
        autoScrollPosition = this.WinFormsPersianDatePickerDayItemSH.Location;
        int x4 = autoScrollPosition.X + this.WinFormsPersianDatePickerDayItemSH.Width;
        autoScrollPosition = this.days[36].Location;
        graphics.DrawLine(pen3, point3, new Point(x4, 1 + autoScrollPosition.Y + this.days[36].Height + 10));
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        this.CalculateComponentSizeAndLocation();
        base.Invalidate();
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
        this.isVisible = false;
        if (base.Visible)
        {
            this.SelectDate(this.GetDateTime(this.CalendarSelectedDate));
        }
        base.OnVisibleChanged(e);
        this.isVisible = base.Visible;
    }

    private void PaintRoundedRectangle(Graphics g, Brush fillBrush, bool is3D, int x, int y, int width, int height, int arcRadius)
    {
        g.SmoothingMode = SmoothingMode.Default;
        Rectangle rectangle = new Rectangle(x, y, width, height);
        Pen pen = new Pen(this.DateSelectorOuterBorderColor);
        g.FillRectangle(fillBrush, x + arcRadius / 2, y, width - arcRadius, height);
        g.FillRectangle(fillBrush, x, y + arcRadius / 2, width, height - arcRadius);
        g.FillPie(fillBrush, x, y, arcRadius, arcRadius, -90, -90);
        g.FillPie(fillBrush, x, y + height - arcRadius, arcRadius, arcRadius, 180, -90);
        g.FillPie(fillBrush, x + width - arcRadius, y, arcRadius, arcRadius, -90, 90);
        g.FillPie(fillBrush, x + width - arcRadius, y + height - arcRadius, arcRadius, arcRadius, 0, 90);
        g.SmoothingMode = SmoothingMode.HighQuality;
        if (is3D)
        {
            this.DrawInnerBorder(g, rectangle, this.DateSelectorInneerBorderColor, arcRadius / 2);
            this.DrawOuterBorder(g, rectangle, this.DateSelectorOuterBorderColor, arcRadius / 2);
        }
        else
        {
            g.DrawLine(pen, x + arcRadius / 2, y, x + width - arcRadius / 2, y);
            g.DrawLine(pen, x + arcRadius / 2, y + height, x + width - arcRadius / 2, y + height);
            g.DrawLine(pen, x, y + arcRadius / 2, x, y + height - arcRadius / 2);
            g.DrawLine(pen, x + width, y + arcRadius / 2, x + width, y + height - arcRadius / 2);
            g.DrawArc(pen, x, y, arcRadius, arcRadius, -90, -90);
            g.DrawArc(pen, x, y + height - arcRadius, arcRadius, arcRadius, 180, -90);
            g.DrawArc(pen, x + width - arcRadius, y, arcRadius, arcRadius, -90, 90);
            g.DrawArc(pen, x + width - arcRadius, y + height - arcRadius, arcRadius, arcRadius, 0, 90);
        }
    }

    private GraphicsPath RoundRect(RectangleF r, float r1, float r2, float r3, float r4)
    {
        float x = r.X;
        float y = r.Y;
        float width = r.Width;
        float height = r.Height;
        GraphicsPath graphicsPath = new GraphicsPath();
        graphicsPath.AddBezier(x, y + r1, x, y, x + r1, y, x + r1, y);
        graphicsPath.AddLine(x + r1, y, x + width - r2, y);
        graphicsPath.AddBezier(x + width - r2, y, x + width, y, x + width, y + r2, x + width, y + r2);
        graphicsPath.AddLine(x + width, y + r2, x + width, y + height - r3);
        graphicsPath.AddBezier(x + width, y + height - r3, x + width, y + height, x + width - r3, y + height, x + width - r3, y + height);
        graphicsPath.AddLine(x + width - r3, y + height, x + r4, y + height);
        graphicsPath.AddBezier(x + r4, y + height, x, y + height, x, y + height - r4, x, y + height - r4);
        graphicsPath.AddLine(x, y + height - r4, x, y + r1);
        return graphicsPath;
    }

    private void SelectDate(DateTime date)
    {
        DateTime dateTime = date;
        int num = 0;
        object[] year = new object[] { this.persianCalendar.GetYear(dateTime), "/", this.persianCalendar.GetMonth(dateTime), "/", this.persianCalendar.GetDayOfMonth(dateTime) };
        this.CalendarSelectedDate = string.Concat(year);
        this.WinFormsLinearListMonth.SelectedIndex = this.persianCalendar.GetMonth(dateTime) - 1;
        this.WinFormsLinearListYear.SelectedIndex = this.persianCalendar.GetYear(dateTime) - this.StartOfYears;
        this.days[this.CalendarSelectedIndex].Selected = false;
        switch (this.persianCalendar.GetDayOfWeek(this.persianCalendar.ToDateTime(this.persianCalendar.GetYear(dateTime), this.persianCalendar.GetMonth(dateTime), 1, 1, 1, 1, 1)))
        {
            case DayOfWeek.Sunday:
                {
                    num = 1;
                    break;
                }
            case DayOfWeek.Monday:
                {
                    num = 2;
                    break;
                }
            case DayOfWeek.Tuesday:
                {
                    num = 3;
                    break;
                }
            case DayOfWeek.Wednesday:
                {
                    num = 4;
                    break;
                }
            case DayOfWeek.Thursday:
                {
                    num = 5;
                    break;
                }
            case DayOfWeek.Friday:
                {
                    num = 6;
                    break;
                }
            case DayOfWeek.Saturday:
                {
                    num = 0;
                    break;
                }
        }
        this.days[this.persianCalendar.GetDayOfMonth(dateTime) + num - 1].Selected = true;
        this.CalendarSelectedIndex = this.persianCalendar.GetDayOfMonth(dateTime) + num - 1;
    }

    private void setDays(DateTime dt)
    {
        int month = this.persianCalendar.GetMonth(dt);
        int year = this.persianCalendar.GetYear(dt);
        int width = (base.Width - 7 * this.WinFormsPersianDatePickerDayItemSH.Width) / 8;
        int num = this.WinFormsPersianDatePickerDayItemCH.Width;
        int daysInMonth = this.persianCalendar.GetDaysInMonth(year, month);
        int num1 = (month > 1 ? this.persianCalendar.GetDaysInMonth(year, month - 1) : this.persianCalendar.GetDaysInMonth(year - 1, 12));
        switch (this.persianCalendar.GetDayOfWeek(this.persianCalendar.ToDateTime(this.persianCalendar.GetYear(dt), this.persianCalendar.GetMonth(dt), 1, 1, 1, 1, 1)))
        {
            case DayOfWeek.Sunday:
                {
                    this.startMonthDay = 1;
                    break;
                }
            case DayOfWeek.Monday:
                {
                    this.startMonthDay = 2;
                    break;
                }
            case DayOfWeek.Tuesday:
                {
                    this.startMonthDay = 3;
                    break;
                }
            case DayOfWeek.Wednesday:
                {
                    this.startMonthDay = 4;
                    break;
                }
            case DayOfWeek.Thursday:
                {
                    this.startMonthDay = 5;
                    break;
                }
            case DayOfWeek.Friday:
                {
                    this.startMonthDay = 6;
                    break;
                }
            case DayOfWeek.Saturday:
                {
                    this.startMonthDay = 0;
                    break;
                }
        }
        for (int i = 0; i < 42; i++)
        {
            this.days[i].Caption = string.Concat((daysInMonth + (i - this.startMonthDay)) % daysInMonth + 1);
            if (i > this.startMonthDay + daysInMonth - 1)
            {
                this.days[i].Enabled = false;
            }
            else if (i >= this.startMonthDay)
            {
                this.days[i].Enabled = true;
            }
            else
            {
                this.days[i].Enabled = false;
            }
            if (this.CalendarHolidays.Contains<WinFormsPersianCalendar.DaysOfWeek>(WinFormsPersianCalendar.DaysOfWeek.JOME))
            {
                if (this.days[i].Location.X == this.WinFormsPersianDatePickerDayItemJO.Location.X)
                {
                    this.days[i].EnabledTextColor = Color.Red;
                }
            }
            if (this.CalendarHolidays.Contains<WinFormsPersianCalendar.DaysOfWeek>(WinFormsPersianCalendar.DaysOfWeek.SHANBE))
            {
                if (this.days[i].Location.X == this.WinFormsPersianDatePickerDayItemSH.Location.X)
                {
                    this.days[i].EnabledTextColor = Color.Red;
                }
            }
            if (this.CalendarHolidays.Contains<WinFormsPersianCalendar.DaysOfWeek>(WinFormsPersianCalendar.DaysOfWeek.YEK_SHANBE))
            {
                if (this.days[i].Location.X == this.WinFormsPersianDatePickerDayItemYE.Location.X)
                {
                    this.days[i].EnabledTextColor = Color.Red;
                }
            }
            if (this.CalendarHolidays.Contains<WinFormsPersianCalendar.DaysOfWeek>(WinFormsPersianCalendar.DaysOfWeek.DO_SHANBE))
            {
                if (this.days[i].Location.X == this.WinFormsPersianDatePickerDayItemDO.Location.X)
                {
                    this.days[i].EnabledTextColor = Color.Red;
                }
            }
            if (this.CalendarHolidays.Contains<WinFormsPersianCalendar.DaysOfWeek>(WinFormsPersianCalendar.DaysOfWeek.SE_SHANBE))
            {
                if (this.days[i].Location.X == this.WinFormsPersianDatePickerDayItemSE.Location.X)
                {
                    this.days[i].EnabledTextColor = Color.Red;
                }
            }
            if (this.CalendarHolidays.Contains<WinFormsPersianCalendar.DaysOfWeek>(WinFormsPersianCalendar.DaysOfWeek.CHAHAR_SHANBE))
            {
                if (this.days[i].Location.X == this.WinFormsPersianDatePickerDayItemCH.Location.X)
                {
                    this.days[i].EnabledTextColor = Color.Red;
                }
            }
            if (this.CalendarHolidays.Contains<WinFormsPersianCalendar.DaysOfWeek>(WinFormsPersianCalendar.DaysOfWeek.PANJ_SHANBE))
            {
                if (this.days[i].Location.X == this.WinFormsPersianDatePickerDayItemPA.Location.X)
                {
                    this.days[i].EnabledTextColor = Color.Red;
                }
            }
        }
        this.days[this.CalendarSelectedIndex].Selected = true;
    }

    private void setHolidays()
    {
        WinFormsPersianCalendar.DaysOfWeek[] calendarHolidays = this.CalendarHolidays;
        for (int i = 0; i < (int)calendarHolidays.Length; i++)
        {
            WinFormsPersianCalendar.DaysOfWeek daysOfWeek = calendarHolidays[i];
        }
    }

    public event WinFormsPersianCalendar.AbrCalendarSelectedDateChangeEventHandler AbrCalendarSelectedDateChange;

    public delegate void AbrCalendarSelectedDateChangeEventHandler(object sender, WinFormsPersianCalendar.AbrSelectedDateChangesEventArgs e);

    public class AbrSelectedDateChangesEventArgs : EventArgs
    {
        private string selectedDate;

        public string SelectedDate
        {
            get
            {
                return this.selectedDate;
            }
        }

        public AbrSelectedDateChangesEventArgs(string date)
        {
            this.selectedDate = date;
        }
    }

    public enum DaysOfWeek
    {
        SHANBE,
        YEK_SHANBE,
        DO_SHANBE,
        SE_SHANBE,
        CHAHAR_SHANBE,
        PANJ_SHANBE,
        JOME
    }
}