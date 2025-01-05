<%--<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEmployee.aspx.cs" Inherits="InvoiceTrackerWebApp.ViewEmployee" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Employees</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f8f9fa;
            padding: 20px;
        }
        .table-container {
            background-color: #ffffff;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        .table-container h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #007bff;
        }
        .action-buttons a {
            margin-right: 10px;
        }
        .action-buttons a:last-child {
            margin-right: 0;
        }
        .btn-view {
            color: #fff;
            background-color: #17a2b8;
            border-color: #17a2b8;
        }
        .btn-edit {
            color: #fff;
            background-color: #ffc107;
            border-color: #ffc107;
        }
        .btn-delete {
            color: #fff;
            background-color: #dc3545;
            border-color: #dc3545;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="table-container">
                <h2>Employee List</h2>
                <asp:GridView ID="gvEmployees" runat="server" CssClass="table table-striped table-bordered"
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                    OnPageIndexChanging="gvEmployees_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="EmpID" HeaderText="Employee ID" />
                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                        <asp:BoundField DataField="Vendor" HeaderText="Vendor" />
                        <asp:BoundField DataField="RoleID" HeaderText="Role ID" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="action-buttons">
                                    <a href='<%# Eval("EmpID", "DetailsEmployee.aspx?id={0}") %>' class="btn btn-sm btn-view">Details</a>
                                    <a href='<%# Eval("EmpID", "EditEmployee.aspx?id={0}") %>' class="btn btn-sm btn-edit">Edit</a>
        
                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="Delete" CommandArgument='<%# Eval("EmpID") %>' CssClass="btn btn-danger btn-sm" />
     
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>--%>
<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEmployee.aspx.cs" Inherits="InvoiceTrackerWebApp.ViewEmployee" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Employees</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f8f9fa;
            padding: 20px;
        }
        .table-container {
            background-color: #ffffff;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        .table-container h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #007bff;
        }
        .action-buttons a {
            margin-right: 10px;
        }
        .btn-view {
            color: #fff;
            background-color: #17a2b8;
        }
        .btn-edit {
            color: #fff;
            background-color: #ffc107;
        }
        .btn-delete {
            color: #fff;
            background-color: #dc3545;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="table-container">
                <h2>Employee List</h2>

                <!-- Success/Error Message Display -->
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblSuccess" runat="server" ForeColor="Green"></asp:Label>

                <!-- GridView -->
                <asp:GridView ID="gvEmployees" runat="server" CssClass="table table-striped table-bordered"
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                    OnPageIndexChanging="gvEmployees_PageIndexChanging" OnRowCommand="gvEmployees_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="EmpID" HeaderText="Employee ID" />
                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                        <asp:BoundField DataField="Vendor" HeaderText="Vendor" />
                        <asp:BoundField DataField="RoleID" HeaderText="Role ID" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <div class="action-buttons">
                                    <a href='<%# Eval("EmpID", "DetailsEmployee.aspx?id={0}") %>' class="btn btn-sm btn-view" aria-label="View Details">Details</a>
                                    <a href='<%# Eval("EmpID", "EditEmployee.aspx?id={0}") %>' class="btn btn-sm btn-edit" aria-label="Edit Employee">Edit</a>
                                    <asp:Button ID="btnDelete" runat="server" CommandName="CustomDelete" Text="Delete"
                                        CommandArgument='<%# Eval("EmpID") %>' CssClass="btn btn-danger btn-sm" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Delete Confirmation Modal -->
        <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="deleteModalLabel">Confirm Deletion</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        Are you sure you want to delete this employee?
                        <asp:HiddenField ID="hfDeleteEmpID" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <asp:Button ID="btnConfirmDelete" runat="server" CssClass="btn btn-danger"
                            Text="Confirm Delete" OnClick="btnConfirmDelete_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- JavaScript -->
    <script>
        function openDeleteModal(empID) {
            document.getElementById('<%= hfDeleteEmpID.ClientID %>').value = empID;
            var modalElement = document.getElementById('deleteModal');
            var deleteModal = new bootstrap.Modal(modalElement, {
                keyboard: false
            });
            deleteModal.show();
        }
    </script>
</body>
</html>

--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEmployee.aspx.cs" Inherits="InvoiceTrackerWebApp.ViewEmployee" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Employees</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js"></script>

    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f8f9fa;
            padding: 20px;
        }
        .table-container {
            background-color: #ffffff;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }
        .table-container h2 {
            text-align: center;
            margin-bottom: 20px;
            color: #007bff;
        }
        .action-buttons a {
            margin-right: 10px;
        }
         .btn-view {
            background-color: #28a745;
            color: white;
        }

        .btn-view:hover {
            background-color: #218838;
            color: white;
        }

        .btn-edit {
            background-color: #ffc107;
            color: black;
        }

        .btn-edit:hover {
            background-color: #e0a800;
            color: black;
        }

        .btn-delete {
            color: #fff;
            background-color: #dc3545;
            border-color: #dc3545;
        }

        /* Breadcrumb Style */
        .breadcrumb {
            background-color: transparent;
            padding-left: 0;
            margin-bottom: 20px;
            font-size: 16px;
        }

        .breadcrumb a {
            color: #007bff;
            text-decoration: none;
        }

        .breadcrumb .active {
            color: #6c757d;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Breadcrumb -->
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="InvoiceList.aspx">Home</a></li>
                    <li class="breadcrumb-item active" aria-current="page">View Employees</li>
                </ol>
            </nav>

            <div class="table-container">
                <h2>Employee List</h2>
                <asp:GridView ID="gvEmployees" runat="server" CssClass="table table-striped table-bordered"
                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                    OnPageIndexChanging="gvEmployees_PageIndexChanging" OnRowCommand="gvEmployees_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="EmpID" HeaderText="Employee ID" />
                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                        <asp:BoundField DataField="Vendor" HeaderText="Vendor" />
                        <asp:BoundField DataField="RoleID" HeaderText="Role ID" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <a href='<%# Eval("EmpID", "DetailsEmployee.aspx?id={0}") %>' class="btn btn-sm btn-view" >Details</a>
                                <a href='<%# Eval("EmpID", "EditEmployee.aspx?id={0}") %>' class="btn btn-sm btn-edit">Edit</a>

                                <!-- Delete button triggers the modal -->
                                <asp:Button ID="btnDelete" runat="server" CommandName="CustomDelete" Text="Delete"
                                    CommandArgument='<%# Eval("EmpID") %>' CssClass="btn btn-danger btn-sm btn-delete" 
                                    OnClientClick='<%# "openDeleteModal(\"" + Eval("EmpID").ToString() + "\"); return false;" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblSuccess" runat="server" CssClass="text-success"></asp:Label>
                <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
            </div>
        </div>

        <!-- Delete Confirmation Modal -->
        <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="deleteModalLabel">Confirm Deletion</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        Are you sure you want to delete this employee?
                        <asp:HiddenField ID="hfDeleteEmpID" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <asp:Button ID="btnConfirmDelete" runat="server" Text="Confirm Delete" CssClass="btn btn-danger" OnClick="btnConfirmDelete_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        function openDeleteModal(empID) {
            // Set the value of the hidden field with the EmpID
            document.getElementById('<%= hfDeleteEmpID.ClientID %>').value = empID;
            // Show the modal
            var modal = new bootstrap.Modal(document.getElementById('deleteModal'));
            modal.show();
        }
    </script>
</body>
</html>

