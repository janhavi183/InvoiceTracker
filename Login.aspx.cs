using InvoiceTrackerWebApp.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InvoiceTrackerWebApp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            DatabaseHelper dbHelper = new DatabaseHelper();
            string query = "SELECT * FROM Employee WHERE EmpName = @username AND Pass = @password";
            SqlParameter[] parameters = {
            new SqlParameter("@username", username),
            new SqlParameter("@password", password)
        };

            DataTable userTable = dbHelper.ExecuteQuery(query, parameters);

            if (userTable.Rows.Count > 0)
            {
                Session["UserID"] = userTable.Rows[0]["EmpID"].ToString();
                Session["RoleID"] = userTable.Rows[0]["RoleID"].ToString();
                Session["UserName"] = userTable.Rows[0]["EmpName"].ToString();
                Response.Redirect("InvoiceList.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }
    }
}