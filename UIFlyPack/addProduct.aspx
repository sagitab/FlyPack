<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="addProduct.aspx.cs" Inherits="UIFlyPack.addProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="addList" >
        <li>
            <span class="UnderLineHeader" >Add shop</span>
        </li>
        <li>
            <span  >Product name</span>
        </li>
        <li>           
            <asp:TextBox ID="ProductName" runat="server" CssClass="TextBox"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMSG" ID="ProductNameValidator" runat="server"  ControlToValidate="ProductName" ErrorMessage="Product nameneed to be 2 until 10 tabs" SetFocusOnError="True" ValidationExpression="^.{2,10}$" ></asp:RegularExpressionValidator>
        </li>
        <li>
            <span  >Product price</span>
        </li>
        <li>           
            <asp:TextBox ID="ProductPrice" runat="server" CssClass="TextBox"></asp:TextBox>
          <asp:RangeValidator runat="server" CssClass="ErrorMSG" SetFocusOnError="True" ErrorMessage="Enter price between 0 to 500" Type="Double" MaximumValue="500" MinimumValue="0" ControlToValidate="ProductPrice"></asp:RangeValidator>
        </li>
        <li>
            <span  >Product picture</span>
        </li>
        <li>
            <asp:FileUpload ID="FileUpload" runat="server"  CssClass="BSearch"  />
        </li>
        <li>
            <asp:Button ID="AddProduct" runat="server" Text="Add product" CssClass="LargeButton" OnClick="AddProduct_OnClick" />
        </li>
       
      
        <li>           
            <asp:Label ID="MSG" runat="server" Text="" CssClass="ErrorMSG"></asp:Label>
        </li>
    </ul>
</asp:Content>
