<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="InvoiceTrackerWebApp.Logout" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Logout</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container d-flex justify-content-center align-items-center vh-100">
            <%--<a href="InvoiceList.aspx.designer.cs">InvoiceList.aspx.designer.cs</a>--%>
            <div class="text-center">
                <h3>You have been logged out.</h3>
                <p>Redirecting to login page...</p>
            </div>
        </div>
    </form>
</body>
</html>
