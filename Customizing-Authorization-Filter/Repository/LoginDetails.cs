using Customizing_Authorization_Filter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Customizing_Authorization_Filter.Repository
{
    public class LoginDetails
    {
        string conn = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        public UserModel GetUserDetails(string Username,string Password)
        {
            UserModel userModel = new UserModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("GetUserDetails", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", Username);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        DataTable dt =dataSet.Tables[0];
                        if (dt != null) {
                            foreach (DataRow dr in dt.Rows)
                            {

                                userModel.UserID = Convert.ToInt32(dr["UserID"]);
                                userModel.UserName = dr["UserName"].ToString();
                                userModel.Password = dr["Password"].ToString();
                                userModel.Roles = dr["Roles"].ToString();
                            }
                        }
                        
                    }
                    conn.Close();
                }
            }
            catch(Exception ex) {
                
            }
            return userModel;
        }
    }
}