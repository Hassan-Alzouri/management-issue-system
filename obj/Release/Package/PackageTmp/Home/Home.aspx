<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="hassan11244WebApp1.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Co-op Experience at King Fahad Medical City</h1>
            <p>
            During my co-op at King Fahad Medical City, I gained practical experience in software development using C#, SQL Server, and ASP.NET. 
            I learned how to build database-driven applications, manage deployment, and apply web development best practices.
            </p>    
        <p><a href="https://www.kfmc.med.sa/" class="btn btn-primary btn-lg">Visit KFMC Official Site &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2> Database Fundamentals and SQL</h2>
           <p>
I started by learning essential database concepts and hands-on SQL skills using SQL Server Management Studio. This included creating tables, defining relationships, and practicing CRUD operations. 
I also studied naming conventions in C# and worked with temporary tables and data manipulation language (DML).
</p>
           
        </div>
        <div class="col-md-4">
            <h2>Application Development with Visual Studio</h2>
           <p>
I progressed to application development with Visual Studio, connecting databases to user interfaces through C#. 
I built simple UI components such as dropdown lists and buttons that interact with the database. Additionally, I practiced writing stored procedures, triggers, and worked with Visual Studio controls.
</p>
            
        </div>
        <div class="col-md-4">
            <h2>Deployment and Web Application Features</h2>
           <p>
I learned how to deploy projects to hosting services like SmarterASP.NET and explored ASP.NET Web Forms lifecycle events. 
I developed features such as email notifications via SMTP, multilingual website support, and integrated security with membership systems. 
Finally, I started working on a Maintenance Issue Tracking System project, covering both backend and frontend development.
</p>
            
        </div>
    </div>

</asp:Content>
