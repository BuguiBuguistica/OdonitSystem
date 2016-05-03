app.factory('MedicalSecurityResource', ['globals', '$resource', '$q', function (globals, $resource, $q) {
    var factory = {
        MedicalSecurity: null,
        get: function () {
            if (!this.MedicalSecurity) {
                var that = this;
                return resource.get(function (data) {
                    that.MedicalSecurity = data;
                }, function () {
                    console.log('Error al cargar obras sociales');
                }).$promise;
            } else {
                return $q.when(this.MedicalSecurity);
            }
        }
    }

    var resource = $resource(globals.webApi + '/MedicalSecurity/v1/:param', {}, {
        get: { method: 'GET', isArray: true },
    });

    return factory;
}])