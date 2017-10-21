

var apiKey = 'AIzaSyA0TauAkhQMOI-v0zlRgWsquI73VjB-3C8';
var closeZoom = 15;
var farZoom = 6;
var selectedClass = 'text-light'
var selectedHeader = 'bg-success';
var mapText = 'Our Locations';

function geocodeAddress(geocoder, resultsMap, address) {
    //var address = document.getElementById('address').value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === 'OK') {
            resultsMap.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: resultsMap,
                position: results[0].geometry.location
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 10,
        center: { lat: -34.397, lng: 150.644 }
    });
    var geocoder = new google.maps.Geocoder();


    var address = $('.information .address');
    var last;
    $.each(address, function (i) {
        last = $(this);
        geocodeAddress(geocoder, map, $(this).text());
    });
    map.setZoom(farZoom);


    $(document).on('click', '.storeLocation', function (element) {
        var address = $(this).find('.address');

        geocodeAddress(geocoder, map, address.text());
        highlightMapMarker(address, map);
    });
}

function highlightMapMarker(object, map) {
    var mapMarker = $(object).parent().parent().find('.location');

    if ($(mapMarker).hasClass(selectedClass)) {
        $('.mapText').html(mapText);
        $(mapMarker).parent().removeClass(selectedHeader);
        $(mapMarker).removeClass(selectedClass);
        map.setZoom(farZoom);
    }
    else {
        $('.location.' + selectedClass).parent().removeClass(selectedHeader);
        $('.location.' + selectedClass).removeClass(selectedClass);
        $('.mapText').html(mapMarker.html() + ' Location');
        $(mapMarker).addClass(selectedClass);
        $(mapMarker).parent().addClass(selectedHeader);
        map.setZoom(closeZoom);
    }

}

