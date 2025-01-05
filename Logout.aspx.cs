using System;

namespace InvoiceTrackerWebApp
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear session
            Session.Clear();
            Session.Abandon();

            // Optionally clear authentication cookie if forms authentication is used
            if (Request.Cookies[".ASPXAUTH"] != null)
            {
                Response.Cookies[".ASPXAUTH"].Expires = DateTime.Now.AddDays(-1);
            }

            // Redirect to login page after a delay (or immediately)
            Response.AppendHeader("Refresh", "2;url=Login.aspx");
        }
    }
}
