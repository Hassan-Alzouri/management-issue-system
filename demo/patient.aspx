<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="patient.aspx.cs"
    Inherits="hassan11244WebApp1.demo.patient" 
    EnableEventValidation="false" ValidateRequest="false"
%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <br />
    <br />
    <div>
        <table class="nav-justified">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblOutput" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-right" style="width: 123px; font-weight: bold">Patient ID</td>
            <td>
                <asp:TextBox ID="textPatientId" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right" style="width: 123px; font-weight: bold">First Name</td>
            <td>
                <asp:TextBox ID="textFName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right" style="width: 123px; font-weight: bold">Last Name</td>
            <td>
                <asp:TextBox ID="textLName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right" style="width: 123px; font-weight: bold">Date Of Birth</td>
            <td>
                <asp:TextBox ID="textDob" runat="server" TextMode="Date"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right" style="width: 123px; font-weight: bold">Active</td>
            <td>
                <asp:CheckBox ID="cbActive" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="text-right" style="width: 123px; font-weight: bold">Country</td>
            <td>
                <asp:DropDownList ID="ddlCountry" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 123px">&nbsp;</td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete"
                    OnClientClick="return confirm('Are you Sure  you want to Delete ?')"/>
                <asp:Button ID="btnGetPatientInfo" runat="server" Text="Get Patient Info" OnClick="btnGetPatientInfo_Click" />
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" />
            </td>
        </tr>
        <tr>
            <td style="width: 123px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </div>

    <div>
        <asp:GridView ID="gvPatientInfo" runat="server" AutoGenerateColumns="False" DataKeyNames="patientId" >
            <Columns>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="patientId">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkupdate" runat="server"  
                                        CommandArgument='<%# Bind("patientId") %>' OnClick="populateForm_Click"
                                        Text='<%# Eval("patientId")  %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                            </asp:TemplateField>
                <asp:BoundField DataField="fName" HeaderText="fName" SortExpression="fName" />
                <asp:BoundField DataField="lName" HeaderText="lName" SortExpression="lName" />
                <asp:BoundField DataField="dob" HeaderText="dob" SortExpression="dob" />
                <asp:BoundField DataField="active" HeaderText="active" ReadOnly="True" SortExpression="active" />
                <asp:BoundField DataField="registration" HeaderText="registration" SortExpression="registration" />
                <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
