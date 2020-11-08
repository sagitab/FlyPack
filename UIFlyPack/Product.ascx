<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Product.ascx.cs" Inherits="UIFlyPack.Product" %>
<div class="ProductDiv">
    <asp:DataList id="ProductsList"
                  CellPadding="20"
                  CellSpacing="30"
                  RepeatDirection="Horizontal"
                  RepeatLayout="Table"
                  RepeatColumns="4"
                  runat="server"
                  CssClass="store">
        <ItemTemplate>
            <div class="Product">
                <asp:Image id="ProductPic" AlternateText="Product picture" 
                           ImageUrl='<%#: "../Images/GameBackgrounds/"+DataBinder.Eval(Container.DataItem, "Background") %>'
                           runat="server" CssClass="ProductPics"/>
                <div class="ProductInfo">
                    <span class="productName"><%#: DataBinder.Eval(Container.DataItem, "GenresString") %></span>
                    <span class="productPrice"><%#: DataBinder.Eval(Container.DataItem, "GenresString") %></span>
                </div>
                <div class="ProductBDiv">
                    <button class="ProductB"><%#: DataBinder.Eval(Container.DataItem, "Price", "${0}") %></button>
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