<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ShopingCart.aspx.cs" Inherits="UIFlyPack.ShoppingCart" %>
<%@ Register src="Product.ascx" tagName="Products" tagPrefix="FlyPackControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
       .ProductB {
           margin: 0.3em 4vh;
           margin-left: 10vh;
           padding-bottom: 6vh;
           position: absolute;
           left: 72%;
           top: 19%;
           background-color: black;
       }
      
       .Product {
           padding-left: 5vh;
           width: 160%;
           position: relative;
       }
       .ProductPics {
           padding: 3vh 3vh;
       }
       .Header {
           margin: 3vh;
           position: absolute;
           left: 70%;
           top: 20%;
       }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label runat="server" ID="MSG" CssClass="BigErrorMSG"></asp:Label>
    <div class="ProductDiv">
        <asp:DataList ID="ProductsCart" CellPadding="20" CellSpacing="30" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="1" runat="server"  OnItemCommand="ProductsCart_OnItemCommand" OnItemDataBound="ProductsCart_OnItemDataBound" >
            <ItemTemplate>
                <div class="Product">
                    <asp:Image id="ProductPic" AlternateText="Product picture" 
                               ImageUrl='<%# "../ProductsImg/"+Eval("ImageUrl") %>'
                               runat="server" CssClass="ProductPics"/>
                    <div class="ProductInfo">
                        <ul style="margin-top: 6vh;">
                            <li style=" margin-left: 7vh;"> <asp:Label runat="server"  ID="productName" Text='<%#  Eval("Description")  %>'></asp:Label></li>
                            <li style=" margin-left: 11vh;">
                                 <asp:Label runat="server" ID="productPrice" Text='<%#Eval( "Price","${0}") %>'></asp:Label>
                                <asp:Label runat="server" ID="amount" Text="" CssClass="Header" ></asp:Label>
                                <div class="ProductBDiv">
                                    <asp:LinkButton runat="server"  CommandName="Remove"   CssClass="ProductB"><img src="Img/delete.png" class="garbageImg" /></asp:LinkButton>
                                </div>
                            </li>
                        </ul>
                    </div>
                    
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <asp:Button runat="server" ID="OrderNow" Visible="False" CssClass="LargeButton" Text="Order now!!!"/>
</asp:Content>
