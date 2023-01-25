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
using ClassLibrary;

namespace CarRental1
{
    public partial class Rental : BaseForm
    {
        public Rental()
        {
            InitializeComponent();
        }

        readonly SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ovi97\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");

        private void fillcombo()
        {
            Conn.Open();
            string query = "select CarReg from [Car] where Available = '" +"YES"+ " ' ";
            SqlCommand cmd = new SqlCommand(query, Conn);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CarReg", typeof(string));
            dt.Load(rdr);
            CarIdCb.ValueMember = "CarReg";
            CarIdCb.DataSource = dt;
            Conn.Close();
        }

        private void fillCustomer()
        {
            Conn.Open();
            string query = "select Customer_id from [Customer]";
            SqlCommand cmd = new SqlCommand(query, Conn);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Customer_id", typeof(int));
            dt.Load(rdr);
           CustCb.ValueMember = "Customer_id";
            CustCb.DataSource = dt;
            Conn.Close();
        }

        private void NameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void populate()
        {
            Conn.Open();
            string query = "select * from [Rental]";
            SqlDataAdapter da = new SqlDataAdapter(query, Conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RentDGV.DataSource = ds.Tables[0];
            Conn.Close();
        }

        private void UpdateOnRent()
        {
            Conn.Open();
            string query = "update [Car] set Available ='" + "NO" + "' where CarReg=' " + CarIdCb.SelectedValue.ToString() + "';";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.ExecuteNonQuery();
          //  MessageBox.Show("Car Updated!");
            Conn.Close();
        }

        private void UpdateOnRentDelete()
        {
            Conn.Open();
            string query = "update [Car] set Available ='" + "YES" + "' where CarReg=' " + CarIdCb.SelectedValue.ToString() + "';";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.ExecuteNonQuery();
            //  MessageBox.Show("Car Updated!");
            Conn.Close();
        }

        private void fetchCustName()
        {
            List<Client> clients = new List<Client>();

            int id_req = int.Parse(CustCb.SelectedValue.ToString());

            Conn.Open();
            string query = "select * from [Customer]";
            SqlCommand cmd = new SqlCommand(query, Conn);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Client cl = new Client()
                {
                    Customer_id = int.Parse(dr["Customer_id"].ToString()),
                    CustomerName = dr["CustomerName"].ToString(),
                    CustomerAdd = dr["CustomerAdd"].ToString(),
                    Phone = dr["Phone"].ToString()
                };
                clients.Add(cl);
            }
            Client client = clients.Where(c => c.Customer_id == id_req).FirstOrDefault();

            CustNameTb.Text = client.CustomerName;

            Conn.Close();
        }

        private void Rental_Load(object sender, EventArgs e)
        {
            fillcombo();
            fillCustomer();
            populate();
        }

        private void CarIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void CustCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchCustName();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                try
                {
                    Conn.Open();
                    string query = "insert into [Rental] values(" + RIdTb.Text + ",'" + CarIdCb.SelectedValue.ToString() + "' , '" + CustNameTb.Text + "', '" + RentDate.Value.ToString() + "', '" + ReturnDate.Value.ToString() + "', "+ FeesTb.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Rented!");
                    Conn.Close();
                    UpdateOnRent();
                    populate();
                }
                catch (Exception Myex)
                {
                    ShowMessage(Myex.Message);
                }
            }
        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RIdTb.Text = RentDGV.Rows[0].Cells[0].Value.ToString();
            CarIdCb.SelectedValue = RentDGV.Rows[0].Cells[1].Value.ToString();
           // CustNameTb.Text = RentDGV.Rows[0].Cells[4].Value.ToString();
            FeesTb.Text = RentDGV.Rows[0].Cells[5].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void ReturnDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                try
                {
                    Conn.Open();
                    string query = "delete from [Rental] where Rent_id=" + RIdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rental deleted!");
                    Conn.Close();
                    populate();
                    UpdateOnRentDelete();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void Rental_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private Class1 var = new Class1();
        private int total = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            int price = -1;
            Conn.Open();
            int totalPrice = 0;
            string query = "select Price from [Car] where CarReg = '" + CarIdCb.SelectedValue.ToString() + "';";
            SqlCommand  cmd = new SqlCommand(query, Conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                price = int.Parse(dr["Price"].ToString());
            }
            DateTime d1 = RentDate.Value.Date;
            DateTime d2 = ReturnDate.Value.Date;
            TimeSpan t = d2 - d1;
            int NrOfDays = Convert.ToInt32(t.TotalDays);
            totalPrice = NrOfDays * price;
            Conn.Close();
            if (AdvanceTb.Text == "")
            {
                MessageBox.Show("Complete input");
            }
            else
            {
                int diff = Convert.ToInt32(AdvanceTb.Text);
                total = var.dif(totalPrice, diff);
                totalTb.Text = total.ToString();
            }
           
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
