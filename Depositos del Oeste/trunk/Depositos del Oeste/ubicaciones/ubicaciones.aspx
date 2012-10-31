<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ubicaciones.aspx.cs" Inherits="Depositos_del_Oeste._Ubicaciones" %>
<%@ Import Namespace="BackEnd.Utils" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Ubicaciones</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    Estanteria
    <asp:DropDownList ID="ddlEstanteria" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEstanteria_SelectedIndexChanged"></asp:DropDownList><br />
    <asp:GridView ID="gridUbicaciones" runat="server">
        <Columns>
            <asp:BoundField HeaderText="Nivel" DataField="Nivel" />
            <asp:BoundField HeaderText="Compartimiento" DataField="NroCompartimiento" />
             <asp:TemplateField HeaderText="Actividad">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Enums.ActividadDesc(int.Parse(Eval("Actividad").ToString())) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Enums.EstadoDesc(int.Parse(Eval("Estado").ToString())) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Articulo" DataField="IdArticulo" />
            <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>
