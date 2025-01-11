<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateInvoice.aspx.cs" Inherits="InvoiceTrackerWebApp.CreateInvoice" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Invoice</title>
    <link href="https://fonts.googleapis.com/css2?family=Barlow:wght@400;500&display=swap" rel="stylesheet"/>
    <style>
        body {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            background-color: #f0f8ff; /* Light blue background */
            font-family: 'Barlow', sans-serif;
        }
        .form-container {
            background-color: #ffffff;
            font-size: 0;
            padding: 40px 30px;
            box-shadow: 0 0 10px rgba(0, 0, 255, 0.2), 0 0 30px rgba(0, 0, 255, 0.4);
            border-radius: 10px;
            position: relative;
        }
        .form-container:before,
        .form-container:after {
            content: '';
            height: 50%;
            width: 20%;
            border-left: 7px dashed #007BFF;
            border-top: 7px dashed #007BFF;
            opacity: 0.5;
            position: absolute;
            left: 5px;
            top: 5px;
        }
        .form-container:after {
            height: 20%;
            width: 50%;
            transform: rotate(180deg);
            top: auto;
            bottom: 5px;
            left: auto;
            right: 5px;
        }
        .form-container .title {
            color: #333;
            font-size: 25px;
            font-weight: 500;
            text-transform: capitalize;
            margin: 0 0 30px;
            text-align: center;
        }
        .form-container .form-horizontal {
            font-size: 0;
        }
        .form-container .form-horizontal .form-group {
            margin: 0 0 25px;
        }
        .form-container .form-horizontal .form-group.row {
            display: flex;
            flex-direction: column;
        }
        .form-container .form-horizontal .form-group label {
            color: #555;
            font-size: 14px;
            font-weight: 500;
            text-transform: uppercase;
            margin-bottom: 8px;
        }
        .form-container .form-horizontal .form-control {
            color: #333;
            background: #f9f9f9;
            font-weight: 400;
            height: 36px;
            padding: 6px 10px;
            border-radius: 5px;
            border: 1px solid #e0e0e0;
            box-shadow: none;
        }
        .form-container .form-horizontal .form-control:focus {
            border-color: #007BFF;
            box-shadow: 0 0 8px rgba(0, 123, 255, 0.5);
            outline: none;
        }
        .form-container .form-horizontal .btn {
            color: #fff;
            background: #007BFF;
            font-size: 15px;
            font-weight: 500;
            text-transform: uppercase;
            width: 100%;
            padding: 10px;
            border-radius: 5px;
            border: none;
            cursor: pointer;
            transition: all 0.3s ease;
        }
        .form-container .form-horizontal .btn:hover {
            background: #0056b3;
            box-shadow: 0 0 10px rgba(0, 123, 255, 0.7);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <div class="title">Create Invoice</div>
            <div class="form-horizontal">
                <div class="form-group row">
                    <label for="ddlInvoiceType">Invoice Type</label>
                    <asp:DropDownList ID="ddlInvoiceType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="" />
                        <asp:ListItem Text="Service Invoice" Value="Service" />
                        <asp:ListItem Text="Product Invoice" Value="Product" />
                    </asp:DropDownList>
                </div>
                <div class="form-group row">
                    <label for="txtAmount">Amount</label>
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Placeholder="Enter amount"></asp:TextBox>
                </div>
                <div class="form-group row">
                    <label for="txtComments">Comments</label>
                    <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine" Placeholder="Add comments"></asp:TextBox>
                </div>
                <div class="form-group row">
                    <label for="fileUpload">Upload File</label>
                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group row">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>

