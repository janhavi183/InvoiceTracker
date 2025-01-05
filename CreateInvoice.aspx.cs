using InvoiceTrackerWebApp.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InvoiceTrackerWebApp
{
    public partial class CreateInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInvoiceTypes();
            }

        }
        private void LoadInvoiceTypes()
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            DataTable invoiceTypes = dbHelper.ExecuteQuery("SELECT * FROM InvoiceType WHERE Eveactive = 'active'");
            ddlInvoiceType.DataSource = invoiceTypes;
            ddlInvoiceType.DataTextField = "TypeName";
            ddlInvoiceType.DataValueField = "InvoiceTypeID";
            ddlInvoiceType.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int invoiceTypeID = Convert.ToInt32(ddlInvoiceType.SelectedValue);
            decimal amount = Convert.ToDecimal(txtAmount.Text);
            string comments = txtComments.Text;
            int createdBy = Convert.ToInt32(Session["UserID"]);
            byte[] fileData = null;

            if (fileUpload.HasFile)
            {
                using (BinaryReader reader = new BinaryReader(fileUpload.PostedFile.InputStream))
                {
                    fileData = reader.ReadBytes(fileUpload.PostedFile.ContentLength);
                }
            }

            string query = "INSERT INTO Invoice (InvoiceTypeID, CreatedBy, Amount, Comments, [FILE]) VALUES (@invoiceTypeID, @createdBy, @amount, @comments, @fileData)";
            SqlParameter[] parameters = {
            new SqlParameter("@invoiceTypeID", invoiceTypeID),
            new SqlParameter("@createdBy", createdBy),
            new SqlParameter("@amount", amount),
            new SqlParameter("@comments", comments),
            new SqlParameter("@fileData", fileData)
        };

            DatabaseHelper dbHelper = new DatabaseHelper();
            dbHelper.ExecuteNonQuery(query, parameters);
            Response.Redirect("InvoiceList.aspx");
        }

    }
}