using System;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            throw new Exception();
        }
    }
}
