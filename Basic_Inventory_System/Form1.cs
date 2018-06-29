using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic_Inventory_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RetrieveConnectionString();
        }
        private void RetrieveConnectionString()
        {
            string readText = Cryptography.Decrypt(File.ReadAllText(Directory.GetCurrentDirectory() + "\\Resources\\ConnectionString.txt"));
            String[] cs = readText.Split(';');
            String[] ip = cs[0].Split('=');
            String[] database = cs[1].Split('=');
            String[] user = cs[2].Split('=');
            String[] password = cs[3].Split('=');
            tbIP.Text = ip[1];
            tbName.Text = database[1];
            tbUserName.Text = user[1];
            tbPassword.Text = password[1];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cnn;
            cnn = new SqlConnection("Data Source = " + tbIP.Text + "; Initial Catalog = " + tbName.Text + "; User ID = " + tbUserName.Text + "; Password = " + tbPassword.Text);
            try
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Open)
                {
                    MessageBox.Show("Connect Succeeded!");
                    cnn.Close();
                }
                else
                {
                    MessageBox.Show("Connection Error!");
                    cnn.Close();
                }
            }
            catch (Exception k)
            {
                MessageBox.Show(k + "Connection Error!");
                cnn.Close();
            }
            cnn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                TextWriter text = new StreamWriter(Directory.GetCurrentDirectory() + "\\Resources\\ConnectionString.txt");
                //TextWriter text = new StreamWriter("D:\\PayrollAndAttendanceSystem\\ConnectionString.txt");
                string cs = "Data Source=" + tbIP.Text + ";Initial Catalog=" + tbName.Text + ";User ID=" + tbUserName.Text + ";Password=" + tbPassword.Text;
                string connectionString = Cryptography.Encrypt(cs);
                text.Write(connectionString);
                text.Close();
                MessageBox.Show("Connection String Save!");
                this.Close();
            }
            catch
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

