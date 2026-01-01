<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="hassan11244WebApp1.Profile.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nav-justified" style="width: 185%">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblTitle" runat="server" Text="Enter you information"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblOutput" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-left" style="width: 94px"><strong>Age</strong></td>
            <td style="width: 292px">
                <asp:TextBox ID="txtAge" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-left" style="width: 94px"><strong>Weight (Kg)</strong></td>
            <td style="width: 292px">
                <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-left" style="width: 94px"><strong>Height(cm)</strong></td>
            <td style="width: 292px">
                <asp:TextBox ID="txtHeight" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-left" style="width: 94px"><strong>Gender</strong></td>
            <td style="width: 292px">
                <asp:RadioButtonList ID="rbGender" runat="server">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="text-left" style="width: 94px"><strong>Active</strong></td>
            <td style="width: 292px">
                <asp:DropDownList ID="ddlActivity" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="text-left" style="width: 94px"><strong>Goal</strong></td>
            <td style="width: 292px">
                <asp:DropDownList ID="ddlGoal" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="text-left" style="width: 94px"><strong>Dietary Resrictions</strong></td>
            <td style="width: 292px">
                <asp:CheckBoxList ID="cbDietaryRestriction" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" />
            </td>
        </tr>
    </table>
</asp:Content>
