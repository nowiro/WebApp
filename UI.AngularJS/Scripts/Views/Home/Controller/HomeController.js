HomeModule.component('mainContent', {
    templateUrl: window.AppConfig.Url + '/Scripts/Views/Home/Controller/mian.content.template.html',
    controller: function ContentController() {

    }
});

HomeModule.component('content', {
    template:
        '<article>' +
        '<h1 class="h3">Step 1: Content</h1>' +
        '<ul>' +
          '<li ng-repeat="item in $ctrl.data">' +
            '<h2 class="h4">{{item.Name}}</h2>' +
            '<p>{{item.Description}}</p>' +
          '</li>' +
        '</ul>' +
        '</article>',
    controller: function ContentController() {
        this.data = [
          {
              Name: 'Name 1',
              Description: 'Description 1'
          }, {
              Name: 'Name 2',
              Description: 'Description 2'
          }, {
              Name: 'Name 3',
              Description: 'Description 3'
          }
        ];
    }
});

HomeModule.component('contentTemplateUrl', {
    templateUrl: window.AppConfig.Url + '/Scripts/Views/Home/Controller/content.template.html',
    controller: function ContentController() {
        this.data = [
          {
              Name: 'Name 1',
              Description: 'Description 1'
          }, {
              Name: 'Name 2',
              Description: 'Description 2'
          }, {
              Name: 'Name 3',
              Description: 'Description 3'
          }
        ];
    }
});

HomeModule.component('contentTemplateUrlAjaxCall', {
    templateUrl: window.AppConfig.Url + '/Scripts/Views/Home/Controller/content.template.ajax.html',
    controller: function ContentController($http) {
        var self = this;
        $http.get(window.AppConfig.UrlApi + '/api/DefaultASYNC').then(function (response) {
            self.data = response.data;
        });
    }
});

HomeModule.component('contentTemplateUrlAjaxCallFiltering', {
    templateUrl: window.AppConfig.Url + '/Scripts/Views/Home/Controller/content.template.ajax.filter.html',
    controller: function ContentController($scope, $http, dataService) {
        var url = '/api/DefaultASYNC';
        var params = {
            //'search': '',
            //'page': '',
            //'sortBy': '',
            //'sortDirection': '',
            //'pageSize': ''
        };
        var sortDirectionOptions = [{ 'name': 'Ascending', 'value': 'asc' }, { 'name': 'Descending', 'value': 'desc' }];
        var pageSizeOptions = [10, 50, 100];
        $scope.data = null;

        $scope.currentPage = 0;
        $scope.countOfPages = 0;
        $scope.goToPageValue = 0;
        $scope.pageCount = 0;

        $scope.search = function () {
            params.search = $scope.searchValue;
            params.page = 1;
            _getData();
        };

        $scope.goToPage = function () {
            if ($scope.goToPageValue > 0 && $scope.goToPageValue <= $scope.pageCount) {
                $scope.setPage($scope.goToPageValue);
            }
        };

        $scope.pageSize = function () {
            params.pageSize = $scope.pageSizeValue;
            params.page = 1;
            _getData();
        };

        $scope.sortBy = function () {
            params.sortBy = $scope.sortByValue;
            params.page = 1;
            _getData();
        };

        $scope.sortDirection = function () {
            params.sortDirection = $scope.sortDirectionValue;
            params.page = 1;
            _getData();
        };

        $scope.prevPage = function () {
            $scope.currentPage = $scope.currentPage > 1 ? $scope.currentPage - 1 : 1;
            params.page = $scope.currentPage;
            _getData();
        };

        $scope.nextPage = function () {
            $scope.currentPage = $scope.currentPage < $scope.pageCount ? $scope.currentPage + 1 : $scope.pageCount;
            params.page = $scope.currentPage;
            _getData();
        };

        $scope.setPage = function (p) {
            $scope.currentPage = p;
            params.page = $scope.currentPage;
            _getData();
        };

        _getData();

        function _getData() {
            dataService.getData(url, params).then(function (dataResponse) {
                $scope.data = dataResponse.data;
                $scope.currentPage = dataResponse.data.Info.Page;
                $scope.pageCount = dataResponse.data.Info.PageCount;
                $scope.pageSizeValue = dataResponse.data.Info.PageSize;
                $scope.sortByValue = dataResponse.data.Info.SortBy;
                $scope.sortDirectionValue = dataResponse.data.Info.SortDirection;
                $scope.goToPageValue = $scope.currentPage;
                $scope.sortByOptions = Object.getOwnPropertyNames(dataResponse.data.Data[0]);
                $scope.sortDirectionOptions = sortDirectionOptions;
                $scope.pageSizeOptions = pageSizeOptions;
                
            });
        }

    }
});
