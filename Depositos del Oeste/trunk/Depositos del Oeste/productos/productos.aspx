<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productos.aspx.cs" Inherits="Depositos_del_Oeste._Productos" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Gestion de Productos</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    Cliente
    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True"></asp:DropDownList><br />
    <asp:GridView ID="gridArticulos" runat="server">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
<asp:TemplateField>
    <ItemTemplate>
        <a href="producto_baja.aspx?id=<%# Eval("IdArticulo") %>">
            Eliminar
        </a>
    </ItemTemplate>
</asp:TemplateField>
        </Columns>
    </asp:GridView><br />
    <a href="productos_alta.aspx" runat="server">Agregar Articulo</a>
</asp:Content>
