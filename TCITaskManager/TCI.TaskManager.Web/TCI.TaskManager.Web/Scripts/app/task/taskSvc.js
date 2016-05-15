(function() {
    window.app.factory('taskSvc', taskSvc);

	taskSvc.$inject = ['$http'];
	function taskSvc($http) {
	    var tasks = [];
	    var errorMessage="";

	    loadTasks();

		var svc = {
		    get: getTask,
		    add: add,
			update: update,
			deleteTask: deleteTask,
			tasks: tasks,
            errorMessage: errorMessage
		};

		return svc;

		function loadTasks() {
           
			$http.post('/Task/All')
				.success(function (data) {                    
				    tasks.addRange(data);				   
				});

		}

		function add(task) {
		    return $http.post('/Task/Add', task)
				.success(function(task) {
					tasks.unshift(task);
				});
		}

		function update(existingTask, updatedTask) {
			return $http.post('/Task/Update', updatedTask)
				.success(function(task) {
					angular.extend(existingTask, task);
				});
		}

		function deleteTask(id) {
		    return $http.post('/Task/Delete/' + id)
		            .success(function () {
		                var index = tasks.indexOf(getTask(id));
		                tasks.splice(index, 1);
		            });
		}

		function getTask(id) {
		 	for (var i = 0; i < tasks.length; i++) {
			    if (tasks[i].id == id)   return tasks[i];                    
			}
			return null;
		}
	}
})();