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
using System.Web;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Login
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtUsername, "");
            errorProvider1.SetError(txtPassword, "");
            if (txtUsername.Text.ToString().Trim() != "" && txtPassword.Text.ToString().Trim() != "")
            {
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("http://localhost:59");
                    client.BaseAddress = new Uri("http://loginwebapidesktopappdemo:8080/");
                    LoginClass lgn = new LoginClass { Email = txtUsername.Text.ToString(), Password = txtPassword.Text.ToString() };
                    var response = client.PostAsJsonAsync("api/Login/Login", lgn).Result;
                    var a = response.Content.ReadAsStringAsync();
                    if (a.Result.ToString().Trim() == "0")
                    {
                        lblErrorMessage.Text = "Invalid login credentials.";
                        lblErrorMessage.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblErrorMessage.Text = "Loggedin successfully.";
                        lblErrorMessage.ForeColor = Color.Green;
                    }
                }
            }
            else if (txtUsername.Text.ToString().Trim() == "" && txtPassword.Text.ToString().Trim() == "")
            {
                errorProvider1.SetError(txtUsername, "Please enter the email");
                errorProvider1.SetError(txtPassword, "Please enter the password");
            }
            else if (txtUsername.Text.ToString().Trim() == "")
            {
                errorProvider1.SetError(txtUsername, "Please enter the email");
            }
            else if (txtPassword.Text.ToString().Trim() == "")
            {
                errorProvider1.SetError(txtPassword, "Please enter the password");
            }
        }       

        private void ValidateEmail()
        {
            Regex regEmail = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+"
                                    + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                    + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                    + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                    + @"[a-zA-Z]{2,}))$",
                                    RegexOptions.Compiled);

            if (!regEmail.IsMatch(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Please enter a Valid Email Address.");
            }
            else
            {
                errorProvider1.SetError(txtUsername, "");
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmail();
        }


        public class LoginClass
        {
            public int id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

       
    }

   
}
