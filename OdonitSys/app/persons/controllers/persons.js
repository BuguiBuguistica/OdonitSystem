app.controller('personController', ['$state', '$scope', function ($state, $scope) {
   
    $state.go('/.persons.search');
}])
app.controller('personSearchController', ['$scope', 'Persons', '$modal', '$state', 'MedicalSecurityResource', '$rootScope', 'globals', function ($scope, Persons, $modal, $state, MedicalSecurityResource, $rootScope, globals) {
    
    //Inicializar listado de Obras Sociales
    $scope.medicalSecurityList = MedicalSecurityResource.MedicalSecurity;

    //Inicializando grilla
    $scope.persons = [];
    $scope.personSelected = [];
    //Buscar todas las personas para popular la tabla
    Persons.get(function (data) {
        $scope.persons = data;
        $scope.setPagingData();
    }, function () {

    });
    $scope.data = [];
    //Paginador
    $scope.totalItems = 0;
    $scope.itemsPerPage = 8;
    $scope.currentPage = 1;
    $scope.setPagingData = function () {
        $scope.totalItems = $scope.persons.length;
        var pagedData = $scope.persons.slice(($scope.currentPage - 1) * $scope.itemsPerPage, $scope.currentPage * $scope.itemsPerPage);
        $scope.data = pagedData;
        $scope.selectedRowIndex = null;
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };

    //Inicialización del modelo persona para realizar la busqueda
    $scope.person = {
        lastName: "",
        code: "",
        medicalSecurity: null,
        isNoActive: false
    };
    //Definicion de los campos de la grilla
    $scope.gridOptions = {
        data: 'data',
        columnDefs: [{ field: 'FirstName', displayName: 'Nombre' },
                    { field: 'LastName', displayName: 'Apellido' },
                    { field: 'BirthDate', displayName: 'Fecha de Nac', cellTemplate: "<div class='gridCell'>{{row.entity[col.field] | date:'dd/MM/yyyy'}}</div>" },
                    { field: 'Code', displayName: 'DNI' }
        ],
        rowTemplate: '<div ng-click="selectRow(row)" ng-style="{ \'cursor\': row.cursor }" ng-repeat="col in renderedColumns" ng-class="col.colIndex()" class="ngCell {{col.cellClass}}"><div class="ngVerticalBar" ng-style="{height: rowHeight}" ng-class="{ ngVerticalBarVisible: !$last }">&nbsp;</div><div ng-cell></div></div>',
        multiSelect: false,
        selectedItems: $scope.personSelected
    };
    //Evento que se dispara al hacer clic en la grilla
    $scope.selectedRowIndex = null;
    $scope.selectRow = function (row) {
        $scope.selectedRowIndex = row.rowIndex;
    };
    //Buscar pesonas segun criterios de busqueda ingresados
    $scope.searchPerson = function () {
        $scope.currentPage = 1;
        Persons.get({
            lastName: $scope.person.lastName,
            code: $scope.person.code,
            medicalSecurityId: $scope.person.medicalSecurity ? $scope.person.medicalSecurity.MedicalSecurityId : null,
            isNoActive: $scope.person.isNoActive
        }, function (data) {
            $scope.persons = data;
            $scope.setPagingData();
            if (!data.length) {
                $rootScope.$broadcast('displayAlert', { type: 'warning', msg: globals.messages.success.noResults });
            };
            $scope.selectedRowIndex = null;
        }, function (status, data) {
            $rootScope.$broadcast('displayAlert', { type: 'danger', msg: globals.messages.error.generic });
        });
    };
    //Levanta el modal con el detalle de la persona seleccionada
    $scope.view = function () {
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: 'app/persons/views/person-modal-detail.html',
            controller: 'ModalInstanceViewPersonController',
            size: 'sm',
            resolve: {
                data: function () {
                    return $scope.personSelected[0];
                }
            }
        });
    };
    //Marca a la persona seleccionada de la grilla como una persona activa en el sistema o NO
    $scope.MarkAsInactive = function () {
        $scope.personSelected[0].IsActive = false;
        update();
    };
    $scope.MarkAsActive = function () {
        $scope.personSelected[0].IsActive = true;
        update();
    };
    //Actualiza la persona al dar de baja
    function update() {
        $scope.person.isNoActive = false;
        $scope.currentPage = 1;
        Persons.save($scope.personSelected[0], function (data) {
            $scope.persons.splice($scope.selectedRowIndex, 1);
            $rootScope.$broadcast('displayAlert', { type: 'success', msg: globals.messages.success.update });
            Persons.get(function (data) {
                $scope.persons = data;
                $scope.setPagingData();
            }, function () {

            });
        }, function () {
            $rootScope.$broadcast('displayAlert', { type: 'danger', msg: globals.messages.error.generic });
        });
    };
    //Edita persona
    $scope.edit = function () {
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: 'app/persons/views/person-modal-edit.html',
            controller: 'ModalInstanceEditPersonController',
            size: 'lg',
            resolve: {
                data: function () {
                    return $scope.personSelected[0];
                }
            }
        });
    };
    //Limpia todos los campos del formulario 
    $scope.cleanInputs = function () {
        $scope.person = {
            lastName: "",
            code: "",
            medicalSecurity: "",
            isNoActive: false
        };
    };
    //Ver Historia Clinica
    $scope.ViewMedicalHistory = function () {
        var person = $scope.personSelected[0];
        $state.go('/.medicalHistory', { 'person': person });
    };
    //Agregar Prestación
    $scope.AddService = function () {
        var person = $scope.personSelected[0];
        $state.go('/.services', { 'person': person });
    };
}]);
app.controller('ModalInstanceEditPersonController', ['$scope', 'data', '$modalInstance', '$rootScope', 'globals', 'Persons', 'MedicalSecurityResource', '$q', 'AddressResource', 'ContactResource', function ($scope, data, $modalInstance, $rootScope, globals, Persons, MedicalSecurityResource, $q, AddressResource, ContactResource) {
    $scope.person = data;
    $scope.medicalSecurityList = MedicalSecurityResource.MedicalSecurity;
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
    $scope.calculateAge = function (birthday) {
        var today = new Date();
        var birthDate = new Date(birthday);
        $scope.person.Age = today.getFullYear() - birthDate.getFullYear();
    };
    //Evento desplegar calendario
    $scope.openCalendar = function ($event, type) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedBirthday = true;
    };
    //Actualizar modelo al seleccionar typeahead
    $scope.updateModel = function (ms) {
        $scope.person.MedicalSecurityId = ms.MedicalSecurityId;
    };
    $scope.save = function () {
        $modalInstance.close();
        var person = Persons.save($scope.person);
        var contact = ContactResource.save($scope.person.Contact);
        var address = AddressResource.save($scope.person.Address);

        var promise = $q.all([person, contact, address]);
        promise.then(function () {
            $rootScope.$broadcast('displayAlert', { type: 'success', msg: globals.messages.success.update });
        }, function () {
            $rootScope.$broadcast('displayAlert', { type: 'danger', msg: globals.messages.error.generic });
        });
    };
}]);
app.controller('ModalInstanceViewPersonController', ['$scope', 'data', '$modalInstance', function ($scope, data, $modalInstance) {
    $scope.person = data;
   
    $scope.close = function () {
        $modalInstance.dismiss('cancel');
    };
}]);
app.controller('personCreateController', ['$scope', 'Persons', 'PersonModel', 'validations', '$stateParams', '$state', 'MedicalSecurityResource', 'globals', '$rootScope', function ($scope, Persons, PersonModel, validations, $stateParams, $state, MedicalSecurityResource, globals, $rootScope) {
    $scope.disabledAddServicesButton = true;
    $scope.onlyReadInputs = false;
    //Inicializar listado de Obras Sociales
    $scope.medicalSecurityList = MedicalSecurityResource.MedicalSecurity;
    $scope.medicalSecurity = null;
    //Actualizar modelo al seleccionar typeahead
    $scope.updateModel = function (ms) {
        $scope.person.MedicalSecurityId = ms.MedicalSecurityId;
    };

    //Configuracion calendario
    $scope.dt = new Date();
    $scope.maxDate = new Date();
        
    //Inicializando persona
    $scope.person = new PersonModel.Person();
    //Calcula la edad cdo se modifica el calendario         
    $scope.calculateAge = function (birthday) {
        var today = new Date();
        var birthDate = new Date(birthday);
        $scope.person.Age = today.getFullYear() - birthDate.getFullYear();
    }
    //Evento desplegar calendario
    $scope.openCalendar = function ($event, type) {
        $event.preventDefault();
        $event.stopPropagation();

        switch (type) {
            case 'birthday':
                $scope.openedBirthday = true;
                break;
            case 'PregnancyDate':
                $scope.openedPregnancyDate = true;
                break;
            case 'TransfusionDate':
                $scope.openedTransfusionDate = true;
                break;
        };        
    };
    $scope.invalidFieldCode = false;
    $scope.invalidFieldSecurityNumber = false;
    $scope.invalidAddressNumber = false;
    $scope.invalidAddressZipCode = false;
    $scope.invalidFieldEmail = false;
   //Validar formulario antes de crear una persona
    isValid = function () {
        var isValid = true;
        if (validations.isEmpty($scope.person.Code)) {
            isValid = false;
        } else if (!validations.isNumeric($scope.person.Code)) {
            isValid = false;
            $scope.invalidFieldCode = true;
        } else {
            $scope.invalidFieldCode = false;
        };

        if (!validations.isEmpty($scope.person.SecurityNumber) && !validations.isNumeric($scope.person.SecurityNumber)) {
            isValid = false;
            $scope.invalidFieldSecurityNumber = true;
        } else {
            $scope.invalidFieldSecurityNumber = false;
        };
        
        if (validations.isEmpty($scope.person.Address.Number)) {
            isValid = false;
        } else if (!validations.isNumeric($scope.person.Address.Number)) {
            isValid = false;
            $scope.invalidAddressNumber = true;
        } else {
            $scope.invalidAddressNumber = false;
        };

        if (validations.isEmpty($scope.person.Address.ZipCode)) {
            isValid = false;
        } else if (!validations.isNumeric($scope.person.Address.ZipCode)) {
            isValid = false;
            $scope.invalidAddressZipCode = true;
        } else {
            $scope.invalidAddressZipCode = false;
        }

        if (!validations.isEmpty($scope.person.Contact.Email) && !validations.isEmail($scope.person.Contact.Email)) {
            isValid = false;
            $scope.invalidFieldEmail = true;
        } else {
            $scope.invalidFieldEmail = false;
        };

        if (validations.isEmpty($scope.person.FirstName) ||
            validations.isEmpty($scope.person.LastName) ||
            validations.isEmpty($scope.person.Address.Street) ||
            validations.isEmpty($scope.person.MedicalHistory.BloodGroup) ||
            validations.isEmpty($scope.person.Contact.CellPhone)) {

            isValid = false;
        };

        return isValid;
    };
    $scope.savePerson = function () {
        $scope.submitted = true;
        if (!isValid()) {
            $rootScope.$broadcast('displayAlert', { type: 'danger', msg: globals.messages.error.invalidData });
            return;
        }
        Persons.save($scope.person, function (data) {
            $scope.submitted = false;
            $scope.disabledAddServicesButton = false;
            $scope.onlyReadInputs = true;
            $scope.person = data;            
            $rootScope.$broadcast('displayAlert', { type: 'success', msg: globals.messages.success.save });
        }, function () {
            $rootScope.$broadcast('displayAlert', { type: 'danger', msg: globals.messages.error.generic });
        });
    };
    //Agregar Prestación
    $scope.AddService = function () {
        if (!$scope.person.PersonId) { return };
        $state.go('/.services', { 'person': $scope.person });
    };
}])