(function () {
    'use strict';

    angular
        .module('6weekschallenge.core')
        .factory('utilsService', utilsService);

    function utilsService($http) {
        return {
            getNormalizeFileName: function (file) {
                return $http.get('/api/utils/getnormalizefilename', { params: { fileName: file.name } })
                .then(function (response) {
                    return { file: file, normalizedName: response.data };
                });
            }
        };
    };


})();