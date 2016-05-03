app.factory('TreatmentResource', ['globals', '$resource','$q', function (globals, $resource,$q) {
    var factory = {
        TreatmentServices: null,
        get: function () {            
            if (!this.TreatmentServices) {
                var that = this;
                return resource.get(function (data) {
                    that.TreatmentServices = data;
                }, function () {
                    console.log('Error al cargar tratamientos');
                }).$promise;
            } else {
                return $q.when(this.TreatmentServices);
            }
        }
    }
    
    var resource = $resource(globals.webApi + '/Treament/v1/:param', {}, {
            get: { method: 'GET', isArray: true },
        });   
    

    return factory;
    
}])