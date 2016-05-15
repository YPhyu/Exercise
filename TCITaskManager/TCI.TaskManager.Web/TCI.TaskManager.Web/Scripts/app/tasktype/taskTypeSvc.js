(function() {
    window.app.factory('taskTypeSvc', taskTypeSvc);

	taskTypeSvc.$inject = ['$http'];
	function taskTypeSvc($http) {
	    var taskTypes = [];
	    var errorMessage="";

	    loadTaskTypes();

		var svc = {
		    get: getTaskType,
		    add: add,
			update: update,
			deleteTaskType: deleteTaskType,
			taskTypes: taskTypes,
            errorMessage: errorMessage
		};

		return svc;

		function loadTaskTypes() {
           
			$http.post('/TaskType/All')
				.success(function (data) {                    
				    taskTypes.addRange(data);				   
				});

		}

		function add(taskType) {
		    return $http.post('/TaskType/Add', taskType)
				.success(function(taskType) {
					taskTypes.unshift(taskType);
				});
		}

		function update(existingTaskType, updatedTaskType) {
			return $http.post('/TaskType/Update', updatedTaskType)
				.success(function(taskType) {
					angular.extend(existingTaskType, taskType);
				});
		}

		function deleteTaskType(id) {
		    return $http.post('/TaskType/Delete/' + id)
		            .success(function () {
		                var index = taskTypes.indexOf(getTaskType(id));
		                taskTypes.splice(index, 1);
		            });
		}

		function getTaskType(id) {
		 	for (var i = 0; i < taskTypes.length; i++) {
			    if (taskTypes[i].id == id)   return taskTypes[i];                    
			}
			return null;
		}
	}
})();