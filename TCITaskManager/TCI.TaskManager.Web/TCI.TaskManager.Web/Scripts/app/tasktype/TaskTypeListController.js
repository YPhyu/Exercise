(function() {
	'use strict';

	window.app.controller('TaskTypeListController', TaskTypeListController);
	
	TaskTypeListController.$inject = ['$modal', 'taskTypeSvc', '$scope','$window'];

	function TaskTypeListController($modal, taskTypeSvc, $scope, $window) {
	    var vm = this;
	    vm.add = add;
	    vm.edit = editTaskType;
	    vm.delete = deleteTaskType;	    
	    vm.taskTypes = taskTypeSvc.taskTypes;
	    vm.errorMessage = null;

	    console.log(vm.taskTypes);

	    function add() {
	        vm.errorMessage = null;
			$modal.open({
				template: '<add-task-type />'
			});
		}
		
	    function editTaskType(id) {
	        vm.errorMessage = null;
		    var editTaskType = taskTypeSvc.get(id);		    
		    $modal.open({
		        template: '<edit-task-type task-type="taskType" />',
		        scope: angular.extend($scope.$new(true), { taskType: editTaskType })
		    });
		}

	    function deleteTaskType(id) {
	        vm.errorMessage = null;
		    var deleteUser = $window.confirm('Are you sure you want to delete?');

		    if (deleteUser) {
		        taskTypeSvc.deleteTaskType(id)
		        .error(function (data) {
		            vm.errorMessage = 'There was a problem deleting the task type: ' + data.errorMessage;
		        });
		    }
		}
		
		vm.gridOptions = {
		    dataSource: vm.taskTypes,
		    sortable: true,
		    selectable: false,
		    pageable: {
		        pageSize: 20
		    },
		    columns: [
                { field: "name", title: "Name", },
                { field: "description", title: "Description" },
                { field: "fullName", title: "Modified By" },
                { field: "lastUpdated", title: "Modified Date", template: "#=  (lastUpdated == null)? '' : kendo.toString(kendo.parseDate(lastUpdated, 'yyyy-MM-dd'), 'dd-MMM-yy') #" },
                { template: "<a href=\"\" ng-click=\"vm.edit(dataItem.id)\"><i class=\"grid-icon fa fa-edit\"></i></a>&nbsp;<a href=\"\" ng-click=\"vm.delete(dataItem.id)\"><i class=\"grid-icon fa fa-minus-circle delete-icon\"></i></a>", width: 100 }
		    ]
		};		
	}
})();