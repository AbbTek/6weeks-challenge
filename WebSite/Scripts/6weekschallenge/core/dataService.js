(function() {
    'use strict';

    angular
        .module('6weekschallenge.core')
        .factory('superAdminService', superAdminService);

    /* @ngInject */
    function superAdminService($http) {

        return {
            getAllBoxes: function (projections) {
                return $http.get('/api/superadminquery/getallboxes', { params: { projections: projections}})
                    .then(function (response) {
                        return response.data;
                    });
            },

            deleteBox: function (id) {
                return $http.delete('/api/superadmincommand/deletebox/' + id);
            },

            createBox: function (box) {
                return $http.post('/api/superadmincommand/createbox', box);
            },

            updateBox: function (box) {
                return $http.post('/api/superadmincommand/updatebox', box);
            }
        };
    }

})();