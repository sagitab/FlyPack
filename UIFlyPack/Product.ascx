<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Product.ascx.cs" Inherits="UIFlyPack.Product" %>
<asp:Label runat="server" ID="MSG" CssClass="BigErrorMSG"></asp:Label>
<div class="ProductDiv" id="storeDiv">
    <asp:DataList ID="ProductsList"
                  CellPadding="20"
                  CellSpacing="30"
                  RepeatDirection="Horizontal"
                  RepeatLayout="Table"
                  RepeatColumns="4"
                  runat="server" OnItemCommand="ProductsList_OnItemCommand"
                  
                >
        <ItemTemplate>
            <div class="Product">
                <asp:Image id="ProductPic" AlternateText="Product picture" 
                           ImageUrl='<%# "../ProductsImg/"+Eval("ImageUrl") %>'
                           runat="server" CssClass="ProductPics"/>
                <div class="ProductInfo">
                    <ul>
                        <li style=" margin-left: 7vh;"> <asp:Label runat="server"  ID="productName" Text='<%#  Eval("Description")  %>'></asp:Label></li>
                        <li style=" margin-left: 11vh;"> <asp:Label runat="server" ID="productPrice" Text='<%#Eval( "Price","${0}") %>'></asp:Label></li>
                    </ul>
                   
                   
              <%--      <span class="productName"></span>
                    <span class="productPrice">''</span>--%>
                </div>
                <div class="ProductBDiv">
            <asp:LinkButton runat="server" CommandName="AddToCart" CssClass="ProductB" Text="Add to cart" ></asp:LinkButton>
              </div>
            </div>
        </ItemTemplate>
 
    </asp:DataList>
    <asp:Label runat="server" ID="addToCartMsg" CssClass="ErrorMSG"></asp:Label>
   <%-- <div class="list_footer">
        <asp:Button Text="&larr; Previous" CssClass="button" OnClick="prevButton_OnClick" ID="prevButton" runat="server" />
        <asp:Label Text="1" ID="PageLabel" runat="server" />
        <asp:Button Text="Next &rarr;" CssClass="button" OnClick="nextButton_OnClick" ID="nextButton" runat="server" />
    </div>--%>
</div>