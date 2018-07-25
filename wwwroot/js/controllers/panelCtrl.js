angular.module("linkHolder")
.constant("UrlAdmin","/api/admin")
.constant("UrlRoleAdmin","/api/roleadmin")
.controller("panelCtrl", function ($scope, $location, $http, UrlAdmin, UrlRoleAdmin) {
    $http.get(UrlAdmin)
        .then(function (response) {
            $scope.users = response.data;
            console.log(response.data);
        },function (error) {
            $location.path("/login");
        });
    $http.get(UrlRoleAdmin)
        .then(function (response) {
            $scope.roles = response.data;
            console.log(response.data);
        },function (error) {
            $location.path("/login");
        });
    $scope.usersAndRoles = {};
    for(var i = 0; i < $scope.roles.length; i++){
        for(var i = 0; i < $scope.users.length; i++){
            if($scope.roles[i].idsToAdd[j])
            if ($scope.users[i].id === id) {
                return $scope.users[i].name;
            }
        }
    }
    
    $scope.getUserName = function(id){
        for(var i = 0; i < $scope.users.length; i++){
            if ($scope.users[i].id === id) {
                return $scope.users[i].name;
            }
        }
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