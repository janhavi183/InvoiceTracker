using System;
using System.Data;
using System.Data.SqlClient;
using InvoiceTrackerWebApp.DAL;

namespace InvoiceTrackerWebApp
{
    public partial class EditEmployee : System.Web.UI.Page
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
                hfEmpID.Value = row["EmpID"].ToString();
                txtEmpName.Text = row["EmpName"].ToString();
                txtCreatedBy.Text = row["CreatedBy"].ToString();
                txtVendor.Text = row["Vendor"].ToString();
                txtRoleID.Text = row["RoleID"].ToString();
            }
            else
            {
                // Redirect to ViewEmployee if no data found
                Response.Redirect("ViewEmployee.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE Employee SET EmpName = @EmpName, CreatedBy = @CreatedBy, Vendor = @Vendor, RoleID = @RoleID WHERE EmpID = @EmpID";

                SqlParameter[] parameters = {
                    new SqlParameter("@EmpID", hfEmpID.Value),
                    new SqlParameter("@EmpName", txtEmpName.Text.Trim()),
                    new SqlParameter("@CreatedBy", txtCreatedBy.Text.Trim()),
                    new SqlParameter("@Vendor", txtVendor.Text.Trim()),
                    new SqlParameter("@RoleID", txtRoleID.Text.Trim())
                };

                int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    // Redirect back to ViewEmployee after saving
                    Response.Redirect("ViewEmployee.aspx");
                }
                else
                {
                    lblMessage.Text = "Failed to save changes. Please try again.";
                    lblMessage.CssClass = "text-danger";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "text-danger";
            }
        }
    }
}
