HomeModule.filter('paginationRange', function () {
    return function (input, current, total) {
        total = parseInt(total);
        current = parseInt(current);
        var numberOfPages = parseInt(window.AppConfig.NumberOfPagesForPagining); //5;
        var number = current - parseInt(numberOfPages / 2);
        if (number < 1) {
            number = 1;
        }

        for (var i = number; (i < number + numberOfPages) && (i <= total) ; i++) {
            input.push(i);
        }

        //console.info(current, total, i, number, input);

        return input;
    };
});


HomeModule.directive('loading', ['$http', function ($http) {
    return {
        restrict: 'E',
        replace: true,
        template: '<div class="loading-spinner"><div>' +
			'<div class="spinner">' +
                  '<div class="rect1"></div>' +
                  '<div class="rect2"></div>' +
                  '<div class="rect3"></div>' +
                  '<div class="rect4"></div>' +
                  '<div class="rect5"></div>' +
                '</div>'+
			'</div></div>',
        link: function (scope, elm, attrs) {
            scope.isLoading = function () {
                return $http.pendingRequests.length > 0;
            };

            scope.$watch(scope.isLoading, function (v) {
                if (v) {
                    elm.show();
                } else {
                    elm.hide();
                }
            });
        }
    };

}]);