mainApp.controller("DepartmentController", ['$scope', '$http', '$location', '$routeParams', function ($scope, $http, $location, $routeParams) {
    $scope.ListOfDepartment;
    $scope.Status;
    $scope.itemsPerPage = 5;
    $scope.currentPage = 0;
    $scope.Back = function () {
        $location.path('/home');
    }

    //Get all department and bind with html table
    $http.get("api/Department/GetAllDepartment").success(function (data) {
        $scope.ListOfDepartment = data;

    })
        .error(function (data) {
            $scope.Status = "data not found";
        });

    //Pagination
    $scope.pageCount = function () {

        return Math.ceil($scope.ListOfDepartment.length / $scope.itemsPerPage);

    };



    //Add new department

    $scope.Close = function () {
        $location.path('/department');
    }
    $scope.Add = function () {
        var departmentData = {
            DepartmentName: $scope.DepartmentName
            
        };
        debugger;
        $http.post("api/Department/InsertDepartment", departmentData).success(function (data) {
            $location.path('/department');
            toastr.success('Added Successfully')
        }).error(function (data) {
            console.log(data);
            $scope.error = "Something wrong when adding new department" + data.ExceptionMessage;
            toastr.error('Unable to add')
        });
    }

    //Fill the department records for update

    if ($routeParams.id) {
        var params = { departmentId: $routeParams.id }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
        };

        //Backend view model
        debugger;
        $http.get('api/Department/GetDepartment', config).success(function (data) {
            $scope.DepartmentID = data.departmentID;
            $scope.DepartmentName = data.departmentName;
           
        });

    }

    //Update the department records
    $scope.Update = function () {

        var departmentData = {
            DepartmentID: $scope.DepartmentID,
            DepartmentName: $scope.DepartmentName,
            
        };
        var config = {
            headers: { 'Accept': 'application/json' }
        };
        if ($scope.DepartmentID > 0) {
            //debugger;
            $http.put("api/Department/UpdateDepartment", departmentData, config).success(function (data) {
                $location.path('/department');
                toastr.error('Updated Successfully')
            }).error(function (data) {
                console.log(data);
                $scope.error = "Something wrong when adding updating department " + data.ExceptionMessage;
                toastr.error('Failed to update')
            });
        }
    }

    $scope.Delete = function () {
        var params = { DepartmentID: $scope.DepartmentID }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
        };
        if ($scope.DepartmentID > 0) {
            debugger;
            $http.delete("api/Department/DeleteDepartment", config).success(function (data) {
                $location.path('/department');
                toastr.success('Deleted Successfully')
            }).error(function (data) {
                console.log(data);
                $scope.error = "Deletion FAILED" + data.ExceptionMessage;
                toastr.error('Failed to delete')
            });
        }
    }



}]);





