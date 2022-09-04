
mainApp.controller("ProjectController", ['$scope', '$filter', '$http', '$location', '$routeParams', function ($scope, $filter, $http, $location, $routeParams) {
    ManagerList = [];

    $scope.itemsPerPage = 5;
    $scope.currentPage = 0;

    $scope.searchtxt = [];
    $scope.searchtxt.status = true;
    $scope.ListOfProject;
    $scope.Status;
    $scope.message = "Inserted succesfully";

    $scope.Back = function () {
        $location.path('/home');
    }

    //For checkbox status value
    $scope.check = 0;
    $scope.status = ($scope.check === 1) ? true : false;



    //$scope.vegetables = ['Corn', 'Onions', 'Kale', 'Arugula', 'Peas', 'Zucchini'];
    //$scope.searchTerm = '';
    //$scope.clearSearchTerm = function () {
    //    $scope.searchTerm = '';
    //};
    //// The md-select directive eats keydown events for some quick select
    //// logic. Since we have a search input here, we don't need that logic.
    //$element.find('input').on('keydown', function (ev) {
    //    ev.stopPropagation();
    //});

   


    //Get all project and bind with html table
    $http.get("api/Project/GetAllProject").success(function (data) {
        $scope.ListOfProject = data;

    })
        .error(function (error) {
           toastr.error('Something went wrong....please try again!!!')
        });

     //Pagination
    $scope.pageCount = function () {

        return Math.ceil($scope.ListOfProject.length / $scope.itemsPerPage);

    };



    //Add new project
    
    $scope.Close = function () {
        $location.path('/project');
    }

    $scope.validation = /^[a-z A-Z]{5,50}$/;
  
    $scope.Add = function () {
        var projectData = {
            ProjectName: $scope.ProjectName,
            Project_Manager: $scope.Project_Manager,
            Description: $scope.Description,
            StartDate: $scope.StartDate,
            EndDate: $scope.EndDate,
            Status: $scope.Status,
       
        };
        debugger;
        $http.post("api/Project/InsertProject", projectData).success(function (data) {
         
            $location.path('/project');
            toastr.success('Data inserted successfully')
         
        }).error(function (data) {
            console.log(data);
            toastr.error('Something went wrong....please try again!!!')

        });
    }

    //Fill the project records for update

    if ($routeParams.id) {
        var params = { projectid: $routeParams.id }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }

        };

        debugger;
        $http.get('api/Project/GetProject', config).success(function (data) {
            $scope.ProjectId = data.projectId;
            $scope.ProjectName = data.projectName;
            $scope.Project_Manager = data.project_Manager;
            $scope.Description = data.description;
            $scope.StartDate = data.startDate.split("T")[0];
            $scope.EndDate = data.endDate.split("T")[0];
            $scope.Status = data.status;

        });

    }

    //Update the project records
    $scope.Update = function () {

        var projectData = {
            ProjectId: $scope.ProjectId,
            ProjectName: $scope.ProjectName,
            Project_Manager: $scope.Project_Manager,
            Description: $scope.Description,
            StartDate: $scope.StartDate,
            EndDate: $scope.EndDate,
            Status: $scope.Status

        };
        var config = {
            headers: { 'Accept': 'application/json' }
        }

        if ($scope.ProjectId > 0) {
            debugger;
            $http.put("api/Project/UpdateProject", projectData, config).success(function (data) {
                $location.path('/project');
                toastr.success('Data updated successfully')
            }).error(function (data) {
                console.log(data);
                toastr.error('Something went wrong....please try again!!!')
            });
        }
    }

    //Delete the project records
    $scope.Delete = function () {
        var params = { projectid: $scope.ProjectId }
        var config = {
            params: params,
            headers: { 'Accept': 'application/json' }

        };


            if ($scope.ProjectId > 0) {
                debugger;
                $http.delete("api/Project/DeleteProject", config).success(function (data) {
                    $location.path('/project');
                    toastr.success('Data deleted successfully')
                }).error(function (data) {
                    console.log(data);
                    toastr.error('Something went wrong....please try again!!!')
                });
            }
    }

    $http.get("api/User/GetAllManager").success(function (data) {
        $scope.ManagerList = data;

    })
        .error(function (data) {
            toastr.error('Data not found!!!')

        });
    
}]);







////By using Services

//(function () {
//    'use strict';
//    mainApp.controller('ProjectController', ProjectController);
//    ProjectController.$inject = ['ProjectService'];

//    function ProjectController(ProjectService) {
//        var vm = this;
//        vm.Project = [];
//        vm.Projects = [];
//        vm.Add = Add;

//        function Add() {
//            var projectModel = {
//                ProjectName: vm.ProjectName
//            };

//            ProjectService.Add(projectModel).then(function () {
//                ProjectService.getProjects().then(function (response) {
//                    vm.Projects = response.data;

//                });
//            })
//        }

//    }
//})();




