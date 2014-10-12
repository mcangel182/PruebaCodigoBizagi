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
        <div class="jumbotron" style="background:url('/Images/banner.jpg');background-size:1420px;margin-top: -30px;">
            <div class="container">
                <div class="row">
                    <div class="col-md-4">
                        <img src="Images/paso1.png" style="height:140px" />
                    </div>
                    <div class="col-md-4">
                        <img src="Images/paso2.png" style="height:140px"/>
                    </div>
                    <div class="col-md-4">
                        <img src="Images/paso3.png" style="height:140px"/>
                    </div>
                </div>
            </div>
        </div>
    </section>
    
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container">
        <div class="row">
            <div class="col-md-5 col-md-offset-3" >
                <h3>Comince a validar sus diagramas ahora!</h3>
                <br>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-md-offset-3" >
                <form id="Form1" method="post" enctype="multipart/form-data" >
                    <input type=file id=File name=File1 runat="server" style="width: 400px;border: none;" />
                    <br>
                    <asp:ImageButton ID="Button1" ImageUrl="Images/btn_cargar.png" runat="server" OnClick="submit" style="width: 400px; border: none;" />
                </form>
                <br><br><br><br>
            </div>
        </div>
    </div>
</asp:Content>

