<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Validacion.aspx.cs" Inherits="PruebaCodigoBizagi.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div style="margin-top:35px;">
        <hgroup class="title">
            <h2>Validación de diagrama de procesos.</h2>
        </hgroup>
        <asp:Literal runat="server" id="imagenDiagrama" EnableViewState="false" />
        <h3><asp:Literal runat="server" id="titulo" EnableViewState="false" /></h3>
        <ol class="round">
            <asp:Literal runat="server" id="listaValidaciones" EnableViewState="false" />
        </ol>
        <br>
        <a href="Default.aspx">
            <img src="Images/btn_volver.png" style="width: 180px; border: none;" />
        </a>
        <br><br>
    </div>
</asp:Content>