<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="mangeRequest.aspx.cs" Inherits="hassan11244WebApp1.admin.mangeRequest" 
    EnableEventValidation="false" ValidateRequest="false"

    %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <h2 class="section-title">Request Overview</h2>
     <p class="section-subtitle">View summary, assign tasks, and manage requests below.</p>

    <div class="circle-container">
        <div class="circle-box" style="background-color: #e9ecef;">
            <h3>Total</h3>
            <span><%= totalRequests %></span>
        </div>

        <div class="circle-box" style="background-color: #ffc107;">
            <h3>Open</h3>
            <span><%= openRequests %></span>
        </div>
        <div class="circle-box" style="background-color: #17a2b8; color:white;">
            <h3>In Progress</h3>
            <span><%= inProgressRequests %></span>
        </div>
        <div class="circle-box" style="background-color: #dc3545; color:white;">
            <h3>Closed</h3>
            <span><%= closedRequests %></span>
        </div>
    </div>


    <p>
        <br />
    </p>
    <table class="nav-justified">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblOutput" runat="server"  Class="output-message" Visible="false"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblOutputAssign" runat="server"  Class="output-message"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 144px; height: 22px;">Submit Request Id</td>
            <td style="height: 22px">
                <asp:TextBox ID="txtSubmitRequestId" runat="server" ></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 144px; height: 22px;">Type Name</td>
            <td style="height: 22px">
                <asp:TextBox ID="txtTypeName" runat="server" Height="33px"></asp:TextBox>
         
            </td>
        </tr>
       <tr>
            <td style="width: 144px">Priority</td>
            <td><asp:TextBox ID="txtPriority" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Location</td>
            <td><asp:TextBox ID="txtLocation" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Assign To</td>
            <td>
                <asp:DropDownList ID="ddlAssigntTo" runat="server" CssClass="dropdown"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Date</td>
            <td><asp:TextBox ID="txtDate" runat="server"></asp:TextBox></td>
        </tr>

        <tr>
            <td style="width: 144px">Descripation</td>
            <td>
                <asp:TextBox ID="txtDescripation" runat="server" Height="51px" TextMode="MultiLine" Width="162px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 144px">&nbsp;</td>
            <td>
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                <asp:Button ID="btnGetData" runat="server" OnClick="btnGetData_Click" Text="Get Data" />
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"   
                    OnClientClick="return confirm('Are you Sure  you want to Delete ?')" />
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" />
                <asp:Button ID="btnExportToExcel" runat="server" OnClick="btnExportToExcel_Click" Text="Export To Excel" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSubmitAssign" runat="server" Text="Submit Assignt" OnClick="btnSubmitAssign_Click" />
            </td>
        </tr>
    </table>

    <div>
        <asp:GridView ID="gvShowInternData" runat="server"  CssClass="GridView" AutoGenerateColumns="False" DataKeyNames="SubmitRequestID" OnRowCommand="gvShowInternData_RowCommand" 
            >
            <Columns>
                <asp:TemplateField HeaderText="SubmitRequestID">
            <ItemTemplate>
                <asp:LinkButton ID="lnkSubmitID" runat="server" 
                    Text='<%# Eval("SubmitRequestID") %>' 
                    CommandName="SelectRequest" 
                    CommandArgument='<%# Eval("SubmitRequestID") %>' />
            </ItemTemplate>
        </asp:TemplateField>
                <asp:BoundField DataField="TypeName" HeaderText="TypeName" SortExpression="TypeName" />
                <asp:BoundField DataField="PriorityName" HeaderText="PriorityName" SortExpression="PriorityName" />
                <asp:BoundField DataField="FullLocation" HeaderText="FullLocation" ReadOnly="True" SortExpression="FullLocation" />
                <asp:BoundField DataField="DateTime" HeaderText="DateTime" SortExpression="DateTime" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
