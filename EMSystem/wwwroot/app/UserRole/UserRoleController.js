mainApp.controller("UserRoleController", ['$scope', '$http', '$location', '$routeParams', function ($scope, $http, $location, $routeParams) {
    $scope.ListOfUserRole;
    $scope.Status;

    $scope.itemsPerPage = 5;
    $scope.currentPage = 0;

    $scope.validationUserRole = /^[a-zA-Z]{1,12}$/;

    $scope.Back = function () {
        $location.path('/home');
    }

    //Get all user role and bind with html table
    $http.get("api/UserRole/GetAllUserRole").success(function (data) {
        $scope.ListOfUserRole = data;

    })
        .error(function (data) {
            $scope.Status = "data not found";
        });


    //Pagination
    $scope.pageCount = function () {

        return Math.ceil($scope.ListOfUserRole.length / $scope.itemsPerPage);

    };



    $scope.Close = function () {
        $location.path('/userRole');
    }
    //add new user role
    $scope.Add = function () {
        var userRoleData = {
            RoleType: $scope.RoleType
        };
        debugger;
        $http.post("api/UserRole/InsertUserRole", userRoleData).success(function (data) {
            $location.path('/userRole');
            toastr.success('User-Role Added!', 'Operation Successful')
        }).error(function (data) {
            console.log(data);
            $scope.error = "Failed To Add" + data.ExceptionMessage;
            toastr.error('Please try again !', 'Unsuccessful!')
        });
    };

    //fill the user role records for update
    if ($routeParams.id) {
        var params = { roleId: $routeParams.id }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
        };

        debugger;
        $http.get('api/UserRole/GetUserRole', config).success(function (data) {
            $scope.RoleID = data.roleID;
            $scope.RoleType = data.roleType;
        });
    }

    //update the user role records

    $scope.Update = function () {

        var userRoleData = {
            RoleID: $scope.RoleID,
            RoleType: $scope.RoleType,
        };
        var config = {
            headers: { 'Accept': 'application/json' }
        };
        if ($scope.RoleID > 0) {
            //debugger;
            $http.put("api/UserRole/UpdateUserRole", userRoleData, config).success(function (data) {
                $location.path('/userRole');
                toastr.success('User-Role Updated!', 'Operation Successful')
            }).error(function (data) {
                console.log(data);
                $scope.error = "Update FAILED" + data.ExceptionMessage;
                toastr.error('Please try again !', 'Unsuccessful!')
            });
        }
    }


    //delete the selected user role form the list
    $scope.Delete = function () {
        var params = { RoleID: $scope.RoleID }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
        };
        if ($scope.RoleID > 0) {
            debugger;
            $http.delete("api/UserRole/DeleteUserRole", config).success(function (data) {
                $location.path('/userRole');
                toastr.success('User-Role Deleted!', 'Operation Successful')
            }).error(function (data) {
                console.log(data);
                $scope.error = "Deletion FAILED" + data.ExceptionMessage;
                toastr.error('Please try again !', 'Unsuccessful!')
            });
        }
    }

}]);