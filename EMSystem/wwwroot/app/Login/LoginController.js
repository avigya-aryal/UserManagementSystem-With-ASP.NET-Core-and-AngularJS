mainApp.controller("LoginController", ['$scope', '$http', '$location', '$routeParams', function ($scope, $http, $location, $routeParams) {

    $scope.Login = function () {
        var loginData = {
            UserName: $scope.UserName,
            Password: $scope.Password
        };
        debugger;
        $http.post("api/Login", loginData).success(function (data) {
            $location.path('/home');
            toastr.success('LoggedIn', 'Successfully')
        }).error(function (data) {
            console.log(data);
            $scope.error = "Login Failed" + data.ExceptionMessage;
            toastr.error('Incorrect Password or UserName', 'Try Again!')
        });
    };
}]);