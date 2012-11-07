<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ubicaciones.aspx.cs" Inherits="Depositos_del_Oeste._Ubicaciones" %>
<%@ Import Namespace="BackEnd.Utils" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Ubicaciones</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    Estanteria
    <asp:DropDownList ID="ddlEstanteria" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEstanteria_SelectedIndexChanged"></asp:DropDownList><br />
    <asp:GridView ID="gridUbicaciones" runat="server" AutoGenerateColumns="false">
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
    </asp:GridView>
    <br />
</asp:Content>
