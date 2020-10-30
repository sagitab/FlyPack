<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewOrders.aspx.cs" Inherits="UIFlyPack.ViewOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul style="display: inline">
        <li >
            <span class="UnderLineHeader" style="margin: 0 auto" >View Orders</span><br/> 
        </li>
      <%--  <li style="margin-right: 1%;">
        
        </li>--%>
       <li>
           <div class="RowDiv" style="margin-left: 15%;">
               <span class="UnderLineHeader"style="margin-bottom: 1%; margin-right: 1%;"  >Search Orders</span>
               <asp:TextBox ID="serchedValue" runat="server" CssClass="TextBox"></asp:TextBox>
    
               <select id="SearchBy" name="D1" style="" runat="server" class="Select">
                   <option value="OrderStutus">Status</option>
                   <option value="ArrivalTime">Arrival time</option>
                   <option value="ShopName">Shop name</option>
                   <option value="FirstName">Delivery name</option>
               </select>
       
               <asp:Button ID="SearchOrderB" runat="server" Text="search" OnClick="SearchOrderB_Click" CssClass="BSearch" />
               <asp:DropDownList ID="NewOrOld" CssClass="Select" runat="server" OnSelectedIndexChanged="NewOrOld_Click" AutoPostBack="true">
               </asp:DropDownList>
           </div>
       </li>

        <li style="">

            <asp:GridView ID="OrderTable" runat="server" AutoGenerateColumns="false" CssClass="content-table" OnRowDeleting="OrderTable_RowDeleting" OnRowCommand="OrderTable_RowCommand"   >
            </asp:GridView>
        </li>
        <li>
            <asp:Label ID="ErMSG" runat="server" Text=""></asp:Label>
        </li>
        <li>
            <asp:Label ID="MSG" runat="server" Text=""></asp:Label>

        </li>
    </ul>























</asp:Content>
