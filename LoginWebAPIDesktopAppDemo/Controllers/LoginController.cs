using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using LoginWebAPIDesktopAppDemo.Models;

namespace LoginWebAPIDesktopAppDemo.Controllers
{
    public class LoginController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["customerDB"].ToString());
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adp = null;

        [HttpGet]
        [ActionName("getCustomerInfo")]        
        public DataTable Get()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from tbl_Cust";
                cmd.Connection = con;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                adp = new SqlDataAdapter(cmd);
                dt.TableName = "tbl_Cust";
                adp.Fill(dt);
                con.Close();
            }
            catch
            {
                
            }
            return dt;
        }

        [HttpPost]
        public int Login([System.Web.Http.FromBody] Login lgn)
        {
            int ret = 0;
            try
            {
                cmd.CommandType = CommandType.Text;                
                cmd.CommandText = "SELECT count(*) FROM tbl_Cust WHERE Email ='" + lgn.Email.Trim() + "' and Password=" + lgn.Password.Trim();
                cmd.Connection = con;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                ret = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            catch
            {
            }
            return ret;
        }
    }
}
