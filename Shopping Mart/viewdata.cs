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
    public partial class viewdata : Form
    {
        int id = 0;
        ShoppingMartEntities db = new ShoppingMartEntities();
        Product model = new Product();
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public viewdata()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void viewdata_Load(object sender, EventArgs e)
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

        private void viewdata_Activated(object sender, EventArgs e)
        {
            bindgridview();
        }
    }
}
