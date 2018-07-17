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
            $http.post(Url, {FolderName : folder, LinkBody : link, LinkDescription : description}, {
                withCredentials: true
                })
            /*$http({
                method: 'POST',
                url: Url,
                headers: {'Content-Type': 'application/json'},
                data: {"FolderName" : folder, "LinkBody" : link, "LinkDescription" : description}
            })*/.then(function (response) {
                $location.path("/main");
            },function (error) {
                $scope.Error = error;
                console.log(error);
            });
        }
    });