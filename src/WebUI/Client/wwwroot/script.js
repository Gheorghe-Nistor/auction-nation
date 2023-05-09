var mapOptions = null;
var map = null;
var infoWindow = null;
var markers = [];

function initialize(coords) {
    mapOptions = {
        center: new google.maps.LatLng(35.68484978380786, -86.60681036623693),
        zoom: 7,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    infoWindow = new google.maps.InfoWindow();
    map = new google.maps.Map(document.getElementById("map"), mapOptions);
}

function addpins(name, lat, lng, desc) {
    var data = { name, lat, lng, desc };
    var myLatLng = new google.maps.LatLng(data.lat, data.lng);
    var marker = new google.maps.Marker({
        position: myLatLng,
        map: map,
        title: data.name
    });
    markers.push(marker);
    (function (marker, data) {
        google.maps.event.addListener(marker, "click", function (e) {
            infoWindow.setContent(data.desc);
            infoWindow.open(map, marker);
        });
    })(marker, data);
    (function bindInfoWindow(marker, map, infoWindow) {
        google.maps.event.addListener(marker, 'click', function () {
            Array.from(document.querySelector("#test").options).forEach(function (option_element) {
                let option_text = option_element.text;

                if (option_text === marker.title) {
                    option_element.selected = true;
                }
            });
        });
    })(marker, map, infoWindow);
}

function clickPin(txt) {
    markers.forEach(function (m) {
        let val = m.title;
        if (txt === val) {
            google.maps.event.trigger(m, 'click');
        }
    });
}






/*
function initialize() {
    var latlng = new google.maps.LatLng(40.716948, -74.003563);
    var options = {
        zoom: 14, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("map"), options);
    var myCenter = { lat: -25.344, lng: 131.031 };
    var marker = new google.maps.Marker({
        position: myCenter
    });
    marker.setMap(map);
} */


