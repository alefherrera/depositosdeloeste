<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productos.aspx.cs" Inherits="Depositos_del_Oeste._Productos" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Gestion de Productos</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    <asp:DropDownList ID="ddlClientes" runat="server" CssClass="ddlcliente" Width="200px" AutoPostBack="true"></asp:DropDownList><br />
    <asp:GridView ID="gridArticulos" runat="server">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="productos_baja.aspx?id=<%# Eval("IdArticulo") %>">Ver/Eliminar
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
        <a href="javascript:void(0);" id="location">Agregar Articulo</a>
    <script type="text/ecmascript">
        $(function () {
            $(".ddlcliente").change(function () {
                VerificarURL($(this).val());
            });

            VerificarURL($(".ddlcliente").val());

        });

        function VerificarURL(valor) {
            if (valor != -1) {
                $("#location").attr("href", "productos_alta.aspx?id=" + valor);
            }
            else {
                $("#location").attr("href", "javascript:void(0);");
            }
        }


    </script>
</asp:Content>
