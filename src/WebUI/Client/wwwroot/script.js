var mapOptions = null;
var map = null;
var infoWindow = null;
var markers = [];

function initialize() {
    mapOptions = {
        center: new google.maps.LatLng(44.436048, 26.102270),
        zoom: 12,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("map"), mapOptions);

    var myLatlng = new google.maps.LatLng(44.436048, 26.102270);
    
    infoWindow = new google.maps.InfoWindow();
    const locationButton = document.createElement("button");
    locationButton.textContent = "Pan to Current Location";
    locationButton.classList.add("custom-map-control-button");
    map.controls[google.maps.ControlPosition.TOP_CENTER].push(locationButton);

    locationButton.addEventListener("click", () => {
        // Try HTML5 geolocation.
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {

                    const pos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

                    console.log(position.coords.accuracy);

                    if (position.coords.accuracy > 50) {
                        infoWindow.setPosition(pos);
                        infoWindow.setContent("Innacurate location");
                        //infoWindow.open(map);
                    } else {
                        infoWindow.setPosition(pos);
                        infoWindow.setContent("Your location");
                        // infoWindow.open(map);
                    }

                    map.setCenter(pos);

                    var iconBase = 'https://maps.google.com/mapfiles/kml/shapes/';
                    var icon = 'cegeka-academy-map-marker.png';

                    const marker = new google.maps.Marker({
                        map: map,
                        position: pos,
                        icon: icon
                    });

                    console.log(marker);

                    if (findMarkerByPosition(pos) === false) {
                        markers.push(marker);
                    }
                },
                () => {
                    handleLocationError(true, infoWindow, map.getCenter());
                },
                {
                    enableHighAccuracy: true,
                    maximumAge: 10000,
                    timeout: 5000,
                }
            );
        } else {
            // Browser doesn't support Geolocation
            handleLocationError(false, infoWindow, map.getCenter());
        }
    });

}

function addMarker(object) {
    const obj = JSON.parse(object);
    const myLatlng = new google.maps.LatLng(obj.Lat, obj.Lon);
    

    var iconBase = 'https://maps.google.com/mapfiles/kml/shapes/';
    var icon = 'cegeka-academy-map-marker.png';

    var marker = new google.maps.Marker({
        position: myLatlng,
        map,
        title: object.titleMarker,
        icon: icon
    });
    console.log(marker);
    markers.push(marker)
    const contentString =
        '<div id="content">' +
        '<div id="siteNotice">' +
        "</div>" +
        '<h1 id="firstHeading" class="firstHeading">' + obj.Title+  '</h1>' +
        '<div id="bodyContent">' +
        "<img src='" + obj.Images[0] + "' alt='no image found' id='hp' style='float: right; margin: 0 0 0 15px;width:200px;height:170px' />"+
        "<p><b>Description:</b> " + obj.Description +  
        "<p><b>Starting price:</b> " + obj.StartingBidAmount + "</p>" +
        "<p><b>Current price:</b> " + obj.CurrentBidAmount + "</p>" +
        "<p><b>Price for buying now:</b> " + obj.BuyItNowPrice + "</p>" +
        "<p><b>Link to auction:</b>" + "<a href='https://localhost:44376/auctions/" + parseInt(obj.Id) +"/view'> Link </a></p>" +
        "</div>" +
        "</div>";
    const infowindow = new google.maps.InfoWindow({
        content: contentString,
        position: myLatlng
    });
    marker.addListener("click", () => {
        infowindow.open({
            anchor: marker,
            map,
        });
    });
} 
function resetMap() {
    setMapOnAll(null);
    markers = [];
}



