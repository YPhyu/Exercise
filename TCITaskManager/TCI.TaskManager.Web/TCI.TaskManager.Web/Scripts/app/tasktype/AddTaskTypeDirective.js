(function() {
	"use strict";

	window.app.directive('addTaskType', addTaskType);

	function addTaskType() {
		return {
			templateUrl: '/tasktype/template/addTaskType.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', 'taskTypeSvc'];
	function controller($scope, taskTypeSvc) {
		var vm = this;
		vm.add = add;

		vm.saving = false;
		vm.taskType = {};
		vm.errorMessage = null;

		function add() {
			vm.saving = true;
			taskTypeSvc.add(vm.taskType)
				.success(function () {
					//Close the modal
					$scope.$close();
				})
				.error(function(data) {
				    vm.errorMessage = 'There was a problem adding the task type: ' + data.errorMessage;				    
				})
				.finally(function() {
					vm.saving = false;
				});
		}
	}
})();