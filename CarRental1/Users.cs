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

namespace CarRental1
{
    public partial class Users : BaseForm
    {
        public Users()
        {
            InitializeComponent();
        }

        readonly SqlConnection Conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ovi97\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                try
                {
                    Conn.Open();
                    string query = "update [User] set Username='"+UserName.Text+"',Password='"+UserPassword.Text+"' where Id="+UserId.Text+"";

                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated!!");
                    Conn.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void populate()
        {
            Conn.Open();
            string query = "select * from [User]";
            SqlDataAdapter da = new SqlDataAdapter(query, Conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            Conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(IsValidForm())
            {
                try
                {
                    Conn.Open();
                    string usr = "insert into [User] values(" + UserId.Text + ",'" + UserName.Text + "' , '" + UserPassword.Text + "')";
                    string query = usr;
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Added!");
                    Conn.Close();
                    populate();
                }
                catch(Exception Myex)
                {
                    MessageBox.Show(Myex.Message); 
                }
            }
        }

        private void Users_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(IsValidForm())
            {
                try
                {
                    Conn.Open();
                    string query = "delete from [User] where Id=" + UserId.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User deleted!");
                    Conn.Close();
                    populate();
                }catch(Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UserId.Text = UserDGV.Rows[0].Cells[0].Value.ToString();
            UserName.Text = UserDGV.Rows[0].Cells[1].Value.ToString();
            UserPassword.Text = UserDGV.Rows[0].Cells[2].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        /*private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Users
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Users";
            this.Load += new System.EventHandler(this.Users_Load_1);
            this.ResumeLayout(false);

        }*/

        private void Users_Load_1(object sender, EventArgs e)
        {

        }

        private void Users_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
