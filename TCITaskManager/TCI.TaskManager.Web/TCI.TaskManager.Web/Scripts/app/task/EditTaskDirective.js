(function() {
	"use strict";

	window.app.directive('editTask', editTask);

	function editTask() {
		return {
			scope: {
				task: "="
			},
			templateUrl: '/task/template/editTask.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', 'taskSvc', 'taskTypeSvc'];
	function controller($scope, taskSvc, taskTypeSvc) {
	    
		var vm = this;
		vm.save = save;

		vm.saving = false;
		vm.task = angular.copy($scope.task);
        vm.taskTypes = taskTypeSvc.taskTypes;
		vm.errorMessage = null;

		function save() {
		    vm.saving = true;
		    console.log(vm.task);
		    taskSvc.update($scope.task, vm.task)
				.success(function () {
					//Close the modal
					$scope.$parent.$close();
				})
				.error(function(data) {
				    vm.errorMessage = 'There was a problem saving changes to the task: ' + data.errorMessage;
				})
				.finally(function() {
					vm.saving = false;
				});
		}
	}
})();