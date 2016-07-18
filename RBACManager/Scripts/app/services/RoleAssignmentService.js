var rbacApp = angular.module("rbacApp")

rbacApp.service("RoleAssignmentService", ['$http', RoleAssignmentService]);

function RoleAssignmentService($http) {
    var self = this;
    self.assignRole = assignRole;

    function assignRole(roleAssignmentData) {

        var roleAssignment = {
            resourceGroup: roleAssignmentData.resourceGroup,
            resourceType: roleAssignmentData.resourceType,
            resourceName: roleAssignmentData.resourceName,
            roleDefinitionId: roleAssignmentData.roleAssignment.roleDefinitionId,
            principalId: roleAssignmentData.roleAssignment.principalId
        };

        return $http({
            method: 'PUT',
            url: '/api/rbac/AssignRole',
            data: roleAssignment 
        });
    }

    return self;
}
