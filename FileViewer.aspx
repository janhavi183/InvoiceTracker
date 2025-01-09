<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileViewer.aspx.cs" Inherits="InvoiceTrackerWebApp.FileViewer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Viewer</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="ltMessage" runat="server" />
            <!-- Dynamic content container for images or PDFs -->
            <div id="iframeContainer" runat="server" style="width:100%; height:auto;"></div>
        </div>
    </form>
</body>
</html>
