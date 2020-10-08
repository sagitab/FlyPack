<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManegerInfo.aspx.cs" Inherits="UIFlyPack.ManagerInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul>
         <li >
 <asp:GridView ID="DeliveriesTable"  runat="server" AutoGenerateColumns="false" CssClass="content-table" >
        <Columns>
            
            <asp:BoundField DataField="FirstName" HeaderText="Delivery name" />
            <asp:BoundField DataField="PhoneNumber" HeaderText="Phone number" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Num of orders" HeaderText="Num of orders" />
          
        </Columns>
    </asp:GridView>
        </li>
        <li >
             <li style="margin:0% 0% 0% 30%">
               <asp:TextBox ID="serchedValue" runat="server" CssClass="TextBox"></asp:TextBox>
        </li>
         <li  style="margin:0% 1% 0% 1%">
<select  id="SearchBy" name="D1" style=""  runat="server" class="Select" >
            <option value="ID" >ID</option>
             <option value="LastName" >Last name</option>
            <option value="FirstName" >First name</option>
        </select>
        </li>
         <li style="margin:0% 0% 0% 1%">
 <asp:Button ID="SearchCustomerB" runat="server" Text="search" OnClick="SearchCustomerB_Click" CssClass="BSearch"  />
        </li>
        </li>
        <li style="margin: 0% 30% 2% 0%;">
            <asp:Label ID="ErCustomer" runat="server" Text=""></asp:Label>
        </li>
        <li style="margin: 0% 30% 2% 0%;">
            <asp:Label ID="ErDelivery" runat="server" Text=""></asp:Label>
        </li>
 <asp:GridView ID="CustomersTable"  runat="server" AutoGenerateColumns="false" CssClass="content-table" >
        <Columns>
            
            <asp:BoundField DataField="FirstName" HeaderText="Customer name" />
            <asp:BoundField DataField="PhoneNumber" HeaderText="Phone number" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Num of orders" HeaderText="Num of orders" />
            
          
        </Columns>
    </asp:GridView>
        
            <%--<li>
            <span>Num of orders</span>
        </li>--%>
            <li>
            <asp:Label ID="NumOfOrders" runat="server" Text=""></asp:Label>
        </li>
    </ul>
</asp:Content>
