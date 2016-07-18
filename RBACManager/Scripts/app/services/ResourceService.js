var rbacApp = angular.module("rbacApp")

rbacApp.service("ResourceService", ['$http', ResourceService]);

function ResourceService($http) {
    var self = this;
    self.get = get;
    self.getResourceGroups = getResourceGroups;
    
    function getResourceGroups()
    {
       return $http({
            method: 'GET',
            url: '/api/rbac/GetResourceGroups'
        });
    }

    function get(resourceGroup)
    {
        return $http({
            method: 'GET',
            url: '/api/rbac/GetResources?resourceGroup=' + resourceGroup
        });
    }
   
    return self;
}
