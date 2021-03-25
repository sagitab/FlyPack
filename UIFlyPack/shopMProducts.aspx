<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="shopMProducts.aspx.cs" Inherits="UIFlyPack.shopMProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="searchPanel" runat="server">
        <div class="RowDiv" style="margin-left: 31%;margin-top: 5.75%; position: absolute;">
            <span class="UnderLineHeader" >Search Products</span>
            <asp:TextBox ID="serchedValue" runat="server" CssClass="TextBox"></asp:TextBox>
    
            <select id="SearchBy" name="D1" style="" runat="server" class="Select">
                <option value="Description">Product name</option>
                <option value="Price">Product price</option>
            </select>
       
            <asp:Button ID="SearchProductB" runat="server"  Text="Search product" OnClick="SearchProductB_OnClick" CssClass="BSearch"  />
            <asp:DropDownList ID="productOrder" CssClass="Select" runat="server" OnSelectedIndexChanged="productOrder_OnSelectedIndexChanged" AutoPostBack="true" >
            </asp:DropDownList>
            <asp:Label ID="MSG" runat="server" Text="" CssClass="ErrorMSG" ></asp:Label>
        </div>
    </asp:Panel>
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
                                <asp:LinkButton runat="server" CommandName="Delete" CssClass="ProductB" Text="Delete" ></asp:LinkButton>
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
</asp:Content>
