<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="retiro.aspx.cs" Inherits="Depositos_del_Oeste._Retiro" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Retiro de Mercaderia</h3>
    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True"></asp:DropDownList><br />
    <br />
    <asp:Label ID="lbArticulo" Text="Lavandina" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lbEstanteria" Text="Estanteria: " runat="server"></asp:Label>
    <asp:Label ID="lbNroEstanteria" Text="1" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lbNivel" Text="Nivel: " runat="server"></asp:Label>
    <asp:Label ID="lbNroNivel" Text="1" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lbCompartimiento" Text="Compartimiento: " runat="server"></asp:Label>
    <asp:Label ID="lbNroCompartimiento" Text="1" runat="server"></asp:Label>
    <br />
    <asp:Label runat="server" ID="lbCantidad" Text="40"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lbEstanteria1" Text="Estanteria: " runat="server"></asp:Label>
    <asp:Label ID="lbNroEstanteria1" Text="1" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lbNivel1" Text="Nivel: " runat="server"></asp:Label>
    <asp:Label ID="lbNroNivel1" Text="1" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lbCompartimiento1" Text="Compartimiento: " runat="server"></asp:Label>
    <asp:Label ID="lbNroCompartimiento1" Text="2" runat="server"></asp:Label>
    <br />
    <asp:Label runat="server" ID="lbCantidad1" Text="7"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lbIngresar" Text="Ingresar Cantidad" runat="server"></asp:Label>
    <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lbFechaRemito" Text="Fecha de Pedido" runat="server"></asp:Label>
    <asp:TextBox ID="txtFechaRemito" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btnSubmit" Text="Registrar Retiro" runat="server"/>
</asp:Content>