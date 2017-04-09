'use strict';
angular.module('todoApp')
.factory('serviceCallsSvc', ['$http', function ($http) {
    return {
        clientToServiceB: function () {
            return $http.get('https://localhost:44371/api/values');
        },
        clientToServiceA: function () {
            return $http.get('https://localhost:44370/api/values');
        },
        serviceAToServiceB : function () {
            return $http.get('https://localhost:44370/api/combinedValues');
        }
    };
}]);