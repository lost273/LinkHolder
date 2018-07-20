angular.module("linkHolder")
    .controller("manipulationsCtrl", 
        function ($scope, $http, Url, $location, infoMessage, changes) {
        
        var link = changes.getObject();
        
        $scope.link = link.body;
        $scope.description = link.description;
        
        $http.get(Url)
        .then(function (response) {
            $scope.folders = response.data;
        },function (error) {
            $location.path("/login");
        });

        $scope.addLink = function(folder, link, description){
            
            var saveLink = { LinkBody : link, LinkDescription : description, FolderName : folder};
            $http({
                method: 'POST',
                url: Url,
                headers: {'Content-Type': 'application/json'},
                data : JSON.stringify(saveLink)
                
            }).then(function (response) {
                $location.path("/main");
                infoMessage.setMessage(response.data);
                console.log(response.data);
            },function (error) {
                $scope.Error = error;
                console.log(error);
            });
        }
        $scope.changeLink = function(link, description){
            var saveLink = { LinkBody : link, LinkDescription : description, FolderName : ""};
            $http({
                method: 'PUT',
                url: Url + '/link/' + changes.getObject().id,
                headers: {'Content-Type': 'application/json'},
                data : JSON.stringify(saveLink)
                
            }).then(function (response) {
                $location.path("/main");
                infoMessage.setMessage(response.data);
                changes.setObject({id : "", body : "", description : ""});
                console.log(response.data);
            },function (error) {
                $scope.Error = error;
                console.log(error);
            });
        }
        $scope.changeFolder = function(name){
            $http({
                method: 'PUT',
                url: Url + '/folder/' + changes.getObject().id,
                headers: {'Content-Type': 'application/json'},
                data : JSON.stringify(name)
                
            }).then(function (response) {
                $location.path("/main");
                infoMessage.setMessage(response.data);
                changes.setObject({id : "", body : "", description : ""});
                console.log(response.data);
            },function (error) {
                $scope.Error = error;
                console.log(error);
            });
        }
    });