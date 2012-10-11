<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productos.aspx.cs" Inherits="Depositos_del_Oeste._productos" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Gestion de Productos</h3>
    Cliente
    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True"></asp:DropDownList><br />
    <asp:GridView ID="gridArticulos" runat="server"></asp:GridView>
</asp:Content>
