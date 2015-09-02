challengeApp.controller('userController', function ($scope, $http) {
    $scope.users = [];
    $scope.user = null;

    $scope.refresh = function () {
        $http.get('/api/superadminquery/getallusers',
            { params: { projections: { Email: 1, EmailConfirmed: 1, Roles: 1 } } }).
    then(function (response) {
        $scope.users = response.data;
    }, function (response) {
        alert("Error:" + response);
    });
    };

    $scope.create = function (user) {
        $http.post('/api/superadmincommand/createuser', user)
            .then(function (response) {
                $scope.refresh();
                $scope.user = null;
            }, function (response) {
                alert(response.statusText);
            });
    };

    $scope.delete = function (id) {
        $http.delete('/api/superadmincommand/deleteUser/' + id)
            .then(function (response) {
                $scope.refresh();
            }, function (response) {
                alert(response.statusText);
            });
    };

    $scope.refresh();
});
