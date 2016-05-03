app.factory('ContactResource', ['globals', '$resource', function (globals, $resource) {
    return $resource(globals.webApi + '/Contact/v1/:param', {}, {});
}])