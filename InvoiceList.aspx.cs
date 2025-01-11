using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using InvoiceTrackerWebApp.DAL;

namespace InvoiceTrackerWebApp
{
    public partial class InvoiceList : System.Web.UI.Page
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInvoices();
            }
        }
        private void LoadInvoices()
        {
            string query = @"
        SELECT 
            Invoice.ID,
            Invoice.Amount,
            Invoice.Comments,
            Invoice.CreatedDate,
            InvoiceType.TypeName AS InvoiceType,
            Employee.EmpName AS CreatedBy
        FROM 
            Invoice
        INNER JOIN InvoiceType ON Invoice.InvoiceTypeID = InvoiceType.InvoiceTypeID
        INNER JOIN Employee ON Invoice.CreatedBy = Employee.EmpID";

            int roleID = Convert.ToInt32(Session["RoleID"]);
            int userID = Convert.ToInt32(Session["UserID"]);

            // Apply filters based on role
            SqlParameter[] parameters = null;

            if (roleID == 2) // Manager role
            {
                query += " WHERE Employee.Vendor IN ('A', 'B')";
            }
            else if (roleID == 3) // Supervisor role
            {
                query += " WHERE Invoice.CreatedBy = @userID";
                parameters = new SqlParameter[] { new SqlParameter("@userID", userID) };
            }
            else if (roleID != 1) // Non-admin roles other than 2 or 3
            {
                // No access to invoices for other roles
                gvInvoices.DataSource = null;
                gvInvoices.DataBind();
                return;
            }

            query += " ORDER BY Invoice.CreatedDate DESC";

            try
            {
                DataTable invoices = dbHelper.ExecuteQuery(query, parameters);

                // Calculate the total amount
                decimal totalAmount = 0;
                foreach (DataRow row in invoices.Rows)
                {
                    totalAmount += Convert.ToDecimal(row["Amount"]);
                }

                // Set the total amount label text
                lblTotalAmount.Text = "Total Amount: " + totalAmount.ToString("C");

                // Bind the data to the GridView
                gvInvoices.DataSource = invoices;
                gvInvoices.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<p style='color:red;'>Error loading invoices: {ex.Message}</p>");
            }
        }

       

        protected void gvInvoices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int invoiceId = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "ViewFile":
                    Response.Redirect($"FileViewer.aspx?InvoiceID={invoiceId}");
                    break;
                case "Edit":
                    Response.Redirect($"EditInvoice.aspx?InvoiceID={invoiceId}");
                    break;
                case "CustomDelete":
                    DeleteInvoice(invoiceId);
                    LoadInvoices(); // Refresh the grid
                    break;
                case "Details":
                    Response.Redirect($"DetailsInvoice.aspx?InvoiceID={invoiceId}");
                    break;
            }
        }
        decimal totalAmount = 0;

        protected void gvInvoices_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Check if the row is a data row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Assuming the Amount column is at index 1 (adjust if necessary)
                decimal amount = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
                totalAmount += amount;
            }
            // If this is the footer row
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total Amount:";
                e.Row.Cells[0].ColumnSpan = 2; // Merge cells if necessary
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

                // Adjusting the total amount display in the next cell
                e.Row.Cells[1].Text = totalAmount.ToString("C");
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;

                // Hide extra cells in the footer row if column spanning is used
                for (int i = 2; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Visible = false;
                }
            }
        }


        private void DeleteInvoice(int invoiceId)
        {
            string query = "DELETE FROM Invoice WHERE ID = @InvoiceID";
            SqlParameter[] parameters = {
        new SqlParameter("@InvoiceID", invoiceId)
    };

            try
            {
                dbHelper.ExecuteNonQuery(query, parameters); // Assuming dbHelper.ExecuteNonQuery() handles the query execution.
            }
            catch (Exception ex)
            {
                Response.Write($"<p style='color:red;'>Error deleting invoice: {ex.Message}</p>");
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            // Validate date inputs
            if (DateTime.TryParse(txtStartDate.Text, out DateTime startDate) &&
                DateTime.TryParse(txtEndDate.Text, out DateTime endDate))
            {
                if (startDate <= endDate)
                {
                    // Filter and bind data within the date range
                    BindInvoices(startDate, endDate);
                }
                else
                {
                    // Display error if start date > end date
                    lblMessage.Text = "Start Date cannot be after End Date.";
                    lblMessage.CssClass = "text-danger";
                }
            }
            else
            {
                // Display error for invalid date formats
                lblMessage.Text = "Please select valid dates.";
                lblMessage.CssClass = "text-danger";
            }
        }
        private void BindInvoices(DateTime startDate, DateTime endDate)
        {
            string query = @"
    SELECT 
        Invoice.ID,
        Invoice.Amount,
        Invoice.Comments,
        Invoice.CreatedDate,
        InvoiceType.TypeName AS InvoiceType,
        Employee.EmpName AS CreatedBy
    FROM 
        Invoice
    INNER JOIN InvoiceType ON Invoice.InvoiceTypeID = InvoiceType.InvoiceTypeID
    INNER JOIN Employee ON Invoice.CreatedBy = Employee.EmpID
    WHERE Invoice.CreatedDate BETWEEN @StartDate AND @EndDate
    ORDER BY Invoice.CreatedDate DESC";

            SqlParameter[] parameters = {
        new SqlParameter("@StartDate", startDate),
        new SqlParameter("@EndDate", endDate)
    };

            try
            {
                DataTable invoices = dbHelper.ExecuteQuery(query, parameters);

                // Calculate the total amount for the filtered invoices
                decimal totalAmount = 0;
                foreach (DataRow row in invoices.Rows)
                {
                    totalAmount += Convert.ToDecimal(row["Amount"]);
                }

                // Update the total amount label
                lblTotalAmount.Text = "Total Amount: " + totalAmount.ToString("C");

                // Bind the filtered data to the GridView
                gvInvoices.DataSource = invoices;
                gvInvoices.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<p style='color:red;'>Error loading invoices: {ex.Message}</p>");
            }
        }




    }
}
