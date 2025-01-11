using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
            return Session["UserName"]?.ToString() ?? "Guest";
        }

        private void LoadInvoiceDetails(int invoiceId)

        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            string query = @"
        SELECT 
            i.ID, 
            i.Amount, 
            i.Comments, 
            it.TypeName AS InvoiceTypeName, 
            i.CreatedDate, 
            i.[FILE]
        FROM 
            Invoice i
        INNER JOIN 
            InvoiceType it ON i.InvoiceTypeID = it.InvoiceTypeID
        WHERE 
            i.ID = @InvoiceID";

            var parameters = new SqlParameter[] { new SqlParameter("@InvoiceID", invoiceId) };

            DataTable dt = dbHelper.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                // Populate fields with retrieved data
                txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                txtComments.Text = dt.Rows[0]["Comments"].ToString();
                txtInvoiceType.Text = dt.Rows[0]["InvoiceTypeName"].ToString(); // Use InvoiceTypeName
                txtCreatedDate.Text = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]).ToString("yyyy-MM-dd");
            
            //DatabaseHelper dbHelper = new DatabaseHelper();
            //string query = "SELECT ID, Amount, Comments, InvoiceTypeID,CreatedDate, [FILE] FROM Invoice WHERE ID = @InvoiceID";
            //var parameters = new SqlParameter[] { new SqlParameter("@InvoiceID", invoiceId) };

            //DataTable dt = dbHelper.ExecuteQuery(query, parameters);
            //if (dt.Rows.Count > 0)
            //{
            //    txtAmount.Text = dt.Rows[0]["Amount"].ToString();
            //    txtComments.Text = dt.Rows[0]["Comments"].ToString();
            //    txtInvoiceType.Text = dt.Rows[0]["InvoiceTypeID"].ToString();
            //    txtCreatedDate.Text = dt.Rows[0]["CreatedDate"].ToString();

            //    //LoadInvoiceTypes();
            //    //ddlInvoiceType.SelectedValue = dt.Rows[0]["InvoiceTypeID"].ToString();

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

        //private void LoadInvoiceTypes()
        //{
        //    DatabaseHelper dbHelper = new DatabaseHelper();
        //    string query = "SELECT InvoiceTypeID, TypeName FROM InvoiceType";
        //    DataTable dt = dbHelper.ExecuteQuery(query);

        //    ddlInvoiceType.DataSource = dt;
        //    ddlInvoiceType.DataTextField = "TypeName";
        //    ddlInvoiceType.DataValueField = "InvoiceTypeID";
        //    ddlInvoiceType.DataBind();
        //}

        //protected void UpdateInvoice(object sender, EventArgs e)
        //{
        //    int invoiceId = Convert.ToInt32(Request.QueryString["InvoiceID"]);
        //    string query = "UPDATE Invoice SET Amount = @Amount, Comments = @Comments, InvoiceTypeID = @InvoiceTypeID, [FILE] = @FileData WHERE ID = @InvoiceID";

        //    byte[] fileData = null;
        //    if (fileUpload.HasFile)
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            fileUpload.PostedFile.InputStream.CopyTo(ms);
        //            fileData = ms.ToArray();
        //        }
        //    }
        //    else if (ViewState["FileData"] != null)
        //    {
        //        fileData = Convert.FromBase64String(ViewState["FileData"].ToString());
        //    }

        //    var parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@Amount", txtAmount.Text),
        //        new SqlParameter("@Comments", txtComments.Text),
        //        new SqlParameter("@InvoiceTypeID", ddlInvoiceType.SelectedValue),
        //        new SqlParameter("@FileData", (object)fileData ?? DBNull.Value),
        //        new SqlParameter("@InvoiceID", invoiceId)
        //    };

        //    DatabaseHelper dbHelper = new DatabaseHelper();
        //    int result = dbHelper.ExecuteNonQuery(query, parameters);
        //    if (result > 0)
        //    {
        //        Response.Redirect("InvoiceList.aspx");
        //    }
        //    else
        //    {
        //        lblMessage.Text = "Failed to update the invoice.";
        //    }
        //}

        protected void ViewFile(object sender, EventArgs e)
        {
            if (ViewState["FileData"] != null)
            {
                string base64FileData = ViewState["FileData"].ToString();
                byte[] fileData = Convert.FromBase64String(base64FileData);

                string mimeType = GetMimeType(fileData);
                string fileDataUrl = $"data:{mimeType};base64,{base64FileData}";

                if (mimeType.StartsWith("image/"))
                {
                    imageViewer.Attributes["src"] = fileDataUrl;
                    imageViewer.Style["display"] = "block";
                    fileViewer.Style["display"] = "none";
                }
                else if (mimeType == "application/pdf")
                {
                    fileViewer.Attributes["src"] = fileDataUrl;
                    fileViewer.Style["display"] = "block";
                    imageViewer.Style["display"] = "none";
                }

                fileViewerContainer.Style["display"] = "block";
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            if (ViewState["FileData"] != null)
            {
                byte[] fileData = Convert.FromBase64String(ViewState["FileData"].ToString());
                string mimeType = GetMimeType(fileData);
                string fileExtension = GetFileExtension(mimeType);

                Response.ContentType = mimeType;
                Response.AppendHeader("Content-Disposition", $"attachment; filename=invoice{fileExtension}");
                Response.BinaryWrite(fileData);
                Response.End();
            }
        }

        private string GetMimeType(byte[] fileData)
        {
            // MIME detection based on file signature
            if (fileData.Length >= 4 && fileData[0] == 0x25 && fileData[1] == 0x50) return "application/pdf"; // PDF
            if (fileData.Length >= 4 && fileData[0] == 0xFF && fileData[1] == 0xD8) return "image/jpeg"; // JPEG
            if (fileData.Length >= 8 && fileData[0] == 0x89 && fileData[1] == 0x50) return "image/png"; // PNG
            return "application/octet-stream";
        }

        private string GetFileExtension(string mimeType)
        {
            if (mimeType == "application/pdf")
                return ".pdf";
            if (mimeType == "image/jpeg")
                return ".jpg";
            if (mimeType == "image/png")
                return ".png";
            return "";

        }
    }
}
