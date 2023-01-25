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

namespace CarRental1
{
    public partial class ReturnCar : BaseForm
    {
        public ReturnCar()
        {
            InitializeComponent();
        }

        readonly SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ovi97\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");

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

        private void populateRet()
        {
            Conn.Open();
            string query = "select * from [Return]";
            SqlDataAdapter da = new SqlDataAdapter(query, Conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnDGV.DataSource = ds.Tables[0];
            Conn.Close();
        }

        private void DeleteOnReturn()
        {
            int rentId;
            rentId = Convert.ToInt32(RentDGV.Rows[0].Cells[0].Value.ToString());
            Conn.Open();
            string query = "delete from [Rental] where Rent_id=" + rentId + ";";
            SqlCommand cmd = new SqlCommand(query, Conn);
            cmd.ExecuteNonQuery();
           // MessageBox.Show("Rental deleted!");
            Conn.Close();
            populate();
           
        }
        private void Return_Load(object sender, EventArgs e)
        {
            populate();
            populateRet();
        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CarIdCb.Text = RentDGV.Rows[0].Cells[1].Value.ToString();
            NameTb.Text = RentDGV.Rows[0].Cells[2].Value.ToString();
            ReturnDate.Text = RentDGV.Rows[0].Cells[4].Value.ToString();
            DateTime d1 = ReturnDate.Value.Date;
            DateTime d2 = DateTime.Now;
            TimeSpan t = d2 - d1;
            int NrOfDays = Convert.ToInt32(t.TotalDays);
            if(NrOfDays < 0)
            {
                DelayTb.Text = "No Delay";
                FineTb.Text = "0";
            }else
            {
                DelayTb.Text = "" + NrOfDays;
                FineTb.Text = "" + (NrOfDays * 100);
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
                    string query = "insert into [Return] values(" + RIdTb.Text + ",'" + CarIdCb.Text + "' , '" + NameTb.Text + "', '" + ReturnDate.Value.ToString() + "', '" + DelayTb.Text + " ','" + FineTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Returned!");
                    Conn.Close();
                    DeleteOnReturn();
                    populateRet();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void ReturnCar_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void FineTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
