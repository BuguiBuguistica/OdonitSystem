var odonitApp = angular.module('odonitApp', ['ngRoute']);

odonitApp.config(function ($routeProvider) {
    $routeProvider.
    	when('/', {
    	    controller: 'indexHomeController',
    	    templateUrl: 'HomeIndexTemplate'
    	})
        .when('/Contact', {
            templateUrl: baseUrl + 'Home/Contact'
        })
        .when('/ObraSociales', {
            templateUrl: baseUrl + 'ObraSocials/Index',
            controller: 'obrasSocialesIndexController'
        })
         .when('/Contactos', {
             templateUrl: baseUrl + 'Contacto/Index'
         })
    .when('/Personas', {
        templateUrl: baseUrl + 'Persona/Index'
    });
    	
});
odonitApp.controller('indexHomeController',
    function ($scope) {
        $scope.title = "ASP.NET";
    });

odonitApp.controller("obrasSocialesIndexController", function ($scope, $http, data) {
    $scope.obrasSociales = data;
    //$scope.getData = function () {
    //    $http.get("/ObraSocials/GetData")
	//		.success(function (data) {
	//		    $scope.obrasSociales = data;
	//		    console.log(data);
	//		})
	//		.error(function (data) {
	//		    console.log('Error: ' + data);
	//		});
    //};
});