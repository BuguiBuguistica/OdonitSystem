app.controller('medicalHistoryController', ['$scope','$state','$stateParams', function ($scope, $state,$stateParams) {
    //Si tiene parametros redirige a editar Historia Clinica
    if ($stateParams.person) {
        $state.go('/.medicalHistory.edit', { person: $stateParams.person });
    } else {
        $state.go('/.medicalHistory.search');
    }    
}]);

app.controller('medicalHistoryEditController', ['$scope', '$state', '$stateParams', 'MedicalHistoryService', 'validations', '$rootScope', 'globals', 'ExamResource', '$q', 'Persons', function ($scope, $state, $stateParams, MedicalHistoryService, validations, $rootScope, globals, ExamResource, $q, Persons) {
    $scope.person = $stateParams.person
    $scope.medicalHistory = $stateParams.person.MedicalHistory;
    //Validar formulario antes de actualizar HC
    isValid = function () {
        if (validations.isEmpty($scope.medicalHistory.BloodGroup)) {
            return false;
        } else {
            return true;
        };
    };
    
    //Actualizar Historia Clinica
    $scope.saveChanges = function () {
        if (!isValid()) { return; };        
        ExamResource.save($scope.medicalHistory.Exam, function (data) {
            $scope.medicalHistory.ExamId = data.ExamId;
            MedicalHistoryService.save($scope.medicalHistory, function () {
                $rootScope.$broadcast('displayAlert', { type: 'success', msg: globals.messages.success.update });
            });
        }, function () {
            $rootScope.$broadcast('displayAlert', { type: 'danger', msg: globals.messages.error.generic });
        });        
    };
    //Evento desplegar calendario
    $scope.openCalendar = function ($event, type) {
        $event.preventDefault();
        $event.stopPropagation();
        switch (type) {
            case 'PregnancyDate':
                $scope.openedPregnancyDate = true;
                break;
            case 'TransfusionDate':
                $scope.openedTransfusionDate = true;
                break;
        }
    };
    //Agregar prestacion
    $scope.AddService = function () {      
        $state.go('/.services', { 'person': $scope.person });
    };
    //Volver a los filtros de busqueda de HC
    $scope.search = function () {
        $state.go('/.medicalHistory', { 'person': null });
    };
}]);

app.controller('medicalHistorySearchController', ['$scope', '$state', '$stateParams', 'Persons', '$modal', '$rootScope', 'MedicalSecurityResource', 'globals', function ($scope, $state, $stateParams, Persons, $modal, $rootScope, MedicalSecurityResource, globals) {
    //Inicializa datos
    $scope.persons = [];
    $scope.personSelected = [];
    //Inicializar listado de Obras Sociales
    $scope.medicalSecurityList = MedicalSecurityResource.MedicalSecurity;

    //Inicializando filtros de busqueda
    $scope.person = {
        lastName: "",
        code: "",
        medicalSecurity: null,
        medicalHistoryId: null
    };
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
    //Funcion que se ejecuta al hacer clic en la fila de la grilla
    $scope.selectRow = function (row) {
        $scope.selectedRowIndex = row.rowIndex;
    }
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
    //Ver Detalle
    $scope.view = function () {
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: 'app/medicalHistory/views/medicalHistory-modal-detail.html',
            controller: 'ModalInstanceViewModalHistoryController',
            size: 'md',
            resolve: {
                data: function () {
                    return $scope.personSelected[0];
                }
            }
        });
    };
    //Editar Historia Clinica
    $scope.edit = function () {
        var person = $scope.personSelected[0];
        $state.go('/.medicalHistory', { 'person': person })
    };
    //Agregar prestacion
    $scope.AddService = function () {
        var person = $scope.personSelected[0];
        $state.go('/.services', { 'person': person });
    };
}]);
//Controller para el modal de HC
app.controller('ModalInstanceViewModalHistoryController', ['$scope', 'data', '$modalInstance', function ($scope, data, $modalInstance) {
    $scope.person = data;

    $scope.close = function () {
        $modalInstance.dismiss('cancel');
    };
}]);

