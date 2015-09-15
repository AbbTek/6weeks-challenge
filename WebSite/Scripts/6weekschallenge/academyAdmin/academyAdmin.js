(function () {
	'use strict';

	angular
		.module('6weekschallenge.academyAdmin')
        .controller('loginController', loginController);

	function loginController($scope, academyAdminService) {
	    $scope.login = {};

	    $scope.submitForm = function (login) {
	        academyAdminService.login(login)
            .then(function (response) {

            });
	    };
	};
})();