<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="pedidos.aspx.cs" Inherits="Depositos_del_Oeste._Pedidos" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Pedidos</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" Width="200px" CssClass="ddlcliente"></asp:DropDownList><br />
    <asp:GridView ID="gridPedidos" runat="server">
        <Columns>
            <asp:BoundField HeaderText="Codigo de Pedido" DataField="Id" />
            <asp:BoundField HeaderText="Fecha de Pedido" DataField="FechaPedido" DataFormatString="{0:d}"/>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="pedidos_detalles.aspx?id=<%# Eval("Id") %>">Ver</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>
