angular.module("linkHolder")
    .controller("manipulationsCtrl", 
        function ($scope, $http, Url, $location) {
        
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
                console.log(response);
            },function (error) {
                $scope.Error = error;
                console.log(error);
            });
        }
    });