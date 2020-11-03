<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="UIFlyPack.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Header {
            color: #b6e1ed;
        }
        body {
            background-image: none;
            background-color: #021030;
        }
        .centerDiv {
            color: #b6e1ed;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <header class="HomePageHeader">
        <div class="centerDiv" style="padding-top: 30%">
            <img src="Img/LogoHeader.png"  class="headerPic" />
            <%--<h1 class="Headers">
                <span class="logoHeader">fly pack</span>
                <span class="logoHeaderSub">drone delivery service</span>
            </h1>--%>
          
        </div>
        <span class="Header" id="customer" runat="server">Use drowns to take care your deliveries fast and accurate,track your order and keep contact with the deliver and the shop you order from. </span><%--customer--%>
        <span class="Header" id="delivery" runat="server">manage your your deliveries with maximum efficiency in the shortest way that possible,keeping contact with the customer and the shop </span><%--deliver--%>
        <span class="Header" id="shopManager" runat="server">manage your deliveries from your shop easier ,keeping contact with your customers and the delivers </span><%--shop manager--%>
        <span class="Header" id="systemManager" runat="server">manage the deliveries system simply and ease  </span><%--system manager--%>
        <span class="Header" id="unconnected" runat="server">control your deliveries simply ,please log in/register the see more details</span><%-- unconnected--%>
    </header>
   
</asp:Content>
