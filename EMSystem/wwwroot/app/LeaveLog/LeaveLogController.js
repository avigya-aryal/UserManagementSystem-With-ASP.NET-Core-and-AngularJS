mainApp.controller("LeaveLogController", ['$scope', '$http', '$location', '$routeParams', function ($scope, $http, $location, $routeParams) {
    $scope.ListOfLeaveLog = [];
    $scope.Status;
    $scope.EmployeeList = [];

    $scope.itemsPerPage = 5;
    $scope.currentPage = 0;

    $scope.Back = function () {
        $location.path('/home');
    }

    //Get all employee and bind with html table
    $http.get("api/LeaveLog/GetAllLeaveLog").success(function (data) {
        $scope.ListOfLeaveLog = data;

    })
        .error(function (data) {
            $scope.Status = "data not found";
        });

    //Pagination
    $scope.pageCount = function () {

        return Math.ceil($scope.ListOfLeaveLog.length / $scope.itemsPerPage);

    };



    $scope.Close = function () {
        $location.path('/leavelog');
    }

    $scope.Add = function () {
        var leavelogData = {
            StartDate: $scope.StartDate,
            EndDate: $scope.EndDate,
            LeaveType: $scope.LeaveType,
            LeaveReason: $scope.LeaveReason,
            EmployeeID: parseInt($scope.EmployeeID),
            LeaveStatus: $scope.LeaveStatus,
            ApprovedBy: $scope.ApprovedBy,
            ApprovedDate: $scope.ApprovedDate
        };
        debugger;
        $http.post("api/LeaveLog/InsertLeaveLog", leavelogData).success(function (data) {
            $location.path('/leavelog');
            toastr.success('The given leave log has been successfully added !', 'Operation Successful')

        }).error(function (data) {
            console.log(data);
            $scope.error = "Something wrong when adding new leavelog " + data.ExceptionMessage;
            toastr.error('Failure to add leave log. Please try again !', 'Operation Unsuccessful!')

        });
    }

    //Fill the employee records for update

    if ($routeParams.id) {
        var params = { leaveId: $routeParams.id }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
        };

        //Backend view model
        //debugger;
        $http.get('api/LeaveLog/GetLeaveLog', config).success(function (data) {
            $scope.LeaveLogID = data.leaveLogID;
            $scope.StartDate = data.startDate;
            $scope.EndDate = data.endDate;
            $scope.LeaveType = data.leaveType;
            $scope.LeaveReason = data.leaveReason;
            $scope.EmployeeID = data.employeeID;
            $scope.LeaveStatus = data.leaveStatus;
            $scope.ApprovedBy = data.approvedBy;
            $scope.ApprovedDate = data.approvedDate;
        });

    }

   
    $scope.Update = function () {

        var leavelogData = {
            LeaveLogID: $scope.LeaveLogID,
            StartDate: $scope.StartDate,
            EndDate: $scope.EndDate,
            LeaveType: $scope.LeaveType,
            LeaveReason: $scope.LeaveReason,
            EmployeeID: $scope.EmployeeID,
            LeaveStatus: $scope.LeaveStatus,
            ApprovedBy: $scope.ApprovedBy,
            ApprovedDate: $scope.ApprovedDate
        };
        var config = {
            headers: { 'Accept': 'application/json' }
        };
        if ($scope.LeaveLogID > 0) {
            debugger;
            $http.put("api/LeaveLog/UpdateLeaveLog", leavelogData,config).success(function (data) {
                $location.path('/leavelog');
                toastr.success('The given leave log has been successfully updated !', 'Operation Successful')

            }).error(function (data) {
                console.log(data);
                $scope.error = "Something wrong when adding updating leavelog " + data.ExceptionMessage;
                toastr.error('Failure to update leave log. Please try again !', 'Operation Unsuccessful!')

            });
        }
    }

    $scope.Delete = function () {
        var params = { LeaveLogID: $scope.LeaveLogID }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
        };
        if ($scope.LeaveLogID > 0) {
            debugger;
            $http.delete("api/LeaveLog/DeleteLeaveLog", config).success(function (data) {
                $location.path('/leavelog');
                toastr.success('The given leave log has been successfully deleted !', 'Operation Successful')

            }).error(function (data) {
                console.log(data);
                $scope.error = "Deletion FAILED" + data.ExceptionMessage;
                toastr.error('Failure to delete leave log. Please try again !', 'Operation Unsuccessful!')
            });
        }
    }

    $http.get("api/Employee/GetAllEmployeeName").success(function (data) {
        $scope.EmployeeList = data;

    })
        .error(function (data) {
            $scope.Status = "data not found";
        });

}]);





