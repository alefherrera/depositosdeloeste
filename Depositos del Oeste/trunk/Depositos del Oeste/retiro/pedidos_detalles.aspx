<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="pedidos_detalles.aspx.cs" Inherits="Depositos_del_Oeste._PedidosDetalles" %>

<%@ Import Namespace="Services" %>
<%@ Import Namespace="BackEnd" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="imprimir">
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
        <asp:GridView ID="gridPedidoDetalles" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField Visible="false" HeaderText="IdArticulo" DataField="articulos_idarticulo" />
                <asp:BoundField HeaderText="Articulo" DataField="nombre" />                
                <asp:BoundField HeaderText="Cantidad Ingresada" DataField="Cantidad" />
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <br />
    <div id="Imprimir">
        <asp:Label ID="lbImprimir" runat="server" Text="Generar Orden de Traslado"></asp:Label>
        <br />
        <a runat="server" id="lnkimprimir" href="pedidos_ver.aspx?id=" class="lnkver">
            <asp:Image runat="server" ID="imgImprimir" ImageUrl="~/Content/Images/imprimir.gif" /></a>
    </div>
    <br />
    <asp:Button Text="Volver" ID="btnVolver" runat="server" OnClick="btnVolver_Click"/>
</asp:Content>

