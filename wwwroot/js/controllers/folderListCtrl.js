angular.module("linkHolder")
    .constant("folderListActiveClass", "active")
    .constant("Url","/api/values")
    .controller("folderListCtrl", 
        function ($scope, $http, Url, $location, folderListActiveClass) {
        
        $http.get(Url)
        .then(function (response) {
            $scope.folders = response.data;
        },function (error) {
            $location.path("/login");
        });

        $scope.selectedFolder = null;

        $scope.selectFolder = function(newFolder){
            $scope.selectedFolder = newFolder;
        }
        $scope.getFolderClass = function(name){
            if($scope.selectedFolder == null) return "";
            return $scope.selectedFolder.name == name ? folderListActiveClass : "";
        }
        $scope.deleteLink = function(){
            
        }
    });