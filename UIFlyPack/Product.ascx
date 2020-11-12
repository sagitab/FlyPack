<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Product.ascx.cs" Inherits="UIFlyPack.Product" %>
<asp:Label runat="server" ID="MSG"></asp:Label>
<div class="ProductDiv">
    <asp:DataList id="ProductsList"
                  CellPadding="20"
                  CellSpacing="30"
                  RepeatDirection="Horizontal"
                  RepeatLayout="Table"
                  RepeatColumns="4"
                  runat="server"
                  CssClass="store"
                 OnItemCommand="ProductsList_OnItemCommand" >
        <ItemTemplate>
            <div class="Product">
                <asp:Image id="ProductPic" AlternateText="Product picture" 
                           ImageUrl='<%# "../ProductsImg/"+Eval("ImageUrl") %>'
                           runat="server" CssClass="ProductPics"/>
                <div class="ProductInfo">
                    <asp:Label runat="server" ID="productName" Text='<%#  Eval("Description")  %>'></asp:Label>
                    <asp:Label runat="server" ID="productPrice" Text='<%#Eval( "Price") %>'></asp:Label>
              <%--      <span class="productName"></span>
                    <span class="productPrice">''</span>--%>
                </div>
                <div class="ProductBDiv">
                 <asp:Button runat="server" CommandName="AddToCart" CssClass="BSearch"/>
                </div>
            </div>
        </ItemTemplate>
 
    </asp:DataList>
   <%-- <div class="list_footer">
        <asp:Button Text="&larr; Previous" CssClass="button" OnClick="prevButton_OnClick" ID="prevButton" runat="server" />
        <asp:Label Text="1" ID="PageLabel" runat="server" />
        <asp:Button Text="Next &rarr;" CssClass="button" OnClick="nextButton_OnClick" ID="nextButton" runat="server" />
    </div>--%>
</div>