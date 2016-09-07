using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;

namespace BookList
{
    public partial class Form3 : MaterialForm
    {
        public Form3()
        {
            InitializeComponent();
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.DeepOrange900, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }        

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen pen = new Pen(Color.DodgerBlue, 4);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            e.Graphics.DrawRectangle(pen, rect);
        }
        
        private void textBox1_Enter(object sender, EventArgs e)
        {
            //LoadLater();
            if (textBox1.Text == "- Select a Book -")
            {
                textBox1.Text = "";
                textBox1.TextAlign = HorizontalAlignment.Left;
                label2.Visible = false;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "- Select a Book -";
                textBox1.TextAlign = HorizontalAlignment.Center;
            }
        }

        private async Task Loadf3()
        {
            //Task task = Task.Run(() => LoadLater());
            //await Task.Delay(TimeSpan.FromSeconds(1));
            await Task.Delay(700);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT name FROM AddNew", con);
            SqlDataReader da = cmd.ExecuteReader();
            AutoCompleteStringCollection ac = new AutoCompleteStringCollection();
            while (da.Read())
            {
                //var str = da.GetString(0);
                //MessageBox.Show(str);
                ac.Add(da["name"].ToString());
                //comboBox1.Items.Add(da["name"].ToString());
                textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox1.AutoCompleteCustomSource = ac;
                textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }
            con.Close();
        }


        bool x;
        private void materialSingleLineTextField1_TextChanged(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text.Contains(" "))
            {
                //materialSingleLineTextField1.Text += "\b";
                string s = materialSingleLineTextField1.Text.Replace(" ", "");
                materialSingleLineTextField1.Text = s;
                materialSingleLineTextField1.SelectionStart = materialSingleLineTextField1.Text.Length;
            }

            //materialSingleLineTextField1.Text.Remove(materialSingleLineTextField1.Text.Length - 1);

            x = System.Text.RegularExpressions.Regex.IsMatch(materialSingleLineTextField1.Text, @"^[789]\d{9}$");
            if (x)
            {
                label3.Text = "✔ Valid number";
                label3.ForeColor = Color.LimeGreen;
                label3.Visible = true;                                
            }
            else if (materialSingleLineTextField1.TextLength >= 10 && materialSingleLineTextField1.Text != phn && x == false && label4.Visible == false)
            {
                label3.Text = "*Not a valid number";
                label3.ForeColor = Color.Red;
                label3.Visible = true;
            }
            else
                label3.Visible = false;
        }

        private void materialSingleLineTextField1_Enter(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == phn)
            {
                materialSingleLineTextField1.Text = "";
                label4.Visible = false;
            }
        }
        static string name = "Enter Borrower's Name"; static string phn = "Enter Phone Number";
        private void materialSingleLineTextField2_Enter(object sender, EventArgs e)
        {
            if (materialSingleLineTextField2.Text == name)
            {
                materialSingleLineTextField2.Text = "";
                label4.Visible = false;
                label1.Visible = false;
            }
        }

        private void materialSingleLineTextField2_Leave(object sender, EventArgs e)
        {
            if (materialSingleLineTextField2.Text == "")
                materialSingleLineTextField2.Text = name;
        }

        private void materialSingleLineTextField1_Leave(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text == "")
            {
                materialSingleLineTextField1.Text = phn;
                label3.Visible = false;
            }
        }

        private void SaveBorrower_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField2.Text == name && materialSingleLineTextField1.Text == phn && textBox1.Text == "- Select a Book -")
            {
                label3.Text = "*Enter phone number";
                label3.ForeColor = Color.Red;
                label1.Visible = true; label2.Visible = true; label3.Visible = true;
            }
            else if (materialSingleLineTextField1.Text == phn && materialSingleLineTextField2.Text == name)
            {
                label3.Text = "*Enter phone number";
                label3.ForeColor = Color.Red;
                label1.Visible = true; label2.Visible = false; label3.Visible = true;
            }

            else if (materialSingleLineTextField1.Text == phn && textBox1.Text == "- Select a Book -")
            {
                label3.Text = "*Enter phone number";
                label3.ForeColor = Color.Red;
                label1.Visible = false; label2.Visible = true; label3.Visible = true;
            }
            else if (textBox1.Text == "- Select a Book -" && materialSingleLineTextField2.Text == name)
            {
                label3.ForeColor = Color.Red;
                label1.Visible = true; label2.Visible = true; label3.Visible = false;
            }
            else if (materialSingleLineTextField2.Text == name)
            {
                label1.Visible = true; label2.Visible = false; label3.Visible = false;
            }
            else if (textBox1.Text == "- Select a Book -")
            {
                label1.Visible = false; label2.Visible = true; label3.Visible = false;
            }
            else if (materialSingleLineTextField1.Text == phn)
            {
                label3.Text = "*Enter phone number";
                label3.ForeColor = Color.Red;
                label1.Visible = false; label2.Visible = false; label3.Visible = true;
            }            
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT into Borrower(name, contact) VALUES (@name, @contact)", con);
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = materialSingleLineTextField2.Text;
                cmd.Parameters.Add("@contact", SqlDbType.VarChar).Value = materialSingleLineTextField1.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Insert Into BorrowedBooks(bk_id, borrowed, [user_id], returned) VALUES ((Select book_id from AddNew where name = @bname), @issued, (Select id from Borrower where contact = @cont), @due)";
                cmd.Parameters.Add("@bname", SqlDbType.VarChar).Value = textBox1.Text;
                cmd.Parameters.Add("@issued", SqlDbType.Date).Value = dtp1.Value.ToShortDateString();
                cmd.Parameters.Add("@cont", SqlDbType.VarChar).Value = materialSingleLineTextField1.Text;
                cmd.Parameters.Add("@due", SqlDbType.Date).Value = dtp2.Value.ToShortDateString();
                cmd.ExecuteNonQuery();
                con.Close();
                label4.Visible = true;
                materialSingleLineTextField1.Text = phn;
                materialSingleLineTextField2.Text = name;
                textBox1.Text = "- Select a Book -";
                textBox1.TextAlign = HorizontalAlignment.Center;
            }
        }


        private void Form3_Activated(object sender, EventArgs e)
        {
            Task t = Loadf3();
        }

        

        //private void DisableControls(Control con)
        //{
        //    foreach (Control c in con.Controls)
        //    {
        //        DisableControls(c);
        //    }
        //    con.Enabled = false;
        //}

        //private void EnableControl(Control con)
        //{
        //    if (con != null)
        //    {
        //        con.Enabled = true;
        //        EnableControl(con.Parent);

        //        Task t = Loadf3();
        //    }
        //}
        //private void EnableAllControls(Control con)
        //{
        //    foreach (Control c in con.Controls)
        //    {
        //        EnableAllControls(c);
        //    }
        //    con.Enabled = true;
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //DisableControls(this);
        //    //EnableControl(lblWait);
        //    //Thread.Sleep(1000);
        //    //EnableAllControls(this);
        //} //To-Do //To-do
    }
}
