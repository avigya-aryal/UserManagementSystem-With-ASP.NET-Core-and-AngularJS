
mainApp.controller("EmployeeScheduleController", ['$scope', '$http', '$location', '$routeParams', function ($scope, $http, $location, $routeParams) {
    $scope.ListOfEmployeeSchedule;
    $scope.Status;
    $scope.itemsPerPage = 5;
    $scope.currentPage = 0;
    $scope.Back = function () {
        $location.path('/home');
    }

    //Get all employeeschedule and bind with html table
    $http.get("api/EmployeeSchedule/GetAllEmployeeSchedule").success(function (data) {
        $scope.ListOfEmployeeSchedule = data;

    })
        .error(function (error) {
            $scope.Status = "data not found";
        });

    //Pagination

    $scope.pageCount = function () {

        return Math.ceil($scope.ListOfEmployeeSchedule.length / $scope.itemsPerPage);

    };

    //Add new employeeschedule

    $scope.Close = function () {
        $location.path('/employeeSchedule');
    }
    $scope.Add = function () {
        var employeeScheduleData = {
            StartHour: $scope.StartHour,
            EndHour: $scope.EndHour
         
            //CreatedBy: $scope.CreatedBy
            

        };
        debugger;
        $http.post("api/EmployeeSchedule/InsertEmployeeSchedule", employeeScheduleData).success(function (data) {
            $location.path('/employeeSchedule');
            toastr.success('Data inserted successfully')
        }).error(function (data) {
            console.log(data);
            toastr.error('Something went wrong....please try again!!!')

        });
    }

    //Fill the employeeschedule records for update

    if ($routeParams.id) {
        var params = { employeescheduleid: $routeParams.id }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
           
        };
       
        debugger;
        $http.get('api/EmployeeSchedule/GetEmployeeSchedule', config).success(function (data) {
            $scope.ScheduleId = data.scheduleId;
            $scope.StartHour = data.startHour;
            $scope.EndHour = data.endHour;
            
        });

    }

    //Update the employeeschedule records
    $scope.Update = function () {
      
        var employeeScheduleData = {
            ScheduleId: $scope.ScheduleId,
            StartHour: $scope.StartHour,
            EndHour: $scope.EndHour
           
        };
        var config = {
            headers: { 'Accept': 'application/json' }
        }

        if ($scope.ScheduleId > 0) {
            debugger;
            $http.put("api/EmployeeSchedule/UpdateEmployeeSchedule", employeeScheduleData,config).success(function (data) {
                $location.path('/employeeSchedule');
                toastr.success('Data updated successfully')
            }).error(function (data) {
                console.log(data);
                toastr.error('Something went wrong....please try again!!!')
            });
        }
    }
    

    //Delete the employeeschedule records
    $scope.Delete = function () {

        var params = { employeescheduleid: $scope.ScheduleId }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }

        };

        if ($scope.ScheduleId > 0) {
            debugger;
            $http.delete("api/EmployeeSchedule/DeleteEmployeeSchedule", config).success(function (data) {
                $location.path('/employeeSchedule');
                toastr.success('Data deleted successfully')
            }).error(function (data) {
                console.log(data);
                $scope.error = "Something wrong when adding updating employee " + data.ExceptionMessage;
                toastr.error('Something went wrong....please try again!!!')
            });
        }
    }
}]);





