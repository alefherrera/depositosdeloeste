<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="remitos_ver.aspx.cs" Inherits="Depositos_del_Oeste._RemitosVer" %>

<html>
<head runat="server">
    <title>
        <asp:Literal runat="server" ID="ltTitle"></asp:Literal></title>
</head>
<body>
     <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label>
    <h4>Remito: <asp:Label runat="server" ID="lbRemito"></asp:Label></h4>
    <div style="font-weight:bold">Cliente: <asp:Label ID="lbCliente" runat="server"></asp:Label></div>
    Fecha de Remito: <asp:Label ID="lbFechaRemito" runat="server"></asp:Label><br /><br />
   <%-- <asp:GridView ID="gridRemitoDetalles" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Nivel" DataField="nivel" />
            <asp:BoundField HeaderText="Compartimiento" DataField="compartimiento" />
             <asp:TemplateField HeaderText="Actividad">
                <ItemTemplate>
                    <asp:Label ID="lbActividad" runat="server" Text='<%# Enums.ActividadDesc(int.Parse(Eval("Actividad").ToString())) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <asp:Label ID="lbEstado" runat="server" Text='<%# Enums.EstadoDesc(int.Parse(Eval("Estado").ToString())) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Articulo" DataField="nombre" />
            <asp:BoundField HeaderText="Cantidad Ingresada" DataField="cantidad" />
            <asp:BoundField HeaderText="Cantidad Reservada" DataField="reservados" />
        </Columns>
    </asp:GridView>--%>
</body>
</html>
