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
namespace Cafemanagement
{
    public partial class UserOrder : Form
    {
        public UserOrder()
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
            string query = "select * from ItemTbl where ItemCat = '"+Categorycb.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            itemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ItemForm item = new ItemForm();
            item.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UsersForm user = new UsersForm();
            user.Show();
           
        }
        int num = 0;
        int price, qty, total;
        string item , cat;

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
        DataTable table = new DataTable();
        int flag = 0;
        int sum = 0;

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if(QtyTb.Text == "")
            {
                MessageBox.Show("What is the Quantity of Item ?");
            }
            else if(flag == 0)
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ordersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filterbycategory();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void labelAmnt_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "insert into OrdersTbl values(" + OrderNum.Text + ",'" + Datelbl.Text + "','" + SellerName.Text + "'," + labelAmnt.Text + ")";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Order Successfully Created");
            Con.Close();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            ViewOrders view = new ViewOrders();
            view.Show();

        }

        private void SellerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserOrder_Load(object sender, EventArgs e)
        {
            populate();
            table.Columns.Add("Number" , typeof(int));
            table.Columns.Add("ItemName", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("UnitPrice", typeof(int));
            table.Columns.Add("TotalAmount", typeof(int));
            ordersGV.DataSource = table;
            Datelbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            SellerName.Text = Form1.user;


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void itemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            item = itemsGV.SelectedRows[0].Cells[1].Value.ToString();
            cat = itemsGV.SelectedRows[0].Cells[2].Value.ToString();
            price =Convert.ToInt32( itemsGV.SelectedRows[0].Cells[3].Value.ToString());
            flag = 1;

        }
    }
}
