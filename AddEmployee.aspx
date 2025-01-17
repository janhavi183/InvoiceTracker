﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="InvoiceTrackerWebApp.AddEmployee" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Employee</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
           .body {
    background-color: #f0f8ff; /* Light blue background */
    font-family: 'Barlow', sans-serif;
    align-items: center;
}
       .body-container{
            margin-top:20px;
            display: flex;
 justify-content: center;
 margin: 0;
 background-color: #f0f8ff; /* Light blue background */
 font-family: 'Barlow', sans-serif;
       }
.form-container {
   top: 10px;
    width:800px;
    background-color: #ffffff;
    font-size: 0;
    padding: 40px 30px;
    box-shadow: 0 0 10px rgba(0, 0, 255, 0.2), 0 0 30px rgba(0, 0, 255, 0.4);
    border-radius: 10px;
    position: relative;
    justify-content: center;
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
nav.navbar {
    position: fixed; /* Keeps the navbar at the top of the page */
    top: 0;
    width: 100%; /* Ensures the navbar spans the entire width */
    background-color: #007BFF; /* Blue background */
    z-index: 1000; /* Ensures the navbar stays above other elements */
    border-radius: 0; /* Remove any rounding for a full-width appearance */
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2); /* Subtle shadow for depth */
}

nav.navbar .navbar-brand {
    color: #fff; /* White text for brand name */
    font-weight: bold;
    font-size: 1.5rem;
    text-transform: uppercase;
}

nav.navbar .navbar-brand:hover {
    color: #e0e0e0; /* Slightly lighter on hover */
}

nav.navbar .d-flex .rounded-circle {
    border: 2px solid #ffffff; /* White border around profile icon */
    box-shadow: 0 0 5px rgba(255, 255, 255, 0.5); /* Glow effect */
}

nav.navbar .d-flex .rounded-circle:hover {
    transform: scale(1.1); /* Slightly larger on hover */
    transition: all 0.3s ease;
}

nav.navbar .d-flex asp\:Label {
    color: #ffffff; /* White text for the username */
    margin-right: 10px;
    font-size: 1rem;
}

/* Add space below the navbar to prevent overlap with the form */
body {
    padding-top: 70px; /* Adjust based on the navbar's height */
}
    .form-group label {
        color: black;
        font-size: 18px;
        font-weight: 300;
        text-transform: capitalize;
        margin-bottom: 2px;
        margin-top: 6px;
    }

    .d-flex {
        display: flex;
        gap: 10px; /* Space between the buttons */
    }

    .flex-fill {
        flex: 1;
    }

    .me-2 {
        margin-right: 10px;
    }

    </style>
</head>
<body>
    <div class="body-container">
                  <nav class="navbar navbar-expand-lg navbar-light bg-light d-flex justify-content-between align-items-center">
    <div class="container-fluid">
        <!-- Breadcrumbs -->
        <nav aria-label="breadcrumb" class="d-flex align-items-center">
            <ol class="breadcrumb mb-0">
                <li class="breadcrumb-item"><a href="InvoiceList.aspx">Home</a></li>
                <li class="breadcrumb-item active" aria-current="page"> Edit Invoice </li>
            </ol>
        </nav>
        <!-- Page Title -->
        <h4 class="mb-0 text-center flex-grow-1">Invoice Tracker</h4>
        <!-- Profile -->
        <div class="position-relative d-flex align-items-center">
            <button id="profileButton" class="border-0 rounded-circle bg-opacity-100 w-10 h-10 " style="background-color: #f8fafc;">
                <img src="https://avatar.iran.liara.run/public" alt="Profile Icon" class="rounded-circle "style="width: 40px; height: 40px;" />
                <asp:Label ID="lblUserName" runat="server" CssClass="username-label"></asp:Label>

            </button>
            <div id="profileDropdown" class="dropdown-menu position-absolute" style="right: 0; top: 45px;">
                <ul class="navbar-nav ms-auto flex-column">
                    <li class="nav-item"><a class="nav-link" href="Profile.aspx">Profile</a></li>
                    <li class="nav-item"><a class="nav-link" href="Logout.aspx">Logout</a></li>
                </ul>
            </div>
        </div>
    </div>
</nav>
    <form id="form1" runat="server">
        <div class="form-container">
            <h2>Add Employee</h2>

            <!-- Message Label -->
            <asp:Label ID="lblMessage" runat="server" CssClass="text-success"></asp:Label>

            <!-- Employee Name -->
            <div class="form-group row">
                <label for="txtEmpName" class="form-label">Employee Name</label>
                <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control" Placeholder="Enter Employee Name"></asp:TextBox>
            </div>

            <!-- Created By -->
            <div class="form-group row">
                <label for="txtCreatedBy" class="form-label">Created By</label>
                <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control" Placeholder="Enter Creator Name" Text="admin"></asp:TextBox>
            </div>

            <!-- Vendor -->
            <div class="form-group row">
                <label for="txtVendor" class="form-label">Vendor</label>
                <asp:TextBox ID="txtVendor" runat="server" CssClass="form-control" Placeholder="Enter Vendor"></asp:TextBox>
            </div>

            <!-- Password -->
            <div class="form-group row">
                <label for="txtPass" class="form-label">Password</label>
                <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" Placeholder="Enter Password"></asp:TextBox>
            </div>

            <!-- Role ID -->
            <div class="form-group row">
                <label for="txtRoleID" class="form-label">Role ID</label>
                <asp:TextBox ID="txtRoleID" runat="server" CssClass="form-control" Placeholder="Enter Role ID"></asp:TextBox>
            </div>

            <!-- Submit Button -->
            <div class="form-group row">
                <asp:Button ID="btnSubmit" runat="server" Text="Add Employee" CssClass="btn btn-primary" OnClick="AddEmployee_Click" />
            </div>
        </div>
    </form>
        </div>
</body>
</html>