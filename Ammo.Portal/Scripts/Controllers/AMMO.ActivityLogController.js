'use strict';

angular.module('AMMO.ActivityLogController', [])
    .controller('ActivityLogController', ['$scope', '$http', function ($scope, $http) {
        $scope.model = {};
        
        $http.get('/ActivityLog/Get').success(function (data) {
            $scope.model = data;
        });

        $scope.getDate = function (date, format) {
            return moment(date).format(format);
        }
}]);