HomeModule.service('dataService', function ($http) {
    delete $http.defaults.headers.common['X-Requested-With'];
    this.getData = function (_url, _params) {
        // $http() returns a $promise that we can add handlers with .then()
        return $http({
            method: 'GET',
            url: window.AppConfig.UrlApi + _url,
            //headers: { 'Authorization': 'Token token=xxxxYYYYZzzz' },
            params: _params
        });
    }
});