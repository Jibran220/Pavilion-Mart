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
    public partial class Add_Product : Form
    {
        ShoppingMartEntities db = new ShoppingMartEntities();
        Product model = new Product();
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Add_Product()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into [Product] values(@name,@price,@discount)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            cmd.Parameters.AddWithValue("@price", textBox2.Text);
            cmd.Parameters.AddWithValue("@discount", textBox3.Text);

            con.Open();
            int s = cmd.ExecuteNonQuery();
            if (s > 0)
            {
                MessageBox.Show("Data  Inserted");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
            else
            {
                MessageBox.Show("Data is not Inserted");

            }
            con.Close();
        }
    }
}
