<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="hassan11244WebApp1.finance._default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div>
        <ul>
            <li>
                <asp:LinkButton ID="lbtnEnglish" runat="server" OnClick="lbtnEnglish_Click" 
                    meta:resourceKey="lbtnEnglish">English</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="lbtnArabic" runat="server" OnClick="lbtnArabic_Click" 
                    meta:resourceKey="lbtnArabic">Arabic</asp:LinkButton>
            </li>
        </ul>

        <hr />

        <table>
            <tr>
                <td>
                    <asp:Label ID="lblOutput" runat="server" meta:resourceKey="lblOutputResource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFirstName" runat="server" meta:resourceKey="lblFirstNameResource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server" meta:resourceKey="txtFirstNameResource1"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        table {
            margin-top: 20px;
        }
        td {
            padding: 5px;
        }
    </style>
</asp:Content>

