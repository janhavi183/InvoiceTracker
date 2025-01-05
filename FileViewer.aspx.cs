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

                            // Write the file to the response as a downloadable or inline file
                            Response.Clear();
                            Response.ContentType = "application/pdf"; // Default to PDF
                            Response.AddHeader("Content-Disposition", "inline; filename=Invoice_" + invoiceId + ".pdf");
                            Response.BinaryWrite(fileBytes);
                            Response.End();
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
    //protected void Page_Load(object sender, EventArgs e)
    //{

    //}
}
}