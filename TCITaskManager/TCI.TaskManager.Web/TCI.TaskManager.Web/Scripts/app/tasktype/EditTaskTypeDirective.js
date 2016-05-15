(function() {
	"use strict";

	window.app.directive('editTaskType', editTaskType);

	function editTaskType() {
		return {
			scope: {
				taskType: "="
			},
			templateUrl: '/tasktype/template/editTaskType.tmpl.cshtml',
			controller: controller,
			controllerAs: 'vm'
		}
	}

	controller.$inject = ['$scope', 'taskTypeSvc'];
	function controller($scope, taskTypeSvc) {
	    
		var vm = this;
		vm.save = save;

		vm.saving = false;
		vm.taskType = angular.copy($scope.taskType);
		vm.errorMessage = null;

		function save() {
			vm.saving = true;
		    taskTypeSvc.update($scope.taskType, vm.taskType)
				.success(function () {
					//Close the modal
					$scope.$parent.$close();
				})
				.error(function(data) {
				    vm.errorMessage = 'There was a problem saving changes to the task type: ' + data.errorMessage;
				})
				.finally(function() {
					vm.saving = false;
				});
		}
	}
})();