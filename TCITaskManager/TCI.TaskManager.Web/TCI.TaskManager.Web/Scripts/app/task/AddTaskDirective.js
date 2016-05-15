(function() {
	"use strict";

	window.app.directive('addTask', addTask);

	function addTask() {
		return {
			templateUrl: '/task/template/addTask.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', 'taskSvc', 'taskTypeSvc'];
	function controller($scope, taskSvc, taskTypeSvc) {
		var vm = this;
		vm.add = add;

		vm.saving = false;
		vm.task = {};
		vm.taskTypes = taskTypeSvc.taskTypes;
		vm.errorMessage = null;

		function add() {
			vm.saving = true;
			taskSvc.add(vm.task)
				.success(function () {
					//Close the modal
					$scope.$close();
				})
				.error(function(data) {
				    vm.errorMessage = 'There was a problem adding the task: ' + data.errorMessage;				    
				})
				.finally(function() {
					vm.saving = false;
				});
		}
	}
})();