(function () {
    'use strict';

    angular
        .module('directorywalker.app', [])
        .run();

    angular
        .module('directorywalker.hierarchy', ['directorywalker.app']);
})();