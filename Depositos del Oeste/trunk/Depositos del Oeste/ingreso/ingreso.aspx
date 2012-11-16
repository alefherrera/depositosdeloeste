<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ingreso.aspx.cs" Inherits="Depositos_del_Oeste._Ingreso" %>

<%@ Import Namespace="Services" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    <h3>Ingreso de Mercadería</h3>
    <asp:Panel ID="pnlCodigo" runat="server">
        <asp:Label Text="Ingrese Codigo: " runat="server" ID="lbIngreseCodigo"></asp:Label>
        <asp:TextBox ID="txtCodigo" MaxLength="8" runat="server" CssClass="required"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnCodigo" runat="server" Text="Buscar Reserva" OnClick="btnCodigo_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlReserva" runat="server" Visible="false">
        <asp:Label ID="lbCliente" Text="" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lbCodigo" Text="" runat="server"></asp:Label>
        <br />
        <asp:Label runat="server" ID="lbFechaReserva"></asp:Label>
        <br />
        <asp:GridView ID="gridArticulos" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField Visible="false" HeaderText="IdArticulo" DataField="IdArticulo" />
                <asp:TemplateField HeaderText="Articulo">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# ServiceProductos.cargarArticulos(Eval("IdArticulo").ToString()).Nombre%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Cantidad Reservada" DataField="Cantidad" />
                <asp:TemplateField HeaderText="Cantidad Remito">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCantidad" MaxLength="8" runat="server" CssClass="number"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lbFechaRemito" Text="Fecha de Remito"  runat="server"></asp:Label>
        <asp:TextBox ID="txtFechaRemito" CssClass="fecha" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lbDescripcion" Text="Detalles" runat="server"></asp:Label>
        <br />
        <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" MaxLength="1000" runat="server" Height="100px" Width="300px"></asp:TextBox>
        <br />
        <asp:Button ID="btnSubmit" CssClass="btnSubmit" Text="Registrar Ingreso" runat="server" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnCancel" CssClass="btnCancel" Text="Cancelar Reserva" runat="server" OnClick="btnCancel_Click"/>
    </asp:Panel>
    <br />
    <asp:Label Visible="false" runat="server" ID="lbSuccess" Text="Ingreso correcto, para verificar el remito ingrese en Ingreso -> Remitos"></asp:Label>

    <script type="text/ecmascript">
        $(".btnSubmit").click(function () {
            return confirm("¿Esta seguro que los datos son correctos? Las cantidades no ingresadas se tomaran como 0.");
        });
        $(".btnCancel").click(function () {
            if (!confirm("¿Esta seguro que desea CANCELAR la reserva?."))
                return false;
            return confirm("Confirme la cancelación de la reserva");
        });

    </script>
</asp:Content>
