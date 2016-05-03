app.factory('AddressResource', ['globals', '$resource', function (globals, $resource) {
    return $resource(globals.webApi + '/Address/v1/:param', {}, {});
}])