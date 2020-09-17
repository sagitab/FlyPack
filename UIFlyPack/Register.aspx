<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UIFlyPack.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="addList" style="margin:20px 50%">
        <li>
             <span class="Header" >ID</span>
        </li>
        <li>           
            <asp:TextBox ID="ID" runat="server" CssClass="TextBox"></asp:TextBox>
             <asp:RegularExpressionValidator ID="IDValidator" runat="server"  ControlToValidate="ID" ErrorMessage="ID need to be  9 tabs" SetFocusOnError="True" ValidationExpression="[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]" ></asp:RegularExpressionValidator>
        </li>
        <li>
             <span class="Header" >Name</span>
        </li>
        <li>           
            <asp:TextBox ID="Name" runat="server" CssClass="TextBox"></asp:TextBox>
            <asp:RegularExpressionValidator ID="NameValidator" runat="server"  ControlToValidate="Name" ErrorMessage="Name need to be 2 until 10 tabs" SetFocusOnError="True" ValidationExpression="^.{2,10}$" ></asp:RegularExpressionValidator>
        </li>
         <li>
             <span  class="Header" >Last Name</span>
        </li>
        <li>           
            <asp:TextBox ID="LName" runat="server" CssClass="TextBox"></asp:TextBox>
            <asp:RegularExpressionValidator ID="LNameValidator" runat="server"  ControlToValidate="LName" ErrorMessage="Name need to be 2 until 10 tabs" SetFocusOnError="True" ValidationExpression="^.{2,10}$" ></asp:RegularExpressionValidator>
        </li>
        <li>
             <span  class="Header">Password</span>
        </li>
        <li>          
            <input runat="server" type="password" id="pass" CssClass="TextBox" style="  position: relative;    border-top: none;    border-left: none;    border-right: none;    height: 1.2em;    width: 150px;    font-size: 0.6em;    display: block;    border-bottom: solid 4px; border-bottom-color: darkblue; display: block; color: darkblue;background-color: white;"  />
           <asp:RegularExpressionValidator ID="passValidator" runat="server" ControlToValidate="pass" ErrorMessage="Password need to be 8 tabs" SetFocusOnError="True" ValidationExpression="^.{8}$" ></asp:RegularExpressionValidator>
        </li>
        <li>
             <span  class="Header">Phone Number</span>
        </li>
        <li>           
            <asp:TextBox ID="Phone" runat="server" CssClass="TextBox"></asp:TextBox>
            <asp:RegularExpressionValidator ID="PhoneValidator" runat="server" ControlToValidate="Phone" ValidationExpression="[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]" ErrorMessage="Unvalid phone" SetFocusOnError="True" ></asp:RegularExpressionValidator>
        </li>
        <li>
             <span  class="Header" style="padding: 2px 80px 0px 0px;">Email</span>
        </li>
        <li>           
            <asp:TextBox ID="Email" runat="server" Text="example@gmail.com" CssClass="TextBox"></asp:TextBox>
            <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="Email" ErrorMessage="Unvalid email" SetFocusOnError="True" ValidationExpression="^.{3,10}@gmail.com" ></asp:RegularExpressionValidator>
        </li>
        <li style="margin-right:150px" >

            <asp:Button ID="regB" runat="server" Text="Register" OnClick="regB_Click" CssClass="BSearch"   />

            

        </li>
        <li>

            <asp:Label ID="MSG" runat="server" Text=""></asp:Label>

        </li>
    </ul>
</asp:Content>
