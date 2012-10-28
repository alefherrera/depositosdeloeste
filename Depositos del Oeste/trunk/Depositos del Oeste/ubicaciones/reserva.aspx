<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reserva.aspx.cs" Inherits="Depositos_del_Oeste._Reserva" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Reserva</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    Cliente
    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" CssClass="cliente" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged"></asp:DropDownList><br />
    <asp:Panel ID="pnlArticulosGeneral" runat="server" Visible="false">
        Articulos<br />
        <asp:HiddenField ID="hdCantidadArticulos" runat="server" Value="1" />
        <asp:Panel ID="pnlArticulos" runat="server">
        </asp:Panel>
        <br />
        <asp:LinkButton ID="linkRemove" runat="server" OnClick="linkRemove_Click">Remover Articulo</asp:LinkButton>
        <br />
        <asp:LinkButton ID="linkAdd" runat="server" OnClick="linkAdd_Click">Agregar Articulo</asp:LinkButton>
        <br /><br />
        <asp:Button ID="btnSubmit" runat="server" Text="Registrar Reserva"/>
    </asp:Panel>
</asp:Content>
