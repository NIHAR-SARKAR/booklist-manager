using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookList
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }
        Timer t; 
        private void Splash_Shown(object sender, EventArgs e)
        {
            Random r = new Random();
            t = new Timer();
            t.Interval = r.Next(2000,2500);
            t.Start();
            t.Tick += t_Tick;
        }
        void t_Tick(object sender, EventArgs e)
        {
            t.Stop();
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
