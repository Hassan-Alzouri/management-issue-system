<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="submitRequest.aspx.cs" Inherits="hassan11244WebApp1.Request.submitRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <br />
   <h2  class="section-title">Submit a Maintenance Request</h2>
    <p  class="section-subtitle">Please fill out the form below to report a maintenance issue.</p>
    <br />
   <div class="centered-form">
    <asp:Label ID="lblOutput" runat="server" Class="output-message"></asp:Label>
    <br />
    <asp:Label ID="lblRequestType" runat="server" Text="Select Request Type:"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlRequestType" runat="server" />
    <br />
    <asp:Label ID="lblPriority" runat="server" Text="Select Priority Level:"></asp:Label>
    &nbsp;<asp:RadioButtonList ID="rblPriority" runat="server" />
    <br />
    <asp:Label ID="lblEquipment" runat="server" Text="Select Affected Equipment:"></asp:Label>
    &nbsp;<asp:CheckBoxList ID="cblEquipment" runat="server" />
    <br />
    <asp:Label ID="lblLocation" runat="server" Text="Enter Location:"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlLocation" runat="server" />
    <br /><br />
    <asp:Label ID="lblDescription" runat="server" Text="Describe the Problem:"></asp:Label>
    <br />
    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" Columns="50" />
    <br /><br />
    <asp:Button ID="btnSubmitRequest" runat="server" Text="Submit Request" OnClick="btnSubmitRequest_Click" />
    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
</div>

    

    
</asp:Content>
