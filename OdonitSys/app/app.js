var app = angular.module('odonitApp',
    ['ui.router',
    'ui.bootstrap',
    'ngGrid',
    'odontograma-module',
    'ngResource',
    'angularSpinner']
    );

app.constant("globals", {
    "webApi": "http://localhost/Odonit.WebApi/api",
    //Id que representa a cada uno de los servicios, utilizado en el dibujo de odontograma
    "services": {
        'fracturaar': 1,
        'cariear': 2,
        'extraccionar': 3,
        'coronaar': 4,
        'tratamientoConductoar':5,            
        'restauracionr': 6,
        'extraccionr': 7,
        'elementoAusenter': 8,
        'coronar': 9,
        'sellanter': 10,
        'tratamientoConductor': 11,
        'elementoEnErupcionSupr': 12,
        'elementoEnErupcionInfr': 13
    },
    "currentDate": function(){
        var today = new Date();
        today.setDate(today.getDate() - 1)
        return today;
    },
    "status": [
        {
            StatusId: 1,
            Name: "Inicial"
        },
        {
            StatusId: 2,
            Name: "Intermedio"
        },
        {
            StatusId: 3,
            Name: "Finalizado"
        }
    ],
    "messages": {
        "error": {
            "generic": "Se ha producido un error. Intentelo mas tarde.",
            "invalidData": "Los campos ingresados son incorrectos.",
            "emptyField":"Debe seleccionar una prestación y una pieza a tratar."
        },
        "success":{
            "save": "Los datos se han guardado correctamente.",
            "update": "Los datos se han actualizado correctamente.",
            "deletePacient": "El paciente se ha dado de baja correctamente.",
            "noResults": "No se encontraron resultados para la búsqueda.",
            "notServicesFound":"El paciente seleccionado no registra prestaciones."
        }
    }
})
app.run(['$state', '$rootScope', function ($state, $rootScope) {
    $rootScope.$state = $state;
    $state.go('/.persons')
}])
app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider','$httpProvider',
    function ($stateProvider, $urlRouterProvider, $locationProvider, $httpProvider) {

   $httpProvider.interceptors.push('myHttpInterceptor');

    $urlRouterProvider.otherwise('/');
    $stateProvider
        .state('/', {
            url: '/',
            abstract: true,
        views: {            
            '':{
                template: '<div ui-view class="container"></div>'
                },
            'header': {
                templateUrl: 'app/shared/views/header.html'
            }
        }
        })
    .state('/.persons', {
        url:'persons',
        views: {
            '':{
                templateUrl: 'app/persons/views/persons.html',
                controller: 'personController'
            }
        }
    })
    .state('/.persons.search', {
        url: '/search',
        views: {
            '': {
                templateUrl: 'app/persons/views/persons-search.html',
                resolve: {
                    init: function (MedicalSecurityResource) {
                        return MedicalSecurityResource.get();
                    }
                }
            }
        }
    })
    .state('/.persons.create', {
        url: '/create',
        views: {
            '': {
                templateUrl: 'app/persons/views/persons-create.html'
            }
        }
    })
    .state('/.services', {
            url: 'services',
            views: {
                '': {
                    templateUrl: 'app/services/views/services.html'                    
                }
            },           
            params: {
                person: null
            }
    })
    .state('/.services.search', {
        url: '/search',
        views: {
            '': {
                templateUrl: 'app/services/views/services-search.html',
                resolve: {
                    init: function (MedicalSecurityResource) {
                        return MedicalSecurityResource.get();
                    }
                }
            }
        }
    })
        .state('/.services.detail', {
            url: '/detail',
            views: {
                '': {
                    templateUrl: 'app/services/views/services-form.html',
                    resolve: {
                        init: function (TreatmentResource, $q, ServiceResource) {
                            var tratments = TreatmentResource.get();
                            var services = ServiceResource.get();
                            return $q.all([tratments, services]);                            
                        }
                    }
                }
            },
            params: {
                person: null
            }
        })
        .state('/.services.odontograma', {
            url: '/detail',
            views: {
                '': {
                    templateUrl: 'app/services/views/services-detail.html',
                    resolve: {
                        init: function (TreatmentResource, $q, ServiceResource) {
                            var tratments = TreatmentResource.get();
                            var services = ServiceResource.get();
                            return $q.all([tratments, services]);
                        }
                    }
                }
            },
            params: {
                person: null
            }
        })
        .state('/.medicalHistory', {
            url: '/medicalHistory',
            views: {
                '': {
                    templateUrl: 'app/medicalHistory/views/medicalHistory.html',
                    resolve: {
                        init: function (MedicalSecurityResource) {
                            return MedicalSecurityResource.get();
                        }
                    }
                }
            },
            params: {
                person: null
            }
        })
        .state('/.medicalHistory.search', {
            url: '/search',
            views: {
                '': {
                    templateUrl: 'app/medicalHistory/views/medicalHistory-search.html'
                }
            }
        })
        .state('/.medicalHistory.edit', {
            url: '/edit',
            views: {
                '': {
                    templateUrl: 'app/medicalHistory/views/medicalHistory-edit.html'
                }
            },
            params: {
                person: null
            }
        })
        .state('/.report', {
            url: '/report',
            views: {
                '': {
                    templateUrl: 'app/report/views/report.html',
                    resolve: {
                        init: function (TreatmentResource, $q, ServiceResource, MedicalSecurityResource) {
                            var tratments = TreatmentResource.get();
                            var services = ServiceResource.get();
                            var medicalSecurity = MedicalSecurityResource.get();
                            return $q.all([tratments, services, medicalSecurity]);
                        }
                    }
                }
            }
        })
}]);
//Servicio para interceptar request a la web api
app.factory('myHttpInterceptor', function ($q, usSpinnerService, $rootScope) {
    return {       
        'request': function (config) {            
            if (config.url.indexOf('/Odonit.WebApi/api') > -1) {
                $rootScope.$broadcast('hideAlert');
                usSpinnerService.spin('spinner-1');
            }
            return config;
        },
        
        'requestError': function (rejection) {
            usSpinnerService.stop('spinner-1');
            console.log('request error')
            if (canRecover(rejection)) {
                return responseOrNewPromise
            }
            return $q.reject(rejection);
        },

        'response': function (response) {
            usSpinnerService.stop('spinner-1');
            return response;
        },
               
        'responseError': function (rejection) {
            usSpinnerService.stop('spinner-1');
            console.log('response error')
            if (canRecover(rejection)) {
                return responseOrNewPromise
            }
            return $q.reject(rejection);
        }
    };
});