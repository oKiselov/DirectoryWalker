(function () {
    'use strict';

    angular
        .module('directorywalker.hierarchy')
        .controller('hierarchyController', hierarchyController);

    hierarchyController.$inject = ['$scope'];

    function hierarchyController($scope) {

        $scope.messages = [1,3,5,7,9];

    }
})();