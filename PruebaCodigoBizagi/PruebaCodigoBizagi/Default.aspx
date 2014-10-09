<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PruebaCodigoBizagi._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <!-- Main jumbotron for a primary marketing message or call to action -->
    <section style="margin-top:35px;">
        <div class="jumbotron" style="background:url('/Images/fondo_blanco.jpg');">
            <div class="container">
                <div class="row">
                    <div class="col-md-3">
                        <img src="Images/logo.png" />
                    </div>
                    <div class="col-md-9" style="color: #009f9a; font-size: 30px; line-height: 30px; font-style: italic;margin-top:15px;font-weight: 400;font-family: 'Source Sans Pro';">
                        Bienvenido a nuestra herramienta de validación de diagramas de procesos. Revise y corrija sus diagramas automáticamente.
                    </div>
                </div>
            </div>
        </div>
        <div class="jumbotron" style="background:url('/Images/banner.png');height:250px;background-size:1420px;margin-top: -30px;">
            
        </div>
    </section>
    
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
            <div class="container">
                <div class="row">
                    <div class="col-md-4 col-md-offset-3" >
                         <form id="Form1" method="post" enctype="multipart/form-data">
                            <input type=file id=File name=File1 runat="server" style="width: 400px;border: none;" />
                            <br>
                            <asp:ImageButton ID="Button1" ImageUrl="Images/btn_cargar.png" runat="server" OnClick="submit" style="width: 400px; border: none;" />
                         </form>
                    </div>
                </div>
            </div>
   
    <h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h5>Getting Started</h5>
            ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245146">Learn more…</a>
        </li>
        <li class="two">
            <h5>Add NuGet packages and jump-start your coding</h5>
            NuGet makes it easy to install and update free libraries and tools.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245147">Learn more…</a>
        </li>
        <li class="three">
            <h5>Find Web Hosting</h5>
            You can easily find a web hosting company that offers the right mix of features and price for your applications.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245143">Learn more…</a>
        </li>
    </ol>
</asp:Content>

