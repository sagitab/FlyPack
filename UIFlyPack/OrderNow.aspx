<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OrderNow.aspx.cs" Inherits="UIFlyPack.OrderNow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="OrdeNowDiv">
        <div class="middleDiv">
             <ul class="addList" style="margin: 5vh 44%;">
        <li>
            <span class="Header" >Place your order</span>
        </li>
                 
<%--        <li>
             <span  > Shop </span>
        </li>
         <li>           
          <asp:DropDownList ID="ShopDropDownList" runat="server" CssClass="Select" >
             </asp:DropDownList>
        <li>
             <span  >Shop order id</span>
        </li>
        <li>           
            <asp:TextBox ID="ShopOrderID" runat="server" CssClass="TextBox"></asp:TextBox>
               <asp:RangeValidator ID="RangeShopOrderID" runat="server" ErrorMessage="no" Type="Integer" MaximumValue="10" MinimumValue="1" ControlToValidate="ShopOrderID"></asp:RangeValidator>
        </li>
         <li>
             <span  class="Header" >Arirval time</span>
        </li>
        <li>           
           <select id="times" runat="server">
               <option value="" id="op1">13:30.09.09.2020</option>
              
           </select>
        </li>--%>
                   
   
        <li>
             <span  style=" margin: 0 20% 2% 35%;" id="instractor" runat="server"> Type your address or click on the map to add address </span>
   
             <div id="map"></div>
            <input id="address" type="text" value="Israel Tel Aviv" class="TextBox" name="10" />
        </li>
        <li style="margin-right: 150px">
            <input type="button" value="Update Address" onclick="codeAddress()" class="BSearch" />
        </li>
        <li>
            <span id="massage"></span>
        </li>
        <%--<li>
             <span  >Address</span>
        </li>
        <li>          
             <asp:TextBox ID="Adress" runat="server" CssClass="TextBox"></asp:TextBox>
        </li>--%>
         <li>
             <span  >Number of floor</span>
        </li>
        <li>          
             <asp:TextBox ID="NumOfFloor" runat="server" CssClass="TextBox"></asp:TextBox>
            <asp:RangeValidator ID="NumOfFloorValidator" CssClass="ErrorMSG" SetFocusOnError="True" runat="server" ErrorMessage="Enter floor between 0 to 120" Type="Integer" MaximumValue="120" MinimumValue="0" ControlToValidate="NumOfFloor"></asp:RangeValidator>
        </li>
         <li>
             <asp:Button runat="server" ID="OrderB" OnClick="OrderB_Click" CssClass="LargeButton" Text="Order" />
        </li>
        <li>
            <asp:Label ID="MSG" runat="server" Text="" CssClass="ErrorMSG"></asp:Label>
        </li>
        </ul>
        </div>
   
    </div>
    <asp:HiddenField runat="server" ID="LatLng" Value="1,1" />
     <script>
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
    </script>
</asp:Content>
