<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportes.aspx.cs" Inherits="Depositos_del_Oeste._Reportes" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Reportes:</h3>
    <asp:Label ID="lbMes" runat="server" Text="Mes"></asp:Label>
    <asp:DropDownList runat="server" ID="ddlMes"></asp:DropDownList>
    <asp:Label ID="lbAno" runat="server" Text="Año"></asp:Label>
    <asp:DropDownList runat="server" ID="ddlAno"></asp:DropDownList>
    <asp:Button runat="server" ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" />

    <asp:GridView runat="server" ID="tblReporte" RowStyle-HorizontalAlign="Center">

    </asp:GridView>

</asp:Content>
