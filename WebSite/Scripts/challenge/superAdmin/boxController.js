challengeApp.controller('boxController', function ($scope, $http, $modal) {
    $scope.boxes = [];

    $scope.refresh = function () {
        $http.get('/api/superadminquery/getallboxes',
            { params: { projections: {} } }).
    then(function (response) {
        $scope.boxes = response.data;
    }, function (response) {
        alert("Error:" + response);
    });
    };

    $scope.delete = function (id) {
        $http.delete('/api/superadmincommand/deleteBox/' + id)
    .then(function (response) {
        $scope.refresh();
    }, function (response) {
        alert(response.statusText);
    });
    };

    $scope.edit = function (box) {
        $scope.box = box;
        $scope.open();
    };

    $scope.open = function () {
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: 'myModalContent.html',
            controller: 'boxModalController',
            size: 'lg',
            resolve: {
                box: function () {
                    return $scope.box;
                }
            }
        });

        modalInstance.result.then(function () {
            //$scope.box = null;
            $scope.refresh();
        }, function () {
            //$log.info('Modal dismissed at: ' + new Date());
            $scope.refresh();
        });
    };

    $scope.refresh();
});
challengeApp.controller('boxModalController',
    function ($scope, $http, $modalInstance, uiGmapIsReady, box) {

        $scope.box = box;

        $scope.map = {
            center: {
                latitude: $scope.box.Location.coordinates[1],
                longitude: $scope.box.Location.coordinates[0]
            },
            zoom: 15,
            events: {
                resize: function (maps, eventName, args) {
                    alert('resize');
                }
            }
        };

        $scope.marker = {
            id: 0,
            coords: {
                latitude: $scope.box.Location.coordinates[1],
                longitude: $scope.box.Location.coordinates[0]
            }
        };

        $scope.searchbox = {
            template: 'searchbox.tpl.html',
            events: {
                places_changed: function (searchBox) {
                    places = searchBox.getPlaces()
                    if (places.length == 0)
                        return;
                    place = places[0];

                    $scope.box.Address = place.formatted_address;

                    $scope.map = {
                        center: {
                            latitude: place.geometry.location.lat(),
                            longitude: place.geometry.location.lng()
                        },
                        zoom: 15
                    };

                    $scope.marker = {
                        id: 0,
                        coords: {
                            latitude: place.geometry.location.lat(),
                            longitude: place.geometry.location.lng()
                        }
                    };

                    $scope.box.Location = {
                        coordinates: [place.geometry.location.lng(), place.geometry.location.lat()]
                    }
                }
            },
            parentdiv: 'divTextMap'
        };


        $scope.ok = function (box) {
            $http.post('/api/superadmincommand/createorupdatebox', box)
        .then(function (response) {
            $modalInstance.close();
        }, function (response) {
            alert(response.statusText);
        });
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        uiGmapIsReady.promise()
            .then(function (map_instances) {
                var map = map_instances[0].map;
                google.maps.event.trigger(map, 'resize');
            });
    });