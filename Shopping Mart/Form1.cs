using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Shopping_Mart
{
    public partial class Form1 : Form
    {
        int srno = 0;
        ShoppingMartEntities db = new ShoppingMartEntities();
        Product model = new Product();
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString; 

        public Form1()
        {
            InitializeComponent();
            invoiceidincremment();
            textBox2.Text = loginadmin.name;
            getnameincombobox();
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);

        }






        void getnameincombobox()
        {
            comboBox1.Items.Clear();
            SqlConnection con = new SqlConnection(cs);
            string query = "Select *from Product";
            SqlCommand cmd = new SqlCommand(query,con);

            con.Open();
            SqlDataReader s = cmd.ExecuteReader();
            while (s.Read())
            {
                string itemnames = s.GetString(1);
                comboBox1.Items.Add(itemnames);
            }
            con.Close();
        }

        void getpriceofproducts()
        {
            if (comboBox1.SelectedItem == null)
            {

            }
            else
            {
                int price = 0;
                SqlConnection con = new SqlConnection(cs);
                string query = "Select price from Product where name =@name";
                SqlDataAdapter cmd = new SqlDataAdapter(query, con);
                cmd.SelectCommand.Parameters.AddWithValue("@name", comboBox1.SelectedItem);
                DataTable data = new DataTable();
                cmd.Fill(data);
                if (data.Rows.Count > 0)
                {
                    price = Convert.ToInt32(data.Rows[0]["price"]);


                }
                textBox4.Text = price.ToString();
            }
        }

        void getdiscountofproducts()
        {
            if (comboBox1.SelectedItem == null)
            {

            }
            else
            {
                int price = 0;
                SqlConnection con = new SqlConnection(cs);
                string query = "Select discount from Product where name =@name";
                SqlDataAdapter cmd = new SqlDataAdapter(query, con);
                cmd.SelectCommand.Parameters.AddWithValue("@name", comboBox1.SelectedItem);
                DataTable data = new DataTable();
                cmd.Fill(data);
                if (data.Rows.Count > 0)
                {
                    price = Convert.ToInt32(data.Rows[0]["discount"]);


                }
                textBox5.Text = price.ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox10.Enabled = true;
            getpriceofproducts();
            getdiscountofproducts();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {

            }
            else
            {
                try
                {
                    int a = Convert.ToInt32(textBox4.Text);
                    int b = Convert.ToInt32(textBox5.Text);
                    int c = Convert.ToInt32(textBox10.Text);

                    int totalprice = a * c;
                    int totaldiscount = b * c;

                    int nettotal = totalprice - totaldiscount;
                    textBox9.Text = nettotal.ToString();
                }
                catch (Exception ex)
                {


                }
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {

            }
            else
            {
                double tax = Convert.ToInt32(textBox9.Text);

                if (tax >= 10000)
                {
                    double taxtotal = tax * 0.15;
                    textBox8.Text = taxtotal.ToString();
                    double nettax = tax + taxtotal;
                    textBox7.Text = nettax.ToString();

                }
                else
                {
                    textBox8.Text = 0.ToString();
                    textBox7.Text = tax.ToString();
                }
            }
        }


        void addintodatagridview(string srno,string name, string price,string discount, string quantity, string subtotal, string tax, string total)
        {
            string[] row = { srno, name, price, discount,quantity, subtotal, tax, total };
            dataGridView1.Rows.Add(row);
            clearfields();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem!=null)
            {

            addintodatagridview((++srno).ToString(),comboBox1.SelectedItem.ToString(),textBox4.Text, textBox5.Text, textBox10.Text, textBox9.Text, textBox8.Text,textBox7.Text);
             addlasstcellnumber();
            }
            else
            {
                MessageBox.Show("Please Select Item");
            }
        }
        void clearfields()
        {
            if (comboBox1.SelectedItem == null)
            {

            }
            else
            {

            comboBox1.SelectedItem = null;
            textBox4.Clear(); 
            textBox5.Clear(); 
            textBox10.Clear();
            textBox9.Clear(); 
            textBox8.Clear();
            textBox7.Clear(); 
            }


        }
            void addlasstcellnumber()
            {
          

                double finalcost = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    finalcost = finalcost + Convert.ToInt32( dataGridView1.Rows[i].Cells[7].Value);
                }
            textBox6.Text = finalcost.ToString();
         

            
            
          

            }

        private void button2_Click(object sender, EventArgs e)
        {
            clearfields();
            //textBox10.Text="";
            textBox12.Clear();
            textBox11.Clear();
            textBox6.Clear();
            textBox10.Enabled = false;
            //dataGridView1.Rows.Clear();

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox6.Text)&& string.IsNullOrEmpty(textBox12.Text))
            {

            }
            else
            {
                try
                {
                    double totalprice = Convert.ToInt32(textBox6.Text);
                    double amountreceived = Convert.ToInt32(textBox12.Text);
                    double change = amountreceived - totalprice;
                    textBox11.Text = change.ToString();
                }
                catch (Exception ex)
                {

                }

              
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            srno = 0;
            dataGridView1.Rows.Clear();
        }








        void invoiceidincremment()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "Select id from OrderMaster";
            SqlDataAdapter cmd = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            cmd.Fill(data);
            if (data.Rows.Count <1)
            {
                textBox1.Text = "1";
            }
            else
            {
                string query2 = "Select max(id) from OrderMaster";
                SqlCommand cmd1 = new SqlCommand(query2, con);

                con.Open();
                int a =Convert.ToInt32( cmd1.ExecuteScalar());

                a = a + 1;
                textBox1.Text = a.ToString();


                con.Close();


            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch)==true)
            {
                e.Handled = false;
            }
             else if(ch == 8)
            {
                e.Handled = false;

            }
            else
            {
                e.Handled = true;

            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;

            }
            else
            {
                e.Handled = true;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "  insert into [OrderMaster] values(@id,@name,@date,@totalcost)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@name", textBox2.Text);
            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@totalcost", textBox6.Text);

            con.Open();
            int s = cmd.ExecuteNonQuery();
            if (s > 0)
            {
                MessageBox.Show("Data  Inserted");
            }
            else
            {
                MessageBox.Show("Data is not Inserted");

            }
            con.Close();
            insertintoorderdetailtable();

        }



        private void button6_Click(object sender, EventArgs e)
        {
            //printDocument1.Print();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Product a = new Add_Product();
            a.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            getnameincombobox();
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Item a = new Edit_Item();
            a.ShowDialog();
        }

        private void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewdata a = new viewdata();
            a.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp =Properties.Resources.Logo_The_Pavilion_Hotel_Nainital_pyf0qp;
            Image img = bmp;
            e.Graphics.DrawImage(img, 30, 5,800,250);
            e.Graphics.DrawString("Invoice Id: " + textBox1.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 300));
            e.Graphics.DrawString("UserName: " + textBox2.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 330));
            e.Graphics.DrawString("Date: " + DateTime.Now.ToShortDateString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 360));
            e.Graphics.DrawString("Time: " + DateTime.Now.ToShortTimeString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 390));
            e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 420));
            e.Graphics.DrawString("Item", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 450));
            e.Graphics.DrawString("Price", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(240, 450));
            e.Graphics.DrawString("Quantity", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(390, 450));
            e.Graphics.DrawString("Discount", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(590, 450));
            e.Graphics.DrawString("-------------------------------------------------------------------------------------------------------", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 480));
            //for item name
            int gap = 510;
            if (dataGridView1.Rows.Count>0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {

                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[1].Value.ToString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, gap));
                    gap = gap + 30;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            //for item Price
            int gap1 = 510;
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {

                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[2].Value.ToString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(260, gap1));
                        gap1 = gap1 + 30;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }



            //for item Discount
            int gap3 = 510;
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {

                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[4].Value.ToString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(410, gap3));
                        gap3 = gap3 + 30;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            //for item Quantity
            int gap2 = 510;
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    try
                    {

                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[3].Value.ToString(), new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(610, gap2));
                        gap2 = gap2 + 30;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            //for sub total
            double Subtotalprint = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Subtotalprint = Subtotalprint + Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
            }
            e.Graphics.DrawString("-----------------------------------------------------", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 850));
            e.Graphics.DrawString("Sub-Total: "+Subtotalprint, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 880));


            //for sub total
            double taxprint = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                taxprint = taxprint + Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
            }
            e.Graphics.DrawString("Total-Tax: " + taxprint, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 910));
            e.Graphics.DrawString("Final-Amount: " + textBox6.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 940));
            e.Graphics.DrawString("-----------------------------------------------------", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 970));
            e.Graphics.DrawString("Amount-Received: " + textBox12.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 1000));
            e.Graphics.DrawString("Amount-Change: " + textBox11.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(30, 1030));


        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }


        int getlastinvoiceid()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "  select max(id) from OrderMaster";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int maxinvoiceid =Convert.ToInt32( cmd.ExecuteScalar());
            con.Close();
            return maxinvoiceid;
        }

        void insertintoorderdetailtable()
        {
            SqlConnection con = new SqlConnection(cs);
                int a = 0;
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    string query = "insert into Order_Details values (@invoice,@name,@price,@discount,@quantity,@subtotal,@tax,@totalcost)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@invoice", getlastinvoiceid());
                    cmd.Parameters.AddWithValue("@name", dataGridView1.Rows[i].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@price", dataGridView1.Rows[i].Cells[2].Value);
                    cmd.Parameters.AddWithValue("@discount", dataGridView1.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@quantity", dataGridView1.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@subtotal", dataGridView1.Rows[i].Cells[5].Value);
                    cmd.Parameters.AddWithValue("@tax", dataGridView1.Rows[i].Cells[6].Value);
                    cmd.Parameters.AddWithValue("@totalcost", dataGridView1.Rows[i].Cells[7].Value);

                    con.Open();
                    a = a + cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            catch
            {
                if (a > 0)
                {

                    MessageBox.Show("Data  Inserted in order detail");
                }

                else
                {
                    MessageBox.Show("Data is not Inserted order detail");

                }
                con.Close();
            }
        }

        private void detailAndSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Detail_and_search a = new Detail_and_search();
            a.ShowDialog();
        }
    }
}















