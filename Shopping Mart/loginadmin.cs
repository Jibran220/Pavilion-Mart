using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shopping_Mart
{
    public partial class loginadmin : Form
    {
        public static string name="";
        ShoppingMartEntities db = new ShoppingMartEntities();
        Product model = new Product();
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public loginadmin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {


                SqlConnection con = new SqlConnection(cs);
                string query = "  select * from login where name=@name and password=@pass";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);

                con.Open();
                SqlDataReader db = cmd.ExecuteReader();
                if (db.HasRows == true)
                {
                    name = textBox1.Text;
                    MessageBox.Show("Login Succesfull");
                    Form1 r = new Form1();
                    r.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Login Failed", "Error");

                }
                con.Close();
            }
            else
            {
                MessageBox.Show("box are empty");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;

            }
        }
    }
}
