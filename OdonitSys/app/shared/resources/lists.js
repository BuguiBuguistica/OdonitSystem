app.factory('listResource', ['$http','$q', function ($http,$q) {
    var statuses = null, faces = null, tooth = null;

    var factory = {
        getStatus: function () {
            if (!statuses) {
                var deferred = $q.defer();
               $http.get('app/shared/data/status.json').then(function (res) {
                   statuses = res.data;
                   deferred.resolve(statuses);
               });
               return deferred.promise;
            } else {
                return $q.when(statuses);
            }
        },
        getFaces: function () {
            if (!faces) {
                var deferred = $q.defer();
                $http.get('app/shared/data/face.json').then(function (res) {
                    faces = res.data;
                    deferred.resolve(faces);
                });
                return deferred.promise;
            } else {
                return $q.when(faces);
            }
        },
        getTooth: function () {
            if (!tooth) {
                var deferred = $q.defer();
                $http.get('app/shared/data/tooth.json').then(function (res) {
                    tooth = res.data;
                    deferred.resolve(tooth);
                });
                return deferred.promise;
            } else {
                return $q.when(tooth);
            }
        }
    }
    

    return factory;
}]);