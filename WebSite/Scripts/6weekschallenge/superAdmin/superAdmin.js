﻿(function () {
    'use strict';

    angular
       .module('6weekschallenge.superAdmin')
       .controller('boxController', boxController)
       .controller('boxModalController', boxModalController);

    function boxController($scope, $modal, superAdminService) {
        $scope.boxes = [];
        $scope.box = {
            _id: null,
            Location: {
                coordinates: [151.20699020000006, -33.8674869]
            }
        };

        $scope.refresh = function () {
            superAdminService.getAllBoxes(
                {
                    Name: 1,
                    Address: 1,
                    Active: 1,
                    EmailManager: 1,
                    UrlLogo: 1,
                    Location: 1,
                    ShortName: 1
                }).then(function (data) {
                    $scope.boxes = data;
                });
        };

        $scope.delete = function (id) {
            superAdminService.deleteBox(id).then(function () {
                $scope.refresh();
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
                $scope.refresh();
            }, function () {
                $scope.refresh();
            });
        };

        $scope.refresh();
    }

    function boxModalController($scope, $modalInstance, uiGmapIsReady, superAdminService, uploadService, box) {
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
                    var places = searchBox.getPlaces();
                    if (places.length == 0)
                        return;
                    var place = places[0];

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
                    $scope.box.UrlLogo = param.urlImage;
                    $scope.uploadProgress = 0;
                }).error(function (data, status, headers, config) {
                    console.log('error status: ' + status);
                });
            });
        };

        $scope.submitForm = function (box) {
            if ($scope.boxForm.$valid) {
                if (box._id != null) {
                    superAdminService.updateBox(box)
                    .then(function () {
                        $modalInstance.close();
                    });
                } else {
                    superAdminService.createBox(box)
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