<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="pedidos_ver.aspx.cs" Inherits="Depositos_del_Oeste._PedidosVer" %>

<%@ Import Namespace="Services" %>
<%@ Import Namespace="BackEnd" %>
<html>
<head runat="server">
    <title>
        <asp:Literal runat="server" ID="ltTitle"></asp:Literal></title>
</head>
<body>
    <form runat="server" style="position: absolute; left: 10%;">
        <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label>
        <h4>Pedido:
        <asp:Label runat="server" ID="lbPedido"></asp:Label></h4>
        <div style="font-weight: bold">
            Cliente:
        <asp:Label ID="lbCliente" runat="server"></asp:Label>
        </div>
        Fecha de Pedido:
    <asp:Label ID="lbFechaPedido" runat="server"></asp:Label><br />
        <br />
        <asp:GridView ID="gridPedidoDetalles" Width="600px" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="Estanteria" DataField="Compartimiento.NroEstanteria" />
                <asp:BoundField HeaderText="Nivel" DataField="Compartimiento.Nivel" />
                <asp:BoundField HeaderText="Compartimiento" DataField="Compartimiento.NroCompartimiento" />
                <asp:BoundField Visible="false" HeaderText="IdArticulo" DataField="IdArticulo" />
                <asp:TemplateField HeaderText="Articulo">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# ServiceProductos.cargarArticulos(Eval("IdArticulo").ToString()).Nombre%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Cantidad Ingresada" DataField="Cantidad" />
            </Columns>
        </asp:GridView>
        <br />
        <br />
        <div id="Imprimir">
            <a href="javascript:window.print();">
                <asp:Image runat="server" ID="imgImprimir" ImageUrl="~/Content/Images/imprimir.gif" />
            </a>
        </div>
    </form>
</body>
</html>
