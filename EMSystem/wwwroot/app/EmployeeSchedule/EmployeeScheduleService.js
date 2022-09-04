//(function () {
//    'use strict';
//    angular.module('app')
//        .service('EmployeeScheduleService', EmployeeScheduleService);
//    EmployeeScheduleService.$inject = ['$http'];
//    function EmployeeScheduleService($http) {
//        this.getEmployeeSchedule = getEmployeeSchedule;
//        this.addEmployeeSchedule = addEmployeeSchedule;

//        function getEmployeeSchedule() {
//            return $http({
//                method: 'GET',
//                url:'/api/EmployeeSchedule/GetEmployeeSchedule'
//            });
//        }

//        function addEmployeeSchedule() {
//            return $http({
//                method: 'POST',
//                url: '/api/EmployeeSchedule/InsertEmployeeSchedule',
//                data:oEmployeeSchedule
//            });
//        }
//    }
//})();