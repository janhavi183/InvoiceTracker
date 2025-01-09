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
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Web;
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
//                // Display logged-in user
//                lblUserName.Text = GetLoggedInUserName();

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

//        private string GetLoggedInUserName()
//        {
//            if (Session["UserName"] != null)
//            {
//                return Session["UserName"].ToString();
//            }
//            return "Guest"; // Default fallback if session is null
//        }
//        private void LoadInvoiceDetails(int invoiceId)
//        {
//            DatabaseHelper dbHelper = new DatabaseHelper();
//            string query = "SELECT ID, Amount, Comments, InvoiceTypeID, [FILE] FROM Invoice WHERE ID = @InvoiceID";
//            var parameters = new SqlParameter[]
//            {
//        new SqlParameter("@InvoiceID", invoiceId)
//            };
//            DataTable dt = dbHelper.ExecuteQuery(query, parameters);
//            if (dt.Rows.Count > 0)
//            {
//                txtAmount.Text = dt.Rows[0]["Amount"].ToString();
//                txtComments.Text = dt.Rows[0]["Comments"].ToString();

//                // Load invoice types in dropdown
//                LoadInvoiceTypes();
//                ddlInvoiceType.SelectedValue = dt.Rows[0]["InvoiceTypeID"].ToString();

//                if (dt.Rows[0]["FILE"] != DBNull.Value)
//                {
//                    byte[] fileData = (byte[])dt.Rows[0]["FILE"];
//                    ViewState["FileData"] = Convert.ToBase64String(fileData);
//                    lnkViewFile.Visible = true;
//                    lnkDownloadFile.Visible = true;
//                }
//                else
//                {
//                    lnkViewFile.Visible = false;
//                    lnkDownloadFile.Visible = false;
//                }

//            }
//        }

//        //private void LoadInvoiceDetails(int invoiceId)
//        //{
//        //    DatabaseHelper dbHelper = new DatabaseHelper();
//        //    string query = "SELECT ID, Amount, Comments, InvoiceTypeID, [FILE] FROM Invoice WHERE ID = @InvoiceID";
//        //    var parameters = new SqlParameter[] {
//        //        new SqlParameter("@InvoiceID", invoiceId)
//        //    };
//        //    DataTable dt = dbHelper.ExecuteQuery(query, parameters);
//        //    if (dt.Rows.Count > 0)
//        //    {
//        //        txtAmount.Text = dt.Rows[0]["Amount"].ToString();
//        //        txtComments.Text = dt.Rows[0]["Comments"].ToString();

//        //        // Load invoice types in dropdown
//        //        LoadInvoiceTypes();
//        //        ddlInvoiceType.SelectedValue = dt.Rows[0]["InvoiceTypeID"].ToString();

//        //        // Display file information
//        //        if (dt.Rows[0]["FILE"] != DBNull.Value)
//        //        {
//        //            byte[] fileData = (byte[])dt.Rows[0]["FILE"];
//        //            string base64File = Convert.ToBase64String(fileData);
//        //            ViewState["FileData"] = base64File;

//        //            // Make the file view and download links visible
//        //            lnkViewFile.Visible = true;
//        //            lnkDownloadFile.Visible = true;
//        //        }
//        //        else
//        //        {
//        //            lnkViewFile.Visible = false;
//        //            lnkDownloadFile.Visible = false;
//        //        }
//        //    }
//        //}

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
//            string query = "UPDATE Invoice SET Amount = @Amount, Comments = @Comments, InvoiceTypeID = @InvoiceTypeID, [FILE] = @FileData WHERE ID = @InvoiceID";

//            byte[] fileData = null;
//            if (fileUpload.HasFile)
//            {
//                // Save the file data to the database
//                using (MemoryStream ms = new MemoryStream())
//                {
//                    fileUpload.PostedFile.InputStream.CopyTo(ms);
//                    fileData = ms.ToArray();
//                }
//            }
//            else if (ViewState["FileData"] != null)
//            {
//                fileData = Convert.FromBase64String(ViewState["FileData"].ToString());
//            }

//            var parameters = new SqlParameter[] {
//                new SqlParameter("@Amount", txtAmount.Text),
//                new SqlParameter("@Comments", txtComments.Text),
//                new SqlParameter("@InvoiceTypeID", ddlInvoiceType.SelectedValue),
//                new SqlParameter("@FileData", (object)fileData ?? DBNull.Value),
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

//        //protected void ViewFile(object sender, EventArgs e)
//        //{
//        //    if (ViewState["FileData"] != null)
//        //    {
//        //        string base64File = ViewState["FileData"].ToString();
//        //        byte[] fileData = Convert.FromBase64String(base64File);

//        //        Response.ContentType = "application/pdf";  // Update to the correct MIME type for your file
//        //        Response.AppendHeader("Content-Disposition", "inline; filename=invoice.pdf");
//        //        Response.BinaryWrite(fileData);
//        //        Response.End();
//        //    }
//        //}
//        protected void ViewFile(object sender, EventArgs e)
//        {
//            if (ViewState["FileData"] != null)
//            {
//                string base64FileData = ViewState["FileData"].ToString();
//                byte[] fileData = Convert.FromBase64String(base64FileData);
//                string mimeType = "application/pdf"; // Example, adapt as needed

//                string fileDataUrl = $"data:{mimeType};base64,{base64FileData}";
//                fileViewer.Attributes["src"] = fileDataUrl;
//                fileViewer.Style["display"] = "block";
//            }
//            else
//            {
//                lblMessage.Text = "The file does not exist.";
//                fileViewer.Style["display"] = "none";
//            }
//        }

//        protected void DownloadFile(object sender, EventArgs e)
//        {
//            if (ViewState["FileData"] != null)
//            {
//                byte[] fileData = Convert.FromBase64String(ViewState["FileData"].ToString());

//                Response.ContentType = "application/pdf";
//                Response.AppendHeader("Content-Disposition", "attachment; filename=invoice.pdf");
//                Response.BinaryWrite(fileData);
//                Response.End();
//            }
//        }
//    }
//}
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Web;
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
//                // Display logged-in user
//                lblUserName.Text = GetLoggedInUserName();

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

//        private string GetLoggedInUserName()
//        {
//            if (Session["UserName"] != null)
//            {
//                return Session["UserName"].ToString();
//            }
//            return "Guest"; // Default fallback if session is null
//        }

//        private void LoadInvoiceDetails(int invoiceId)
//        {
//            DatabaseHelper dbHelper = new DatabaseHelper();
//            string query = "SELECT ID, Amount, Comments, InvoiceTypeID, [FILE] FROM Invoice WHERE ID = @InvoiceID";
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

//                if (dt.Rows[0]["FILE"] != DBNull.Value)
//                {
//                    byte[] fileData = (byte[])dt.Rows[0]["FILE"];
//                    ViewState["FileData"] = Convert.ToBase64String(fileData);
//                    lnkViewFile.Visible = true;
//                    lnkDownloadFile.Visible = true;
//                }
//                else
//                {
//                    lnkViewFile.Visible = false;
//                    lnkDownloadFile.Visible = false;
//                }
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
//            string query = "UPDATE Invoice SET Amount = @Amount, Comments = @Comments, InvoiceTypeID = @InvoiceTypeID, [FILE] = @FileData WHERE ID = @InvoiceID";

//            byte[] fileData = null;
//            if (fileUpload.HasFile)
//            {
//                // Save the file data to the database
//                using (MemoryStream ms = new MemoryStream())
//                {
//                    fileUpload.PostedFile.InputStream.CopyTo(ms);
//                    fileData = ms.ToArray();
//                }
//            }
//            else if (ViewState["FileData"] != null)
//            {
//                fileData = Convert.FromBase64String(ViewState["FileData"].ToString());
//            }

//            var parameters = new SqlParameter[]
//            {
//                new SqlParameter("@Amount", txtAmount.Text),
//                new SqlParameter("@Comments", txtComments.Text),
//                new SqlParameter("@InvoiceTypeID", ddlInvoiceType.SelectedValue),
//                new SqlParameter("@FileData", (object)fileData ?? DBNull.Value),
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

//        protected void ViewFile(object sender, EventArgs e)
//        {
//            if (ViewState["FileData"] != null)
//            {
//                string base64FileData = ViewState["FileData"].ToString();
//                byte[] fileData = Convert.FromBase64String(base64FileData);

//                string mimeType = DetectMimeType(fileData); // New helper method to detect MIME type.

//                if (mimeType.StartsWith("image/"))
//                {
//                    // If it's an image, display in <img> tag.
//                    string fileDataUrl = $"data:{mimeType};base64,{base64FileData}";
//                    imageViewer.Attributes["src"] = fileDataUrl;
//                    imageViewer.Style["display"] = "block";
//                    fileViewer.Style["display"] = "none";
//                }
//                else if (mimeType == "application/pdf")
//                {
//                    // If it's a PDF, display in <iframe>.
//                    string fileDataUrl = $"data:{mimeType};base64,{base64FileData}";
//                    fileViewer.Attributes["src"] = fileDataUrl;
//                    fileViewer.Style["display"] = "block";
//                    imageViewer.Style["display"] = "none";
//                }
//                else
//                {
//                    lblMessage.Text = "Unsupported file type.";
//                    imageViewer.Style["display"] = "none";
//                    fileViewer.Style["display"] = "none";
//                }

//                fileViewerContainer.Style["display"] = "block";
//            }
//            else
//            {
//                lblMessage.Text = "The file does not exist.";
//                fileViewerContainer.Style["display"] = "none";
//            }
//        }

//        // Helper method to detect MIME type from file data.
//        private string DetectMimeType(byte[] fileData)
//        {
//            using (var ms = new MemoryStream(fileData))
//            {
//                System.Drawing.Image img = null;
//                try
//                {
//                    img = System.Drawing.Image.FromStream(ms);
//                    return "image/" + img.RawFormat.ToString().ToLower();
//                }
//                catch
//                {
//                    return "application/pdf"; // Assume PDF if not an image (can expand for other formats).
//                }
//                finally
//                {
//                    img?.Dispose();
//                }
//            }
//        }


//        protected void DownloadFile(object sender, EventArgs e)
//        {
//            if (ViewState["FileData"] != null)
//            {
//                byte[] fileData = Convert.FromBase64String(ViewState["FileData"].ToString());
//                string mimeType = GetMimeType(fileData);
//                string fileExtension = GetFileExtension(mimeType);

//                Response.ContentType = mimeType;
//                Response.AppendHeader("Content-Disposition", $"attachment; filename=invoice{fileExtension}");
//                Response.BinaryWrite(fileData);
//                Response.End();
//            }
//        }

//        private string GetMimeType(byte[] fileData)
//        {
//            // A simple example: Use file signature (magic number) detection or fallback to a default.
//            if (fileData.Length >= 4 && fileData[0] == 0x25 && fileData[1] == 0x50 && fileData[2] == 0x44 && fileData[3] == 0x46)
//            {
//                return "application/pdf"; // PDF file signature
//            }
//            else if (fileData.Length >= 4 && fileData[0] == 0xFF && fileData[1] == 0xD8)
//            {
//                return "image/jpeg"; // JPEG file signature
//            }
//            else if (fileData.Length >= 8 && fileData[0] == 0x89 && fileData[1] == 0x50 && fileData[2] == 0x4E && fileData[3] == 0x47)
//            {
//                return "image/png"; // PNG file signature
//            }
//            return "application/octet-stream"; // Default binary type
//        }

//        private string GetFileExtension(string mimeType)
//        {
//            switch (mimeType)
//            {
//                case "application/pdf":
//                    return ".pdf";
//                case "image/jpeg":
//                    return ".jpg";
//                case "image/png":
//                    return ".png";
//                default:
//                    return ""; // Unknown file type
//            }
//        }
//    }
//}
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
            string query = "SELECT ID, Amount, Comments, InvoiceTypeID, [FILE] FROM Invoice WHERE ID = @InvoiceID";
            var parameters = new SqlParameter[] { new SqlParameter("@InvoiceID", invoiceId) };

            DataTable dt = dbHelper.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                txtComments.Text = dt.Rows[0]["Comments"].ToString();

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

            var parameters = new SqlParameter[]
            {
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

