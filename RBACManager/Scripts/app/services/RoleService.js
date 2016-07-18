var rbacApp = angular.module("rbacApp")

rbacApp.service("RoleService", ['$http', RoleService]);

function RoleService($http) {
    var self = this;
    self.get = get;

    function get() {
        return $http({
            method: 'GET',
            url: '/api/rbac/GetRoles'
        });
    }

    return self;
}
