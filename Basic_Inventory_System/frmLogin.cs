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

namespace Basic_Inventory_System
{
    public partial class frmLogin : Form
    {
        int attempt = 3;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Connection cn = new Connection();

          
            if (attempt == 0)
            {
                label2.Text = ("ALL 3 ATTEMPTS HAVE FAILED - CONTACT ADMIN");
            }
            Connection cn = new Connection();

            SqlCommand scmd = new SqlCommand("select count(*) from tblLogin where userName=@usr and userPassword=@pwd", cn.connect());
            scmd.Parameters.Clear();
            scmd.Parameters.AddWithValue("@usr", tbuser.Text);
            scmd.Parameters.AddWithValue("@pwd", Cryptography.Encrypt(tbPassword.Text));
            int readLogin = Convert.ToInt32(scmd.ExecuteScalar());

            if (readLogin == 1)
            {
                Form f = Application.OpenForms["mainForm"];
                if (f == null)
                {
                    mainForm objUI = new mainForm();
                    objUI.ShowDialog();
                    
                }
                else
                {
                    MessageBox.Show("Edit User Form is Already Open.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Hide();
            }

            else {

                
                MessageBox.Show("YOU ARE NOT GRANTED WITH ACCESS");
                label2.Text = ("You Have Only " + Convert.ToString(attempt) + " Attempt Left To Try");
                --attempt;
                tbuser.Clear();
                tbPassword.Clear();
                return;
            }
            
        }
    }
}
