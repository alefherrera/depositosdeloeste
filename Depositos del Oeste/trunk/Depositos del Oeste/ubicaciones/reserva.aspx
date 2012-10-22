<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reserva.aspx.cs" Inherits="Depositos_del_Oeste._Reserva" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Reserva</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    Cliente
    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" CssClass="cliente"></asp:DropDownList><br />
    Articulos<br />
    <asp:DropDownList ID="ddlArticulos" runat="server" AutoPostBack="True" Width="108px">
        <asp:ListItem Selected="True">Lavandina</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="txtCantidad" runat="server" Width="48px"></asp:TextBox>
    <asp:LinkButton ID="linkAdd" runat="server">Agregar Articulo</asp:LinkButton>
    <br /><br />
    <asp:Button ID="btnSubmit" runat="server" Text="Registrar Reserva"/>
</asp:Content>
