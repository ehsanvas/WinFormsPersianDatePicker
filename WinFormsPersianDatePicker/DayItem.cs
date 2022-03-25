using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace AbrAfzarGostaran
{
    [DefaultEvent("AbrDayItemChecked")]
    public class WinFormsPersianDatePickerDayItem : UserControl
    {
        private IContainer components = null;

        private Color ItemHoverBorderColor = Color.SkyBlue;

        private Color ItemSelectedBorderColor = Color.SteelBlue;

        private Color ItemStartGradiantColor = Color.White;

        private Color ItemEndGradiantColor = Color.White;

        private Color ItemSelectedStartColor = Color.LightBlue;

        private Color ItemSelectedEndColor = Color.LightCyan;

        private Color ItemCheckedStartColor = Color.LightBlue;

        private Color ItemCheckedEndColor = Color.WhiteSmoke;

        private Color ItemEnabledTextColor = Color.Black;

        private Color ItemDisabledTextColor = Color.Silver;

        private Font font = new Font("B Nazanin", 15f, FontStyle.Bold);

        private string ItemText = "";

        private bool isSelected = false;

        private bool isSelectable = true;

        private bool isHover = false;

        public string Caption
        {
            get
            {
                return this.ItemText;
            }
            set
            {
                this.ItemText = value;
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

        public Font ItemFont
        {
            get
            {
                return this.font;
            }
            set
            {
                this.font = value;
                base.Invalidate();
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

        public WinFormsPersianDatePickerDayItem()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
        }

        public WinFormsPersianDatePickerDayItem(string Caption)
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            this.Caption = Caption;
        }

        private void WinFormsPersianDatePickerDayItem_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.isSelectable)
            {
                this.isSelected = !this.isSelected;
                if (this.AbrDayItemChecked != null)
                {
                    this.AbrDayItemChecked(this, e);
                }
                base.Parent.Visible = false;
                base.Invalidate();
            }
        }

        private void WinFormsPersianDatePickerDayItem_MouseEnter(object sender, EventArgs e)
        {
            this.isHover = true;
            if ((!this.isSelectable ? false : !this.isSelected))
            {
                base.Invalidate();
            }
        }

        private void WinFormsPersianDatePickerDayItem_MouseLeave(object sender, EventArgs e)
        {
            this.isHover = false;
            if ((this.isSelected ? false : this.isSelectable))
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

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "WinFormsPersianDatePickerDayItem";
            base.Size = new Size(25, 18);
            base.MouseLeave += new EventHandler(this.WinFormsPersianDatePickerDayItem_MouseLeave);
            base.MouseClick += new MouseEventHandler(this.WinFormsPersianDatePickerDayItem_MouseClick);
            base.MouseEnter += new EventHandler(this.WinFormsPersianDatePickerDayItem_MouseEnter);
            base.ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.TranslateTransform((float)base.AutoScrollPosition.X, (float)base.AutoScrollPosition.Y);
            Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, this.ItemStartGradiantColor, this.ItemEndGradiantColor, LinearGradientMode.Vertical);
            LinearGradientBrush linearGradientBrush1 = new LinearGradientBrush(rectangle, this.ItemSelectedStartColor, this.ItemSelectedEndColor, LinearGradientMode.Vertical);
            LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rectangle, this.ItemCheckedStartColor, this.ItemCheckedEndColor, LinearGradientMode.Vertical);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            if (!(!this.isSelected ? true : !base.Enabled))
            {
                graphics.FillRectangle(linearGradientBrush2, 0, 0, base.Width, base.Height);
                graphics.DrawRectangle(new Pen(this.ItemSelectedBorderColor), rectangle);
            }
            else if ((!this.isHover ? false : this.isSelectable))
            {
                graphics.FillRectangle(linearGradientBrush1, 0, 0, base.Width, base.Height);
                graphics.DrawRectangle(new Pen(this.ItemHoverBorderColor), rectangle);
            }
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            StringFormat stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddString(this.ItemText, this.font.FontFamily, (int)this.font.Style, this.font.Size, new Rectangle(0, 0, base.Width - 1, base.Height - 2), stringFormat);
            if (!base.Enabled)
            {
                graphics.FillPath(new SolidBrush(this.ItemDisabledTextColor), graphicsPath);
            }
            else
            {
                graphics.FillPath(new SolidBrush(this.ItemEnabledTextColor), graphicsPath);
            }
        }

        public event WinFormsPersianDatePickerDayItem.AbrDayItemCheckedHandler AbrDayItemChecked;

        public delegate void AbrDayItemCheckedHandler(object sender, MouseEventArgs e);
    }
}