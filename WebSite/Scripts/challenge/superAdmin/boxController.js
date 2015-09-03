challengeApp.controller('boxController', function ($scope, $http, $modal) {
    $scope.boxes = [];
    $scope.box = {
        _id: null,
        Location: {
            coordinates: [151.20699020000006, -33.8674869]
        }
    };

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

    $scope.new = function () {
        $scope.box._id = null;
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
    function ($scope, $http, $modalInstance, uiGmapIsReady, Upload, box) {

        $scope.box = box;

        $scope.map = {
            center: {
                latitude: $scope.box.Location.coordinates[1],
                longitude: $scope.box.Location.coordinates[0]
            },
            zoom: 15
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

                    $scope.box.Location = {
                        coordinates: [place.geometry.location.lng(), place.geometry.location.lat()]
                    }

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
                }
            },
            parentdiv: 'divTextMap'
        };

        $scope.upload = function (file) {
            Upload.upload({
                url: '/fileupload',
                fields: { },
                file: file
            }).progress(function (evt) {
                var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + progressPercentage + '% ' + evt.config.file.name);
            }).success(function (data, status, headers, config) {
                console.log('file ' + config.file.name + 'uploaded. Response: ' + data);
                $scope.box.UrlLogo = data;
            }).error(function (data, status, headers, config) {
                console.log('error status: ' + status);
            })
        };

        $scope.submitForm = function (box) {
            if ($scope.boxForm.$valid) {
                $http.post('/api/superadmincommand/createorupdatebox', box)
            .then(function (response) {
                $modalInstance.close();
            }, function (response) {
                alert(response.statusText);
            });
            }
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        uiGmapIsReady.promise()
            .then(function (map_instances) {
                var map = map_instances[0].map;
                var currCenter = map.getCenter();
                google.maps.event.trigger(map, 'resize');
                map.setCenter(currCenter);
            });
    });