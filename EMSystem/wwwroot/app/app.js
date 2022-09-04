var mainApp = angular.module("mainApp", ['ngRoute']);
mainApp.config(function ($routeProvider) {
	$routeProvider
		// define urls/ state
		.when('/login', {
			templateUrl: 'View/LoginView/login.html'
		})
		.when('/home', {
			templateUrl: 'View/Home/home.html'
		})
		.when('/employee', {
			templateUrl: 'View/Employee/EmployeeList.html'
		})
		.when('/AddEmployee', {
			templateUrl: 'View/Employee/AddEmployee.html'
		})
		.when('/EditEmployee/:id', {
			templateUrl: 'View/Employee/UpdateEmployee.html'
		})
		.when('/DeleteEmployee/:id', {
			templateUrl: 'View/Employee/DeleteEmployee.html'
		})
		.when('/employeeSchedule', {
			templateUrl: 'View/EmployeeScheduleView/EmployeeScheduleList.html'
		})
		.when('/AddEmployeeSchedule', {
			templateUrl: 'View/EmployeeScheduleView/AddEmployeeSchedule.html',

		})
		.when('/contacts', {
			templateUrl: 'View/ContactsView/ContactsList.html',
		})
		.when('/AddContacts', {
			templateUrl: 'View/ContactsView/AddContacts.html',

		})
		.when('/UpdateContacts/:id', {
			templateUrl: 'View/ContactsView/UpdateContacts.html',

		})

		.when('/DeleteContacts/:id', {
			templateUrl: 'View/ContactsView/DeleteContacts.html',

		})
		.when('/EditEmployeeSchedule/:id', {
			templateUrl: 'View/EmployeeScheduleView/UpdateEmployeeSchedule.html'
		})
		.when('/DeleteEmployeeSchedule/:id', {
			templateUrl: 'View/EmployeeScheduleView/DeleteEmployeeSchedule.html'
		})
		.when('/user', {
			templateUrl: 'View/UserView/UserList.html',
		})
		.when('/AddUser', {
			templateUrl: 'View/UserView/AddUser.html',
		})
		.when('/UpdateUser/:id', {
			templateUrl: 'View/UserView/UpdateUser.html',
		})
		.when('/DeleteUser/:id', {
			templateUrl: 'View/UserView/DeleteUser.html',
		})
		.when('/leavelog', {
			templateUrl: 'View/LeaveLog/LeaveLogList.html'
		})
		.when('/AddLeaveLog', {
			templateUrl: 'View/LeaveLog/AddLeaveLog.html'
		})
		.when('/EditLeaveLog/:id', {
			templateUrl: 'View/LeaveLog/UpdateLeaveLog.html'
		})
		.when('/DeleteLeaveLog/:id', {
			templateUrl: 'View/LeaveLog/DeleteLeaveLog.html'
		})
		.when('/userRole', {
			templateUrl: 'View/UserRoleView/UserRoleList.html',
		})
		.when('/AddUserRole', {
			templateUrl: 'View/UserRoleView/AddUserRole.html',
		})
		.when('/UpdateUserRole/:id', {
			templateUrl: 'View/UserRoleView/UpdateUserRole.html',
		})
		.when('/DeleteUserRole/:id', {
			templateUrl: 'View/UserRoleView/DeleteUserRole.html',
		})

		.when('/project', {
			templateUrl: 'View/ProjectView/ProjectList.html'
		})
		.when('/AddProject', {
			templateUrl: 'View/ProjectView/AddProject.html'
		})
		.when('/EditProject/:id', {
			templateUrl: 'View/ProjectView/UpdateProject.html'
		})
		.when('/DeleteProject/:id', {
			templateUrl: 'View/ProjectView/DeleteProject.html'
		})
		.when('/department', {
			templateUrl: 'View/DepartmentView/DepartmentList.html',
		})
		.when('/AddDepartment', {
			templateUrl: 'View/DepartmentView/AddDepartment.html',
		})
		.when('/UpdateDepartment/:id', {
			templateUrl: 'View/DepartmentView/UpdateDepartment.html',
		})
		.when('/DeleteDepartment/:id', {
			templateUrl: 'View/DepartmentView/DeleteDepartment.html',
		})
		.otherwise({
			redirectTo: '/login'
			// if no state / Url provided redirected to #/home
		});
});

mainApp.filter('pagination', function () {
	return function (input, start) {
		if (input == undefined)
			return;
		start = +start;
		return input.slice(start);
	};
});