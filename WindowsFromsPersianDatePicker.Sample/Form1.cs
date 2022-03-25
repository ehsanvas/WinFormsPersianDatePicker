using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AbrAfzarGostaran.Windows.Forms;
namespace WindowsFromsPersianDatePicker.Sample
{
    
    public partial class Form1 : Form
    {
        WinFormsPersianDatePicker _datePicker;
        public Form1()
        {
            InitializeComponent();
            _datePicker = new WinFormsPersianDatePicker();
            _datePicker.SelectedDateChange += _datePicker_SelectedDateChange;
            this.Controls.Add(this._datePicker);
        }

        private void _datePicker_SelectedDateChange(object sender, WinFormsPersianDatePicker.AbrSelectedDateChangesEventArgs e)
        {            
            DateLabel.Text = ((WinFormsPersianDatePicker)sender).SelectedDate.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
