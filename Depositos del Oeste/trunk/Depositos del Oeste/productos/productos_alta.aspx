<%@ Page Title="Agregar Articulo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productos_alta.aspx.cs" Inherits="Depositos_del_Oeste._Productos_Alta" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Agregar Articulo</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    <asp:Label runat="server" ID="lbCliente"></asp:Label><br />
    <table>
        <tr>
            <td>Nombre
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtNombre" MaxLength="45" CssClass="required"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Descripcion
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox runat="server" ID="txtDescripcion" TextMode="MultiLine" Width="300px" MaxLength="45" CssClass="required txtarea"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Alto
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtAlto" MaxLength="8" CssClass="required number"></asp:TextBox>
            </td>
            <td>milímetros
            </td>
        </tr>
        <tr>
            <td>Largo
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtLargo" MaxLength="8" CssClass="required number"></asp:TextBox>
            </td>
            <td>milímetros
            </td>
        </tr>
        <tr>
            <td>Ancho
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtAncho" MaxLength="8" CssClass="required number"></asp:TextBox>
            </td>
            <td>milímetros
            </td>
        </tr>
        <tr>
            <td>Peso
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPeso" MaxLength="8" CssClass="required number"></asp:TextBox>
            </td>
            <td>gramos
            </td>
        </tr>
    </table>
    Índice de Actividad 
    <asp:RadioButtonList runat="server" ID="rblActividad">
        <asp:ListItem Value="0" Selected="True">Bajo</asp:ListItem>
        <asp:ListItem Value="1">Medio</asp:ListItem>
        <asp:ListItem Value="2">Alto</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <asp:Button ID="btnSubmit" runat="server" Text="Confirmar" CssClass="btnConfirmar" OnClick="btnSubmit_Click" />
    <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" CssClass="cancel" />
    <script type="text/ecmascript">
        $(".btnConfirmar").click(function () {
            return confirm("¿Esta seguro que los datos son correctos?");
        });
        $(function () {
            $(".txtarea").attr("maxlength", "45");
        });

        $("document").ready(function () {
            $('textarea[maxlength]').live('keyup change', function () {
                var str = $(this).val()
                var mx = parseInt($(this).attr('maxlength'))
                if (str.length > mx) {
                    $(this).val(str.substr(0, mx))
                    return false;
                }
            }
          )
        })
    </script>
</asp:Content>
