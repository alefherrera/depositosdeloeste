<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ingreso.aspx.cs" Inherits="Depositos_del_Oeste._Ingreso" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Ingreso de Mercaderia</h3>
    <asp:Label ID="lbCliente" Text="Blisard" runat="server"></asp:Label>
    <br />
    <asp:Label runat="server" ID="lbFecha" Text="Fecha de Reserva 15/10/2012"></asp:Label>
    <br /><br />
    <asp:Label ID="lbArticulo" Text="Lavandina " runat="server"></asp:Label>
    <asp:Label ID="lbCantidad" Text=" 47" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lbIngresar" Text="Ingresar Cantidad" runat="server"></asp:Label>
    <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lbArticulo2" Text="Desodorantes " runat="server"></asp:Label>
    <asp:Label ID="lbCantidad2" Text=" 32" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lbIngresar2" Text="Ingresar Cantidad" runat="server"></asp:Label>
    <asp:TextBox ID="txtCantidad2" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lbFechaRemito" Text="Fecha de Remito" runat="server"></asp:Label>
    <asp:TextBox ID="txtFechaRemito" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lbDescripcion" Text="Detalles" runat="server"></asp:Label>
    <br />
    <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" runat="server" Height="100px" Width="300px"></asp:TextBox>
    <br />
    <asp:Button ID="btnSubmit" Text="Registrar Ingreso" runat="server"/>
</asp:Content>