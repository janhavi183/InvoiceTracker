<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailsEmployee.aspx.cs" Inherits="InvoiceTrackerWebApp.DetailsEmployee" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Details</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .details-container {
            margin: 50px auto;
            padding: 20px;
            border-radius: 8px;
            background-color: #fff;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            max-width: 600px;
        }
        h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #007bff;
        }
        .detail-row {
            margin-bottom: 10px;
        }
        .detail-label {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="details-container">
                <h2>Employee Details</h2>
                <div class="detail-row">
                    <span class="detail-label">Employee ID:</span>
                    <asp:Label ID="lblEmpID" runat="server" Text=""></asp:Label>
                </div>
                <div class="detail-row">
                    <span class="detail-label">Employee Name:</span>
                    <asp:Label ID="lblEmpName" runat="server" Text=""></asp:Label>
                </div>
                <div class="detail-row">
                    <span class="detail-label">Created By:</span>
                    <asp:Label ID="lblCreatedBy" runat="server" Text=""></asp:Label>
                </div>
                <div class="detail-row">
                    <span class="detail-label">Vendor:</span>
                    <asp:Label ID="lblVendor" runat="server" Text=""></asp:Label>
                </div>
                <div class="detail-row">
                    <span class="detail-label">Role ID:</span>
                    <asp:Label ID="lblRoleID" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
