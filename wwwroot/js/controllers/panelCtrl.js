angular.module("linkHolder")
.constant("UrlRoleAdmin","/api/roleadmin")
.constant("UrlAdmin","/api/admin")
.controller("panelCtrl", function ($scope, $location, $http, UrlAdmin, UrlRoleAdmin) {
    $scope.roles = {};
    $scope.users = {};
    $scope.usersInRoles = {};
    $scope.listOfUsers = {};
    
    $http.get(UrlRoleAdmin)
        .then(function (response) {
            $scope.roles = response.data;
            for(var i = 0; i < $scope.roles.length; i++) {
                var roleName = $scope.roles[i].roleName;
                var listOfUsers = Object.assign({}, $scope.listOfUsers);
                $scope.usersInRoles[roleName] = listOfUsers;
                for(var j = 0; j < $scope.roles[i].idsToAdd.length; j++) {
                    var userName = $scope.getUserName($scope.roles[i].idsToAdd[j]);
                    $scope.usersInRoles[roleName][userName] = true;
                }
            }
        },function (error) {
            $location.path("/login");
        });
        
    $http.get(UrlAdmin)
        .then(function (response) {
            $scope.users = response.data;
            for(var i = 0; i < $scope.users.length; i++){
                var name = $scope.users[i].name;
                $scope.listOfUsers[name] = false;
            }
        },function (error) {
            $location.path("/login");
        });
    
    $scope.getUserName = function(id) {
        for(var i = 0; i < $scope.users.length; i++){
            if ($scope.users[i].id === id) {
                return $scope.users[i].name;
            }
        }
    }

    $scope.getUserId = function(name) {
        for(var i = 0; i < $scope.users.length; i++){
            if ($scope.users[i].name === name) {
                return $scope.users[i].id;
            }
        }
    }

    $scope.changeRole = function(roleName, users) {
        var roleId;
        var usersToAdd = [];
        var usersToDelete = [];
        console.log(users);

        for(var key in users) {
            if(users[key] == true){
                var userId = $scope.getUserId(key);
                usersToAdd.push(userId);
            } else {
                var userId = $scope.getUserId(key);
                usersToDelete.push(userId);
            }
        }
        
        for(var i = 0; i < $scope.roles.length; i++) {
            if($scope.roles[i].roleName == roleName) {
                roleId = $scope.roles[i].roleId;
                for(var j = 0; j < $scope.roles[i].idsToAdd.length; j++){
                    var index = usersToAdd.indexOf($scope.roles[i].idsToAdd[j]);
                    if(index != -1) {
                        usersToAdd.splice(index,1);
                    }
                }
                for(var j = 0; j < $scope.roles[i].idsToDelete.length; j++){
                    var index = usersToDelete.indexOf($scope.roles[i].idsToDelete[j]);
                    if(index != -1) {
                        usersToDelete.splice(index,1);
                    }
                }
                console.log({RoleName : roleName, 
                    RoleId : roleId, 
                    IdsToAdd : usersToAdd, 
                    IdsToDelete : usersToDelete});
                $location.path("/panel");
                return;
            }
        }
    }
});