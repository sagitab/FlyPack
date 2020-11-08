<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="UIFlyPack.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-image: url(Img/skyBG.jpg);
            background-size: 100% 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
        <asp:Label ID="seccessMSG" runat="server" Text="" CssClass="Header"></asp:Label>
   
    <div id="Move">
        <asp:Image runat="server" ImageUrl="Img/FlyPackDrone.png" ID="DroneLogIn" />
        <div id="LogInDiv" >
            <ul class="addList" style="margin: 0vh 35%">
                <li>
                    <span class="Header">User Name </span>
                </li>
                <li>
                    <asp:TextBox runat="server" ID="Name" CssClass="TextBox"></asp:TextBox>
                    <asp:RegularExpressionValidator CssClass="ErrorMSG" ID="NameValidator" runat="server" ControlToValidate="Name" ErrorMessage="Name need to be 2 until 10 tabs" SetFocusOnError="True" ValidationExpression="^.{2,10}$"></asp:RegularExpressionValidator>
                </li>
                <li>
                    <span class="Header">Password </span>
                </li>
                <li>
                    <asp:TextBox runat="server" ID="Pass" CssClass="TextBox" TextMode="Password"></asp:TextBox>
                    <asp:RegularExpressionValidator CssClass="ErrorMSG" ID="passValidator" runat="server" ControlToValidate="Pass" ErrorMessage="Password need to be 8 tabs" SetFocusOnError="True" ValidationExpression="^.{8}$"></asp:RegularExpressionValidator>
                </li>
                <li>
                    <asp:Button runat="server" ID="LogInB" OnClick="LogInB_Click" CssClass="LargeButton" Text="Log In" />
                </li>
                <li>
                    <asp:Label runat="server" ID="massage" CssClass="ErrorMSG"></asp:Label>
                </li>
            </ul>
            <asp:ImageButton runat="server" ImageUrl="Img/recycling-symbol.png" ID="CleanB" OnClick="CleanB_Click" />
        </div>
    </div>
    <asp:HiddenField runat="server" ID="WhichAnimation" Value="1" />
    <%-- 1-spin-animation 2-spinRight-animation 0-no animation--%>
    <script>
        document.body.onload = function MoveDrone() {
            var WhichAnimation = document.getElementById('ContentPlaceHolder1_WhichAnimation').value.toString();
            var MoveDiv = document.getElementById('Move');
            debugger;
            switch (WhichAnimation) {
                case "1":
                    MoveDiv.classList.remove('spin-animation');
                    setTimeout(function () { MoveDiv.classList.add('spin-animation'); }, 100);
                    break;
                case "2":
                    MoveDiv.classList.remove('spinRight-animation');
                    MoveDiv.classList.add('spinRight-animation')
                            .bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd",
                                function () {
                                    window.location.replace("HomePage.aspx");
                                });
                    break;
                case "3":
                    var DroneImage = document.getElementById('ContentPlaceHolder1_DroneLogIn');
                    DroneImage.src = 'Img/angryDrone.png'; 
                    MoveDiv.classList.remove('angryDrone-animation');
                    setTimeout(function () { MoveDiv.classList.add('angryDrone-animation'); }, 100); 
                    break;
                default:
                    break;

            }


        }
    </script>
</asp:Content>
