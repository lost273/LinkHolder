angular.module("linkHolder")
.constant("UrlRoleAdmin","/api/roleadmin")
.constant("UrlAdmin","/api/admin")
.controller("panelCtrl", function ($scope, $location, $http, UrlAdmin, UrlRoleAdmin) {
    $scope.usersInRoles = {};
    $scope.listOfUsers = {};
    
    $http.get(UrlRoleAdmin)
        .then(function (response) {
            $scope.roles = response.data;
            for(var i = 0; i < $scope.roles.length; i++) {
                var roleName = $scope.roles[i].roleName;
                var listOfUsers = $scope.listOfUsers;
                $scope.usersInRoles[roleName] = listOfUsers;
                for(var j = 0; j < $scope.roles[i].idsToAdd.length; j++) {
                    var userName = $scope.getUserName($scope.roles[i].idsToAdd[j]);
                    $scope.usersInRoles[roleName][userName] = true;
                }
            }

            
            console.log($scope.usersInRoles);
            
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
    

   
    var getRoles = function() {
        var Users = {};
        var Roles = {Users};
        var usersAndRoles = {Roles};
        for(var i = 0; i < $scope.roles.length; i++) {
            var roleName = $scope.roles[i].name;

            for(var j = 0; j < $scope.roles[i].idsToAdd.length; j++){
                var userName = $scope.getUserName($scope.roles[i].idsToAdd[j]);
                usersAndRoles.Roles[roleName].Users[userName] = true;
            }
            for(var j = 0; j < $scope.roles[i].idsToDelete.length; j++){
                var userName = $scope.getUserName($scope.roles[i].idsToDelete[j]);
                usersAndRoles.Roles[roleName].Users[userName] = false;
            }
        }
        console.log(usersAndRoles);
        return usersAndRoles;
    }

    $scope.getUserName = function(id) {
        for(var i = 0; i < $scope.users.length; i++){
            if ($scope.users[i].id === id) {
                return $scope.users[i].name;
            }
        }
    }
    $scope.changeRole = function(id) {
        console.log($scope.usersInRoles);
    }
});

/*
<div ng-controller="MainCtrl">
  <label ng-repeat="(color,enabled) in colors">
      <input type="checkbox" ng-model="colors[color]" /> {{color}} 
  </label>
  <p>colors: {{colors}}</p>
</div>

<script>
  var app = angular.module('plunker', []);

  app.controller('MainCtrl', function($scope){
      $scope.colors = {Blue: true, Orange: true};
  });
</script>
*/