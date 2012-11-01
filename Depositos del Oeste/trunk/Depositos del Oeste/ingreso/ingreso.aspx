<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ingreso.aspx.cs" Inherits="Depositos_del_Oeste._Ingreso" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    <h3>Ingreso de Mercadería</h3>
    <asp:Panel ID="pnlCodigo" runat="server">
        <asp:Label Text="Ingrese Codigo: " runat="server" ID="lbIngreseCodigo"></asp:Label>
        <asp:TextBox ID="txtCodigo" MaxLength="8" runat="server"></asp:TextBox>
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
        <asp:Panel ID="pnlArticulios" runat="server">

        </asp:Panel>
        <asp:Label ID="lbFechaRemito" Text="Fecha de Remito" CssClass="fecha" runat="server"></asp:Label>
        <asp:TextBox ID="txtFechaRemito" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lbDescripcion" Text="Detalles" runat="server"></asp:Label>
        <br />
        <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" runat="server" Height="100px" Width="300px"></asp:TextBox>
        <br />
        <asp:Button ID="btnSubmit" Text="Registrar Ingreso" runat="server" />
    </asp:Panel>
    <script type="text/ecmascript">
        $(".btnSubmit").click(function () {
            return confirm("¿Esta seguro que los datos son correctos?");
        });
        $(".fecha").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true }).mask("99/99/9999", { placeholder: " " });
    </script>
</asp:Content>
