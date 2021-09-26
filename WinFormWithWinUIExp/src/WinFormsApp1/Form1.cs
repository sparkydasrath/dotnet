using System;
using System.Diagnostics;
using System.Windows.Forms;
using ClassLibrary1;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("before c1");

            Class1 c1 = new();

            Debug.WriteLine("after c1");

        }
    }
}