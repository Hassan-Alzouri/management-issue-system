<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" 
    Inherits="hassan11244WebApp1.Account.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="login-title">Login</h2>
<p class="login-subtitle">Please enter your credentials to access the system.</p>

<div class="login-container">
    <table class="login-table">
        <tr>
            <td class="label-cell">User Name</td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="input-box"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label-cell">Password</td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input-box"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="login-btn" OnClick="btnLogin_Click" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label ID="lblOutput" runat="server" CssClass="output-success"></asp:Label>
            </td>
        </tr>
    </table>
</div>

</asp:Content>
