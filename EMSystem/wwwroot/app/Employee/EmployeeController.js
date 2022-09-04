mainApp.controller("EmployeeController", ['$scope', '$http', '$location', '$routeParams', function ($scope, $http, $location, $routeParams) {
    $scope.ListOfEmployee = [];
    $scope.Status;
    $scope.ContactList = [];
    $scope.UpdateContactList = [];

    $scope.searchtxt = []
    $scope.searchtxt.status = true;

    $scope.itemsPerPage = 5;
    $scope.currentPage = 0;

    $scope.validation = /^[a-zA-Z]{1,50}$/;

    $scope.Back = function () {
        $location.path('/home');
    }


    //For checkbox status value
  
    $scope.check = 0;
    $scope.Status = ($scope.check === 1) ? true : false;


    //Get all employee and bind with html table
    $http.get("api/Employee/GetAllEmployee").success(function (data) {
        $scope.ListOfEmployee = data;
    })
        .error(function (data) {
            $scope.Status = "data not found";
        });

    //Pagination
    $scope.pageCount = function () {

        return Math.ceil($scope.ListOfEmployee.length / $scope.itemsPerPage);

    };


    //Add new employee

    $scope.Close = function () {
        $location.path('/employee');
    }
    $scope.Add = function () {
        var employeeData = {
            Designation: $scope.Designation,
            Status: $scope.Status,
            ContactID: parseInt($scope.ContactID),
            JoinedDate: $scope.JoinedDate,
            LeftDate: $scope.LeftDate
        };
        debugger;
        $http.post("api/Employee/InsertEmployee", employeeData).success(function (data) {
            $location.path('/employee');
            
            toastr.success('The given employee has been successfully added !', 'Operation Successful')

        }).error(function (data) {
            console.log(data);
            /*$scope.error = "Something wrong when adding new employee " + data.ExceptionMessage;*/

            toastr.error('Failure to add employee. Please try again !', 'Operation Unsuccessful!')
        });
    }

    //Fill the employee records for update

    if ($routeParams.id) {
        var params = { employeeId: $routeParams.id }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
        };

        //Backend view model
       /* debugger;*/
        $http.get('api/Employee/GetEmployee', config).success(function (data) {
            $scope.EmployeeID = data.employeeID;
            $scope.Designation = data.designation;
            $scope.Status = data.status;
            $scope.ContactID = data.contactID;
            $scope.JoinedDate = data.joinedDate.split("T")[0];
            $scope.LeftDate = data.leftDate;
            $scope.CitizenshipImage = data.citizenshipImage;
            $scope.ImageOwnerUniqueName = data.imageOwnerUniqueName;
        });

    }

    //Update the employee records
    $scope.Update = function () {

        var employeeData = {
            EmployeeID: $scope.EmployeeID,
            Designation: $scope.Designation,
            Status: $scope.Status,
            ContactID: $scope.ContactID,
            JoinedDate: $scope.JoinedDate,
            LeftDate: $scope.LeftDate,
            CitizenshipImage: $scope.CitizenshipImage,
            ImageOwnerUniqueName: $scope.ImageOwnerUniqueName
        };
        var config = {
            headers: { 'Accept': 'application/json' }
        };
        if ($scope.EmployeeID > 0) {
            //debugger;
            $http.put("api/Employee/UpdateEmployee", employeeData,config).success(function (data) {
                $location.path('/employee');
                toastr.success('The given employee has been successfully updated !', 'Operation Successful')

            }).error(function (data) {
                console.log(data);
                $scope.error = "Something wrong when adding updating employee " + data.ExceptionMessage;
                toastr.error('Failure to update employee. Please try again !', 'Operation Unsuccessful!')
            });
        }
    }

    $scope.Delete = function () {
        var params = { EmployeeID: $scope.EmployeeID }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
        };
        if ($scope.EmployeeID > 0) {
            debugger;
            $http.delete("api/Employee/DeleteEmployee", config).success(function (data) {
                $location.path('/employee');
                toastr.success('The given employee has been successfully deleted !', 'Operation Successful')

            }).error(function (data) {
                console.log(data);
                $scope.error = "Deletion FAILED" + data.ExceptionMessage;
                toastr.error('Failure to delete employee. Please try again !', 'Operation Unsuccessful!')
            });
        }
    }


    $http.get("api/Contacts/GetAllContactName").success(function (data) {
        $scope.ContactList = data;

    })
        .error(function (data) {
            $scope.Status = "data not found";

        });


    $http.get("api/Contacts/GetAllContactNameForUpdate").success(function (data) {
        $scope.UpdateContactList = data;

    })
        .error(function (data) {
            $scope.Status = "data not found";

        });
    

    $scope.selectFileforUpload = function (file) {
        $scope.SelectedFileForUpload = file[0];
    }

    $scope.uploadFiles = function () {
        var file = $scope.SelectedFileForUpload;
        var formData = new FormData();

        var fileName = $scope.EmployeeID + "_" + $scope.Designation

        formData.append("file", file, fileName);

        $http.post("api/Employee/UploadFiles", formData,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
            .success(function (d) {
                $scope.CitizenshipImage = d;
                $scope.ImageOwnerUniqueName = fileName;
            })
            .error(function () {

            });

    };
}]);





