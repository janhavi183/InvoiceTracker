using System;
using System.Data.SqlClient;
using InvoiceTrackerWebApp.DAL; // Namespace for DatabaseHelper
using System.Web.UI;

namespace InvoiceTrackerWebApp
{
    public partial class AddEmployee : System.Web.UI.Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper(); // Ensure proper initialization

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialization logic here if needed
        }

        protected void AddEmployee_Click(object sender, EventArgs e)
        {
            // Ensure all controls are not null
            if (txtEmpName == null || txtRoleID == null || lblMessage == null)
            {
                lblMessage.Text = "One or more controls are not properly initialized.";
                lblMessage.CssClass = "text-danger";
                return;
            }

            // Collect form input values
            string empName = txtEmpName.Text.Trim();
            string createdBy = txtCreatedBy.Text.Trim();
            string vendor = txtVendor.Text.Trim();
            string password = txtPass.Text.Trim();
            int roleID;

            // Validate RoleID input
            if (!int.TryParse(txtRoleID.Text.Trim(), out roleID))
            {
                lblMessage.Text = "Invalid Role ID. Please enter a valid number.";
                lblMessage.CssClass = "text-danger";
                return;
            }

            try
            {
                // Define the SQL query with parameterized inputs
                string query = "INSERT INTO Employee (EmpName, CreatedBy, Vendor, Pass, RoleID) VALUES (@EmpName, @CreatedBy, @Vendor, @Pass, @RoleID)";

                // Prepare SQL parameters
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmpName", empName),
                    new SqlParameter("@CreatedBy", createdBy),
                    new SqlParameter("@Vendor", vendor),
                    new SqlParameter("@Pass", password),
                    new SqlParameter("@RoleID", roleID)
                };

                // Execute the query using DatabaseHelper
                int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);

                // Provide feedback to the user
                if (rowsAffected > 0)
                {
                    lblMessage.Text = "Employee added successfully!";
                    lblMessage.CssClass = "text-success";
                    ClearForm();
                }
                else
                {
                    lblMessage.Text = "Failed to add employee. Please try again.";
                    lblMessage.CssClass = "text-danger";
                }
            }
            catch (Exception ex)
            {
                // Handle errors and display a friendly message
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.CssClass = "text-danger";
            }
        }

        private void ClearForm()
        {
            // Clear all input fields
            txtEmpName.Text = string.Empty;
            txtCreatedBy.Text = "admin"; // Reset to default
            txtVendor.Text = string.Empty;
            txtPass.Text = string.Empty;
            txtRoleID.Text = string.Empty;
        }
    }
}
