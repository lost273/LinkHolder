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
    $scope.getUserName = function(id){
        for(var i = 0; i < $scope.users.length; i++){
            if ($scope.users[i].id === id) {
                return $scope.users[i].name;
            }
        }
    }
});