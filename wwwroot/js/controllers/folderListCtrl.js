angular.module("linkHolder")
    .constant("folderListActiveClass", "btn-primary")
    .constant("Url","/api/values")
    .constant("linkListPageCount", 5)
    .controller("folderListCtrl", function ($scope, $http, Url, $location, 
        folderListActiveClass, linkListPageCount) {
        
        
        $http({
            url: Url,
            method: "GET",
            withCredentials: true,
            headers: {
                Authorization: $http.defaults.headers.common.Authorization
            }
        })
        .then(function (response) {
            console.log(response);

            $scope.folders = response;
            
            
        },function (error) {
            $location.path("/login");
            console.log("ERROR" + error.status);
            console.log($http.defaults.headers.common.Authorization);
        });

        var selectedFolder = null;

        $scope.selectedPage = 1;
        $scope.pageSize = linkListPageCount;
        $scope.selectFolder = function (newFolder) {
            selectedFolder = newFolder;
            $scope.selectedPage = 1;
        }

        $scope.selectPage = function (newPage) {
            $scope.selectedPage = newPage;
        }

        $scope.folderFilterFn = function (folder) {
            return selectedFolder == null ||
                folder.name == selectedFolder;
        }
        $scope.getFolderClass = function(folder){
            return selectedFolder == folder ? folderListActiveClass : "";
        }
        $scope.getPageClass = function (page) {
            return $scope.selectedPage == page ? folderListActiveClass : "";
        }
    });