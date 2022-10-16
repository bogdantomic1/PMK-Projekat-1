using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prodavnica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Aplikacija za trgovinu";
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 forma = new Form2();
            forma.Show();
        }

        private void btnProdaja_Click(object sender, EventArgs e)
        {
            Form3 forma = new Form3();
            forma.Show();
        }
    }
}
