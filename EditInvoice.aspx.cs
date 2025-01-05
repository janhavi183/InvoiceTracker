//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web.UI;
//using InvoiceTrackerWebApp.DAL;

//namespace InvoiceTrackerWebApp
//{
//    public partial class EditInvoice : Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                // Check if InvoiceID is passed in query string
//                if (Request.QueryString["InvoiceID"] != null)
//                {
//                    int invoiceId = Convert.ToInt32(Request.QueryString["InvoiceID"]);
//                    LoadInvoiceDetails(invoiceId);
//                }
//                else
//                {
//                    lblMessage.Text = "Invoice ID is missing.";
//                }
//            }
//        }

//        private void LoadInvoiceDetails(int invoiceId)
//        {
//            DatabaseHelper dbHelper = new DatabaseHelper();
//            string query = "SELECT ID, Amount, Comments, InvoiceTypeID FROM Invoice WHERE ID = @InvoiceID";
//            var parameters = new SqlParameter[]
//            {
//                new SqlParameter("@InvoiceID", invoiceId)
//            };
//            DataTable dt = dbHelper.ExecuteQuery(query, parameters);
//            if (dt.Rows.Count > 0)
//            {
//                txtAmount.Text = dt.Rows[0]["Amount"].ToString();
//                txtComments.Text = dt.Rows[0]["Comments"].ToString();

//                // Load invoice types in dropdown
//                LoadInvoiceTypes();
//                ddlInvoiceType.SelectedValue = dt.Rows[0]["InvoiceTypeID"].ToString();
//            }
//        }

//        private void LoadInvoiceTypes()
//        {
//            DatabaseHelper dbHelper = new DatabaseHelper();
//            string query = "SELECT InvoiceTypeID, TypeName FROM InvoiceType";
//            DataTable dt = dbHelper.ExecuteQuery(query);
//            ddlInvoiceType.DataSource = dt;
//            ddlInvoiceType.DataTextField = "TypeName";
//            ddlInvoiceType.DataValueField = "InvoiceTypeID";
//            ddlInvoiceType.DataBind();
//        }

//        protected void UpdateInvoice(object sender, EventArgs e)
//        {
//            int invoiceId = Convert.ToInt32(Request.QueryString["InvoiceID"]);
//            string query = "UPDATE Invoice SET Amount = @Amount, Comments = @Comments, InvoiceTypeID = @InvoiceTypeID WHERE ID = @InvoiceID";

//            var parameters = new SqlParameter[]
//            {
//                new SqlParameter("@Amount", txtAmount.Text),
//                new SqlParameter("@Comments", txtComments.Text),
//                new SqlParameter("@InvoiceTypeID", ddlInvoiceType.SelectedValue),
//                new SqlParameter("@InvoiceID", invoiceId)
//            };

//            DatabaseHelper dbHelper = new DatabaseHelper();
//            int result = dbHelper.ExecuteNonQuery(query, parameters);
//            if (result > 0)
//            {
//                Response.Redirect("InvoiceList.aspx");
//            }
//            else
//            {
//                lblMessage.Text = "Failed to update the invoice.";
//            }
//        }
//    }
//}
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using InvoiceTrackerWebApp.DAL;

namespace InvoiceTrackerWebApp
{
    public partial class EditInvoice : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Display logged-in user
                lblUserName.Text = GetLoggedInUserName();

                // Check if InvoiceID is passed in query string
                if (Request.QueryString["InvoiceID"] != null)
                {
                    int invoiceId = Convert.ToInt32(Request.QueryString["InvoiceID"]);
                    LoadInvoiceDetails(invoiceId);
                }
                else
                {
                    lblMessage.Text = "Invoice ID is missing.";
                }
            }
        }

        private string GetLoggedInUserName()
        {
            if (Session["UserName"] != null)
            {
                return Session["UserName"].ToString();
            }
            return "Guest"; // Default fallback if session is null
        }
        private void LoadInvoiceDetails(int invoiceId)
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            string query = "SELECT ID, Amount, Comments, InvoiceTypeID, [FILE] FROM Invoice WHERE ID = @InvoiceID";
            var parameters = new SqlParameter[]
            {
        new SqlParameter("@InvoiceID", invoiceId)
            };
            DataTable dt = dbHelper.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                txtComments.Text = dt.Rows[0]["Comments"].ToString();

                // Load invoice types in dropdown
                LoadInvoiceTypes();
                ddlInvoiceType.SelectedValue = dt.Rows[0]["InvoiceTypeID"].ToString();

                if (dt.Rows[0]["FILE"] != DBNull.Value)
                {
                    byte[] fileData = (byte[])dt.Rows[0]["FILE"];
                    ViewState["FileData"] = Convert.ToBase64String(fileData);
                    lnkViewFile.Visible = true;
                    lnkDownloadFile.Visible = true;
                }
                else
                {
                    lnkViewFile.Visible = false;
                    lnkDownloadFile.Visible = false;
                }

            }
        }

        //private void LoadInvoiceDetails(int invoiceId)
        //{
        //    DatabaseHelper dbHelper = new DatabaseHelper();
        //    string query = "SELECT ID, Amount, Comments, InvoiceTypeID, [FILE] FROM Invoice WHERE ID = @InvoiceID";
        //    var parameters = new SqlParameter[] {
        //        new SqlParameter("@InvoiceID", invoiceId)
        //    };
        //    DataTable dt = dbHelper.ExecuteQuery(query, parameters);
        //    if (dt.Rows.Count > 0)
        //    {
        //        txtAmount.Text = dt.Rows[0]["Amount"].ToString();
        //        txtComments.Text = dt.Rows[0]["Comments"].ToString();

        //        // Load invoice types in dropdown
        //        LoadInvoiceTypes();
        //        ddlInvoiceType.SelectedValue = dt.Rows[0]["InvoiceTypeID"].ToString();

        //        // Display file information
        //        if (dt.Rows[0]["FILE"] != DBNull.Value)
        //        {
        //            byte[] fileData = (byte[])dt.Rows[0]["FILE"];
        //            string base64File = Convert.ToBase64String(fileData);
        //            ViewState["FileData"] = base64File;

        //            // Make the file view and download links visible
        //            lnkViewFile.Visible = true;
        //            lnkDownloadFile.Visible = true;
        //        }
        //        else
        //        {
        //            lnkViewFile.Visible = false;
        //            lnkDownloadFile.Visible = false;
        //        }
        //    }
        //}

        private void LoadInvoiceTypes()
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            string query = "SELECT InvoiceTypeID, TypeName FROM InvoiceType";
            DataTable dt = dbHelper.ExecuteQuery(query);
            ddlInvoiceType.DataSource = dt;
            ddlInvoiceType.DataTextField = "TypeName";
            ddlInvoiceType.DataValueField = "InvoiceTypeID";
            ddlInvoiceType.DataBind();
        }

        protected void UpdateInvoice(object sender, EventArgs e)
        {
            int invoiceId = Convert.ToInt32(Request.QueryString["InvoiceID"]);
            string query = "UPDATE Invoice SET Amount = @Amount, Comments = @Comments, InvoiceTypeID = @InvoiceTypeID, [FILE] = @FileData WHERE ID = @InvoiceID";

            byte[] fileData = null;
            if (fileUpload.HasFile)
            {
                // Save the file data to the database
                using (MemoryStream ms = new MemoryStream())
                {
                    fileUpload.PostedFile.InputStream.CopyTo(ms);
                    fileData = ms.ToArray();
                }
            }
            else if (ViewState["FileData"] != null)
            {
                fileData = Convert.FromBase64String(ViewState["FileData"].ToString());
            }

            var parameters = new SqlParameter[] {
                new SqlParameter("@Amount", txtAmount.Text),
                new SqlParameter("@Comments", txtComments.Text),
                new SqlParameter("@InvoiceTypeID", ddlInvoiceType.SelectedValue),
                new SqlParameter("@FileData", (object)fileData ?? DBNull.Value),
                new SqlParameter("@InvoiceID", invoiceId)
            };

            DatabaseHelper dbHelper = new DatabaseHelper();
            int result = dbHelper.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                Response.Redirect("InvoiceList.aspx");
            }
            else
            {
                lblMessage.Text = "Failed to update the invoice.";
            }
        }

        //protected void ViewFile(object sender, EventArgs e)
        //{
        //    if (ViewState["FileData"] != null)
        //    {
        //        string base64File = ViewState["FileData"].ToString();
        //        byte[] fileData = Convert.FromBase64String(base64File);

        //        Response.ContentType = "application/pdf";  // Update to the correct MIME type for your file
        //        Response.AppendHeader("Content-Disposition", "inline; filename=invoice.pdf");
        //        Response.BinaryWrite(fileData);
        //        Response.End();
        //    }
        //}
        protected void ViewFile(object sender, EventArgs e)
        {
            if (ViewState["FileData"] != null)
            {
                string base64FileData = ViewState["FileData"].ToString();
                byte[] fileData = Convert.FromBase64String(base64FileData);
                string mimeType = "application/pdf"; // Example, adapt as needed

                string fileDataUrl = $"data:{mimeType};base64,{base64FileData}";
                fileViewer.Attributes["src"] = fileDataUrl;
                fileViewer.Style["display"] = "block";
            }
            else
            {
                lblMessage.Text = "The file does not exist.";
                fileViewer.Style["display"] = "none";
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            if (ViewState["FileData"] != null)
            {
                byte[] fileData = Convert.FromBase64String(ViewState["FileData"].ToString());

                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=invoice.pdf");
                Response.BinaryWrite(fileData);
                Response.End();
            }
        }
    }
}
