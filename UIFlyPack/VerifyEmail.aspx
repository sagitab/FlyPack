<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="VerifyEmail.aspx.cs" Inherits="UIFlyPack.VerifyEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .addList {
            margin: 0 auto;
        }
        .ErrorMSG {
            margin: 3% 1%;
        }
        /*.addList li {
          
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="verifyEmailDiv">
        <ul class="addList">
            <li><span class="Header">Please verify your email</span></li>
            <li>
                <asp:TextBox runat="server" ID="verifyCode" CssClass="TextBox"></asp:TextBox>
                <asp:RegularExpressionValidator CssClass="ErrorMSG" ID="verifyCodeValidator" runat="server" ControlToValidate="verifyCode" ErrorMessage="invalid code" SetFocusOnError="True" ValidationExpression="^.{6}$"></asp:RegularExpressionValidator>
            </li>
            <li>   <asp:Button runat="server" ID="verifyB" OnClick="verifyB_OnClick" CssClass="LargeButton" Text="Verify Code" /></li>
            <li>    <asp:Label ID="MSG" runat="server" Text="" CssClass="ErrorMSG"></asp:Label> </li>
        </ul>
    </div>
</asp:Content>
