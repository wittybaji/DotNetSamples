using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RuntimeException
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown", "/r /t 0");
            TestClass.TestFunc();
        }

    }

    class TestClass
    {
        static TestClass()
        {
            System.IO.File.ReadAllText($"C:\\null.txt");
        }

        public static void TestFunc()
        {
            Console.WriteLine("Hello World!");
        }
    }

}
