<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UIFlyPack.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style> .TextBox {
      background-color: black;
      color: white;
  }
  .Header {
      color: #03c1ae;
  }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="RegDiv">
    <ul class="addList" >
        <li>
            <span class="Header" id="PageHeader"  runat="server">Register</span>
        </li>
        <li>
            <span >ID</span>
        </li>
        <li>
            <asp:TextBox ID="ID" runat="server" CssClass="TextBox" ></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMSG"  ID="IDValidator" runat="server" ControlToValidate="ID" ErrorMessage="ID need to be  9 tabs" SetFocusOnError="True" ValidationExpression="[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]"></asp:RegularExpressionValidator>
        </li>
        <li>
            <span >Name</span>
        </li>
        <li>
            <asp:TextBox ID="Name" runat="server" CssClass="TextBox" ></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMSG" ID="NameValidator" runat="server" ControlToValidate="Name" ErrorMessage="Name need to be 2 until 10 tabs" SetFocusOnError="True" ValidationExpression="^.{2,10}$"></asp:RegularExpressionValidator>
        </li>
        <li>
            <span >Last Name</span>
        </li>
        <li>
            <asp:TextBox ID="LName" runat="server" CssClass="TextBox"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMSG" ID="LNameValidator"  runat="server" ControlToValidate="LName" ErrorMessage="Name need to be 2 until 10 tabs" SetFocusOnError="True" ValidationExpression="^.{2,10}$"></asp:RegularExpressionValidator>
        </li>
        <li>
            <span >Password</span>
        </li>
        <li>
            <asp:TextBox ID="pass" runat="server" CssClass="TextBox" TextMode="Password"  Text="00000000" ></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMSG" ID="passValidator" runat="server" ControlToValidate="pass" ErrorMessage="Password need to be 8 tabs" SetFocusOnError="True" ValidationExpression="^.{8}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator runat="server" ID="PassNotEmpty" CssClass="ErrorMSG" ControlToValidate="pass" ErrorMessage="Password need to be 8 tabs" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </li>
        <li>
            <span >Phone Number</span>
        </li>
        <li>
            <asp:TextBox ID="Phone" runat="server" CssClass="TextBox" TextMode="Phone"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMSG" ID="PhoneValidator" runat="server" ControlToValidate="Phone" ValidationExpression="[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]" ErrorMessage="Unvalid phone" SetFocusOnError="True"></asp:RegularExpressionValidator>
        </li>
        <li>
            <span  style="padding: 2px 80px 0px 0px;">Email</span>
        </li>
        <li>
            <asp:TextBox ID="Email" runat="server" Text="example@gmail.com" CssClass="TextBox" TextMode="Email"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMSG" ID="EmailValidator" runat="server" ControlToValidate="Email" ErrorMessage="Unvalid email" SetFocusOnError="True" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
        </li>
        <li>
            <asp:Label ID="MSG" runat="server" Text=""></asp:Label>
        </li>
    </ul>
    <span  style=" margin: 0 20% 2% 35%;" id="instractor" runat="server"> Type your address or click on the map to add address </span>
    <div id="map"></div>
    <ul class="addList" style="margin: 0px 50% ">
        <li>
            <input id="address" type="text" value="Israel Tel Aviv" class="TextBox" name="10" />
        </li>
        <li style="margin-right: 150px">
            <input type="button" value="Update Address" onclick="codeAddress()" class="BSearch" />
        </li>
        <li>
            <span id="massage"></span>
        </li>
        <li style="margin-right: 150px">
            <asp:Button ID="regB" runat="server" Text="Register" OnClick="regB_Click" CssClass="BSearch" OnClientClick=" return BtnClick()" CausesValidation="True" />
        </li>
    </ul>
    <asp:HiddenField runat="server" ID="LatLng" Value="1,1" />
    </div>
  
 <%--   <script>
        var geocoder;
        var map;
        var googleMarkerPoints = [];
        var NumOfCalls = 0;
        document.body.onload = function () {
            geocoder = new google.maps.Geocoder();
            var latlng = new google.maps.LatLng(32.085270, 34.784028);
            var mapOptions = {
                zoom: 8,
                center: latlng
            }
            map = new google.maps.Map(document.getElementById('map'), mapOptions);
            google.maps.event.addListener(map, 'click', function (event) {
                var location = event.latLng;
                PlaceMarker(0, location);
            });
        }

        function PlaceMarker(index, location) {
            if (NumOfCalls == 0) {
                googleMarkerPoints[index] = new google.maps.Marker({
                    map: map,
                    position: location
                });
            } else {
                googleMarkerPoints[index].setMap(null);
                googleMarkerPoints[index] = new google.maps.Marker({
                    map: map,
                    position: location,
                    label: address
                });
            }
            var lat = location.lat();
            var lng = location.lng();
            document.getElementById("<%=LatLng.ClientID%>").value = "" + lat + "," + lng;
            NumOfCalls++;
            document.getElementById('massage').innerHTML = "success to add address!!!";
        }
        function AddMarker(index, latitude, longitude, address) {
            googleMarkerPoints[index] = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(latitude, longitude),
                label: address
            });
            //var marker = googleMarkerPoints[index];
        }

        function UpdateMarker(index, lat, lng, address) {
            googleMarkerPoints[index].setMap(null);
            googleMarkerPoints[index] = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(lat, lng),
                label: address
            });
        }
        //function SelectMarker(index) {
        //    map.panTo(googleMarkerPoints[index]);
        //}

        function codeAddress() {
            var RealAddress,
                address = document.getElementById('address');
            if (address != null&&address!="") {
                RealAddress = address.value;
            }
            else {
                document.getElementById('massage').innerHTML = "fail to add address";
            }
            console.log(RealAddress);
            geocoder.geocode({ 'address': RealAddress },
                function (results, status) {
                    if (status == 'OK') {
                        var location = results[0].geometry.location;
                        map.setCenter(location);
                        var lat = location.lat();
                        var lng = location.lng();
                        if (NumOfCalls == 0) {
                            AddMarker(0, lat, lng, RealAddress);
                        } else {
                            UpdateMarker(0, lat, lng, RealAddress);
                        }

                        document.getElementById("<%=LatLng.ClientID%>").value = "" + lat + "," + lng;
                        var infowindow = new google.maps.InfoWindow({
                            content: "hohoo"
                        });
                        google.maps.event.addListener(location,
                            'click',
                            function () {
                                infowindow.open(map, location);
                            });
                        document.getElementById('massage').innerHTML = "success to add address!!!";
                    } else {
                        alert('Geocode was not successful for the following reason: ' + status);
                    }
                    NumOfCalls++;
                });
        }

    </script>
    <script async defer
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCfNHGvBm3VSe6XZ9oVKrYfW4YqyJJq9v4&callback=initMap">
    </script>--%>
</asp:Content>
