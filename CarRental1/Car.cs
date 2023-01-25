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
    public partial class Car : BaseForm
    {
        public Car()
        {
            InitializeComponent();
        }

        readonly SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ovi97\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Conn.Open();
            string query = "select * from [Car]";
            SqlDataAdapter da = new SqlDataAdapter(query, Conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CarsDGV.DataSource = ds.Tables[0];
            Conn.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void UserName_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                try
                {
                    Conn.Open();
                    string query = "insert into [Car] values(" + CarIdTb.Text + ",'" + BrandTb.Text + "' , '" + ModelTb.Text + "', '"+AvailableCb.SelectedItem.ToString()+"',"+PriceTb.Text+")";
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Added!");
                    Conn.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void Car_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (CarIdTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Conn.Open();
                    string query = "delete from [Car] where CarReg='" + CarIdTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car deleted!");
                    Conn.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        } 

        private void CarsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CarIdTb.Text = CarsDGV.Rows[0].Cells[0].Value.ToString();
            BrandTb.Text = CarsDGV.Rows[0].Cells[1].Value.ToString();
            ModelTb.Text = CarsDGV.Rows[0].Cells[2].Value.ToString();
            AvailableCb.SelectedItem = CarsDGV.Rows[0].Cells[3].Value.ToString();
            PriceTb.Text = CarsDGV.Rows[0].Cells[4].Value.ToString();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                try
                {
                    Conn.Open();
                    string query = "update [Car] set Brand='" + BrandTb.Text + "',Model='" + ModelTb.Text + "', Available ='"+AvailableCb.SelectedItem.ToString()+"', Price="+PriceTb.Text+" where CarReg=" + CarIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Updated!");
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            populate();
        }

        private void Car_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
