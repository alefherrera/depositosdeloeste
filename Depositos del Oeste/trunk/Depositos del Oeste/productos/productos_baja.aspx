<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productos_baja.aspx.cs" Inherits="Depositos_del_Oeste._Productos_Baja" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Eliminar Articulo</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    <asp:Label runat="server" ID="lbCliente"></asp:Label><br />
    Nombre
    <asp:Label runat="server" ID="txtNombre"></asp:Label><br />
    Descripcion
    <asp:Label runat="server" ID="txtDescripcion"></asp:Label><br />
    Alto
    <asp:Label runat="server" ID="txtAlto"></asp:Label>
    milímetros
    <br />
    Largo
    <asp:Label runat="server" ID="txtLargo"></asp:Label>
    milímetros
    <br />
    Ancho
    <asp:Label runat="server" ID="txtAncho"></asp:Label>
    milímetros
    <br />
    Peso
    <asp:Label runat="server" ID="txtPeso"></asp:Label>
    gramos
    <br />
    Índice de Actividad 
    <asp:RadioButtonList runat="server" ID="rblActividad">
        <asp:ListItem Value="0">Bajo</asp:ListItem>
        <asp:ListItem Value="1">Medio</asp:ListItem>
        <asp:ListItem Value="2">Alto</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Button runat="server" ID="btnEliminar" CssClass="btnEliminar" Text="Eliminar" OnClick="btnEliminar_Click" />
    <script type="text/ecmascript">
        $(".btnEliminar").click(function () {
            return confirm("Esta seguro de eliminar el producto");
        });
    </script>
</asp:Content>
