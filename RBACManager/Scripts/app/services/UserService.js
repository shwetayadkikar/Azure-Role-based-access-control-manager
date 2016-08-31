var rbacApp = angular.module("rbacApp")

rbacApp.service("UserService", ['$http', UserService]);

function UserService($http) {
    var self = this;
    self.get = get;

    function get(searchQuery) {
        return $http({
            method: 'GET',
            url: '/api/rbac/GetUsers?searchQuery=' + searchQuery
        });
    }

    return self;
}
