<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="remitos_ver.aspx.cs" Inherits="Depositos_del_Oeste._RemitosVer" %>

<%@ Import Namespace="Services" %>
<%@ Import Namespace="BackEnd" %>
<html>
<head runat="server">
    <title>
        <asp:Literal runat="server" ID="ltTitle"></asp:Literal></title>
</head>
<body>
    <script type="text/javascript" src="/Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.print.js"></script>
    <form runat="server" style="position: absolute; left: 10%;">
        <div class="imprimir">
        <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label>
        <h4>Remito:
        <asp:Label runat="server" ID="lbRemito"></asp:Label></h4>
        <div style="font-weight: bold">
            Cliente:
        <asp:Label ID="lbCliente" runat="server"></asp:Label>
        </div>
        Fecha de Remito:
    <asp:Label ID="lbFechaRemito" runat="server"></asp:Label><br />
        <br />
        <asp:GridView ID="gridRemitoDetalles" Width="600px" runat="server" AutoGenerateColumns="false">
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
        Descripcion:
    <br />
        <asp:Label ID="lbDescripcion" runat="server" Width="600px"></asp:Label>
        </div>
        <br />
        <br />        
        <div id="Imprimir">
            <a href="javascript:$('.imprimir').print();">
                <asp:Image runat="server" ID="imgImprimir" ImageUrl="~/Content/Images/imprimir.gif" />
            </a>
        </div>
    </form>
</body>
</html>
