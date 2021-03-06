﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="UIFlyPack.Store" EnableEventValidation="false"  %>
<%@ Register src="Product.ascx" tagName="Products" tagPrefix="FlyPackControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style>
     #ContentPlaceHolder1_Shops {
         left: 59%;
         position: absolute;
         bottom: 71%;
         font-size: 1em;
     }
     #serchedValue {
         margin-left: 1vh;
     }
     #storeDiv {
         position: absolute;
         top: 35%;
         left: -2%;
     }
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul>
        <li>
            <span class="Header" style="text-decoration: underline; font-size: 3em; position: absolute; left: 50%; bottom:76%;">Store</span>
        </li>
        <li>
            <asp:Button runat="server" ID="ChangeShop" OnClick="ChangeShop_OnClick" Text="Change shop" CssClass="LargeButton" OnClientClick="Confirm()"/>
        </li>
        <li>
            <asp:Panel runat="server" ID="shopSelectDiv">
            <div class="RowDiv">
                <span class="Header" style="text-decoration: underline; font-size: 3em; position: absolute; left: 45%; bottom: 69%;">choose shop-</span>
                <asp:DropDownList ID="Shops" CssClass="Select" runat="server" OnSelectedIndexChanged="Shops_OnSelectedIndexChanged"  AutoPostBack="True"  >
                </asp:DropDownList>
            </div>
            </asp:Panel>
        </li>
        <li>
            <asp:Label runat="server" ID="shopName" CssClass="Header" Text=""></asp:Label>
        <li>
            <asp:Panel ID="searchPanel" runat="server">
            <div class="RowDiv" style="margin-left: 31%;margin-top: 5.75%; position: absolute;">
                <span class="UnderLineHeader" >Search Products</span>
                <asp:TextBox ID="serchedValue" runat="server" CssClass="TextBox"></asp:TextBox>
    
                <select id="SearchBy" name="D1" style="" runat="server" class="Select">
                    <option value="Description">Product name</option>
                    <option value="Price">Product price</option>
                </select>
       
                <asp:Button ID="SearchProductB" runat="server"  Text="Search product" OnClick="SearchProductB_OnClick" CssClass="BSearch"  />
                <asp:DropDownList ID="productOrder" CssClass="Select" runat="server" OnSelectedIndexChanged="productOrder_OnSelectedIndexChanged" AutoPostBack="true" >
                </asp:DropDownList>
                <asp:Label ID="MSG" runat="server" Text="" CssClass="ErrorMSG" ></asp:Label>
            </div>
            </asp:Panel>
        </li>
        <li style="top: 50%; position: absolute;"> 
            <FlyPackControls:Products ID="Products" runat="server" ></FlyPackControls:Products>
           
        </li>
    </ul>
    <asp:HiddenField runat="server" ID="isConfirm"/>
    <script type="text/javascript">
        function Confirm() {
            return confirm('Are you sure you want to switch shop? all your products in shopping cart will deleted');
        }
    </script>
</asp:Content>
