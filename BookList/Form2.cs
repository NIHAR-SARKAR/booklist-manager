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
    public partial class Form2 : MaterialForm
    {
        public Form2()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.DeepOrange900, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);            
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
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

        private async Task Loadf2()
        {
            await Task.Delay(300);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM AddNew", con);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                ListViewItem LItem = new ListViewItem(da["book_id"].ToString());
                LItem.SubItems.Add(da["name"].ToString());
                LItem.SubItems.Add(da["author"].ToString());
                string x = da["bought"].ToString();
                string y = x.Substring(0, 10);
                LItem.SubItems.Add(y);

                LItem.SubItems.Add(da["status"].ToString());
                listView1.Items.Add(LItem);
            }
            con.Close();
        }


        private void Form2_Activated(object sender, EventArgs e)
        {
            Task t = Loadf2();
        }
    }
}
