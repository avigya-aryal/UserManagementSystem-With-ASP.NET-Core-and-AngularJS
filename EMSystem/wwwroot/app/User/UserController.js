mainApp.controller("UserController", ['$scope', '$http', '$location', '$routeParams', function ($scope, $http, $location, $routeParams) {
    $scope.ListOfUser = [];
    $scope.Status;
    $scope.EmployeeList = [];
    $scope.UpdateEmployeeList = [];
    $scope.RoleList = [];

    $scope.itemsPerPage = 5;
    $scope.currentPage = 0;


    $scope.searchtxt = []
    $scope.searchtxt.status = true;

    $scope.Back = function () {
        $location.path('/home');
    }

    //For checkbox status value
    $scope.check = 0;
    $scope.Status = ($scope.check === 1) ? true : false;

    //Get all user and bind with html table
    $http.get("api/User/GetAllUser").success(function (data) {
        $scope.ListOfUser = data;

    })
        .error(function (data) {
            $scope.error = "data not found";
        });


    //Pagination
    $scope.pageCount = function () {

        return Math.ceil($scope.ListOfUser.length / $scope.itemsPerPage);

    };

    $scope.Close = function () {
        $location.path('/user');
    }
    //add new user
    $scope.Add = function () {
        var userData = {
            EmployeeID: parseInt($scope.EmployeeID),
            Role: $scope.Role,
            Status: $scope.Status

        };
        debugger;
        $http.post("api/User/InsertUser", userData).success(function (data) {
            $location.path('/user');
            toastr.success('User Added !!', 'Successful')
        }).error(function (data) {
            console.log(data);
            $scope.error = "Failed To Add" + data.ExceptionMessage;
            toastr.error('Please try again !', 'Unsuccessful!')
        });
    };

    //fill the user records for update
    if ($routeParams.id) {
        var params = { userId: $routeParams.id }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
        };

        debugger;
        $http.get('api/User/GetUser', config).success(function (data) {
            $scope.UserID = data.userID;
            $scope.EmployeeID = data.employeeID;
            $scope.Role = data.role;
            $scope.Status = data.status;
        });
    }

    //update the user records

    $scope.Update = function () {

        var userData = {
            UserID: $scope.UserID,
            EmployeeID: $scope.EmployeeID,
            Role: $scope.Role,
            Status: $scope.Status
        };
        var config = {
            headers: { 'Accept': 'application/json' }
        };
        if ($scope.UserID > 0) {
            //debugger;
            $http.put("api/User/UpdateUser", userData, config).success(function (data) {
                $location.path('/user');
                toastr.success('User Updated!', 'Operation Successful')
            }).error(function (data) {
                console.log(data);
                $scope.error = "Update FAILED" + data.ExceptionMessage;
                toastr.error('Please try again !', 'Unsuccessful!')
            });
        }
    }

    //delete the selected user form the list
    $scope.Delete = function () {
        var params = { UserID: $scope.UserID }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
        };
        if ($scope.UserID > 0) {
            debugger;
            $http.delete("api/User/DeleteUser", config).success(function (data) {
                $location.path('/user');
                toastr.success('User Deleted!', 'Operation Successful')
            }).error(function (data) {
                console.log(data);
                $scope.error = "Deletion FAILED" + data.ExceptionMessage;
                toastr.error('Please try again !', 'Unsuccessful!')
            });
        }
    }

    $http.get("api/Employee/GetAllEmployeeForUser").success(function (data) {
        $scope.EmployeeList = data;

    })
        .error(function (data) {
            $scope.error = "data not found";
        });

    $http.get("api/Employee/GetAEmployeeForUpdateView").success(function (data) {
        $scope.UpdateEmployeeList = data;

    })
        .error(function (data) {
            $scope.error = "data not found";
        });

    $http.get("api/UserRole/GetAllUserRoleForUser").success(function (data) {
        $scope.RoleList = data;

    })
        .error(function (data) {
            $scope.error = "data not found";
        });
}]);