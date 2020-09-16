<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewOrders.aspx.cs" Inherits="UIFlyPack.ViewOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul style="display:inline">
        <li>
               <asp:TextBox ID="serchedValue" runat="server" CssClass="TextBox"></asp:TextBox>
        </li>
         <li >
<select  id="SearchBy" name="D1" style=""  runat="server" class="Select" >
            <option value="OrderStutus" >Status</option>
            <option value="ArrivalTime" >Arrival time</option>
            <option value="ShopName" >Shop name</option>
            <option value="FirstName" >Delivery name</option>
        </select>
        </li>
         <li style="">
 <asp:Button ID="SearchOrderB" runat="server" Text="search" OnClick="SearchOrderB_Click" CssClass="BSearch"  />
        </li>
         <li >
             <asp:DropDownList ID="NewOrOld" CssClass="Select" runat="server" OnSelectedIndexChanged="NewOrOld_Click" AutoPostBack="true" >
              </asp:DropDownList>
<%--<select  id="NewOrOld" class="Select"  runat="server" onchange="NewOrOld_Click" >
            <option value="N" >New Orders</option>
            <option value="O" >Old Orders</option>
        </select>--%>
        </li>
         <li style="">
 <asp:GridView ID="OrderTable"  runat="server" AutoGenerateColumns="false" CssClass="content-table" OnRowDeleting="OrderTable_RowDeleting">
        <Columns>
            
            <asp:BoundField DataField="DeliveryName" HeaderText="Delivery name" />
            <asp:BoundField DataField="ShopName" HeaderText="Shop name" />
            <asp:BoundField DataField="OrderStutus" HeaderText="Status" />
            <asp:BoundField DataField="ArrivalTime" HeaderText="Arrival time" />
            <%--<asp:ButtonField ButtonType="Button" Text="cencel" Visible="true"  />--%>
            <asp:CommandField ButtonType="Button" Visible="true" DeleteText="cencel" ShowDeleteButton="true"  />
        </Columns>
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
