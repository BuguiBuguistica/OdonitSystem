app.factory('ToothServiceResource', ['globals', '$resource', function (globals, $resource) {
    return $resource(globals.webApi + '/ToothService/v1/:param', {}, {
        getById: { method: 'GET' },
        get: { method: 'GET', isArray: true },
    });
}])