(function () {
    'use strict';

    angular
        .module('6weekschallenge.core')
        .factory('superAdminService', superAdminService)
        .factory('academyAdminService', academyAdminService);

    function superAdminService($http) {

        return {
            getAllAcademies: function (projections) {
                return $http.get('/api/superadminquery/getallacademies', { params: { projections: projections } })
                    .then(function (response) {
                        return response.data;
                    });
            },

            deleteAcademy: function (id) {
                return $http.delete('/api/superadmincommand/deleteacademy/' + id);
            },

            createAcademy: function (academy) {
                return $http.post('/api/superadmincommand/createacademy', academy);
            },

            updateAcademy: function (academy) {
                return $http.post('/api/superadmincommand/updateacademy', academy);
            }
        };
    }

    function academyAdminService($http) {
        return {
            login: function (login) {
                return $http.post('/api/academyadmincommand/login', login);
            }
        };
    };

})();