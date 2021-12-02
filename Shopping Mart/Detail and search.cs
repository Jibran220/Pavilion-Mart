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
    public partial class Detail_and_search : Form
    {
        ShoppingMartEntities db = new ShoppingMartEntities();
        Product model = new Product();
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Detail_and_search()
        {
            InitializeComponent();
        }

        private void Detail_and_search_Load(object sender, EventArgs e)
        {
            bindgridview();
        }
        void bindgridview()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "sp_orderdetailandmaster ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;


            SqlDataAdapter cmd1 = new SqlDataAdapter();
            cmd1.SelectCommand = cmd;

            DataTable data = new DataTable();
            cmd1.Fill(data);

            dataGridView1.DataSource = data;
        }
        void bindgridviewbyid()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "sp_orderdetailandmasterbyid ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", textBox1.Text);


            SqlDataAdapter cmd1 = new SqlDataAdapter();
            cmd1.SelectCommand = cmd;

            DataTable data = new DataTable();
            cmd1.Fill(data);

            dataGridView1.DataSource = data;
            dataGridView1.Columns[10].Visible = false;
            label3.Text = dataGridView1.Rows[0].Cells[10].Value.ToString();
        }
        void bindgridviewbydatetime()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "sp_orderdetailandmasterbydatetime ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@first", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@second", dateTimePicker2.Value);


            SqlDataAdapter cmd1 = new SqlDataAdapter();
            cmd1.SelectCommand = cmd;

            DataTable data = new DataTable();
            cmd1.Fill(data);

            dataGridView1.DataSource = data;
            //dataGridView1.Columns[10].Visible = false;
            //label3.Text = dataGridView1.Rows[0].Cells[10].Value.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bindgridviewbyid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bindgridviewbydatetime();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bindgridview();

        }
    }
}
