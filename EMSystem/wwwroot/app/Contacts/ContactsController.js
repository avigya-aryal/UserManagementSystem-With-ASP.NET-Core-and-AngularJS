mainApp.controller("ContactsController", ['$scope', '$http', '$location', '$routeParams', function ($scope, $http, $location, $routeParams) {
    $scope.ListOfContacts;
    $scope.Status;

    $scope.itemsPerPage = 5;
    $scope.currentPage = 0;
    $scope.Back = function () {
        $location.path('/home');
    }

    //Get all contacts and bind with html table
    $http.get("api/Contacts/GetAllContact").success(function (data) {
        $scope.ListOfContacts = data;

    })
        .error(function (data) {
            $scope.Status = "data not found";
        });

    $scope.pageCount = function () {

        return Math.ceil($scope.ListOfContacts.length / $scope.itemsPerPage);

    };

    //Add new contact

    $scope.Close = function () {
        $location.path('/contacts');
    }

    $scope.Add = function () {
        var contactsData = {

            FirstName: $scope.FirstName,
            MiddleName: $scope.MiddleName,
            LastName: $scope.LastName,
            ContactNumber: $scope.ContactNumber,
            DateOfBirth: $scope.DateOfBirth,
            Gender: $scope.Gender,
            Address: $scope.Address,
            Email: $scope.Email
            

        };
        debugger;
        $http.post("api/Contacts/InsertContact", contactsData).success(function (data) {
            $location.path('/contacts');
            toastr.success('Added successfully')
        }).error(function (data) {
            console.log(data);

            toastr.error('Failed to add')
        });
    }




    //Fill the contacts records for update

    if ($routeParams.id) {
        var params = { contactid: $routeParams.id }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
        };

        debugger;
        $http.get('api/Contacts/GetContact', config).success(function (data) {
            $scope.ContactID = data.contactID;
            $scope.FirstName = data.firstName;
            $scope.MiddleName = data.middleName;
            $scope.LastName = data.lastName;
            $scope.ContactNumber = data.contactNumber;
            $scope.Gender = data.gender;
            $scope.DateOfBirth = data.dateOfBirth.split("T")[0];
            $scope.Address = data.address;
            $scope.Email = data.email;
            $scope.ProfileImageUrl = data.profileImageUrl;
            $scope.ImageName = data.imageName;

        });

    }

    //Update the contacts records
    $scope.Update = function () {

        var contactsData = {
            ContactID: $scope.ContactID,
            FirstName: $scope.FirstName,
            MiddleName: $scope.MiddleName,
            LastName: $scope.LastName,
            ContactNumber: $scope.ContactNumber,
            DateOfBirth: $scope.DateOfBirth,
            Gender: $scope.Gender,
            Address: $scope.Address,
            Email: $scope.Email,
            ProfileImageUrl: $scope.ProfileImageUrl,
            ImageName: $scope.ImageName
        };

        var config = {
            headers: { 'Accept': 'application/json' }
        }

        if ($scope.ContactID > 0) {
            debugger;
            $http.put("api/Contacts/UpdateContact", contactsData, config).success(function (data) {
                $location.path('/contacts');
                toastr.success('Updated successfully')
            }).error(function (data) {
                console.log(data);
                $scope.error = "Something wrong when adding updating contacts " + data.ExceptionMessage;
                toastr.error('Failed to update')
            });
        }
    }






    $scope.Delete = function () {
        var params = { ContactID: $scope.ContactID }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }
        };
        if ($scope.ContactID > 0) {
            debugger;
            $http.delete("api/Contacts/DeleteContact", config).success(function (data) {
                $location.path('/contacts');
                toastr.success('Deleted')
            }).error(function (data) {
                console.log(data);

                toastr.error('Failed to delete')
            });
        }
    }

   
    $scope.selectFileforUpload = function (file) {
        $scope.SelectedFileForUpload = file[0];
    }
     //NOW UPLOAD THE FILES.
   
    $scope.uploadFiles = function () {
        var file = $scope.SelectedFileForUpload;
        var formData = new FormData();
      
            var fileName = $scope.ContactNumber + "_" + $scope.FirstName + "_" + $scope.LastName 

            formData.append("file", file, fileName);
            //We can send more data to server using append         
            //formData.append("description", description);

            //var defer = $q.defer();
            $http.post("api/Contacts/UploadFiles", formData,
                {
                    withCredentials: true,
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                })
                .success(function (d) {
                    $scope.ProfileImageUrl = d;
                    $scope.ImageName = fileName;
                })
                .error(function () {

                });
        
    };

}]);

