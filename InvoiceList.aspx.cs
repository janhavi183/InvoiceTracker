////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Web;
////using System.Web.UI;
////using System.Web.UI.WebControls;
////using InvoiceTrackerWebApp.DAL;
//////using System;
////using System.Data;
////using System.Data.SqlClient;


////namespace InvoiceTrackerWebApp
////{
////    public partial class InvoiceList : System.Web.UI.Page
////    {
////        protected void Page_Load(object sender, EventArgs e)
////        {
////            if (!IsPostBack)
////            {
////                LoadInvoices();
////            }

////        }
////        private void LoadInvoices()
////        {
////            DatabaseHelper dbHelper = new DatabaseHelper();
////            string query = "";
////            int roleID = Convert.ToInt32(Session["RoleID"]);
////            int userID = Convert.ToInt32(Session["UserID"]);

////            if (roleID == 1)
////            {
////                query = "SELECT * FROM Invoice";
////            }
////            else if (roleID == 2)
////            {
////                query = "SELECT * FROM Invoice WHERE CreatedBy IN (SELECT EmpID FROM Employee WHERE Vendor = 'A' OR Vendor = 'B')";
////            }
////            else if (roleID == 3)
////            {
////                query = "SELECT * FROM Invoice WHERE CreatedBy = @userID";
////            }

////            SqlParameter[] parameters = roleID == 3 ? new SqlParameter[] { new SqlParameter("@userID", userID) } : null;

////            DataTable invoices = dbHelper.ExecuteQuery(query, parameters);
////            gvInvoices.DataSource = invoices;
////            gvInvoices.DataBind();
////        }
////        protected void gvInvoices_RowCommand(object sender, GridViewCommandEventArgs e)
////        {
////            int invoiceID = Convert.ToInt32(e.CommandArgument);

////            if (e.CommandName == "View")
////            {
////                ViewInvoice(invoiceID);
////            }
////            else if (e.CommandName == "Download")
////            {
////                DownloadInvoice(invoiceID);
////            }
////            else if (e.CommandName == "Edit")
////            {
////                Response.Redirect($"EditInvoice.aspx?id={invoiceID}");
////            }
////            else if (e.CommandName == "Delete")
////            {
////                DeleteInvoice(invoiceID);
////            }
////            else if (e.CommandName == "Details")
////            {
////                Response.Redirect($"InvoiceDetails.aspx?id={invoiceID}");
////            }
////        }

////        private void ViewInvoice(int invoiceID)
////        {
////            DatabaseHelper dbHelper = new DatabaseHelper();
////            string query = "SELECT [FILE] FROM Invoice WHERE ID = @ID";
////            SqlParameter[] parameters = { new SqlParameter("@ID", invoiceID) };

////            DataTable result = dbHelper.ExecuteQuery(query, parameters);
////            if (result.Rows.Count > 0)
////            {
////                byte[] fileData = (byte[])result.Rows[0]["FILE"];
////                Response.Clear();
////                Response.ContentType = "application/pdf"; // Assuming PDF files
////                Response.BinaryWrite(fileData);
////                Response.End();
////            }
////        }

////        private void DownloadInvoice(int invoiceID)
////        {
////            DatabaseHelper dbHelper = new DatabaseHelper();
////            string query = "SELECT [FILE] FROM Invoice WHERE ID = @ID";
////            SqlParameter[] parameters = { new SqlParameter("@ID", invoiceID) };

////            DataTable result = dbHelper.ExecuteQuery(query, parameters);
////            if (result.Rows.Count > 0)
////            {
////                byte[] fileData = (byte[])result.Rows[0]["FILE"];
////                Response.Clear();
////                Response.ContentType = "application/octet-stream";
////                Response.AddHeader("Content-Disposition", $"attachment; filename=Invoice_{invoiceID}.pdf");
////                Response.BinaryWrite(fileData);
////                Response.End();
////            }
////        }

////        private void DeleteInvoice(int invoiceID)
////        {
////            DatabaseHelper dbHelper = new DatabaseHelper();
////            string query = "DELETE FROM Invoice WHERE ID = @ID";
////            SqlParameter[] parameters = { new SqlParameter("@ID", invoiceID) };

////            dbHelper.ExecuteNonQuery(query, parameters);
////            LoadInvoices(); // Reload the grid
////        }
////        public override void VerifyRenderingInServerForm(Control control)
////        {
////            // This is required to render the GridView control when binding programmatically.
////        }
////    }
////}

//////public partial class InvoiceList : System.Web.UI.Page
//////{
//////    protected void Page_Load(object sender, EventArgs e)
//////    {

//////    }


//////}
//using InvoiceTrackerWebApp.DAL;
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace InvoiceTrackerWebApp
//{
//    public partial class InvoiceList : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                LoadInvoices();
//            }
//        }

//        //private void LoadInvoices()
//        //{
//        //    DatabaseHelper dbHelper = new DatabaseHelper();
//        //    string query = "SELECT * FROM Invoice";
//        //    DataTable invoices = dbHelper.ExecuteQuery(query, null);
//        //    gvInvoices.DataSource = invoices;
//        //    gvInvoices.DataBind();
//        //}
//        private void LoadInvoices()
//        {
//            DatabaseHelper dbHelper = new DatabaseHelper();
//            string query = @"
//        SELECT 
//            Invoice.ID,
//            Invoice.Amount,
//            Invoice.Comments,
//            Invoice.CreatedDate,
//            Invoice.[File] AS FileData,
//            InvoiceType.TypeName AS InvoiceType,
//            Employee.EmpName AS CreatedBy
//        FROM 
//            Invoice
//        JOIN 
//            InvoiceType ON Invoice.InvoiceTypeID = InvoiceType.InvoiceTypeID
//        JOIN 
//            Employee ON Invoice.CreatedBy = Employee.EmpID";

//            DataTable invoices = dbHelper.ExecuteQuery(query, null); // No parameters needed in this query
//            gvInvoices.DataSource = invoices;
//            gvInvoices.DataBind();
//        }



//        protected void gvInvoices_RowCommand(object sender, GridViewCommandEventArgs e)
//        {
//            int invoiceID = Convert.ToInt32(e.CommandArgument);

//            if (e.CommandName == "Edit")
//            {
//                Response.Redirect($"EditInvoice.aspx?ID={invoiceID}");
//            }
//            else if (e.CommandName == "Delete")
//            {
//                DeleteInvoice(invoiceID);
//            }
//            else if (e.CommandName == "Details")
//            {
//                Response.Redirect($"InvoiceDetails.aspx?ID={invoiceID}");
//            }
//        }

//        private void DeleteInvoice(int invoiceID)
//        {
//            DatabaseHelper dbHelper = new DatabaseHelper();
//            string query = "DELETE FROM Invoice WHERE ID = @ID";
//            SqlParameter[] parameters = { new SqlParameter("@ID", invoiceID) };
//            dbHelper.ExecuteNonQuery(query, parameters);
//            LoadInvoices();
//        }
//        public override void VerifyRenderingInServerForm(Control control)
//        {
//            // This is required to render the GridView control when binding programmatically.
//        }
//    }
//}
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
                gvInvoices.DataSource = invoices;
                gvInvoices.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<p style='color:red;'>Error loading invoices: {ex.Message}</p>");
            }
        }

        //        private void LoadInvoices()
        //        {
        //            DatabaseHelper dbHelper = new DatabaseHelper();
        //            string query = @"
        //        SELECT 
        //            Invoice.ID,
        //            Invoice.Amount,
        //            Invoice.Comments,
        //            Invoice.CreatedDate,
        //            InvoiceType.TypeName AS InvoiceType,
        //            Employee.EmpName AS CreatedBy
        //        FROM 
        //            Invoice
        //        INNER JOIN InvoiceType ON Invoice.InvoiceTypeID = InvoiceType.InvoiceTypeID
        //        INNER JOIN Employee ON Invoice.CreatedBy = Employee.EmpID
        //";

        //            int roleID = Convert.ToInt32(Session["RoleID"]);
        //            int userID = Convert.ToInt32(Session["UserID"]);

        //            // Apply filters based on role
        //            if (roleID == 2)  // Manager role
        //            {
        //                query += " WHERE Employee.Vendor IN ('A', 'B')";
        //            }
        //            else if (roleID == 3)  // Supervisor role
        //            {
        //                query += " WHERE Invoice.CreatedBy = @userID";
        //            }
        //            query += " ORDER BY Invoice.CreatedDate DESC";
        //            SqlParameter[] parameters = roleID == 3 ? new SqlParameter[] { new SqlParameter("@userID", userID) } : null;

        //            DataTable invoices = dbHelper.ExecuteQuery(query, parameters);
        //            gvInvoices.DataSource = invoices;
        //            gvInvoices.DataBind();

        //        }
        //private void LoadInvoices()
        //{
        //    string query = @"
        //        SELECT 
        //            i.ID,
        //            it.TypeName AS InvoiceTypeName,
        //            e.EmpName AS CreatedByName,
        //            i.Amount,
        //            i.Comments,
        //            i.CreatedDate
        //        FROM 
        //            Invoice i
        //        INNER JOIN 
        //            InvoiceType it ON i.InvoiceTypeID = it.InvoiceTypeID
        //        INNER JOIN 
        //            Employee e ON i.CreatedBy = e.EmpID";

        //    try
        //    {
        //        DataTable invoices = dbHelper.ExecuteQuery(query);
        //        gvInvoices.DataSource = invoices;
        //        gvInvoices.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write($"<p style='color:red;'>Error loading invoices: {ex.Message}</p>");
        //    }
        //}

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


        //private void DeleteInvoice(int invoiceId)
        //{
        //    string query = "DELETE FROM Invoice WHERE ID = @InvoiceID";
        //    SqlParameter[] parameters = {
        //        new SqlParameter("@InvoiceID", invoiceId)
        //    };

        //    try
        //    {
        //        dbHelper.ExecuteNonQuery(query, parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write($"<p style='color:red;'>Error deleting invoice: {ex.Message}</p>");
        //    }
        //}
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


    }
}
