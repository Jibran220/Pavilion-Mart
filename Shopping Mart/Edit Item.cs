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
    public partial class Edit_Item : Form
    {
        int id = 0;
        ShoppingMartEntities db = new ShoppingMartEntities();
        Product model = new Product();
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Edit_Item()
        {
            InitializeComponent();
        }

        private void Edit_Item_Load(object sender, EventArgs e)
        {
            bindgridview();
        }
        void bindgridview()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Select * from Product ";
            SqlDataAdapter cmd = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            cmd.Fill(data);

            dataGridView1.DataSource = data;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            id =Convert.ToInt32( textBox1.Text); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update Product set name=@name,price=@price,discount=@discount where id=@id";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", textBox2.Text);
            cmd.Parameters.AddWithValue("@price", textBox3.Text);
            cmd.Parameters.AddWithValue("@discount", textBox4.Text);

            con.Open();
            int s = cmd.ExecuteNonQuery();
            if (s > 0)
            {
                MessageBox.Show("Data  Updated");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                bindgridview();
            }
            else
            {
                MessageBox.Show("Data is not Updated");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Delete Product   where id=@id";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", id);
 

            con.Open();
            int s = cmd.ExecuteNonQuery();
            if (s > 0)
            {
                MessageBox.Show("Data Deleted");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                bindgridview();
            }
            else
            {
                MessageBox.Show("Data is not Deleted");

            }
        }
    }
}
