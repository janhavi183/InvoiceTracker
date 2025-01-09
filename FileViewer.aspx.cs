using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InvoiceTrackerWebApp
{
    public partial class FileViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int invoiceId;
                if (int.TryParse(Request.QueryString["InvoiceID"], out invoiceId))
                {
                    LoadFile(invoiceId);
                }
                else
                {
                    ltMessage.Text = "<p style='color:red;'>Invalid or missing Invoice ID.</p>";
                }
            }
        }
        private void LoadFile(int invoiceId)
        {
            string connectionString = "Server=DESKTOP-2JBP0OL\\SQLEXPRESS;Database=InvoiceTrackDB;Trusted_Connection=True";
            string query = "SELECT [File] FROM Invoice WHERE ID = @InvoiceID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@InvoiceID", invoiceId);
                        conn.Open();

                        object fileData = cmd.ExecuteScalar();
                        if (fileData != null && fileData != DBNull.Value)
                        {
                            byte[] fileBytes = (byte[])fileData;

                            // Determine file type by file signature
                            string fileType = GetFileType(fileBytes);

                            string base64String = Convert.ToBase64String(fileBytes);
                            string htmlContent = string.Empty;

                            if (fileType.StartsWith("image/"))
                            {
                                // Render image using <img>
                                htmlContent = $"<img src='data:{fileType};base64,{base64String}' style='width:100%; height:auto;' />";
                            }
                            else if (fileType == "application/pdf")
                            {
                                // Render PDF using <iframe>
                                htmlContent = $"<iframe src='data:{fileType};base64,{base64String}' style='width:100%; height:600px;'></iframe>";
                            }
                            else
                            {
                                ltMessage.Text = "<p style='color:red;'>Unsupported file type. Please upload a valid image or PDF.</p>";
                                return;
                            }

                            // Insert the HTML content into the iframeContainer
                            iframeContainer.InnerHtml = htmlContent;
                        }
                        else
                        {
                            ltMessage.Text = "<p style='color:red;'>No file found for the provided Invoice ID.</p>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ltMessage.Text = $"<p style='color:red;'>Error loading file: {ex.Message}</p>";
            }
        }

        private string GetFileType(byte[] fileBytes)
        {
            // Example logic to identify file type from the byte array
            if (fileBytes.Length >= 4)
            {
                // Check for PDF magic number
                if (fileBytes[0] == 0x25 && fileBytes[1] == 0x50 && fileBytes[2] == 0x44 && fileBytes[3] == 0x46)
                {
                    return "application/pdf";
                }
                // Check for JPEG magic number
                else if (fileBytes[0] == 0xFF && fileBytes[1] == 0xD8)
                {
                    return "image/jpeg";
                }
                // Check for PNG magic number
                else if (fileBytes[0] == 0x89 && fileBytes[1] == 0x50 && fileBytes[2] == 0x4E && fileBytes[3] == 0x47)
                {
                    return "image/png";
                }
            }
            // Default to binary/octet-stream for unsupported file types
            return "application/octet-stream";
        }

        //private void LoadFile(int invoiceId)
        //{
        //    string connectionString = "Server=DESKTOP-2JBP0OL\\SQLEXPRESS;Database=InvoiceTrackDB;Trusted_Connection=True";
        //    string query = "SELECT [File] FROM Invoice WHERE ID = @InvoiceID";

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand(query, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@InvoiceID", invoiceId);
        //                conn.Open();

        //                object fileData = cmd.ExecuteScalar();
        //                if (fileData != null && fileData != DBNull.Value)
        //                {
        //                    byte[] fileBytes = (byte[])fileData;

        //                    // Determine file type by file signature or infer by content
        //                    string fileType = GetFileType(fileBytes);

        //                    string base64String = Convert.ToBase64String(fileBytes);
        //                    string htmlContent = string.Empty;

        //                    if (fileType.StartsWith("image/"))
        //                    {
        //                        htmlContent = $"<img src='data:{fileType};base64,{base64String}' style='width:100%; height:auto;' />";
        //                    }
        //                    else if (fileType == "application/pdf")
        //                    {
        //                        htmlContent = $"<iframe src='data:{fileType};base64,{base64String}' style='width:100%; height:600px;'></iframe>";
        //                    }
        //                    else
        //                    {
        //                        ltMessage.Text = "<p style='color:red;'>Unsupported file type.</p>";
        //                        return;
        //                    }

        //                    iframeContainer.InnerHtml = htmlContent;
        //                }
        //                else
        //                {
        //                    ltMessage.Text = "<p style='color:red;'>No file found for the provided Invoice ID.</p>";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ltMessage.Text = $"<p style='color:red;'>Error loading file: {ex.Message}</p>";
        //    }
        //}

        //private string GetFileType(byte[] fileBytes)
        //{
        //    // Example logic to identify file type from the byte array.
        //    if (fileBytes.Length >= 4)
        //    {
        //        // Check for PDF magic number
        //        if (fileBytes[0] == 0x25 && fileBytes[1] == 0x50 && fileBytes[2] == 0x44 && fileBytes[3] == 0x46)
        //        {
        //            return "application/pdf";
        //        }
        //        // Check for JPEG magic number
        //        else if (fileBytes[0] == 0xFF && fileBytes[1] == 0xD8)
        //        {
        //            return "image/jpeg";
        //        }
        //        // Check for PNG magic number
        //        else if (fileBytes[0] == 0x89 && fileBytes[1] == 0x50 && fileBytes[2] == 0x4E && fileBytes[3] == 0x47)
        //        {
        //            return "image/png";
        //        }
        //    }
        //    // Default to binary/octet-stream for unsupported file types
        //    return "application/octet-stream";
        //}

        ////private void LoadFile(int invoiceId)
        ////{
        ////    string connectionString = "Server=DESKTOP-2JBP0OL\\SQLEXPRESS;Database=InvoiceTrackDB;Trusted_Connection=True";
        ////    string query = "SELECT [File] FROM Invoice WHERE ID = @InvoiceID";

        ////    try
        ////    {
        ////        using (SqlConnection conn = new SqlConnection(connectionString))
        ////        {
        ////            using (SqlCommand cmd = new SqlCommand(query, conn))
        ////            {
        ////                cmd.Parameters.AddWithValue("@InvoiceID", invoiceId);
        ////                conn.Open();

        ////                object fileData = cmd.ExecuteScalar();
        ////                if (fileData != null && fileData != DBNull.Value)
        ////                {
        ////                    byte[] fileBytes = (byte[])fileData;

        ////                    // Write the file to the response as a downloadable or inline file
        ////                    Response.Clear();
        ////                    Response.ContentType = "application/pdf"; // Default to PDF
        ////                    Response.AddHeader("Content-Disposition", "inline; filename=Invoice_" + invoiceId + ".pdf");
        ////                    Response.BinaryWrite(fileBytes);
        ////                    Response.End();
        ////                }
        ////                else
        ////                {
        ////                    ltMessage.Text = "<p style='color:red;'>No file found for the provided Invoice ID.</p>";
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ltMessage.Text = $"<p style='color:red;'>Error loading file: {ex.Message}</p>";
        ////    }
        ////}
        ////protected void Page_Load(object sender, EventArgs e)
        //{

        //}
    }
}