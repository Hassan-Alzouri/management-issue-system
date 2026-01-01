<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="myAssignedRequests.aspx.cs" Inherits="hassan11244WebApp1.employee.myAssignedRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="section-title">Employee Task Requests</h2>
<p class="section-subtitle">Manage and review all submitted requests below.</p>
    <div>
        <asp:Label ID="lblOutputActive" runat="server"  Class="output-success"></asp:Label>
        <asp:Repeater ID="rptRequests" runat="server" OnItemDataBound="rptRequests_ItemDataBound">
    <HeaderTemplate>
        <table class="table table-bordered">
            <tr>
                <th>Task ID</th>
                <th>Request ID</th>
                <th>Description</th>
                <th>Type</th>
                
                <th>Submitted On</th>
                <th>Action</th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><%# Eval("TaskID") %></td>
            <td><%# Eval("SubmitRequestID") %></td>
            <td><%# Eval("Description") %></td>
            <td><%# Eval("RequestType") %></td>
            
            <td><%# Eval("DateTime", "{0:yyyy-MM-dd}") %></td>
            <td>
                  <asp:HiddenField ID="hfTaskID" runat="server" Value='<%# Eval("TaskID") %>' />
                <asp:CheckBox ID="cbActivate" runat="server" AutoPostBack="true"
                                        OnCheckedChanged="cbActivate_CheckedChanged"
                                        Checked='<%# Convert.ToBoolean(Eval("IsActive")) %>'
                                        Text="Activate"
                                        onclick="return confirm('Are you sure you want to change the status?');" />



            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>

        <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click" />


    </div>

     <div >  
         <h2 class="chat-title">Employee Chat Room</h2>
            <p class="chat-subtitle">You can chat and exchange messages with your colleagues here.</p>

        <asp:Repeater ID="RepterDetails" runat="server">
            <HeaderTemplate>
                <table  class="chat-table">
                    <tr >
                        <td colspan="2">
                            <b>Comments</b>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr >
                    <td>
                        <table class="chat-table">
                            <tr>
                                <td>Subject:  
    <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("Subject") %>' Font-Bold="true" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblComment" runat="server" Text='<%#Eval("CommentOn") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="chat-table" >
                            <tr>
                                <td>Post By:
                                    <asp:Label ID="lblUser" runat="server" Font-Bold="true" Text='<%#Eval("UserName") %>' /></td>
                                <td>Created Date:<asp:Label ID="lblDate" runat="server" Font-Bold="true" Text='<%# Eval("Post_Date", "{0:dd/MM/yyyy hh:mm tt}") %>' /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>  
            </FooterTemplate>
        </asp:Repeater>  
    </div>

    
    <h2 class="chat-form-title">Start a Conversation</h2>
<p class="chat-form-subtitle">Employees can chat by entering their message details below.</p>

   <div class="chat-form-container">  

    <table class="chat-form-table">  
        <tr>  
            <td>Enter Name:</td>  
            <td><asp:TextBox ID="txtName" runat="server" CssClass="input-box"></asp:TextBox></td>  
        </tr>  
        <tr>  
            <td>Enter Subject:</td>  
            <td><asp:TextBox ID="txtSubject" runat="server" CssClass="input-box"></asp:TextBox></td>  
        </tr>  
        <tr>  
            <td valign="top">Enter Comments:</td>  
            <td><asp:TextBox ID="txtComment" runat="server" Rows="5" Columns="20" TextMode="MultiLine" CssClass="input-box textarea-box"></asp:TextBox></td>  
        </tr>  
        <tr>  
            <td></td>  
            <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="submit-btn" OnClick="btnSubmit_click" />  <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" /></td>
           
        </tr>  
    </table>  
</div>
 
   
    



</asp:Content>


