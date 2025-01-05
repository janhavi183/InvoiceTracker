using System;
using System.Data;
using System.Data.SqlClient;
using InvoiceTrackerWebApp.DAL;

namespace InvoiceTrackerWebApp
{
    public partial class DetailsEmployee : System.Web.UI.Page
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string empID = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(empID))
                {
                    LoadEmployeeDetails(empID);
                }
                else
                {
                    // Redirect if no ID is provided
                    Response.Redirect("ViewEmployee.aspx");
                }
            }
        }

        private void LoadEmployeeDetails(string empID)
        {
            string query = "SELECT EmpID, EmpName, CreatedBy, Vendor, RoleID FROM Employee WHERE EmpID = @EmpID";
            SqlParameter[] parameters = { new SqlParameter("@EmpID", empID) };

            DataTable employeeData = dbHelper.ExecuteQuery(query, parameters);

            if (employeeData.Rows.Count > 0)
            {
                DataRow row = employeeData.Rows[0];
                lblEmpID.Text = row["EmpID"].ToString();
                lblEmpName.Text = row["EmpName"].ToString();
                lblCreatedBy.Text = row["CreatedBy"].ToString();
                lblVendor.Text = row["Vendor"].ToString();
                lblRoleID.Text = row["RoleID"].ToString();
            }
            else
            {
                // Display message if no data found
                lblEmpID.Text = "Not Found";
                lblEmpName.Text = "Not Found";
                lblCreatedBy.Text = "Not Found";
                lblVendor.Text = "Not Found";
                lblRoleID.Text = "Not Found";
            }
        }
    }
}
