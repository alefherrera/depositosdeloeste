<%@ Page Title="Agregar Articulo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productos_alta.aspx.cs" Inherits="Depositos_del_Oeste._Productos_Alta" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Agregar Articulo</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    <asp:Label runat="server" ID="lbCliente"></asp:Label><br />
    Nombre <asp:TextBox runat="server" ID="txtNombre"></asp:TextBox><br />
    Descripcion <asp:TextBox runat="server" ID="txtDescripcion"></asp:TextBox><br />
    Alto <asp:TextBox runat="server" ID="txtAlto"></asp:TextBox> milímetros <br />
    Largo <asp:TextBox runat="server" ID="txtLargo"></asp:TextBox> milímetros <br />
    Ancho <asp:TextBox runat="server" ID="txtAncho"></asp:TextBox> milímetros <br />
    Peso <asp:TextBox runat="server" ID="txtPeso"></asp:TextBox> gramos <br />
    Índice de Actividad 
    <asp:RadioButtonList runat="server" ID="rblActividad">
        <asp:ListItem Value="0">Bajo</asp:ListItem>
        <asp:ListItem Value="1">Medio</asp:ListItem>
        <asp:ListItem Value="2">Alto</asp:ListItem>
    </asp:RadioButtonList>
</asp:Content>
