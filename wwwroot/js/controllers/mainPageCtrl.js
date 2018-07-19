angular.module("linkHolder")
.controller("mainPageCtrl", function ($scope, $location, $http, infoMessage, userName){
    $scope.information = function(){
        return infoMessage.getMessage();
    }
    $scope.username = function(){
        return userName.getUserName();
    }
    $scope.logout = function (){
        $http.defaults.headers.common.Authorization = "";
        $location.path("/login");
    }
});