<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DeliveryMap.aspx.cs" Inherits="UIFlyPack.DeliveryMap" %>
<%@ Import Namespace="BLFlyPack" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="map"></div>
    <script>
        var map;
        var points = [];
        var shops = JSON.parse(`<%=Json.Serialize(Shops.AsReadOnly())%>`);
        var customersAddresses = JSON.parse(`<%=Json.Serialize(CustomersAddresses.AsReadOnly())%>`);
            console.log(shops);
      

        function initMap() {
            var mapOptions = {
                zoom: 8,
                center: new google.maps.LatLng(0,0)
            }
            map = new google.maps.Map(document.getElementById('map'), mapOptions);
            var DeliverLocation = <% =new Point(((BLUser) Session["user"]).location) %>;
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
        }
        function CenterPoint() {
            debugger;
            var Lat = 0;
            var Lng = 0;
            for (let point of points) {
                Lat += point.lat();
                Lng += point.lng();
            }
            var NumOfPoints = points.length;
            return new google.maps.LatLng(Lat / NumOfPoints, Lng / NumOfPoints);
        }
        function  ShopMarker(shop) {
            return new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(shop.position.Lat, shop.position.Lng),
                label: shop.name
            });
        }
        function CustomerMarker(customersAddress) {
            return new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(customersAddress.position.Lat, customersAddress.position.Lng),
                label: customersAddress.toString()
            });
        }
    </script>
    <script async defer
            src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCfNHGvBm3VSe6XZ9oVKrYfW4YqyJJq9v4&callback=initMap">
    </script>
</asp:Content>
