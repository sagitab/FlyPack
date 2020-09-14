<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OrderNow.aspx.cs" Inherits="UIFlyPack.OrderNow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="addList" style="margin:20px 35%">
        <li>
             <span class="Header" > Shop </span>
        </li>
         <li>           
          <asp:DropDownList ID="ShopDropDownList" runat="server" >
             </asp:DropDownList>
        <li>
             <span class="Header" > Shop order id</span>
        </li>
        <li>           
            <asp:TextBox ID="ShopOrderID" runat="server" CssClass="TextBox"></asp:TextBox>
               <asp:RangeValidator ID="RangeShopOrderID" runat="server" ErrorMessage="no" Type="Integer" MaximumValue="10" MinimumValue="1" ControlToValidate="ShopOrderID"></asp:RangeValidator>
        </li>
         <li>
             <span  class="Header" >Arirval time</span>
        </li>
        <li>           
           <select id="times" runat="server">
               <option value="" id="op1">13:30.09.09.2020</option>
              
           </select>
        </li>
        <li>
             <span  class="Header">Adress</span>
        </li>
        <li>          
             <asp:TextBox ID="Adress" runat="server" CssClass="TextBox"></asp:TextBox>
        </li>
         <li>
             <span  class="Header">Number of floor</span>
        </li>
        <li>          
             <asp:TextBox ID="NumOfFloor" runat="server" CssClass="TextBox"></asp:TextBox>
        </li>
         <li>
             <asp:Button runat="server" ID="OrderB" OnClick="OrderB_Click" CssClass="BSearch" Text="Order" />
        </li>
        <li>          
            
            <asp:Label ID="MSG" runat="server" Text=""></asp:Label>
            
        </li>
        </ul>
</asp:Content>
