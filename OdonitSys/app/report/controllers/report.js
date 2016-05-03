app.controller('ReportController', ['$scope', 'ReportResource', '$window', 'ReportFilterModel', 'TreatmentResource', 'ServiceResource', 'MedicalSecurityResource','$rootScope',
function ($scope, ReportResource, $window, ReportFilterModel, TreatmentResource, ServiceResource, MedicalSecurityResource, $rootScope) {
    $rootScope.$broadcast('hideAlert');

    $scope.medicalSecurity = null;
    $scope.service = null;
    $scope.treatment = null;
    $scope.filter = new ReportFilterModel.Filter();
    //Inicializando select controllers
    $scope.treatmentServices = TreatmentResource.TreatmentServices;
    //Listado de servicios genéricos
    $scope.odontogramServices = ServiceResource.Services;
    //Inicializar listado de Obras Sociales
    $scope.medicalSecurityList = MedicalSecurityResource.MedicalSecurity;
    $scope.data = [];
    //Paginador
    $scope.totalItems = 0;
    $scope.itemsPerPage = 8;
    $scope.currentPage = 1;
    $scope.setPagingData = function () {
        $scope.totalItems = $scope.report.length;
        var pagedData = $scope.report.slice(($scope.currentPage - 1) * $scope.itemsPerPage, $scope.currentPage * $scope.itemsPerPage);
        $scope.data = pagedData;
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };
   
    $scope.report = [];
    //Definicion de los campos de la grilla
    $scope.gridOptions = {
        data: 'data',
        columnDefs: [{ field: 'FirstName', displayName: 'Nombre' },
                    { field: 'LastName', displayName: 'Apellido' },                    
                    { field: 'PersonCode', displayName: 'DNI' },
                    { field: 'Address', displayName: 'Address' }
        ]
    };

    //Buscar pacientes por filtros
    $scope.searchPatients = function () {
        $scope.currentPage = 1;
        ReportResource.get({
            serviceId: $scope.filter.ServiceId,
            treatmentId: $scope.filter.TreatmentId,
            medicalSecurityId: $scope.filter.MedicalSecurityId,
            fromDate: $scope.filter.FromDate,
            endDate: $scope.filter.EndDate
        }, function (data) {
            $scope.report = data;
            $scope.setPagingData();
        }, function () {
            console.log('Hubo un error');
        });
    };

    //Actualizar modelo al seleccionar typeahead
    $scope.updateModel = function (ms) {
        $scope.filter.MedicalSecurityName = ms.Name;
        $scope.filter.MedicalSecurityId = ms.MedicalSecurityId;
    };
    //Actualizar modelo Servicio al cambiar
    $scope.updateServiceFilter = function () {        
        var service = JSON.parse($scope.service);
        $scope.filter.ServiceId = service.ServiceId;
        $scope.filter.ServiceName = service.Name;
    };
    //Actualizar modelo Treatment al cambiar
    $scope.updateTreatmentFilter = function () {
        var treatment = JSON.parse($scope.treatment);
        $scope.filter.TreatmentId = treatment.TreatmentId;
        $scope.filter.TreatmentName = treatment.Code + ' - ' + treatment.Name;
    }

    //Calendario
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

    //Descargar PDF
    $scope.donwloadPDF = function () {
        ReportResource.report($scope.filter, function (data) {
            var url = URL.createObjectURL(data.response);
            $window.open(url);
        })
    };

    //Limpiar inputs
    $scope.cleanInputs = function () {
        $scope.filter = new ReportFilterModel.Filter();
        $scope.service = null;
        $scope.treatment = null;
        $scope.medicalSecurity = null;
    };
}]);