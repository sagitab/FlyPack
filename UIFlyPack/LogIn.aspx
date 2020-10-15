<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="UIFlyPack.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-image: none;
            background-color: #28679b;
            background-size: 100% 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <img src="Img/droneIMG.jpg" id="DroneLogIn" style=" margin: 0px 40% -0.3% 42.5%;width: 15%;" />
    <div id="LogInDiv">
    <ul class="addList" style="margin: 0vh 35%">
        <li>
            <span class="Header">User Name </span>
        </li>
         <li>
             <asp:TextBox runat="server" ID="Name" CssClass="TextBox"></asp:TextBox>
               <asp:RegularExpressionValidator CssClass="ErrorMSG" ID="NameValidator" runat="server"  ControlToValidate="Name" ErrorMessage="Name need to be 2 until 10 tabs" SetFocusOnError="True" ValidationExpression="^.{2,10}$" ></asp:RegularExpressionValidator>
        </li>
         <li>
           <span class="Header">Password </span>
        </li>
         <li>
              <input runat="server" type="password" id="Pass" CssClass="TextBox" style="  position: relative;    border-top: none;    border-left: none;    border-right: none;    height: 1.2em;    width: 150px;    font-size: 0.6em;    display: block;    border-bottom: solid 4px; border-bottom-color: darkblue; display: block; color: darkblue;background-color: white;"  />
             <asp:RegularExpressionValidator CssClass="ErrorMSG" ID="passValidator" runat="server" ControlToValidate="Pass" ErrorMessage="Password need to be 8 tabs" SetFocusOnError="True" ValidationExpression="^.{8}$" ></asp:RegularExpressionValidator>
        </li>
         <li>
             <asp:Button runat="server" ID="LogInB" OnClick="LogInB_Click" CssClass="BSearch" Text="Log In" />
        </li>
        <li>
           <asp:Label runat="server" ID="massage" CssClass="ErrorMSG"></asp:Label>
        </li>
    </ul>
        </div>
</asp:Content>
