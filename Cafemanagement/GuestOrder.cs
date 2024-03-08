using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace Cafemanagement
{
    public partial class GuestOrder : Form
    {
        public GuestOrder()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Usman\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");

        void populate()
        {
            Con.Open();
            string query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            itemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        void filterbycategory()
        {
            Con.Open();
            string query = "select * from ItemTbl where ItemCat = '" + Categorycb.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            itemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void GuestOrder_Load(object sender, EventArgs e)
        {
            populate();
            table.Columns.Add("Number", typeof(int));
            table.Columns.Add("ItemName", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("UnitPrice", typeof(int));
            table.Columns.Add("TotalAmount", typeof(int));
            ordersGV.DataSource = table;
            Datelbl.Text = DateTime.Today.Day.ToString()+"/"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();
        }

        int num = 0;
        int price, qty, total;
        string item, cat;
        DataTable table = new DataTable();
        int flag = 0;

        private void itemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            item = itemsGV.SelectedRows[0].Cells[1].Value.ToString();
            cat = itemsGV.SelectedRows[0].Cells[2].Value.ToString();
            price = Convert.ToInt32(itemsGV.SelectedRows[0].Cells[3].Value.ToString());
            flag = 1;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "insert into OrdersTbl values(" + OrderNoTb.Text + ",'" + Datelbl.Text + "','" + SellerName.Text + "',"+labelAmnt.Text+")";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Order Successfully Created");
            Con.Close();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            populate();

        }

        private void Categorycb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void QtyTb_TextChanged(object sender, EventArgs e)
        {

        }

        int sum = 0;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "")
            {
                MessageBox.Show("What is the Quantity of Item ?");
            }
            else if (flag == 0)
            {
                MessageBox.Show("Select the product to be ordered");

            }
            else
            {
                num = num + 1;
                total = price * Convert.ToInt32(QtyTb.Text);
                table.Rows.Add(num, item, cat, price, total);
                ordersGV.DataSource = table;
                flag = 0;
            }
            sum = sum + total;
            labelAmnt.Text = "" + sum;
        }


        private void Categorycb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filterbycategory();
        }
    }
}
