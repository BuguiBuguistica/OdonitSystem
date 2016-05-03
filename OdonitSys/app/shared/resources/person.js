app.factory('Persons', ['globals', '$resource', function (globals, $resource) {
    return $resource(globals.webApi + '/Person/v1/:param', {},{
        getById: {method:'GET'},
        get: {method:'GET', isArray:true },
        });
}])