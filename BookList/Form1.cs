using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Configuration;
using System.Data.SqlClient;
//using raks = System.String;

namespace BookList
{
    public partial class Form1 : MaterialForm
    {        
        public Form1()
        {
            InitializeComponent();
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.DeepOrange900, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        static string a = "Enter a Book Name";
        static string b = "Enter Author's Name";
        private void materialSingleLineTextField1_Enter(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == a)
            {
                materialSingleLineTextField1.Text = "";
                label1.Visible = false;
                label3.Visible = false; 
            }
        }
        
        private void materialSingleLineTextField2_Enter(object sender, EventArgs e)
        {
            if (materialSingleLineTextField2.Text == b)
            {
                materialSingleLineTextField2.Text = "";
                label2.Visible = false;
                label3.Visible = false;
            }

        }

        private void materialSingleLineTextField1_Leave(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == "")
                materialSingleLineTextField1.Text = a;  
        }

        private void materialSingleLineTextField2_Leave(object sender, EventArgs e)
        {
            if (materialSingleLineTextField2.Text == "")
                materialSingleLineTextField2.Text = b;
        }
                
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen pen = new Pen(Color.DodgerBlue, 4);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            e.Graphics.DrawRectangle(pen, rect);
        }
        
        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
            //f2.Dispose();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == a && materialSingleLineTextField2.Text == b)
                {
                    label1.Visible = true; label2.Visible = true;
                }
                else if (materialSingleLineTextField1.Text == a)
                {
                    label1.Visible = true; label2.Visible = false;
                }
            else if (materialSingleLineTextField2.Text == b)
                {
                    label2.Visible = true; label1.Visible = false;
                }
                else
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("INSERT into AddNew(name, author, bought) VALUES (@name, @author, @bought)", con);
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = materialSingleLineTextField1.Text;
                    cmd.Parameters.Add("@author", SqlDbType.VarChar).Value = materialSingleLineTextField2.Text;
                    cmd.Parameters.Add("@bought", SqlDbType.Date).Value = dateTimePicker1.Value.ToShortDateString();
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    label3.Visible = true;
                    materialSingleLineTextField1.Text = a;
                    materialSingleLineTextField2.Text = b;
                }
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.ShowDialog();
            //f3.Dispose();
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.ShowDialog();
            //f4.Dispose();
        }
     }
}
