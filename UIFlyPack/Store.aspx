<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="UIFlyPack.Store" %>
<%@ Register src="Product.ascx" tagName="Products" tagPrefix="FlyPackControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul>
        <li>
            <span class="Header">Store</span>
        </li>
        <li>
            <asp:DropDownList ID="Shops" CssClass="Select" runat="server" OnSelectedIndexChanged="Shops_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <div class="RowDiv" style="margin-left: 15%;margin-top: 5.75%;">
                <span class="UnderLineHeader"style="margin-bottom: 1%; margin-right: 1%; margin-top: 0%" >Search Products</span>
                <asp:TextBox ID="serchedValue" runat="server" CssClass="TextBox"></asp:TextBox>
    
                <select id="SearchBy" name="D1" style="" runat="server" class="Select">
                    <option value="Description">Product name</option>
                    <option value="Price">Product price</option>
                </select>
       
                <asp:Button ID="SearchProductB" runat="server" Text="Search product" OnClick="SearchProductB_OnClick" CssClass="BSearch"  />
                <asp:DropDownList ID="productOrder" CssClass="Select" runat="server" OnSelectedIndexChanged="productOrder_OnSelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:Label ID="MSG" runat="server" Text="" CssClass="ErrorMSG" ></asp:Label>
            </div>
        </li>
        <li>
            <FlyPackControls:Products></FlyPackControls:Products>
        </li>
    </ul>
</asp:Content>
