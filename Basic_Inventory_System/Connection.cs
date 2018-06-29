using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic_Inventory_System
{
    class Connection
    {
        SqlConnection cnn;
        public SqlConnection connect()
        {
            try
            {
                string readText = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Resources\\ConnectionString.txt");
                string connectionString = null;
                connectionString = Cryptography.Decrypt(readText);
                cnn = new SqlConnection(connectionString);
                cnn.Open();
            }
            catch (Exception k)
            {
                MessageBox.Show(k.Message + "Connection");
                cnn.Close();
            }
            return cnn;
        }
    }
}

