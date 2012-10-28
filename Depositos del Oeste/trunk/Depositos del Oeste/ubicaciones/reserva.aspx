<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reserva.aspx.cs" Inherits="Depositos_del_Oeste._Reserva" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Reserva</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    Cliente
    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" CssClass="cliente" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged"></asp:DropDownList><br />
    <asp:Panel ID="pnlArticulosGeneral" runat="server" Visible="false">
        Articulos<br />
        <asp:DropDownList ID="ddlArticulo" runat="server">
        </asp:DropDownList>
        <asp:TextBox ID="txtCantidad" runat="server">
        </asp:TextBox>
        <asp:GridView ID="gridArticulos" runat="server">
            <Columns>
                <asp:TemplateField Visible = "false" HeaderText="ID">
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Articulo">
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Cantidad">
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
       
        <br />
        <asp:LinkButton ID="linkAdd" runat="server" OnClick="linkAdd_Click">Agregar Articulo</asp:LinkButton>
        <br /><br />
        <asp:Button ID="btnSubmit" runat="server" Text="Registrar Reserva"/>
    </asp:Panel>
</asp:Content>
