using System;
using System.Windows.Forms;

namespace AbrAfzarGostaran
{
    public class LinearListItem
    {
        private WinFormsLinearList owner;

        private string text = "";

        public WinFormsLinearList Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                if (this.owner != null)
                {
                    this.owner.Invalidate();
                }
            }
        }

        public LinearListItem(WinFormsLinearList o)
        {
            this.owner = o;
        }

        public LinearListItem()
        {
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}