<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reserva.aspx.cs" Inherits="Depositos_del_Oeste._Reserva" %>
<%@ Import Namespace="BackEnd.Utils" %>
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
        <asp:LinkButton ID="linkAdd" runat="server" OnClick="linkAdd_Click">Agregar Articulo</asp:LinkButton>
        <asp:GridView ID="gridArticulos" runat="server" AutoGenerateColumns="false">
            <Columns>                
                <asp:BoundField HeaderText="Articulo" DataField="desc" />
                <asp:BoundField HeaderText="Cantidad" DataField="cant"/>
                 <asp:TemplateField HeaderText="Eliminar">
                     <ItemTemplate>
                         <asp:LinkButton runat="server" ID="btnEliminar" Text="Eliminar" OnClick="Eliminar" CommandArgument='<%# Eval("index") %>' />
                     </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
       <asp:GridView ID="gridUbicaciones" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="Estanteria" DataField="NroEstanteria" />
                <asp:BoundField HeaderText="Nivel" DataField="Nivel" />
                <asp:BoundField HeaderText="Compartimiento" DataField="NroCompartimiento" />
                 <asp:TemplateField HeaderText="Actividad">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Enums.ActividadDesc(int.Parse(Eval("Actividad").ToString())) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Enums.EstadoDesc(int.Parse(Eval("Estado").ToString())) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Articulo" DataField="IdArticulo" />
                <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
            </Columns>
        </asp:GridView>
        
        <br />
        <br /><br />
        <asp:Button ID="btnSubmit" runat="server" Text="Registrar Reserva" OnClick="btnSubmit_Click"/>
    </asp:Panel>
</asp:Content>
