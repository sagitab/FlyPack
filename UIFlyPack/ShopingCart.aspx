<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ShopingCart.aspx.cs" Inherits="UIFlyPack.ShoppingCart" %>
<%@ Register src="Product.ascx" tagName="Products" tagPrefix="FlyPackControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*.shoppingCart .ProductB {
            margin: 4vh 45.5%;
            margin-left: 10vh;
            padding-bottom: 8vh;
            position: absolute;
            left: 72%;
            top: -2%;
            background-color: black;
        }
      
        .shoppingCart .Product {
            padding-left: 5vh;
            width: 160%;
            position: relative;
            display: inline-flex;
            float: left;
            transform: translate(-46vh, 18px);
        }
        .shoppingCart .ProductPics {
            padding: 3vh 3vh;
        }
        .shoppingCart .Header {
            margin: 3vh;
            position: absolute;
            left: 70%;
            top: 20%;
        }
        .shoppingCart .LargeButton {
            margin: 4vh 45.85%;
        }
        .SumOrder {
            float: left;
            transform: translate(34vh, 0vh);
        }
        .SumOrder .Header {
            padding: 3vh;
            margin-bottom: 2vh;
        }
        .SumOrder span {
            padding: 3vh;
            left: auto;
            margin: auto;
            transform: translate(-20vh, -1vh);
            position: inherit;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:Panel runat="server" CssClass="shoppingCart" ID="shoppingCartPanel">
        <asp:Label runat="server" ID="MSG" CssClass="BigErrorMSG"></asp:Label>
        <asp:ImageButton runat="server" ImageUrl="Img/x-button.png" CssClass="Xbutton" OnClick="XButton_OnClick" ID="XButton"/>
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
        <div class="SumOrder" style="">
            <asp:Label runat="server" Text="" CssClass="Header" ID="NumOfProducts"></asp:Label>
            <asp:Label runat="server" Text="" CssClass="Header" ID="TotalPrice"></asp:Label>
        </div>
       
    <asp:Button runat="server" ID="OrderNow" Visible="False" CssClass="LargeButton" Text="Order now!!!" OnClick="OrderNow_OnClick"/>
    </asp:Panel>
</asp:Content>
