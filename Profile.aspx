<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="InvoiceTrackerWebApp.Profile" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Profile</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: #e2e8f0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        .profile-container {
            width: 800px;
            margin: auto;
            padding: 20px;
            background: #fff;
            border-radius: 15px;
            box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.2);
        }
        .profile-container h3 {
            margin-bottom: 20px;
            font-weight: bold;
            color: #333;
        }
        .profile-container label {
            font-weight: bold;
            color: #555;
        }
        .profile-container .form-control {
            margin-bottom: 15px;
        }
        .btn-primary {
            background-color: #4338ca;
            border: none;
        }
        .btn-primary:hover {
            background-color: #86198f;
        }
        .profile-img {
            display: block;
            margin: 0 auto 15px;
            width: 100px;
            height: 100px;
            border-radius: 50%;
            object-fit: cover;
        }
        .navbar-custom {
    background-color: #4338ca;
}

.navbar-custom .navbar-brand,
.navbar-custom .nav-link {
    color: white;
}
    </style>
</head>
<body>
     <!-- Top Navigation Bar -->
 <nav class="navbar navbar-expand-lg navbar-custom">
     <div class="container-fluid">
         <a class="navbar-brand" href="#">Invoice Tracker</a>
         <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
             <span class="navbar-toggler-icon"></span>
         </button>
         <div class="collapse navbar-collapse" id="navbarNav">
             <ul class="navbar-nav ms-auto">
                 <li class="nav-item">
                     <a class="nav-link" href="Logout.aspx">Logout</a>
                 </li>
             </ul>
         </div>
     </div>
 </nav>

    <form id="form1" runat="server">
        <div class="d-flex justify-content-center align-items-center vh-100">
            <div class="profile-container text-center">
                <!-- Profile Heading -->
                <h3 class="text-start">Profile</h3>
                <!-- Profile Form -->
                <div class="text-start">
                    <label for="txtEmpName">Employee Name:</label>
                    <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                    <label for="txtEmpID">Employee ID:</label>
                    <asp:TextBox ID="txtEmpID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                    <label for="txtCreatedBy">Created By:</label>
                    <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control"></asp:TextBox>

                    <label for="txtVendor">Vendor:</label>
                    <asp:TextBox ID="txtVendor" runat="server" CssClass="form-control"></asp:TextBox>

                    <label for="txtPass">Password:</label>
                    <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>

                    <label for="txtRoleID">Role ID:</label>
                    <asp:TextBox ID="txtRoleID" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <!-- Feedback Message -->
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block mt-3"></asp:Label>

                <!-- Update Button -->
                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary w-100 mt-3" Text="Update Profile" OnClick="btnUpdate_Click" />
            </div>
        </div>
    </form>
</body>
</html>
