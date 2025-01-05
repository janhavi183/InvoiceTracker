<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="InvoiceList.aspx.cs" Inherits="InvoiceTrackerWebApp.InvoiceList" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice List</title>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
    
    <style>
        body {
            display: flex;
            min-height: 100vh;
            flex-direction: column;
        }

        .dashboard-container {
            display: flex;
            flex: 1;
        }

        .sidebar {
            width: 240px;
            background-color: #e5e7eb;
            color: #020617;
            padding-top: 20px;
            height: 100vh;
            position: sticky;
            top:0;
        }

        .sidebar a {
            color: black;
            text-decoration: none;
            padding: 10px 20px;
            display: block;
        }

        .sidebar a:hover {
            background-color: #495057;
            color:white;
        }

        .content {
            flex: 1;
            padding: 0px;
            background-color: #f8f9fa;
        }

        .navbar-custom {
            width: 100vw;
            display:flex;
            justify-content:space-between;
            background-color: #f8fafc;
            padding-top:14px;
        }

        .navbar-custom .navbar-brand,
        .navbar-custom .nav-link {
            color: black;
        }
        .breadcrumb {
  display: flex;
  list-style: none;
  padding: 0;
  margin: 0;
}

.breadcrumb-item + .breadcrumb-item::before {
  content: ">";
  margin: 0 5px;
  color: #6c757d;
}

.breadcrumb-item a {
  text-decoration: none;
  color: #007bff;
}

.breadcrumb-item a:hover {
  text-decoration: underline;
}
    </style>
    <script>
        $(document).ready(function () {
            // Search filter
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#gvInvoices tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
            $("#dropdownprofilebutton").on("click", function () {
                $("#dropdownprofile").toggleClass("show");
            });
            // Dropdown filter
            $("#dropdownRadioButton").on("click", function () {
                $("#dropdownRadio").toggleClass("show");
            });

            $(document).on("click", function (e) {
                if (!$(e.target).closest("#dropdownRadio, #dropdownRadioButton").length) {
                    $("#dropdownRadio").removeClass("show");
                }
            });

            $("#filterOptions input[type='radio']").on("change", function () {
                const selectedValue = $(this).val();
                filterInvoices(selectedValue);
            });

            function filterInvoices(filterValue) {
                const today = new Date();
                $("#gvInvoices tr").filter(function () {
                    const rowDate = new Date($(this).find("td:nth-child(5)").text());
                    let isVisible = false;

                    switch (filterValue) {
                        case "1":
                            isVisible = (today - rowDate) / (1000 * 60 * 60 * 24) <= 1;
                            break;
                        case "7":
                            isVisible = (today - rowDate) / (1000 * 60 * 60 * 24) <= 7;
                            break;
                        case "30":
                            isVisible = (today - rowDate) / (1000 * 60 * 60 * 24) <= 30;
                            break;
                        case "30_1":
                            isVisible = rowDate.getMonth() === today.getMonth() - 1 && rowDate.getFullYear() === today.getFullYear();
                            break;
                        case "12":
                            isVisible = rowDate.getFullYear() === today.getFullYear();
                            break;
                    }

                    $(this).toggle(isVisible);
                });
            }
        });
    </script>
</head>
<body>

    <form id="form1" runat="server">
        <div class="dashboard-container">
            <% if (Session["RoleID"] != null && Convert.ToInt32(Session["RoleID"]) == 1) { %>
            <!-- Sidebar -->
            <div class="sidebar ml-4">
                <h4 class="text-center ml-2">Invoice Tracker</h4>
                <a href="InvoiceList.aspx" class="active">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-receipt-text">
                        <path d="M4 2v20l2-1 2 1 2-1 2 1 2-1 2 1 2-1 2 1 2-1V2l-2 1-2-1-2 1-2-1-2 1-2-1-2 1Z" />
                        <path d="M14 8H8" />
                        <path d="M16 12H8" />
                        <path d="M13 16H8" />
                    </svg> Invoices
                </a>
                <a href="CreateInvoice.aspx"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="currentColor"><path d="M15 4H5V20H19V8H15V4ZM3 2.9918C3 2.44405 3.44749 2 3.9985 2H16L20.9997 7L21 20.9925C21 21.5489 20.5551 22 20.0066 22H3.9934C3.44476 22 3 21.5447 3 21.0082V2.9918ZM11 11V8H13V11H16V13H13V16H11V13H8V11H11Z"></path></svg>Create Invoice</a>
                <a href="AddEmployee.aspx"> <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24" fill="currentColor"><path d="M14 14.252V16.3414C13.3744 16.1203 12.7013 16 12 16C8.68629 16 6 18.6863 6 22H4C4 17.5817 7.58172 14 12 14C12.6906 14 13.3608 14.0875 14 14.252ZM12 13C8.685 13 6 10.315 6 7C6 3.685 8.685 1 12 1C15.315 1 18 3.685 18 7C18 10.315 15.315 13 12 13ZM12 11C14.21 11 16 9.21 16 7C16 4.79 14.21 3 12 3C9.79 3 8 4.79 8 7C8 9.21 9.79 11 12 11ZM18 17V14H20V17H23V19H20V22H18V19H15V17H18Z"></path></svg>Add employee</a>
                <a href="ViewEmployee.aspx"> <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24" fill="currentColor"><path d="M12 14V16C8.68629 16 6 18.6863 6 22H4C4 17.5817 7.58172 14 12 14ZM12 13C8.685 13 6 10.315 6 7C6 3.685 8.685 1 12 1C15.315 1 18 3.685 18 7C18 10.315 15.315 13 12 13ZM12 11C14.21 11 16 9.21 16 7C16 4.79 14.21 3 12 3C9.79 3 8 4.79 8 7C8 9.21 9.79 11 12 11ZM14.5946 18.8115C14.5327 18.5511 14.5 18.2794 14.5 18C14.5 17.7207 14.5327 17.449 14.5945 17.1886L13.6029 16.6161L14.6029 14.884L15.5952 15.4569C15.9883 15.0851 16.4676 14.8034 17 14.6449V13.5H19V14.6449C19.5324 14.8034 20.0116 15.0851 20.4047 15.4569L21.3971 14.8839L22.3972 16.616L21.4055 17.1885C21.4673 17.449 21.5 17.7207 21.5 18C21.5 18.2793 21.4673 18.551 21.4055 18.8114L22.3972 19.3839L21.3972 21.116L20.4048 20.543C20.0117 20.9149 19.5325 21.1966 19.0001 21.355V22.5H17.0001V21.3551C16.4677 21.1967 15.9884 20.915 15.5953 20.5431L14.603 21.1161L13.6029 19.384L14.5946 18.8115ZM18 19.5C18.8284 19.5 19.5 18.8284 19.5 18C19.5 17.1716 18.8284 16.5 18 16.5C17.1716 16.5 16.5 17.1716 16.5 18C16.5 18.8284 17.1716 19.5 18 19.5Z"></path></svg>View employee</a>

            </div>
            <% } %>


            <!-- Content Area -->
            <div class="content ml-4"  <%= Session["RoleID"] == null || Convert.ToInt32(Session["RoleID"]) != 1 ? "style='width: 100%;'" : "" %>>
               
                <nav class="navbar navbar-expand-lg navbar-light bg-light d-flex justify-content-between align-items-center">
    <div class="container-fluid">
        <!-- Left-aligned Breadcrumbs -->
        <nav aria-label="breadcrumb" class="d-flex align-items-center">
            <ol class="breadcrumb mb-0">
                <li class="breadcrumb-item"><a href="/">Home</a></li>
                <li class="breadcrumb-item active" aria-current="page">Invoice Tracker</li>
            </ol>
        </nav>

        <!-- Center-aligned Heading -->
        <div class="flex-grow-1 text-center">
            <% if (Session["RoleID"] == null || Convert.ToInt32(Session["RoleID"]) != 1) { %>
                <h4 class="mb-0">Invoice Tracker</h4>
            <% } %>
        </div>

        <!-- Right-aligned Profile Section -->
        <div class="position-relative d-flex align-items-center">
            <button id="dropdownprofilebutton" class="border-0 rounded-circle bg-opacity-100 h-10 w-10 " style="background-color:#f8fafc;" type="button"><img src="https://avatar.iran.liara.run/public" alt="Profile Icon" class="rounded-circle" style="width: 40px; height: 40px;" /></button>
 <div id="dropdownprofile" class=" position-absolute dropdown-menu" style="right:2px; top:45px; ">
 <ul  class="navbar-nav ms-auto flex-column">
         <li class="nav-item">

             <a class="nav-link d-flex" href="Profile.aspx"><svg xmlns="http://www.w3.org/2000/svg"  width="24" height="24" viewBox="0 0 24 24" fill="currentColor"><path d="M12 2C17.5228 2 22 6.47715 22 12C22 17.5228 17.5228 22 12 22C6.47715 22 2 17.5228 2 12C2 6.47715 6.47715 2 12 2ZM12.1597 16C10.1243 16 8.29182 16.8687 7.01276 18.2556C8.38039 19.3474 10.114 20 12 20C13.9695 20 15.7727 19.2883 17.1666 18.1081C15.8956 16.8074 14.1219 16 12.1597 16ZM12 4C7.58172 4 4 7.58172 4 12C4 13.8106 4.6015 15.4807 5.61557 16.8214C7.25639 15.0841 9.58144 14 12.1597 14C14.6441 14 16.8933 15.0066 18.5218 16.6342C19.4526 15.3267 20 13.7273 20 12C20 7.58172 16.4183 4 12 4ZM12 5C14.2091 5 16 6.79086 16 9C16 11.2091 14.2091 13 12 13C9.79086 13 8 11.2091 8 9C8 6.79086 9.79086 5 12 5ZM12 7C10.8954 7 10 7.89543 10 9C10 10.1046 10.8954 11 12 11C13.1046 11 14 10.1046 14 9C14 7.89543 13.1046 7 12 7Z"></path></svg><p style="margin-left:2px;">Profile</p></a>
         </li>
         <li class="nav-item">
             <a class="nav-link d-flex" href="Logout.aspx"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="24" height="24" fill="currentColor"><path d="M12 22C6.47715 22 2 17.5228 2 12C2 6.47715 6.47715 2 12 2C15.2713 2 18.1757 3.57078 20.0002 5.99923L17.2909 5.99931C15.8807 4.75499 14.0285 4 12 4C7.58172 4 4 7.58172 4 12C4 16.4183 7.58172 20 12 20C14.029 20 15.8816 19.2446 17.2919 17.9998L20.0009 17.9998C18.1765 20.4288 15.2717 22 12 22ZM19 16V13H11V11H19V8L24 12L19 16Z"></path></svg><p style="margin-left:2px;">Logout</p></a>
         </li>
     </ul>
     </div>
        </div>
    </div>
</nav>


                <div class="container mt-4">
                    <div class="d-flex justify-content-between">
                    <h2 class="mb-4">Invoice List</h2>
                        <div class="d-flex flex-row align-content-center justify-content-between gap-4">
                        <div class="position-relative d-flex align-items-center">
                                            <% if (Session["RoleID"] == null || Convert.ToInt32(Session["RoleID"]) != 1) { %>
    <a href="CreateInvoice.aspx" class="btn btn-success mb-3 ">Create Invoice</a>
<% } %></div>
                                 <div class="mb-3">
                <button id="dropdownRadioButton" class="btn btn-secondary dropdown-toggle d-flex align-items-center" type="button">
 
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="16" height="16" fill="currentColor"><path d="M14 14V20L10 22V14L4 5V3H20V5L14 14ZM6.4037 5L12 13.3944L17.5963 5H6.4037Z"></path></svg>
 <span class="ms-2">Filter</span>
   </button>
                 <div id="dropdownRadio" class="dropdown-menu">
                     <asp:RadioButtonList ID="filterOptions" runat="server" CssClass="p-3 space-y-1 text-sm text-gray-700">
                         <asp:ListItem Value="1">Today</asp:ListItem>
                         <asp:ListItem Value="7" >Last 7 days</asp:ListItem>
                         <asp:ListItem Value="30" Selected="True">Last 30 days</asp:ListItem>
                         <asp:ListItem Value="30_1">Last month</asp:ListItem>
                         <asp:ListItem Value="12">Last year</asp:ListItem>
                     </asp:RadioButtonList>
                 </div>
             </div>

                        </div>
                        </div>
                   
 

                    <!-- Dropdown Menu -->
                 

                    <!-- Search Input -->
                    <div class="row mb-3">
                        <div class="col-md-12">
                            <input type="text" id="myInput" class="form-control" placeholder="Search invoices..." />
                        </div>
                    </div>

                    <asp:GridView ID="gvInvoices" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover" OnRowCommand="gvInvoices_RowCommand" ShowFooter="True" OnRowDataBound="gvInvoices_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="Invoice ID" SortExpression="ID" />
                            <asp:BoundField DataField="InvoiceType" HeaderText="Invoice Type" SortExpression="InvoiceType" />
                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:C}" SortExpression="Amount" />
            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd}" SortExpression="CreatedDate" />
            <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" />
            <asp:TemplateField HeaderText="File Viewer">
                <ItemTemplate>
                    <iframe src='<%# "FileViewer.aspx?InvoiceID=" + Eval("ID") %>' width="150" height="100"></iframe>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' Text="Edit" CssClass="btn btn-warning btn-sm"></asp:LinkButton>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="CustomDelete" CommandArgument='<%# Eval("ID") %>' Text="Delete" CssClass="btn btn-danger btn-sm"></asp:LinkButton>
                    <asp:LinkButton ID="btnDetails" runat="server" CommandName="Details" CommandArgument='<%# Eval("ID") %>' Text="Details" CssClass="btn btn-info btn-sm"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

        </div>
    </form>
</body>
</html>

