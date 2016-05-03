app.directive('toothSelector', ['listResource', function (listResource) {
    return {
        restrict: 'E',
        templateUrl: 'app/shared/directives/toothSelector-template.html',
        controller:function($scope){
            $scope.toothList = [];
            console.log($scope.toothSelected);
            listResource.getTooth().then(function (data) {                
                $scope.toothList = data;
            });
            $scope.selectTooth = function (tooth) {
                $scope.toothSelected = tooth;
            }
        },
        link: function ($scope, elem, attr) {
            $scope.showTableTooth = false;
            elem.on('click', function (e) {
                //$scope.toothSelected = e.target.id;
                $scope.showTableTooth = !$scope.showTableTooth;
                $scope.$digest();
            });
        }
    }
}]);