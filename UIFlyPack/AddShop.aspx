﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddShop.aspx.cs" Inherits="UIFlyPack.AddShop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <%--  <style>
    
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="AddShopDiv">
    <ul class="addList" >
        <li>
            <span class="UnderLineHeader" >Add shop</span>
        </li>
         <li>
             <span  >Shop name</span>
        </li>
        <li>           
            <asp:TextBox ID="ShopName" runat="server" CssClass="TextBox"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMSG" ID="ShopNameValidator" runat="server"  ControlToValidate="ShopName" ErrorMessage="Shop name need to be 2 until 10 tabs" SetFocusOnError="True" ValidationExpression="^.{2,10}$" ></asp:RegularExpressionValidator>
        </li>
        <li>
             <span  >Select shop manager</span>
        </li>
         <li>           
            <asp:DropDownList ID="ShopMSelect" runat="server" CssClass="Select">
            </asp:DropDownList>
        </li>
         <li>
             <asp:Button ID="AddShop1" runat="server" Text="Add shop" CssClass="LargeButton" OnClick="AddShop_Click" />
        </li>
        <li>           
             <asp:Label ID="MSG" runat="server" Text="" CssClass="ErrorMSG"></asp:Label>
        </li>
    </ul>
    </div>
</asp:Content>
