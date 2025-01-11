using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using InvoiceTrackerWebApp.DAL;

namespace InvoiceTrackerWebApp
{
    public partial class ViewEmployee : System.Web.UI.Page
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmployees();
            }
        }

        private void LoadEmployees()
        {
            string query = "SELECT EmpID, EmpName, CreatedBy, Vendor, RoleID FROM Employee";
            gvEmployees.DataSource = dbHelper.ExecuteQuery(query);
            gvEmployees.DataBind();
        }

        protected void gvEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmployees.PageIndex = e.NewPageIndex;
            LoadEmployees();
        }

        protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CustomDelete")
            {
                string empID = e.CommandArgument.ToString();
                hfDeleteEmpID.Value = empID;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowDeleteModal",
                    "openDeleteModal();", true);
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string empID = hfDeleteEmpID.Value;

                if (!string.IsNullOrEmpty(empID))
                {
                    string query = "DELETE FROM Employee WHERE EmpID = @EmpID";
                    SqlParameter[] parameters = { new SqlParameter("@EmpID", empID) };

                    int rowsAffected = dbHelper.ExecuteNonQuery(query, parameters);

                    if (rowsAffected > 0)
                    {
                        lblSuccess.Text = "Employee deleted successfully.";
                        lblError.Text = string.Empty;
                        LoadEmployees();
                    }
                    else
                    {
                        lblError.Text = "Error: Unable to delete employee.";
                        lblSuccess.Text = string.Empty;
                    }
                }
                else
                {
                    lblError.Text = "Error: Employee ID is empty.";
                    lblSuccess.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
                lblSuccess.Text = string.Empty;
            }
        }
    }
}
