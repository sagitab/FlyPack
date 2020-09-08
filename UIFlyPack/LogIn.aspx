<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="UIFlyPack.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="LogInDiv">
    <ul class="addList">
        <li>
            <span class="Header">User Name </span>
        </li>
         <li>
             <asp:TextBox runat="server" ID="Name" CssClass="TextBox"></asp:TextBox>
               <asp:RegularExpressionValidator ID="NameValidator" runat="server"  ControlToValidate="Name" ErrorMessage="Name need to be 2 until 10 tabs" SetFocusOnError="True" ValidationExpression="^.{2,11}$" ></asp:RegularExpressionValidator>
        </li>
         <li>
           <span class="Header">Password </span>
        </li>
         <li>
             <asp:TextBox runat="server" ID="Pass" CssClass="TextBox"></asp:TextBox>
             <asp:RegularExpressionValidator ID="passValidator" runat="server" ControlToValidate="Pass" ErrorMessage="Password need to be 8 tabs" SetFocusOnError="True" ValidationExpression="^.{8}$" ></asp:RegularExpressionValidator>
        </li>
         <li>
             <asp:Button runat="server" ID="LogInB" OnClick="LogInB_Click" CssClass="BSearch" Text="Log In" />
        </li>
        <li>
           <asp:Label runat="server" ID="massage"></asp:Label>
        </li>
    </ul>
        </div>
</asp:Content>
