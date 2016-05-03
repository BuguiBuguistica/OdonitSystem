app.factory('MedicalHistoryService', ['globals', '$resource', function (globals, $resource) {
    return $resource(globals.webApi + '/MedicalHistory/v1/:param', {}, {
        getById: { method: 'GET' },
        get: { method: 'GET', isArray: true },
    });
}])