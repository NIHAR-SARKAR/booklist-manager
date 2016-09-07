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

namespace BookList
{
    public partial class Form4 : MaterialForm
    {
        public Form4()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.DeepOrange900, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen pen = new Pen(Color.DodgerBlue, 4);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            e.Graphics.DrawRectangle(pen, rect);
        }
        

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void bookReturnedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                foreach (ListViewItem LItem in listView1.SelectedItems)
                {
                    DialogResult dr = MessageBox.Show("Do You Want to Remove it from the list?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        LItem.Remove();

                        //UPDATE TABLE
                    } 
                }
            }
        }

        private async Task Loadf4()
        {
            await Task.Delay(300);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select bb.bk_id, bb.borrowed, bb.returned as returned, b.contact,b.name as Borrower, a.name as Book_name from BorrowedBooks bb Join Borrower b ON bb.[user_id]=b.id Join AddNew a ON bb.bk_id=a.book_id", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                ListViewItem LItem = new ListViewItem(da["bk_id"].ToString());
                LItem.SubItems.Add(da["Book_name"].ToString());
                LItem.SubItems.Add(da["Borrower"].ToString());
                string x = da["borrowed"].ToString();
                string y = x.Substring(0, 10);
                LItem.SubItems.Add(y);

                //Calculating Days remaining
                DateTime today = DateTime.Today;
                string returned = da["returned"].ToString();
                DateTime conv = Convert.ToDateTime(returned);
                TimeSpan ts = conv - today;
                double tot = ts.TotalDays;
                LItem.SubItems.Add(tot.ToString());
                double positive = 0;
                if (tot < 0)
                {
                    positive = System.Math.Abs(tot) * 10; //10 rupee fine  
                }
                LItem.SubItems.Add("₹" + positive.ToString());
                LItem.SubItems.Add(da["contact"].ToString());
                listView1.Items.Add(LItem);

                //LItem.SubItems.Add(y);
                //LItem.SubItems.Add(da["status"].ToString());
                //listView1.Items.Add(LItem);

                //Select * from BorrowedBooks Inner Join Borrower on BorrowedBooks.[user_id]=Borrower.id
                //    Select * from BorrowedBooks Inner Join AddNew on BorrowedBooks.bk_id= AddNew.book_id

                //(startDate.Date - endDate.Date).TotalDays                
            }
            con.Close();
        }

        private void Form4_Activated(object sender, EventArgs e)
        {
            Task t = Loadf4();
        }
        
    }
}
