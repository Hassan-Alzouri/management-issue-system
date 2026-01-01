<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="hassan11244WebApp1.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <h2>About Us</h2>

<p><strong>Project Title:</strong> Management Issue System</p>

<p>
The Management Issue System is a web-based application developed to streamline the process of submitting, managing, and resolving maintenance and operational requests within an organization. It features role-based access for admins, employees, and requesters, allowing each to interact with the system according to their responsibilities. Key functions include submitting maintenance requests, assigning tasks, updating request statuses, and generating reports for better decision-making.
</p>

<p>
This project was developed as part of my Cooperative Training (Co-op) at <strong>Imam Abdulrahman Bin Faisal University (IAU)</strong>, within the College of Computer Science and Information Technology. The Co-op program offers senior students practical experience by engaging in real-world development projects and applying what they have learned in a working environment.
</p>

<p>
Through this project, I had the opportunity to enhance my skills in web development, particularly in technologies such as ASP.NET Web Forms, SQL Server, HTML, CSS, and C#. I worked on both the frontend and backend of the system, implementing CRUD functionality, authentication, role-based access, and user interface styling.
</p>

<p><strong>Student Information:</strong></p>
<ul>
  <li><strong>Name:</strong> Hassan Mohammed Hassan Al Zourei</li>
  <li><strong>University ID:</strong> 2220004853</li>
  <li><strong>Major:</strong> Computer Science</li>
  <li><strong>Institution:</strong> Imam Abdulrahman Bin Faisal University (IAU)</li>
    <li><strong>Co-op Placement Location:</strong> King Fahad Medical City</li>
  <li><strong>Co-op Period:</strong> Start 2025/06/22 - End 2025/08/22</li>
</ul>

<p>
This project not only improved my technical capabilities but also gave me valuable experience in time management, problem-solving, and communicating effectively with supervisors.
</p>

    <hr />
<h3>Contact Me</h3>

<p>
If you have any questions, suggestions, or need any assistance regarding this project, feel free to contact me directly at:
<br />
📧 <a href="mailto:your-email@example.com">Hassan-Alzourei@outlook.com</a>
</p>

<p>Or, you can send your message using the form below:</p>
<div style="margin-top: 40px;">
    <div>
        <asp:Label ID="lblMsg" runat="server" Text="" Class="output-message"></asp:Label>
    </div>

    <table border="0" class="form-table">
        <tr>
            <td>From - Email</td>
            <td>
                <asp:TextBox ID="txtSenderEmail" runat="server" Height="16px" Width="450px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Subject</td>
            <td>
                <asp:TextBox ID="txtSubject" runat="server" Width="450px" OnLoad="lblOutputClear_txtSubject"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>File Attachments:</td>
            <td>
                <asp:FileUpload ID="fuAttachment" runat="server" Enabled="true" AllowMultiple="true" Width="449px" />
            </td>
        </tr>

        <tr>
            <td valign="top">Message</td>
            <td>
                <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Height="103px" Width="450px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnSendMailViaMailMgr" runat="server" Text="Send" OnClick="btnSendMailViaMailMgr_Click" />
                <asp:Button ID="btnSendViaCode" runat="server" Text="Send email via Code" OnClick="btnSendViaCode_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
            </td>
        </tr>
    </table>
</div>

    
</asp:Content>
