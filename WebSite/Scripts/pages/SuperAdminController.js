angular.module('challenge.superAdmin', ['ngAnimate', 'ui.bootstrap']);
angular.module('challenge.superAdmin').controller('UsersCtrl', function ($scope, $http) {
    $scope.users = [];
    $scope.user = null;

    $scope.addItem = function () {
        var newItemNo = $scope.items.length + 1;
        $scope.items.push('Item ' + newItemNo);
    };

    $scope.refresh = function () {
        $http.get('/api/superadminservices/getallusers').
    then(function (response) {
        $scope.users = response.data;
    }, function (response) {
        alert("Error:" + response);
    });
    };

    $scope.create = function (user) {
        $http.post('/api/superadminservices/createuser', user)
            .then(function (response) {
                $scope.refresh();
            }, function (response) {
                alert(response.statusText);
            });
    };

    $scope.delete = function (id) {
        $http.delete('/api/superadminservices/deleteUser/' + id)
            .then(function (response) {
                $scope.refresh();
            }, function(response){
                alert(response.statusText);
            });
    };

    $scope.refresh();
});