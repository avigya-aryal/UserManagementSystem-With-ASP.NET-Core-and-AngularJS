//(function () {
//    'use strict';
//    angular.module('app')
//        .service('ProjectService', ProjectService);
//    ProjectService.$inject = ['$http'];
//    function ProjectService($http) {
//        this.getProject = getProject;
//        this.Add = Add;

//        function getProject() {
//            return $http({
//                method: 'GET',
//                url:'/api/Project/GetProject'
//            });
//        }

//        function Add(projectModel) {
//            return $http({
//                method: 'POST',
//                url: '/api/Project/InsertProject',
//                data: projectModel
//            });
//        }
//    }
//})();