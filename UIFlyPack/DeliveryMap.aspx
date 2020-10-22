<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DeliveryMap.aspx.cs" Inherits="UIFlyPack.DeliveryMap" %>
<%@ Import Namespace="BLFlyPack" %>
<%@ Import Namespace="System.Web.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span class="Header" >Deliveries Map</span>
    <div id="map"></div>
    <br/>
   <%-- <input type="button" onclick="showMap" value="Show map" id="showMapB"/>--%>
    <asp:Button runat="server" CssClass="BSearch" ID="updateMap" Text="Update Map" OnClick="updateMap_OnClick"/>
    <asp:Label runat="server" CssClass="ErrorMSG" ID="errorMSG"></asp:Label>
    <asp:HiddenField runat="server" ID="IsUpdated" Value="-1" />
    <script>
        var map;
        var points = [];
        var shops = [];
        var customers = [];
        var infoWindows = [];
        <%--    var customersAddresses = JSON.parse(`<%=Json.Serialize(CustomersAddresses.AsReadOnly())%>`);--%>
      
        document.body.onload = function () {
            
            var hiddenField = document.getElementById('ContentPlaceHolder1_IsUpdated');
            if (hiddenField.value = "0")
            {
                initMap();
                hiddenField.value = "-1";
            } 
            //debugger;
            //if (hiddenField.value = "-1")
            //{
            //    document.getElementById('showMapB').style.visibility = 'hidden';
            //}
            //else
            //{
            //    document.getElementById('showMapB').style.visibility = 'visible';
            //}



        }
        function initMap() {
            debugger;
            var shops =  <%=Shops%>;
            var customersAddresses = <%=Customers%>;
          <%--  var mapOptions = {
                zoom: 8,
                center: new google.maps.LatLng(0, 0)
            }
            map = new google.maps.Map(document.getElementById('map'), mapOptions);
            var DeliverLocation = <% =Json.Encode(new Point(((BlUser) Session["user"]).Location))%>;
            var startPoint = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(DeliverLocation.Lat, DeliverLocation.Lng),
                label: "start point",
                icon: "Img/flagIcon.png" 
            });
            points[0] = startPoint;
            debugger;
            for (var i = 0; i < shops.length; i++) {
                var Index = 2 * i + 1;
                var shop = ShopMarker(shops[i]);
                var customersAddress = CustomerMarker(customersAddresses[i]);
                //var shop = (shops[i]);
                //var customersAddress = (customersAddresses[i]);


                points[Index] = shop;
                points[Index + 1] = customersAddress;
            }
            //var flightPath = new google.maps.Polyline({
            //    path: points,
            //    map: map
            //});
            
            var flightPath = new google.maps.Polyline({
                path: points,
                strokeColor: "#0000FF",
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: "#0000FF",
                fillOpacity: 0.4
            });
            map.setCenter(startPoint);--%>
        }

        <%-- function initMap() {
            var mapOptions = {
                zoom: 8,
                center: new google.maps.LatLng(0, 0)
            }
            map = new google.maps.Map(document.getElementById('map'), mapOptions);
            var DeliverLocation = <% =new Point(((BlUser) Session["user"]).Location) %>;
            console.log(DeliverLocation);
            var startPoint = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(DeliverLocation.Lat, DeliverLocation.Lng),
                label: shop.name
            });
            points.add(startPoint);
            for (var i = 0; i < shops.length; i++) {
                var shop = ShopMarker(shops[i]);
                var customersAddress = CustomerMarker(customersAddresses[i]);
                points.add(shop);
                points.add(customersAddress);
            }
            var flightPath = new google.maps.Polyline({
                path: points,
                map: map
            });
            map.setCenter(CenterPoint());
        }
        document.body.onload = function () {
            initMap();
        }--%>
        //document.body.onload = function () {
        //    debugger;
        //    var hiddenField = document.getElementById('ContentPlaceHolder1_shopJson');
        //    var JsonString = hiddenField.value;
        //    console.log(JsonString);
        //    var shops = JSON.parse(JsonString);
        //    debugger;
        //    console.log(shops);
        //}

        //function CenterPoint() {
        //    debugger;
        //    var Lat = 0;
        //    var Lng = 0;
        //    for (let point of points) {
        //        Lat += parseFloat(point.getPosition().lat());
        //        Lng += parseFloat(point.getPosition().lng());
        //    }
        //    var NumOfPoints = points.length;
        //    return new google.maps.LatLng(parseFloat(Lat / NumOfPoints), parseFloat(Lng / NumOfPoints));
        //}
        function ShopMarker(shop) {
            var lat = parseFloat(shop.Location.Lat) ;
            var lng = parseFloat(shop.Location.Lng);
            return new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(lat, lng),
                label: shop.ShopName,
                icon: "Img/shopIcon.png" 
            });
        }
        function CustomerMarker(customersAddress) {
            debugger;
            var lat = parseFloat( customersAddress.Location.Lat);
            var lng = parseFloat(customersAddress.Location.Lng); 
            var  customerDescription = "Customer name- "+customersAddress.CustomerName;
         <%--   var customerDescription = <% Customers[%>i<%].ToString(); %>;--%>
         var CustomerMarker = new google.maps.Marker({
             map: map,
             position: new google.maps.LatLng(lat, lng),
             label: customerDescription.toString(),
             icon: "Img/houseIcon.png" 
         });
            CustomerMarker.info = new google.maps.InfoWindow({
                content: "Floor- " + customersAddress.NumOfFloor
            });

            google.maps.event.addListener(CustomerMarker, 'click', function () {
                CustomerMarker.info.open(map, CustomerMarker);
            });
            return CustomerMarker;
        }
    </script>
    <script async defer
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCfNHGvBm3VSe6XZ9oVKrYfW4YqyJJq9v4&callback=initMap">
    </script>
</asp:Content>
