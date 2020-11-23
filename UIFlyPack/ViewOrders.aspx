<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewOrders.aspx.cs" Inherits="UIFlyPack.ViewOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ErrorMSG {
            margin-left: 2%;
        }
        .shoppingCart {
            position: absolute;
            left: 35vh;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul style="display: inline">
        <li >
            <span class="UnderLineHeader" style="margin: 0 auto" >View Orders</span><br/> 
        </li>
      <%--  <li style="margin-right: 1%;">
        
        </li>--%>
       <li>
           <div class="RowDiv" style="margin-left: 15%;margin-top: 5.75%;">
               <span class="UnderLineHeader"style="margin-bottom: 1%; margin-right: 1%; margin-top: 0%" >Search Orders</span>
               <asp:TextBox ID="serchedValue" runat="server" CssClass="TextBox"></asp:TextBox>
    
               <select id="SearchBy" name="D1" style="" runat="server" class="Select">
                   <option value="OrderStutus">Status</option>
                   <option value="ArrivalTime">Arrival time</option>
                   <option value="ShopName">Shop name</option>
                   <option value="FirstName">Delivery name</option>
               </select>
       
               <asp:Button ID="SearchOrderB" runat="server" Text="search" OnClick="SearchOrderB_Click" CssClass="BSearch"  />
               <asp:DropDownList ID="NewOrOld" CssClass="Select" runat="server" OnSelectedIndexChanged="NewOrOld_Click" AutoPostBack="true">
               </asp:DropDownList>
               <asp:Label ID="MSG" runat="server" Text="" CssClass="ErrorMSG" ></asp:Label>
           </div>
       </li>

        <li style="">
            <div  id="OrderTableDiv"style="position: relative;">
            <asp:GridView ID="OrderTable" runat="server" AutoGenerateColumns="false" CssClass="content-table" OnRowDeleting="OrderTable_RowDeleting" OnRowCommand="OrderTable_RowCommand"   >
            </asp:GridView>
            </div>
        </li>
        <li>
            <asp:Label ID="ErMSG" runat="server" Text="" CssClass="BigErrorMSG"></asp:Label>
            
        </li>
    </ul>

    <asp:Panel runat="server" CssClass="shoppingCart" ID="shoppingCartPanel" Visible="False">
     <asp:Label runat="server" ID="ProductError" CssClass="BigErrorMSG"></asp:Label>
    <div class="ProductDiv">
        <asp:ImageButton runat="server" ImageUrl="Img/x-button.png" CssClass="Xbutton" OnClick="XButton_OnClick" ID="XButton"/>
        <asp:DataList ID="ProductsCart" CellPadding="20" CellSpacing="30" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="1" runat="server"   >
            <ItemTemplate>
                <div class="Product">
                    <asp:Image id="ProductPic" AlternateText="Product picture" 
                               ImageUrl='<%# "../ProductsImg/"+Eval("Product.ImageUrl") %>'
                               runat="server" CssClass="ProductPics"/>
                    <div class="ProductInfo">
                        <ul style="margin-top: 6vh;">
                            <li style=" margin-left: 7vh;"> <asp:Label runat="server"  ID="productName" Text='<%#  Eval("Product.Description")  %>'></asp:Label></li>
                            <li style=" margin-left: 11vh; display: contents;">
                                 <asp:Label runat="server" ID="productPrice" Text='<%#Eval( "Product.Price","${0}") %>'></asp:Label>
                                <br/>
                                <asp:Label runat="server" ID="amount" Text='<%#Eval( "Amount") %>' CssClass="Header" ></asp:Label>
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
    </asp:Panel>





















</asp:Content>
