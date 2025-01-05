<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileViewer.aspx.cs" Inherits="InvoiceTrackerWebApp.FileViewer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Viewer</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>File Viewer</h2>
            <asp:Literal ID="ltMessage" runat="server" />
            <iframe id="fileFrame" runat="server" style="width:100%; height:600px;" />
        </div>
    </form>
</body>
</html>
