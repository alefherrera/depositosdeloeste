<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="retiro.aspx.cs" Inherits="Depositos_del_Oeste._Retiro" %>
<%@ Import Namespace="BackEnd.Utils" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Retiro de Mercaderia</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    <asp:Panel runat="server" ID="pnlCliente"> 
    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged"></asp:DropDownList><br />
    <br />
    </asp:Panel>
    <asp:Panel ID="pnlPedido" runat="server" Visible="false">
    <asp:Label ID="lbCliente" Text="" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="gridIngresados" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Id" DataField="idcompartimiento" Visible="false"/>
            <asp:BoundField HeaderText="Estante" DataField="nroestanteria" />
            <asp:BoundField HeaderText="Nivel" DataField="nivel" />
            <asp:BoundField HeaderText="Compartimiento" DataField="compartimiento" />
             <asp:TemplateField HeaderText="Actividad">
                <ItemTemplate>
                    <asp:Label ID="lbActividad" runat="server" Text='<%# Enums.ActividadDesc(int.Parse(Eval("Actividad").ToString())) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Articulo" DataField="nombre" />
            <asp:BoundField HeaderText="Cantidad Ingresada" DataField="cantidad" />
            <asp:TemplateField HeaderText="Cantidad Pedido">
                <ItemTemplate>
                    <asp:TextBox ID="txtCantidad" runat="server" MaxLength="8"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lbFechaPedido" Text="Fecha de Pedido"  runat="server"></asp:Label>
    <asp:TextBox ID="txtFechaPedido" CssClass="fecha" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btnSubmit" CssClass="btnSubmit" Text="Registrar Retiro" runat="server" OnClick="btnSubmit_Click"/>
    <asp:Button ID="btnCancel" Text="Cancelar Retiro" CssClass="btn" runat="server" OnClick="btnCancel_Click" style="height: 26px"/>
    </asp:Panel>
    <asp:Label Visible="false" runat="server" ID="lbSuccess" Text="Retiro correcto, para verificar el pedido ingrese en Retiro -> Pedidos"></asp:Label>

    <script type="text/ecmascript">
        $(".btnSubmit").click(function () {
            return confirm("¿Esta seguro que los datos son correctos? Las cantidades no ingresadas se tomaran como 0.");
        });
        $(".fecha").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true }).mask("99/99/9999", { placeholder: " " });
    </script>
</asp:Content>
