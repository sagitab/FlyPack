<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OrderNow.aspx.cs" Inherits="UIFlyPack.OrderNow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="addList" style="margin:20px 35%">
        <li>
             <span class="Header" > Shop order id</span>
        </li>
        <li>           
            <asp:TextBox ID="ShopOrderID" runat="server" CssClass="TextBox"></asp:TextBox>
               <asp:RangeValidator ID="RangeShopOrderID" runat="server" ErrorMessage="RangeValidator" Type="Integer"<%-- MaximumValue="163"--%> MinimumValue="1" ControlToValidate="ShopOrderID"></asp:RangeValidator>
        </li>
         <li>
             <span  class="Header" >Arirval time</span>
        </li>
        <li>           
           <select id="times">
               <option value="13:00">13:00</option>
                 <option value="13:30">13:30</option>
           </select>
        </li>
        <li>
             <span  class="Header">Change adress</span>
        </li>
        <li>          
             <asp:TextBox ID="Adress" runat="server" CssClass="TextBox"></asp:TextBox>
        </li>
        </ul>
</asp:Content>
