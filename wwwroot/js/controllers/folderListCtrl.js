angular.module("linkHolder")
    .constant("folderListActiveClass", "btn-primary")
    .constant("Url","/api/values")
    .constant("linkListPageCount", 5)
    .controller("folderListCtrl", function ($scope, $http, Url, $location) {
        
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
    });