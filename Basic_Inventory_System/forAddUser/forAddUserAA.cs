using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic_Inventory_System.forAddUser
{
    class forAddUserAA
    {
        public static void addingUser(string userID, string userName, string userPassword)
        {
            Connection cn = new Connection();

            if (userName == "")
            {
                return;
            }
            try
            {
                SqlCommand toCheckIfExisting = new SqlCommand("SELECT * FROM dbo.tblLogin WHERE userName = @userName", cn.connect());
                toCheckIfExisting.Parameters.AddWithValue("@userName", userName);
                SqlDataReader toCheckIfExistingReader = toCheckIfExisting.ExecuteReader();
                if (toCheckIfExistingReader.HasRows)
                {
                    MessageBox.Show("User Already Exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.connect().Close();
                }
                else
                {
                    SqlCommand toSaveNew = new SqlCommand("INSERT INTO dbo.tblLogin(userID,userName,userPassword)"
                    + " VALUES(@userName,@userPass,@userLevel)", cn.connect());

                    toSaveNew.Parameters.AddWithValue("@userName", userID);
                    toSaveNew.Parameters.AddWithValue("@userPass", userName);
                    toSaveNew.Parameters.AddWithValue("@userLevel", Cryptography.Encrypt(userPassword));

                    toSaveNew.ExecuteNonQuery();

                    MessageBox.Show("One User Added!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.connect().Close();
                }
            }
            catch (Exception k)
            {
                MessageBox.Show(k.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cn.connect().Close();
            }
        }
    }
}
