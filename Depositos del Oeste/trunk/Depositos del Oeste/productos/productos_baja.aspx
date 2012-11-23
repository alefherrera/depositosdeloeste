<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productos_baja.aspx.cs" Inherits="Depositos_del_Oeste._Productos_Baja" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Eliminar Articulo</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    <b><asp:Label runat="server" ID="lbCliente"></asp:Label></b><br /><br />
    <asp:Label runat="server" ID="lbNombre"></asp:Label><br />
    Descripcion
    <br />
    <asp:TextBox TextMode="MultiLine" Enabled="false" Width="300px" runat="server" ID="txtDescripcion"></asp:TextBox><br />
    Alto
    <asp:Label runat="server" ID="lbAlto"></asp:Label>
    milímetros
    <br />
    Largo
    <asp:Label runat="server" ID="lbLargo"></asp:Label>
    milímetros
    <br />
    Ancho
    <asp:Label runat="server" ID="lbAncho"></asp:Label>
    milímetros
    <br />
    Peso
    <asp:Label runat="server" ID="lbPeso"></asp:Label>
    gramos
    <br />
    Índice de Actividad 
    <asp:Label runat="server" ID="lbActividad"></asp:Label>
    <br />
    <asp:Button runat="server" ID="btnEliminar" CssClass="btnEliminar" Text="Eliminar" OnClick="btnEliminar_Click" />
    <asp:Button id="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click"/>
    <script type="text/ecmascript">
        $(".btnEliminar").click(function () {
            return confirm("¿Esta seguro de eliminar el producto?");
        });
    </script>
</asp:Content>
