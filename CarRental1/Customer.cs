using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CarRental1
{
    public partial class Customer : BaseForm
    {
        public Customer()
        {
            InitializeComponent();
        }

        readonly SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ovi97\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Conn.Open();
            string query = "select * from [Customer]";
            SqlDataAdapter da = new SqlDataAdapter(query, Conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            Conn.Close();
        }


        private void CarsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CIdTb.Text = CustomerDGV.Rows[0].Cells[0].Value.ToString();
            NameTb.Text = CustomerDGV.Rows[0].Cells[1].Value.ToString();
            AdressTb.Text = CustomerDGV.Rows[0].Cells[2].Value.ToString();
            PhoneTb.Text = CustomerDGV.Rows[0].Cells[3].Value.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                try
                {
                    Conn.Open();
                    string query = "delete from [Customer] where Customer_id='" + CIdTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer deleted!");
                    Conn.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValidForm()) 
            {
                try
                {
                    Conn.Open();
                    string usr = "insert into [Customer] values(" + CIdTb.Text + ",'" + NameTb.Text + "' , '" + AdressTb.Text + "', '" + PhoneTb.Text + "')";
                    string query = usr;
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added!");
                    Conn.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                try
                {
                    Conn.Open();
                    string query = "update [Customer] set Customer_id='" + CIdTb.Text + "',CustomerName='" + NameTb.Text + "', CustomerAdd ='" + AdressTb.Text + "', Phone=" + PhoneTb.Text + " where Customer_id=" + CIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated!");
                    Conn.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        /*private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Customer
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Customer";
            this.Load += new System.EventHandler(this.Customer_Load_1);
            this.ResumeLayout(false);

        }*/

        private void Customer_Load_1(object sender, EventArgs e)
        {

        }

        private void Customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
