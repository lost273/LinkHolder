angular.module("linkHolder")
    .constant("folderListActiveClass", "active")
    .constant("Url","/api/values")
    .controller("folderListCtrl", 
        function ($scope, $http, Url, $location, folderListActiveClass, infoMessage, changes) {
        
        $http.get(Url)
        .then(function (response) {
            $scope.folders = response.data;
        },function (error) {
            $location.path("/login");
        });

        $scope.selectedFolder = null;

        $scope.getFolders = function(){
            $http.get(Url)
            .then(function (response) {
                $scope.folders = response.data;
            },function (error) {
                $location.path("/login");
            });
        }

        $scope.selectFolder = function(newFolder){
            $scope.selectedFolder = newFolder;
            infoMessage.setMessage(null);
        }
        $scope.getFolderClass = function(name){
            if($scope.selectedFolder == null) return "";
            return $scope.selectedFolder.name == name ? folderListActiveClass : "";
        }
        $scope.changeView = function(loc){
            $location.path(loc);
        }
        $scope.deleteLink = function(id){
            $http.delete(Url+"/link/"+id)
            .then(function (response) {
                $scope.selectedFolder = null;
                infoMessage.setMessage(response.data);
                $scope.getFolders();
            },function (error) {
                infoMessage.setMessage(error);
            });
        }
        $scope.deleteFolder = function(id){
            $http.delete(Url+"/folder/"+id)
            .then(function (response) {
                $scope.selectedFolder = null;
                infoMessage.setMessage(response.data);
                $scope.getFolders();
            },function (error) {
                infoMessage.setMessage(error);
            });
        }
        $scope.changeLink = function(_id, _body, _description){
            var link = {id : _id,body : _body,description : _description};
            changes.setObject(link);
            $location.path("/changeLink");
        }
        $scope.changeFolder = function(_id, _body){
            var link = {id : _id,body : _body,description : ""};
            changes.setObject(link);
            $location.path("/changeFolder");
        }
    });