(function () {
	'use strict';

	window.app.controller('LoginController', LoginController);

	LoginController.$inject = ['$window', '$http', 'loginConfig', 'model'];
	function LoginController($window, $http, loginConfig, model) {
		var vm = this;
		vm.errorMessage = null;
		vm.loggingIn = false;
		vm.login = login;

		function login() {
			
			vm.loggingIn = true;
			vm.errorMessage = null;
			
			console.log(model);
			console.log(loginConfig.loginUrl);

		    $http.post(loginConfig.loginUrl, { emailAddress: vm.emailAddress, password: vm.password })
				.success(function() {
					$window.location.href = "/";
				})
				.error(function(data) {
				    vm.errorMessage = "There was a problem logging in: " + data.errorMessage;
				})
				.finally(function() {
					vm.loggingIn = false;
				});
		}
	}
})();
