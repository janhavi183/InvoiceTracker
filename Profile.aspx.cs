using InvoiceTrackerWebApp.DAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace InvoiceTrackerWebApp
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                // Redirect to login page if session is not set
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadProfileData();
            }
        }

        private void LoadProfileData()
        {
            try
            {
                int empID = Convert.ToInt32(Session["UserID"]); // Fetch the EmpID from the session
                DatabaseHelper dbHelper = new DatabaseHelper();

                string query = "SELECT * FROM Employee WHERE EmpID = @EmpID";
                SqlParameter[] parameters = {
                    new SqlParameter("@EmpID", empID)
                };

                DataTable userTable = dbHelper.ExecuteQuery(query, parameters);

                if (userTable.Rows.Count > 0)
                {
                    DataRow row = userTable.Rows[0];
                    txtEmpID.Text = row["EmpID"].ToString();
                    txtEmpName.Text = row["EmpName"].ToString();
                    txtCreatedBy.Text = row["CreatedBy"].ToString();
                    txtVendor.Text = row["Vendor"].ToString();
                    txtPass.Text = row["Pass"].ToString();
                    txtRoleID.Text = row["RoleID"].ToString();
                }
                else
                {
                    lblMessage.Text = "User profile not found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred while loading the profile: " + ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int empID = Convert.ToInt32(Session["UserID"]);
                string empName = txtEmpName.Text;
                string createdBy = txtCreatedBy.Text;
                string vendor = txtVendor.Text;
                string password = txtPass.Text;
                int roleID = Convert.ToInt32(txtRoleID.Text);

                DatabaseHelper dbHelper = new DatabaseHelper();

                string updateQuery = @"
                    UPDATE Employee
                    SET EmpName = @EmpName,
                        CreatedBy = @CreatedBy,
                        Vendor = @Vendor,
                        Pass = @Pass,
                        RoleID = @RoleID
                    WHERE EmpID = @EmpID";

                SqlParameter[] parameters = {
                    new SqlParameter("@EmpID", empID),
                    new SqlParameter("@EmpName", empName),
                    new SqlParameter("@CreatedBy", createdBy),
                    new SqlParameter("@Vendor", vendor),
                    new SqlParameter("@Pass", password),
                    new SqlParameter("@RoleID", roleID)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery(updateQuery, parameters);

                if (rowsAffected > 0)
                {
                    lblMessage.Text = "Profile updated successfully.";
                }
                else
                {
                    lblMessage.Text = "Failed to update the profile.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred while updating the profile: " + ex.Message;
            }
        }
    }
}
