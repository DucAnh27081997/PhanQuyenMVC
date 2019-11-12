var app = angular.module('myApp', []);
app.controller('LogCtrl', function ($scope, $http) {
   
    function parseJsonDate(jsonDateString) {
        return new Date(parseInt(jsonDateString.replace('/Date(', '')));
    }
    $scope.list = null;
    $scope.LoadData = function () {
        $http.get("/Security/GetListHistoryAccess")
            .then(function (response) {
                $scope.list = response.data;
                for (var i = 0; i < $scope.list.length; i++) {
                    $scope.list[i].DateAccess = parseJsonDate($scope.list[i].DateAccess)
                }

            });
    }
    $scope.LoadData();
});
