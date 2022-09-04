
mainApp.service("userService", function ($http)
{
    var apiUrl = "https://localhost:44389/api/User"
    
    //function GetAllUser() {
    //    $http.get("api/User/GetAllUser").success(function (data) {
    //        $scope.ListOfUser = data;

    //    })
    //        .error(function (data) {
    //            $scope.error = "data not found";
    //        });
    //}

    this.GetAllUser = function () {
        return $http(
        {
                url: apiUrl + '/GetAllUser',
            method: 'GET',
        })
    }
});
