<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewOrders.aspx.cs" Inherits="UIFlyPack.ViewOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul>
        <%--<li>
               <asp:TextBox ID="serchedValue" runat="server"></asp:TextBox>
        </li>
         <li>
<select id="SearchBy" name="D1">
            <option value="" >status</option>
        </select>
        </li>
         <li>
 <asp:Button ID="SearchOrderB" runat="server" Text="search" />
        </li>--%>
         <li>
 <asp:GridView ID="OrderTable"  runat="server" AutoGenerateColumns="False" CssClass="content-table">
        <Columns>
            
            <asp:BoundField DataField="userName" HeaderText="Delivery name" />
            <asp:BoundField DataField="phoneNum" HeaderText="Shop name" />
            <asp:BoundField DataField="OrderStutus" HeaderText="Status" />
            <asp:BoundField DataField="Time" HeaderText="Arrival time" />
        </Columns>
    </asp:GridView>
        </li>
    </ul>
 
   
        
   
   
</asp:Content>
