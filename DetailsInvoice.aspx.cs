using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using InvoiceTrackerWebApp.DAL;

namespace InvoiceTrackerWebApp
{
    public partial class DetailsInvoice : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUserName.Text = GetLoggedInUserName();

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
            string query = "SELECT ID, Amount, Comments, InvoiceTypeID, CreatedDate, [FILE] FROM Invoice WHERE ID = @InvoiceID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@InvoiceID", invoiceId)
            };
            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                lblAmount.Text = dt.Rows[0]["Amount"].ToString();
                lblComments.Text = dt.Rows[0]["Comments"].ToString();
                lblCreatedDate.Text = dt.Rows[0]["CreatedDate"].ToString();
                lblInvoiceType.Text = GetInvoiceTypeName(Convert.ToInt32(dt.Rows[0]["InvoiceTypeID"]));

                if (dt.Rows[0]["FILE"] != DBNull.Value)
                {
                    ViewState["FileData"] = dt.Rows[0]["FILE"];
                    lnkViewFile.Visible = true;
                    lnkDownloadFile.Visible = true;
                }
                else
                {
                    lnkViewFile.Visible = false;
                    lnkDownloadFile.Visible = false;
                }
            }
            else
            {
                lblMessage.Text = "Invoice not found.";
            }
        }

        private string GetInvoiceTypeName(int invoiceTypeId)
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            string query = "SELECT TypeName FROM InvoiceType WHERE InvoiceTypeID = @InvoiceTypeID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@InvoiceTypeID", invoiceTypeId)
            };
            DataTable dt = dbHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["TypeName"].ToString();
            }

            return "Unknown";
        }

        protected void ViewFile(object sender, EventArgs e)
        {
            if (ViewState["FileData"] != null)
            {
                byte[] fileData = (byte[])ViewState["FileData"];
                string mimeType = MimeMapping.GetMimeMapping("dummy.pdf");

                string base64FileData = Convert.ToBase64String(fileData);
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
                byte[] fileData = (byte[])ViewState["FileData"];

                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=invoice.pdf");
                Response.BinaryWrite(fileData);
                Response.End();
            }
        }
    }
}
