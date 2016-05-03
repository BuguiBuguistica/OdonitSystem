app.factory('ServiceResource', ['globals', '$resource', '$q', function (globals, $resource, $q) {
    var factory = {
        Services: null,
        get: function () {
            if (!this.Services) {
                var that = this;
                return resource.get(function (data) {
                    that.Services = data;                    
                }, function () {
                    console.log('Error al cargar tratamientos');
                }).$promise;
            } else {
                return $q.when(this.Services);
            }
        }
    }

    var resource = $resource(globals.webApi + '/Services/v1/:param', {}, {
        get: { method: 'GET', isArray: true },
    });

    return factory;
}])