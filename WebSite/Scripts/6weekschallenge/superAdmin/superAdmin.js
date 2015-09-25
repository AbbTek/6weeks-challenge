(function () {
    'use strict';

    angular
       .module('6weekschallenge.superAdmin')
       .controller('academyController', academyController)
       .controller('academyModalController', academyModalController);

    function academyController($scope, $modal, superAdminService) {
        $scope.academies = [];
        $scope.academy = {
            _id: null,
            Location: {
                coordinates: [151.20699020000006, -33.8674869]
            }
        };

        $scope.refresh = function () {
            superAdminService.getAllAcademies(
                {
                    Name: 1,
                    Address: 1,
                    State: 1,
                    EmailManager: 1,
                    UrlLogo: 1,
                    Location: 1,
                    ShortName: 1
                }).then(function (data) {
                    $scope.academies = data;
                });
        };

        $scope.delete = function (id) {
            superAdminService.deleteAcademy(id).then(function () {
                $scope.refresh();
            });
        };

        $scope.edit = function (academy) {
            $scope.academy = academy;
            $scope.open();
        };

        $scope.activate = function (id) {
            superAdminService.activateAcademy(id).then(function () {
                $scope.refresh();
            });
        };

        $scope.new = function () {
            $scope.academy._id = null;
            $scope.academy.Name = "";
            $scope.academy.Address = "";
            $scope.academy.Active = false;
            $scope.academy.EmailManager = "";
            $scope.academy.ShortName = "";
            $scope.academy.UrlLogo = "";

            $scope.open();
        };

        $scope.open = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'myModalContent.html',
                controller: 'academyModalController',
                size: 'lg',
                resolve: {
                    academy: function () {
                        return $scope.academy;
                    }
                }
            });

            modalInstance.result.then(function () {
                $scope.refresh();
            }, function () {
                $scope.refresh();
            });
        };

        $scope.refresh();
    }

    function academyModalController($scope, $modalInstance, uiGmapIsReady, superAdminService, uploadService, academy) {
        $scope.academy = academy;

        $scope.map = {
            center: {
                latitude: $scope.academy.Location.coordinates[1],
                longitude: $scope.academy.Location.coordinates[0]
            },
            zoom: 15
        };

        $scope.marker = {
            id: 0,
            coords: {
                latitude: $scope.academy.Location.coordinates[1],
                longitude: $scope.academy.Location.coordinates[0]
            }
        };

        $scope.searchbox = {
            template: 'searchbox.tpl.html',
            events: {
                places_changed: function (searchBox) {
                    var places = searchBox.getPlaces();
                    if (places.length == 0)
                        return;
                    var place = places[0];

                    $scope.academy.Address = place.formatted_address;

                    $scope.academy.Location = {
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

        $scope.uploadProgress = 0;

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        $scope.upload = function (file) {

            uploadService.getNormalizedParamsImage(file)
            .then(function (param) {
                uploadService.imageUpload(param)
                .progress(function (evt) {
                    var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                    $scope.uploadProgress = progressPercentage;
                }).success(function (data, status, headers, config) {
                    $scope.academy.UrlLogo = param.urlImage;
                    $scope.uploadProgress = 0;
                }).error(function (data, status, headers, config) {
                    console.log('error status: ' + status);
                });
            });
        };

        $scope.submitForm = function (academy) {
            if ($scope.academyForm.$valid) {
                if (academy._id != null) {
                    superAdminService.updateAcademy(academy)
                    .then(function () {
                        $modalInstance.close();
                    });
                } else {
                    superAdminService.createAcademy(academy)
                    .then(function () {
                        $modalInstance.close();
                    });
                }

            }
        };

        uiGmapIsReady.promise()
            .then(function (map_instances) {
                var map = map_instances[0].map;
                var currCenter = map.getCenter();
                google.maps.event.trigger(map, 'resize');
                map.setCenter(currCenter);
            });
    }
})();