app.controller('alertController', ['$scope', '$timeout', function ($scope, $timeout) {
    $scope.show = false;
    $scope.type = 'danger';
    $scope.msg = '';
    $scope.closeAlert = function (index) {
        $scope.show = false;
    };
    $scope.displayAlert = $scope.$on('displayAlert', function (event, data) {
        if (data.msg) {
            $scope.show = true;
            $scope.type = data.type || 'danger';
            $scope.msg = data.msg || '';
        };

        $timeout(function () {
            $scope.show = false;
        },10000)
    });
    $scope.hideAlert = $scope.$on('hideAlert', function () {
        $scope.show = false;
    });
}]);