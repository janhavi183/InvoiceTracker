<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditEmployee.aspx.cs" Inherits="InvoiceTrackerWebApp.EditEmployee" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Employee</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
            padding: 20px;
        }
        .form-container {
            background-color: #ffffff;
            border-radius: 8px;
            padding: 20px;
            max-width: 600px;
            margin: 0 auto;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        .form-container h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #007bff;
        }
        .alert {
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="form-container">
                <h2>Edit Employee</h2>
                <asp:Label ID="lblMessage" runat="server" CssClass="alert d-none"></asp:Label>
                <asp:HiddenField ID="hfEmpID" runat="server" />
                <div class="mb-3">
                    <label for="txtEmpName" class="form-label">Employee Name</label>
                    <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtCreatedBy" class="form-label">Created By</label>
                    <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtVendor" class="form-label">Vendor</label>
                    <asp:TextBox ID="txtVendor" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="txtRoleID" class="form-label">Role ID</label>
                    <asp:TextBox ID="txtRoleID" runat="server" CssClass="form-control" />
                </div>
                <div class="text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" PostBackUrl="~/ViewEmployee.aspx" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
