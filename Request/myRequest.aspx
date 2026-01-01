<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="myRequest.aspx.cs" Inherits="hassan11244WebApp1.Request.myRequest" 
    EnableEventValidation="false" ValidateRequest="false"
    %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="section-title">My Maintenance Requests</h2>
    <p class="section-subtitle">Below is a list of all the maintenance requests you have submitted. You can view the status and details of each request.</p>
    <br />
    <p>
        <br />
    </p>
    <table class="nav-justified">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblOutput" runat="server"  Class="output-message"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 144px; height: 22px;"><strong>Submit Request Id</strong></td>
            <td style="height: 22px">
                <asp:TextBox ID="txtSubmitRequestId" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 144px; height: 22px;"><strong>Type Name</strong></td>
            <td style="height: 22px">
                <asp:TextBox ID="txtTypeName" runat="server" Height="33px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 144px"><strong>Priority<br />
                </strong>
                <br />
                <strong>Location<br />
                </strong>
                <br />
                <strong>Date</strong></td>
            <td>
                <asp:TextBox ID="txtPriority" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 144px"><strong>Descripation</strong></td>
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
            </td>
        </tr>
    </table>

    <div>
        <asp:GridView ID="gvShowInternData" runat="server" CssClass="GridView" AutoGenerateColumns="False" DataKeyNames="SubmitRequestID" OnRowCommand="gvShowInternData_RowCommand"
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
