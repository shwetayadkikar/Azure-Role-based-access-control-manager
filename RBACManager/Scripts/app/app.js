var rbacApp = angular.module("rbacApp", []);


rbacApp.controller('RbacController', ['$scope', 'ResourceService', 'RoleService', 'RoleAssignmentService', RbacController]); //'ResourcesService', 'RolesService', 'RoleAssignmentService'


function RbacController($scope, ResourceService, RoleService, RoleAssignmentService) { //ResourcesService, RolesService, RoleAssignmentService
    var self = this;
    self.getResources = getResources;
    self.assignRole = assignRole;

    var resourceGroupsPromise = ResourceService.getResourceGroups();
    resourceGroupsPromise.then(function (response) { console.log(response); self.ResourceGroups = response.data; }, function (error) { console.log(error); });

    self.Resources = [];



    //self.Roles = [
    //    { "roleId": '1', "roleName": "owner" },
    //    { "roleId": '2', "roleName": "reader" }
    //];


    var rolesPromise = RoleService.get();
    rolesPromise.then(function (response) { console.log(response); self.Roles = response.data; }, function (error) { console.log(error); });



    self.roleAssignmentData = {};
    self.roleAssignmentData.principalId = "Add user's principal Id here..";

    function assignRole() {
        var data = {};
        data.resourceGroup = self.roleAssignmentData.ResourceGroup.name;
        data.resourceType = self.roleAssignmentData.Resource.type;
        data.resourceName = self.roleAssignmentData.Resource.name;
        data.roleAssignment = {};
        data.roleAssignment.roleDefinitionId = self.roleAssignmentData.Role;
        data.roleAssignment.principalId = self.roleAssignmentData.principalId;
        var promise = RoleAssignmentService.assignRole(data);
        promise.then(function (response) { console.log(response); }, function (error) { console.log(error); });
    }

    function getResources() {
        var resourcesPromise = ResourceService.get(self.roleAssignmentData.ResourceGroup.name);
        resourcesPromise.then(function (response) { console.log(response); self.Resources = response.data; }, function (error) { console.log(error); });
    }
}