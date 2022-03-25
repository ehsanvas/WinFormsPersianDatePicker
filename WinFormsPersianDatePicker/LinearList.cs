using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace AbrAfzarGostaran
{
    [DefaultEvent("WinFormsLinearListIndexChange")]
    public class WinFormsLinearList : UserControl
    {
        private Color ItemHoverBorderColor = Color.SkyBlue;

        private Color ItemSelectedBorderColor = Color.DimGray;

        private Color ItemStartGradiantColor = Color.WhiteSmoke;

        private Color ItemEndGradiantColor = Color.LightGray;

        private Color ItemSelectedStartColor = Color.FromArgb(50, Color.LightCyan);

        private Color ItemSelectedEndColor = Color.FromArgb(50, Color.LightBlue);

        private Color ItemCheckedStartColor = Color.WhiteSmoke;

        private Color ItemCheckedEndColor = Color.LightGray;

        private Color ItemEnabledTextColor = Color.Black;

        private Color ItemDisabledTextColor = Color.LightGray;

        private Color ItemArrowColor = Color.DimGray;

        private Color ItemSelectedArrowColor = Color.SkyBlue;

        private Font ItemTextFont = new Font("B Nazanin", 15f, FontStyle.Bold);

        private bool isSelected = false;

        private bool isFocused = false;

        private bool isSelectable = true;

        private bool isHover = false;

        private int ArcYDiameter;

        private MouseOnArrowsState CurrentMousePosition;

        private LinearListItems ListItems;

        private int currentItem;

        private WinFormsLinearList.DatePart selectionpart = WinFormsLinearList.DatePart.NONE;

        private IContainer components = null;

        private Timer timerLinearList;

        public Color ArrowColor
        {
            get
            {
                return this.ItemArrowColor;
            }
            set
            {
                this.ItemArrowColor = value;
                base.Invalidate();
            }
        }

        public Color CheckedEndColor
        {
            get
            {
                return this.ItemCheckedEndColor;
            }
            set
            {
                this.ItemCheckedEndColor = value;
                base.Invalidate();
            }
        }

        public Color CheckedStartColor
        {
            get
            {
                return this.ItemCheckedStartColor;
            }
            set
            {
                this.ItemCheckedStartColor = value;
                base.Invalidate();
            }
        }

        public Color DisabledTextColor
        {
            get
            {
                return this.ItemDisabledTextColor;
            }
            set
            {
                this.ItemDisabledTextColor = value;
                base.Invalidate();
            }
        }

        public Color EnabledTextColor
        {
            get
            {
                return this.ItemEnabledTextColor;
            }
            set
            {
                this.ItemEnabledTextColor = value;
                base.Invalidate();
            }
        }

        public Color EndGradiantColor
        {
            get
            {
                return this.ItemEndGradiantColor;
            }
            set
            {
                this.ItemEndGradiantColor = value;
                base.Invalidate();
            }
        }

        public Color HoverBorderColor
        {
            get
            {
                return this.ItemHoverBorderColor;
            }
            set
            {
                this.ItemHoverBorderColor = value;
                base.Invalidate();
            }
        }

        public bool IsFocused
        {
            get
            {
                return this.isFocused;
            }
            set
            {
                this.isFocused = value;
                base.Invalidate();
            }
        }

        public Font ItemFont
        {
            get
            {
                return this.ItemTextFont;
            }
            set
            {
                this.ItemTextFont = value;
                base.Invalidate();
            }
        }

        public LinearListItems Items
        {
            get
            {
                return this.ListItems;
            }
        }

        public bool Selectable
        {
            get
            {
                return this.isSelectable;
            }
            set
            {
                this.isSelectable = value;
                base.Invalidate();
            }
        }

        public bool Selected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
                base.Invalidate();
            }
        }

        public Color SelectedArrowColor
        {
            get
            {
                return this.ItemSelectedArrowColor;
            }
            set
            {
                this.ItemSelectedArrowColor = value;
                base.Invalidate();
            }
        }

        public Color SelectedBorderColor
        {
            get
            {
                return this.ItemSelectedBorderColor;
            }
            set
            {
                this.ItemSelectedBorderColor = value;
                base.Invalidate();
            }
        }

        public Color SelectedEndColor
        {
            get
            {
                return this.ItemSelectedEndColor;
            }
            set
            {
                this.ItemSelectedEndColor = value;
                base.Invalidate();
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this.currentItem;
            }
            set
            {
                this.currentItem = value;
                if (this.WinFormsLinearListIndexChange != null)
                {
                    this.WinFormsLinearListIndexChange(this, new WinFormsLinearList.WinFormsLinearListEventArgs(this.ListItems[value]));
                }
                base.Invalidate();
            }
        }

        public Color SelectedStartColor
        {
            get
            {
                return this.ItemSelectedStartColor;
            }
            set
            {
                this.ItemSelectedStartColor = value;
                base.Invalidate();
            }
        }

        public string SelectedText
        {
            get
            {
                return this.ListItems[this.SelectedIndex].Text;
            }
        }

        public Color StartGradiantColor
        {
            get
            {
                return this.ItemStartGradiantColor;
            }
            set
            {
                this.ItemStartGradiantColor = value;
                base.Invalidate();
            }
        }

        public WinFormsLinearList()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            this.ArcYDiameter = base.Height - 1;
            this.ListItems = new LinearListItems(this);
            this.currentItem = 0;
        }

        public WinFormsLinearList(string sText)
        {
            this.ListItems.Add(sText);
        }

        private void WinFormsLinearList_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.CurrentMousePosition == MouseOnArrowsState.OnCenter && this.WinFormsLinearListMiddleMouseClick != null)
            {
                this.WinFormsLinearListMiddleMouseClick(this, e);
            }
        }

        private void WinFormsLinearList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.CurrentMousePosition == MouseOnArrowsState.OnLeft)
                {
                    if (this.SelectedIndex > 0)
                    {
                        WinFormsLinearList selectedIndex = this;
                        selectedIndex.SelectedIndex = selectedIndex.SelectedIndex - 1;
                        this.InvalidateLeftOrRightOrMiddle(MouseOnArrowsState.OnCenter);
                    }
                }
                if (this.CurrentMousePosition == MouseOnArrowsState.OnRight)
                {
                    if (this.SelectedIndex < this.ListItems.Count - 1)
                    {
                        WinFormsLinearList WinFormsLinearList = this;
                        WinFormsLinearList.SelectedIndex = WinFormsLinearList.SelectedIndex + 1;
                        this.InvalidateLeftOrRightOrMiddle(MouseOnArrowsState.OnCenter);
                    }
                }
                if (this.CurrentMousePosition == MouseOnArrowsState.OnCenter)
                {
                    this.isHover = false;
                    base.Invalidate();
                }
            }
        }

        private void WinFormsLinearList_MouseMove(object sender, MouseEventArgs e)
        {
            MouseOnArrowsState currentMousePosition = this.CurrentMousePosition;
            this.CurrentMousePosition = this.getMouseState(e.X, e.Y);
            if (currentMousePosition != this.CurrentMousePosition)
            {
                this.InvalidateLeftOrRightOrMiddle(this.CurrentMousePosition);
                if ((this.CurrentMousePosition == MouseOnArrowsState.NONE ? true : this.CurrentMousePosition == MouseOnArrowsState.OnCenter))
                {
                    this.InvalidateLeftOrRightOrMiddle(currentMousePosition);
                }
            }
        }

        private void WinFormsLinearList_MouseUp(object sender, MouseEventArgs e)
        {
            this.timerLinearList.Stop();
            if (this.CurrentMousePosition != MouseOnArrowsState.OnCenter)
            {
                this.InvalidateLeftOrRightOrMiddle(this.CurrentMousePosition);
            }
            else
            {
                this.isHover = true;
                base.Invalidate();
            }
        }

        private void WinFormsPersianDatePickerDayItem_MouseEnter(object sender, EventArgs e)
        {
            this.isHover = true;
            base.Invalidate();
        }

        private void WinFormsPersianDatePickerDayItem_MouseLeave(object sender, EventArgs e)
        {
            this.isHover = false;
            this.CurrentMousePosition = MouseOnArrowsState.NONE;
            if (!this.isSelected)
            {
                base.Invalidate();
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

        private MouseOnArrowsState getMouseState(int x, int y)
        {
            MouseOnArrowsState mouseOnArrowsState;
            if (!(y > base.Height ? false : y >= 0))
            {
                mouseOnArrowsState = MouseOnArrowsState.NONE;
            }
            else if (!(x >= this.ArcYDiameter ? true : x <= 0))
            {
                mouseOnArrowsState = MouseOnArrowsState.OnLeft;
            }
            else if ((x <= base.Width - this.ArcYDiameter ? true : x >= base.Width))
            {
                mouseOnArrowsState = ((x <= this.ArcYDiameter ? true : x >= base.Width - this.ArcYDiameter) ? MouseOnArrowsState.NONE : MouseOnArrowsState.OnCenter);
            }
            else
            {
                mouseOnArrowsState = MouseOnArrowsState.OnRight;
            }
            return mouseOnArrowsState;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.timerLinearList = new Timer(this.components);
            base.SuspendLayout();
            this.timerLinearList.Tick += new EventHandler(this.timerLinearList_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Transparent;
            base.Name = "WinFormsLinearList";
            base.Size = new Size(164, 29);
            base.MouseLeave += new EventHandler(this.WinFormsPersianDatePickerDayItem_MouseLeave);
            base.MouseMove += new MouseEventHandler(this.WinFormsLinearList_MouseMove);
            base.MouseClick += new MouseEventHandler(this.WinFormsLinearList_MouseClick);
            base.MouseDown += new MouseEventHandler(this.WinFormsLinearList_MouseDown);
            base.MouseUp += new MouseEventHandler(this.WinFormsLinearList_MouseUp);
            base.MouseEnter += new EventHandler(this.WinFormsPersianDatePickerDayItem_MouseEnter);
            base.ResumeLayout(false);
        }

        private void InvalidateLeftOrRightOrMiddle(MouseOnArrowsState position)
        {
            if (position == MouseOnArrowsState.OnLeft)
            {
                base.Invalidate(new Rectangle(0, 0, this.ArcYDiameter, this.ArcYDiameter));
            }
            if (position == MouseOnArrowsState.OnRight)
            {
                base.Invalidate(new Rectangle(base.Width - this.ArcYDiameter, 0, this.ArcYDiameter, this.ArcYDiameter));
            }
            if (position == MouseOnArrowsState.OnCenter)
            {
                base.Invalidate(new Rectangle(this.ArcYDiameter, 0, base.Width - 2 * this.ArcYDiameter, this.ArcYDiameter));
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            this.IsFocused = true;
            base.OnGotFocus(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.IsFocused = false;
            base.OnLostFocus(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen pen;
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.TranslateTransform((float)base.AutoScrollPosition.X, (float)base.AutoScrollPosition.Y);
            Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, this.ItemStartGradiantColor, this.ItemEndGradiantColor, LinearGradientMode.Vertical);
            LinearGradientBrush linearGradientBrush1 = new LinearGradientBrush(rectangle, this.ItemSelectedStartColor, this.ItemSelectedEndColor, LinearGradientMode.Vertical);
            LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rectangle, this.ItemCheckedStartColor, this.ItemCheckedEndColor, LinearGradientMode.Vertical);
            SolidBrush solidBrush = new SolidBrush(this.ItemArrowColor);
            SolidBrush solidBrush1 = new SolidBrush(this.ItemSelectedArrowColor);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            if (this.isSelectable)
            {
                graphics.FillRectangle(linearGradientBrush2, this.ArcYDiameter / 2 - 1, 0, base.Width - this.ArcYDiameter, base.Height - 1);
                pen = new Pen(this.ItemSelectedBorderColor);
                if (this.isFocused)
                {
                    pen.DashStyle = DashStyle.Dash;
                }
                graphics.FillPie(linearGradientBrush2, 0, 0, this.ArcYDiameter, this.ArcYDiameter, -90, -180);
                graphics.FillPie(linearGradientBrush2, base.Width - this.ArcYDiameter - 2, 0, this.ArcYDiameter, base.Height - 1, -90, 180);
                graphics.DrawArc(pen, 0, 0, this.ArcYDiameter, this.ArcYDiameter, -90, -180);
                graphics.DrawArc(pen, base.Width - this.ArcYDiameter - 2, 0, this.ArcYDiameter, base.Height - 1, -90, 180);
                graphics.DrawLine(pen, this.ArcYDiameter / 2, 0, base.Width - this.ArcYDiameter / 2, 0);
                graphics.DrawLine(pen, this.ArcYDiameter / 2, base.Height - 1, base.Width - this.ArcYDiameter / 2, base.Height - 1);
            }
            if ((!this.isHover ? false : this.isSelectable))
            {
                graphics.FillRectangle(linearGradientBrush1, this.ArcYDiameter / 2, 0, base.Width - this.ArcYDiameter - 1, base.Height - 1);
                pen = new Pen(this.ItemHoverBorderColor);
                graphics.FillPie(linearGradientBrush2, 0, 0, this.ArcYDiameter, this.ArcYDiameter, -90, -180);
                graphics.FillPie(linearGradientBrush2, base.Width - this.ArcYDiameter - 2, 0, this.ArcYDiameter, base.Height - 1, -90, 180);
                graphics.DrawArc(pen, 0, 0, this.ArcYDiameter, this.ArcYDiameter, -90, -180);
                graphics.DrawArc(pen, base.Width - this.ArcYDiameter - 2, 0, this.ArcYDiameter, base.Height - 1, -90, 180);
                graphics.DrawLine(pen, this.ArcYDiameter / 2, 0, base.Width - this.ArcYDiameter / 2, 0);
                graphics.DrawLine(pen, this.ArcYDiameter / 2, base.Height - 1, base.Width - this.ArcYDiameter / 2, base.Height - 1);
            }
            Point[] point = new Point[] { new Point(this.ArcYDiameter / 3, this.ArcYDiameter / 2), new Point(3 * this.ArcYDiameter / 4, this.ArcYDiameter / 3), new Point(3 * this.ArcYDiameter / 4, 2 * this.ArcYDiameter / 3) };
            if (this.CurrentMousePosition != MouseOnArrowsState.OnLeft)
            {
                graphics.FillPolygon(solidBrush, point);
            }
            else
            {
                graphics.FillPolygon(solidBrush1, point);
            }
            point[0] = new Point(base.Width - this.ArcYDiameter / 3 - 2, this.ArcYDiameter / 2);
            point[1] = new Point(base.Width - 3 * this.ArcYDiameter / 4 - 2, this.ArcYDiameter / 3);
            point[2] = new Point(base.Width - 3 * this.ArcYDiameter / 4 - 2, 2 * this.ArcYDiameter / 3);
            if (this.CurrentMousePosition != MouseOnArrowsState.OnRight)
            {
                graphics.FillPolygon(solidBrush, point);
            }
            else
            {
                graphics.FillPolygon(solidBrush1, point);
            }
            if (this.Items.Count != 0)
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                StringFormat stringFormat = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                GraphicsPath graphicsPath = new GraphicsPath();
                graphicsPath.AddString(string.Concat(this.ListItems[this.SelectedIndex]), this.ItemFont.FontFamily, (int)this.ItemFont.Style, this.ItemFont.Size, new Rectangle(0, 0, base.Width - 1, base.Height), stringFormat);
                if (!base.Enabled)
                {
                    graphics.FillPath(new SolidBrush(this.ItemDisabledTextColor), graphicsPath);
                }
                else
                {
                    graphics.FillPath(new SolidBrush(this.ItemEnabledTextColor), graphicsPath);
                }
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (this.selectionpart == WinFormsLinearList.DatePart.NONE)
            {
                MouseOnArrowsState currentMousePosition = this.CurrentMousePosition;
                if (e.KeyCode == Keys.Left)
                {
                    this.CurrentMousePosition = MouseOnArrowsState.OnLeft;
                    this.WinFormsLinearList_MouseDown(null, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                }
                else if (e.KeyCode == Keys.Right)
                {
                    this.CurrentMousePosition = MouseOnArrowsState.OnRight;
                    this.WinFormsLinearList_MouseDown(null, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                }
                this.CurrentMousePosition = currentMousePosition;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.ArcYDiameter = base.Height - 1;
            base.Invalidate();
        }

        private void timerLinearList_Tick(object sender, EventArgs e)
        {
            if (this.CurrentMousePosition == MouseOnArrowsState.OnLeft)
            {
                if (this.SelectedIndex > 0)
                {
                    WinFormsLinearList selectedIndex = this;
                    selectedIndex.SelectedIndex = selectedIndex.SelectedIndex - 1;
                    this.InvalidateLeftOrRightOrMiddle(MouseOnArrowsState.OnCenter);
                }
            }
            if (this.CurrentMousePosition == MouseOnArrowsState.OnRight)
            {
                if (this.SelectedIndex < this.ListItems.Count - 1)
                {
                    WinFormsLinearList WinFormsLinearList = this;
                    WinFormsLinearList.SelectedIndex = WinFormsLinearList.SelectedIndex + 1;
                    this.InvalidateLeftOrRightOrMiddle(MouseOnArrowsState.OnCenter);
                }
            }
        }

        public event WinFormsLinearList.WinFormsLinearListIndexChangeHandler WinFormsLinearListIndexChange;

        public event WinFormsLinearList.WinFormsLinearListMiddleMouseClickHandler WinFormsLinearListMiddleMouseClick;

        public class WinFormsLinearListEventArgs : EventArgs
        {
            private LinearListItem listItem;

            public LinearListItem Item
            {
                get
                {
                    return this.listItem;
                }
            }

            public WinFormsLinearListEventArgs(LinearListItem item)
            {
                this.listItem = item;
            }
        }

        public delegate void WinFormsLinearListIndexChangeHandler(object sender, WinFormsLinearList.WinFormsLinearListEventArgs e);

        public delegate void WinFormsLinearListMiddleMouseClickHandler(object sender, MouseEventArgs e);

        public enum DatePart
        {
            NONE,
            DAY,
            MONTH,
            YEAR
        }
    }
}