var rbacApp = angular.module("rbacApp", ['ngAnimate', 'ngSanitize', 'ui.bootstrap']);


rbacApp.controller('RbacController', ['$scope', 'ResourceService', 'RoleService', 'RoleAssignmentService', 'UserService', RbacController]); //'ResourcesService', 'RolesService', 'RoleAssignmentService'


function RbacController($scope, ResourceService, RoleService, RoleAssignmentService, UserService) { //ResourcesService, RolesService, RoleAssignmentService
    var self = this;
    self.getResources = getResources;
    self.assignRole = assignRole;
    self.getUsers = getUsers;
    self.states = ['Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California', 'Colorado', 'Connecticut', 'Delaware', 'Florida', 'Georgia', 'Hawaii', 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana', 'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota', 'Mississippi', 'Missouri', 'Montana'];

    var resourceGroupsPromise = ResourceService.getResourceGroups();
    resourceGroupsPromise.then(function (response) { console.log(response); self.ResourceGroups = response.data; }, function (error) { console.log(error); });

    self.Resources = [];

    self.Users = [];

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

    function getUsers(searchQuery) {
        var usersPromise = UserService.get(searchQuery);
        return usersPromise.then(function (response) { console.log(response); return response.data; }, function (error) { console.log(error); });
    }
}