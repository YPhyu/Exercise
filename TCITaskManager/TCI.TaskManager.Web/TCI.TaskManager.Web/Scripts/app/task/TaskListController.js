(function() {
	'use strict';

	window.app.controller('TaskListController', TaskListController);
	
	TaskListController.$inject = ['$modal', 'taskSvc', '$scope','$window'];

	function TaskListController($modal, taskSvc, $scope, $window) {
	    var vm = this;
	    vm.add = add;
	    vm.edit = editTask;
	    vm.delete = deleteTask;	    
	    vm.tasks = taskSvc.tasks;
	    vm.errorMessage = null;

	    console.log(vm.tasks);

	    function add() {
	        vm.errorMessage = null;
			$modal.open({
				template: '<add-task />'
			});
		}
		
	    function editTask(id) {
	        vm.errorMessage = null;
		    var editTask = taskSvc.get(id);		    
		    $modal.open({
		        template: '<edit-task task="task" />',
		        scope: angular.extend($scope.$new(true), { task: editTask })
		    });
		}

	    function deleteTask(id) {
	        vm.errorMessage = null;
		    var deleteUser = $window.confirm('Are you sure you want to delete?');

		    if (deleteUser) {
		        taskSvc.deleteTask(id)
		        .error(function (data) {
		            vm.errorMessage = 'There was a problem deleting the task type: ' + data.errorMessage;
		        });
		    }
		}
		
		vm.gridOptions = {
		    dataSource: vm.tasks,
		    sortable: true,
		    selectable: false,
		    pageable: {
		        pageSize: 20
		    },
		    columns: [
                { field: "name", title: "Name", },
                { field: "taskTypeName", title: "Task Type", },                
                { field: "description", title: "Description" },
                { field: "isActive", title: "Active Task", },
                { field: "fullName", title: "Modified By" },
                { field: "lastUpdated", title: "Modified Date", template: "#=  (lastUpdated == null)? '' : kendo.toString(kendo.parseDate(lastUpdated, 'yyyy-MM-dd'), 'dd-MMM-yy') #" },
                { template: "<a href=\"\" ng-click=\"vm.edit(dataItem.id)\"><i class=\"grid-icon fa fa-edit\"></i></a>&nbsp;<a href=\"\" ng-click=\"vm.delete(dataItem.id)\"><i class=\"grid-icon fa fa-minus-circle delete-icon\"></i></a>", width: 100 }
		    ]
		};		
	}
})();