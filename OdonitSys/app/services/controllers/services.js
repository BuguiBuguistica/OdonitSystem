app.controller('servicesController', ['$scope', '$state', '$stateParams', function ($scope, $state, $stateParams) {
    if ($stateParams.person) {
        $state.go('/.services.detail', { 'person': $stateParams.person })
    } else {
        $state.go('/.services.search')
    };
}]);
app.controller('servicesSearchController', ['$scope', '$state', 'Persons', '$rootScope', 'MedicalSecurityResource', 'globals', function ($scope, $state, Persons, $rootScope, MedicalSecurityResource, globals) {
    //Inicializar listado de Obras Sociales
    $scope.medicalSecurityList = MedicalSecurityResource.MedicalSecurity;
    //Inicializando filtros de busqueda
    $scope.person = {
        lastName: "",
        code: "",
        medicalSecurity: "",
        medicalHistoryId: null
    };
    //Inicializa datos
    $scope.persons = [];
    $scope.personSelected = [];
    $scope.data = [];
    //Paginador
    $scope.totalItems = 0;
    $scope.itemsPerPage = 8;
    $scope.currentPage = 1;
    $scope.setPagingData = function () {
        $scope.totalItems = $scope.persons.length;
        var pagedData = $scope.persons.slice(($scope.currentPage - 1) * $scope.itemsPerPage, $scope.currentPage * $scope.itemsPerPage);
        $scope.data = pagedData;
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };
    //Definicion de la grillla
    $scope.gridOptions = {
        data: 'data',
        columnDefs: [{ field: 'FirstName', displayName: 'Nombre' },
                    { field: 'LastName', displayName: 'Apellido' },
                    { field: 'Code', displayName: 'DNI' },
                    { field: 'MedicalHistoryId', displayName: 'Número de HC' },
                    { field: 'MedicalSecurity.Name', displayName: 'Obra Social' },
                    { field: 'MedicalHistory.CurrentMedication', displayName: 'Medicación' },
                    { field: 'MedicalHistory.BloodGroup', displayName: 'Grupo Sanguineo' }
        ],
        rowTemplate: '<div ng-click="selectRow(row)" ng-style="{ \'cursor\': row.cursor }" ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell {{col.cellClass}}"><div class="ngVerticalBar" ng-style="{height: rowHeight}" ng-class="{ ngVerticalBarVisible: !$last }">&nbsp;</div><div ng-cell></div></div>',
        multiSelect: false,
        selectedItems: $scope.personSelected
    };
    //Busqueda de persona segun filtros
    $scope.searchPerson = function () {
        $scope.currentPage = 1;
        Persons.get({
            lastName: $scope.person.lastName,
            code: $scope.person.code,
            medicalSecurityId: $scope.person.medicalSecurity ? $scope.person.medicalSecurity.MedicalSecurityId : null,
            medicalHistoryId: $scope.person.medicalHistoryId
        }, function (data) {
            $scope.persons = data;
            $scope.setPagingData();
            if (!data.length) {                
                $rootScope.$broadcast('displayAlert', { type: 'warning', msg: globals.messages.success.noResults });
            };
        }, function (status, data) {
            $rootScope.$broadcast('displayAlert', { type: 'danger', msg: globals.messages.error.generic });
        });
    };
    //Inicializa la grilla con los datos de todas las personas
    Persons.get(function (data) {
        $scope.persons = data;
        $scope.setPagingData();
    });
    //Limpia todos los campos
    $scope.cleanInputs = function () {
        $scope.person = {
            lastName: "",
            code: "",
            medicalSecurity: "",
            medicalHistoryId: ""
        };
    };
    //Administrar prestaciones
    $scope.ManageService = function () {
        var person = $scope.personSelected[0];
        $state.go('/.services.detail', { 'person': person });
    };
}])
app.controller('servicesDetailController', ['$scope', '$state', 'globals', 'OdontogramaService', '$stateParams', 'ToothServiceResource', '$q', '$rootScope', 'TreatmentResource', 'ServiceResource', '$modal',
    function ($scope, $state, globals, OdontogramaService, $stateParams, ToothServiceResource, $q, $rootScope, TreatmentResource, ServiceResource, $modal) {
        $rootScope.$broadcast('hideAlert');
        $scope.person = $stateParams.person;

        OdontogramaService.setPersonId($scope.person.PersonId);
        //Listado de servicios existentes en el odontograma
        $scope.services = globals.services;

        $scope.serviceSelected = 0;
        //Seleccionar un servicio para vincular con la directiva
        $scope.selectService = function (service) {
            $scope.serviceSelected = service;
        };
        //Crear prestacion mediante odontogram
        $scope.save = function () {
            OdontogramaService.createService();
            console.log(OdontogramaService.servicesCollection);            
            if (!OdontogramaService.servicesCollection.length) {
                $rootScope.$broadcast('displayAlert', { type: 'warning', msg: globals.messages.error.emptyField });
                return;
            }
            var promises = [];
            angular.forEach(OdontogramaService.servicesCollection, function (value) {
                promises.push(ToothServiceResource.save(value));
            });
            $q.all(promises).then(function () {
                $rootScope.$broadcast('displayAlert', { type: 'success', msg: globals.messages.success.save });
                //Borramos los datos almacenados
                OdontogramaService.cleanCollection();
                $rootScope.$broadcast('cleanOdontogram');
            }, function () {
                $rootScope.$broadcast('displayAlert', { type: 'danger', msg: globals.messages.error.generic });
                OdontogramaService.cleanCollection();
                $rootScope.$broadcast('cleanOdontogram');
            });
        };
       
        //Volver a la busqueda de pacientes
        $scope.search = function () {
            $state.go('/.services.search')
        };
        
        //Limpiar Odontograma
        $scope.cleanOdontogram = function () {
            $rootScope.$broadcast('cleanOdontogram');
        }
        $scope.undo = function () {
            $scope.serviceSelected = 0;
            $rootScope.$broadcast('undoOdontogram');
        };
        
        $scope.edit = function () {           
            $state.go('/.services.detail', { 'person': $scope.person });
        }
    }]);
app.controller('ServiceFormController', ['$scope', '$state', 'globals', 'OdontogramaService', '$stateParams', 'ToothServiceResource', '$q', '$rootScope', 'TreatmentResource', 'ServiceResource', '$modal',
    function ($scope, $state, globals, OdontogramaService, $stateParams, ToothServiceResource, $q, $rootScope, TreatmentResource, ServiceResource, $modal) {
        var fdate = new Date();
        fdate.setFullYear(fdate.getFullYear() - 10);
        var tdate = new Date();
        tdate.setDate(tdate.getDate() + 1);
        $scope.filter = {
            fromDate: fdate,
            toDate: tdate,
            treatment: null,
            service: null,
            status: null
        };
        $scope.data = [];
        //Paginador
        $scope.totalItems = 0;
        $scope.itemsPerPage = 5;
        $scope.currentPage = 1;
        $scope.setPagingData = function () {
            $scope.totalItems = $scope.toothServices.length;
            var pagedData = $scope.toothServices.slice(($scope.currentPage - 1) * $scope.itemsPerPage, $scope.currentPage * $scope.itemsPerPage);
            $scope.data = pagedData;
            if (!$scope.$$phase) {
                $scope.$apply();
            }
        };
        //Inicializando select controllers
        $scope.treatmentServices = TreatmentResource.TreatmentServices;
        //Listado de servicios genéricos
        $scope.odontogramServices = ServiceResource.Services;
        //Listado estado del tratamiento
        $scope.status = globals.status;

        $scope.toothServices = [];
        $scope.person = $stateParams.person;
        //Completar la grilla con los datos de las prestaciones
        $scope.getToothServices = function () {
            $scope.currentPage = 1;
            ToothServiceResource.get({
                serviceId: $scope.filter.service,
                fromDate: $scope.filter.fromDate,
                toDate: $scope.filter.toDate,
                statusId: $scope.filter.status,
                treatmentId: $scope.filter.treatment,
                personId: $scope.person.PersonId
            }, function (data) {
                $scope.toothServices = data;
                $scope.setPagingData();
                if (!data.length) {
                    $rootScope.$broadcast('displayAlert', { type: 'warning', msg: globals.messages.success.notServicesFound });
                };
            }, function () {
                $rootScope.$broadcast('displayAlert', { type: 'danger', msg: globals.messages.error.generic });
            });
        };
        $scope.getToothServices();
        $scope.$on('refreshGridService', function () {
            $scope.getToothServices();
        });
        OdontogramaService.setPersonId($scope.person.PersonId);
        //Listado de servicios existentes en el odontograma
        $scope.services = globals.services;

        
        //Definicion grilla prestaciones
        $scope.serviceSelectedFromGrid = [];
        $scope.gridOptions = {
            data: 'data',
            columnDefs: [{ field: 'Diagnosis', displayName: 'Diagnóstico' },
                        { field: 'InitialDate', displayName: 'Fecha Inicio', cellTemplate: "<div class='gridCell'>{{row.entity[col.field] | date:'dd/MM/yyyy'}}</div>" },
                        { field: 'EndDate', displayName: 'Fecha Fin', cellTemplate: "<div class='gridCell'>{{row.entity[col.field] | date:'dd/MM/yyyy'}}</div>" },
                        { field: 'Tooth.Code', displayName: 'Pieza' },
                        { field: 'Face.Name', displayName: 'Cara' },
                        { field: 'Treatment.Name', displayName: 'Tratamiento' },
                        { field: 'Service.Name', displayName: 'Servicio' }
            ],
            rowTemplate: '<div ng-click="selectRow(row)" ng-style="{ \'cursor\': row.cursor }" ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell {{col.cellClass}}"><div class="ngVerticalBar" ng-style="{height: rowHeight}" ng-class="{ ngVerticalBarVisible: !$last }">&nbsp;</div><div ng-cell></div></div>',
            multiSelect: false,
            selectedItems: $scope.serviceSelectedFromGrid
        };
        //Evento que se dispara al hacer clic en la grilla
        $scope.selectRow = function (row) {
            $scope.selectedRowIndex = row.rowIndex;
        };
        //Configuracion del calendario
        $scope.maxDate = new Date();

        //Evento desplegar calendario
        $scope.datepicker = {};
        $scope.datepicker.openedFromDate = false;
        $scope.datepicker.openedToDate = false;
        $scope.openCalendar = function ($event, type) {
            $event.preventDefault();
            $event.stopPropagation();
            switch (type) {
                case 'fromDate':
                    $scope.datepicker.openedFromDate = true;
                    break;
                case 'toDate':
                    $scope.datepicker.openedToDate = true;
                    break;
            }
        };
        //Volver a la busqueda de pacientes
        $scope.search = function () {
            $state.go('/.services.search')
        };
        //Limpiar filtros
        $scope.cleanInputs = function () {
            $scope.filter = {
                fromDate: fdate,
                toDate: tdate,
                treatment: null,
                service: null,
                status: null
            };
        };
       
        //Editar
        $scope.edit = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'app/services/views/services-modal.html',
                controller: 'ModalInstanceServiceController',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            service: $scope.serviceSelectedFromGrid[0],
                            personId: $scope.person.PersonId
                        }
                    }
                }
            });
        };
        
        //Crear
        $scope.new = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'app/services/views/services-modal.html',
                controller: 'ModalInstanceServiceController',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            service: null,
                            personId: $scope.person.PersonId
                        }
                    }
                }
            });
        };
        $scope.goToOdontogram = function () {
            $state.go('/.services.odontograma', { 'person': $scope.person })
        };
    }]);
app.controller('ModalInstanceServiceController', ['$scope', 'data', 'ToothServiceModel', '$modalInstance','TreatmentResource','globals','listResource','$filter','ToothServiceResource','$rootScope','$timeout',
function ($scope, data, ToothServiceModel, $modalInstance, TreatmentResource, globals, listResource, $filter, ToothServiceResource, $rootScope, $timeout) {
    $scope.toothSelected = null;
    //Listado tratamientos por obra social
    $scope.treatmentServices = TreatmentResource.TreatmentServices;
    $scope.faceList = [];
    $scope.faces = [];
    $scope.quadrant = 0;
    
    listResource.getStatus().then(function (data) {
        $scope.status = data;
    });
    listResource.getFaces().then(function (data) {
        $scope.faceList = data;
    });
    //Completar listado de caras al cambiar la seleccion del cuadrante
    $scope.fillFacesSelect = function () {
        $scope.faces = $filter('filter')($scope.faceList, { quadrant: $scope.quadrant })[0].data;
    };
    $scope.isInvalid = true;
    if (data.service) {
        $timeout(function () {            
            $scope.service = angular.copy(data.service);
        },0);
        $scope.edit = true;
        $scope.tituloModal = 'Editar Prestación';
    } else {
        var personId = data.personId;
        $scope.service = new ToothServiceModel.ToothService(personId);
        $scope.edit = false;
        $scope.tituloModal = 'Nueva Prestación';
    };

    $scope.$watch('toothSelected', function (val) {
        if (!$scope.edit) {
            $scope.isInvalid = ($scope.service && !$scope.service.TreatmentId) || ($scope.toothSelected && !$scope.toothSelected.ToothId);
        } else {
            $scope.isInvalid = false;
        }
    },true);
   
    $scope.$watch('service', function (val) {
        if (!$scope.edit) {
            $scope.isInvalid = ($scope.service && !$scope.service.TreatmentId) || (!$scope.toothSelected);
        } else {
            $scope.isInvalid = false;
        }
        if ($scope.service && $scope.service.Amount && $scope.service.Payment) {
            $scope.service.Remainder = $scope.service.Amount - $scope.service.Payment;
        }
    }, true);

    $scope.close = function () {
        $modalInstance.dismiss('cancel');
    };

    //Evento desplegar calendario
    $scope.datepicker = {};
    $scope.datepicker.openedFromDate = false;
    $scope.datepicker.openedToDate = false;
    $scope.openCalendar = function ($event, type) {
        $event.preventDefault();
        $event.stopPropagation();
        switch (type) {
            case 'fromDate':
                $scope.datepicker.openedFromDate = true;
                break;
            case 'toDate':
                $scope.datepicker.openedToDate = true;
                break;
        }
    };

    $scope.save = function () {
        //Actualizo los datos del servicio a guardar
        if (!$scope.edit) {
            //$scope.service.InitialDate = globals.currentDate();
            $scope.service.ToothId = $scope.toothSelected.ToothId;
        };
        $modalInstance.close();
        ToothServiceResource.save($scope.service, function () {            
            $rootScope.$broadcast('displayAlert', { type: 'success', msg: globals.messages.success.save });
            $rootScope.$broadcast('refreshGridService');
        }, function () {
            $rootScope.$broadcast('displayAlert', { type: 'danger', msg: globals.messages.error.generic });
        });
    };
}]);