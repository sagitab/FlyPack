<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManegerInfo.aspx.cs" Inherits="UIFlyPack.ManagerInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ErrorMSG {
            margin-left: 2%;
            color: darkred;
        }
         #deliverSearchDiv {
             padding-top: 2%;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul>
        <li> <span class="UnderLineHeader" style=" margin-left: 15%; margin-right: 1%; padding-bottom: 1%;">Search deliver</span> </li>
        
        <li>
            <div class="RowDiv" id="deliverSearchDiv"><asp:TextBox ID="DeliverSearchVal" runat="server" CssClass="TextBox" s></asp:TextBox>
      
                <select  id="DeliverSearchBy" name="D1" style="height: 2.5em;"  runat="server" class="Select" >
                    <option value="ID" >ID</option>
                    <option value="LastName" >Last name</option>
                    <option value="FirstName" >First name</option>
                </select>
        
                <asp:Button ID="deliverSearchB" runat="server" Text="search" OnClick="deliverSearchB_OnClick" CssClass="BSearch"  />
                <asp:Label ID="DeliverMSG" runat="server" Text="" CssClass="ErrorMSG"></asp:Label>
            </div>
        </li>

         <li>
 <asp:GridView ID="DeliveriesTable"  runat="server" AutoGenerateColumns="false" CssClass="content-table" >
        <Columns>
            
            <asp:BoundField DataField="FirstName" HeaderText="Delivery name" />
            <asp:BoundField DataField="PhoneNumber" HeaderText="Phone number" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <%--<asp:BoundField DataField="Num of orders" HeaderText="Num of orders" />--%>
          
        </Columns>
    </asp:GridView>
        </li>
        <li> <span class="UnderLineHeader" style=" margin-left: 15%; margin-right: 1%;">Search customer</span> </li>
        
        <li>
            <div class="RowDiv"><asp:TextBox ID="serchedValue" runat="server" CssClass="TextBox"></asp:TextBox>
      
                <select  id="SearchBy" name="D1" style="height: 2.5em;"  runat="server" class="Select" >
                    <option value="ID" >ID</option>
                    <option value="LastName" >Last name</option>
                    <option value="FirstName" >First name</option>
                </select>
        
                <asp:Button ID="SearchCustomerB" runat="server" Text="search" OnClick="SearchCustomerB_Click" CssClass="BSearch"  />
            <asp:Label ID="ErCustomer" runat="server" Text="" CssClass="ErrorMSG"></asp:Label>
            </div>
        </li>
        <li style="margin: 0% 30% 2% 0%;">
            <asp:Label ID="ErDelivery" runat="server" Text=""></asp:Label>
        </li>

 <asp:GridView ID="CustomersTable"  runat="server" AutoGenerateColumns="false" CssClass="content-table" >
        <Columns>
            
            <asp:BoundField DataField="FirstName" HeaderText="Customer name" />
            <asp:BoundField DataField="PhoneNumber" HeaderText="Phone number" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
          <%--  <asp:BoundField DataField="Num of orders" HeaderText="Num of orders" />--%>
        </Columns>
    </asp:GridView>
        
            <%--<li>
            <span>Num of orders</span>
        </li>--%>
       
            <li style="margin: 0 35%">
                <div style="display: inline-grid">
                    <asp:Label ID="NumOfCustomers" runat="server" Text=""  CssClass="NumInfo"></asp:Label>
                    <asp:Label ID="NumOfOrders" runat="server" Text="" CssClass="NumInfo"></asp:Label>
                </div>
            </li>
       
        <asp:GridView ID="ShopTable"  runat="server" AutoGenerateColumns="false" CssClass="content-table" Visible="False" >
            <Columns>
                <asp:BoundField DataField="ManagerName" HeaderText="Manager name" />
                <asp:BoundField DataField="ShopName" HeaderText="Shop name" />
                <%--<asp:BoundField DataField="Num of orders" HeaderText="Num of orders" />--%>
            </Columns>
        </asp:GridView>
       
        <li style="margin: 0% 30% 2% 0%;">
            <asp:Label ID="ErShopTable" runat="server" Text="" CssClass="BigErrorMSG"></asp:Label>
        </li>
    </ul>
</asp:Content>
