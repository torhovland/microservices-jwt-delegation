'use strict';
angular.module('todoApp')
.controller('serviceCallsCtrl', ['$scope', '$location', 'serviceCallsSvc', 'adalAuthenticationService', function ($scope, $location, serviceCallsSvc, adalService) {
    $scope.error = "";
    $scope.loadingMessage = "Loading...";

    $scope.populate = function () {
        serviceCallsSvc.clientToServiceB().success(function(results) {
            $scope.clientToServiceB = results;
            $scope.loadingMessage = "";
        }).error(function(err) {
            $scope.error = err;
            $scope.loadingMessage = "";
        });

        serviceCallsSvc.clientToServiceA().success(function (results) {
            $scope.clientToServiceA = results;
            $scope.loadingMessage = "";
        }).error(function (err) {
            $scope.error = err;
            $scope.loadingMessage = "";
        });

        serviceCallsSvc.serviceAToServiceB().success(function (results) {
            $scope.serviceAToServiceB = results;
            $scope.loadingMessage = "";
        }).error(function (err) {
            $scope.error = err;
            $scope.loadingMessage = "";
        });
    };
}]);