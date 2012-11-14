﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="remitos.aspx.cs" Inherits="Depositos_del_Oeste._Remitos" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Remitos</h3>
    <asp:Label runat="server" ID="lbError" CssClass="error"></asp:Label><br />
    Cliente
    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" CssClass="cliente"></asp:DropDownList><br />
    <asp:GridView ID="gridRemitos" runat="server">
        <Columns>
            <asp:BoundField HeaderText="Codigo de Remito" DataField="Id" />
            <asp:BoundField HeaderText="Fecha de Remito" DataField="FechaRemito" DataFormatString="{0:d}"/>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="remitos_ver.aspx?id=<%# Eval("Id") %>">Ver</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>
