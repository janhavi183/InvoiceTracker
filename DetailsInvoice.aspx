<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailsInvoice.aspx.cs" Inherits="InvoiceTrackerWebApp.DetailsInvoice" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Invoice</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
<script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
    
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
    <script>
        $("#dropdownprofilebutton").on("click", function () {
            $("#dropdownprofile").toggleClass("show");
        });
    </script>

</head>
   <body>
    <form id="form1" runat="server">
    <div class="body-container">
          <nav class="navbar navbar-expand-lg navbar-light bg-light d-flex justify-content-between align-items-center">
    <div class="container-fluid">
        <!-- Breadcrumbs -->
        <nav aria-label="breadcrumb" class="d-flex align-items-center">
            <ol class="breadcrumb mb-0">
                <li class="breadcrumb-item"><a href="InvoiceList.aspx">Home</a></li>
                <li class="breadcrumb-item active" aria-current="page"> Invoice Details </li>
            </ol>
        </nav>
        <!-- Page Title -->
        <h4 class="mb-0 text-center flex-grow-1">Invoice Tracker</h4>
        <!-- Profile -->
               <div class="position-relative d-flex align-items-center">
    <button 
        id="dropdownprofilebutton" 
        class="border-0 rounded-circle bg-opacity-100" 
        style="background-color: #f8fafc;" 
        type="button" 
        data-bs-toggle="dropdown" 
        aria-expanded="false">
        <img 
            src="https://avatar.iran.liara.run/public" 
            alt="Profile Icon" 
            class="rounded-circle" 
            style="width: 40px; height: 40px;" />
         <asp:Label ID="lblUserName" runat="server" CssClass="username-label"></asp:Label>

    </button>
    <ul id="dropdownprofile" class="dropdown-menu dropdown-menu-end" style="z-index: 1050;">
        <li class="nav-item">
            <a class="nav-link d-flex align-items-center" href="Profile.aspx">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24" fill="currentColor"><path d="M12 2C17.5228 2 22 6.47715 22 12C22 17.5228 17.5228 22 12 22C6.47715 22 2 17.5228 2 12C2 6.47715 6.47715 2 12 2ZM12.1597 16C10.1243 16 8.29182 16.8687 7.01276 18.2556C8.38039 19.3474 10.114 20 12 20C13.9695 20 15.7727 19.2883 17.1666 18.1081C15.8956 16.8074 14.1219 16 12.1597 16ZM12 4C7.58172 4 4 7.58172 4 12C4 13.8106 4.6015 15.4807 5.61557 16.8214C7.25639 15.0841 9.58144 14 12.1597 14C14.6441 14 16.8933 15.0066 18.5218 16.6342C19.4526 15.3267 20 13.7273 20 12C20 7.58172 16.4183 4 12 4ZM12 5C14.2091 5 16 6.79086 16 9C16 11.2091 14.2091 13 12 13C9.79086 13 8 11.2091 8 9C8 6.79086 9.79086 5 12 5ZM12 7C10.8954 7 10 7.89543 10 9C10 10.1046 10.8954 11 12 11C13.1046 11 14 10.1046 14 9C14 7.89543 13.1046 7 12 7Z"></path></svg>
                Profile
            </a>
        </li>
        <li class="nav-item">
            <a class="nav-link d-flex align-items-center" href="Logout.aspx">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24" fill="currentColor"><path d="M5 22C4.44772 22 4 21.5523 4 21V3C4 2.44772 4.44772 2 5 2H19C19.5523 2 20 2.44772 20 3V6H18V4H6V20H18V18H20V21C20 21.5523 19.5523 22 19 22H5ZM18 16V13H11V11H18V8L23 12L18 16Z"></path></svg>
                Logout
            </a>
        </li>
    </ul>
</div>
    </div>
</nav>
  <div class="body-container">
            <div class="form-container">
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="txtAmount">Amount</label>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtComments">Comments</label>
                        <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtInvoiceType">Invoice Type</label>
                        <asp:TextBox ID="txtInvoiceType" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtCreatedDate">Created Date</label>
                        <asp:TextBox ID="txtCreatedDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    
<div class="d-flex mt-3">
     <asp:LinkButton ID="lnkDownloadFile" runat="server" CssClass="btn btn-secondary flex-fill" Text="Download File" OnClick="DownloadFile"></asp:LinkButton>    

</div>
                                        <div class="d-flex mt-3">
    <asp:LinkButton ID="lnkViewFile" runat="server" CssClass="btn btn-primary flex-fill" Text="View File" OnClick="ViewFile"></asp:LinkButton>
   
</div>
                    <div id="fileViewerContainer" runat="server" style="display:none;">
                        <iframe id="fileViewer" runat="server" style="display:none; width:100%; height:400px;" frameborder="0"></iframe>
                        <img id="imageViewer" runat="server" style="display:none; max-width:100%; height:auto;" />
                    </div>
                    
                </div>
            </div>
        </div>
        </div>
    </form>
       
  
</body>

</html>

