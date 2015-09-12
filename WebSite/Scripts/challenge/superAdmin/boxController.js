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
                url: 'https://6weekschallenge-dev.s3.amazonaws.com/',
                fields: {
                    key: 'images/' + file.name, // the key to store the file on S3, could be file name or customized
                    AWSAccessKeyId: 'AKIAJDM4MVKTULA4W6ZQ',
                    acl: 'public', // sets the access to the uploaded file in the bucket: private or public
                    policy: 'ew0KICAiZXhwaXJhdGlvbiI6ICIyMDE2LTA5LTA5VDIzOjM2OjI1WiIsDQogICJjb25kaXRpb25zIjogWw0KICAgIHsiYnVja2V0IjogIjZ3ZWVrc2NoYWxsZW5nZS1kZXYifSwNCiAgICBbInN0YXJ0cy13aXRoIiwgIiRrZXkiLCAiaW1hZ2VzLyJdLA0KICAgIHsiYWNsIjogInByaXZhdGUifSwNCiAgICBbInN0YXJ0cy13aXRoIiwgIiRDb250ZW50LVR5cGUiLCAiaW1hZ2UvIl0sDQogICAgWyJzdGFydHMtd2l0aCIsICIkZmlsZW5hbWUiLCAiIl0sDQogICAgWyJjb250ZW50LWxlbmd0aC1yYW5nZSIsIDAsIDEwNDg1NzYwXQ0KICBdDQp9', // base64-encoded json policy (see article below)
                    signature: 'QebJtKkhLatYypvMLEyjKrusIMA=', // base64-encoded signature based on policy string (see article below)
                    "Content-Type": file.type != '' ? file.type : 'application/octet-stream', // content type of the file (NotEmpty)
                    filename: file.name // this is needed for Flash polyfill IE8-9
                },
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