app.controller("ProductController", ['$scope', '$http', '$window', '$location', function ($scope, $http, $window, $location) {
    var apiURL = 'http://localhost:13518/api/';

    var product;
    $scope.basicDetailsEditing = false;
    // var product = ProductService.getProduct($location.absUrl().substring(($location.absUrl().lastIndexOf("/") + 1)));//$http.get(apiURL + "product?id=" + $location.absUrl().substring(($location.absUrl().lastIndexOf("/") + 1))); //ProductService.GetProduct(id);
    // console.log(product["ProductID"]);
    apiURL += "product?id=" + $location.absUrl().substring(($location.absUrl().lastIndexOf("/") + 1));//$http.get(apiURL + "product?id=" + $location.absUrl().substring(($location.absUrl().lastIndexOf("/") + 1));

    $http({
        method: "GET",
        url: apiURL
    }).success(function (data, status, header, config) {
        $scope.product = data;
    });

  
    $scope.editBasicDetails = function () {
        $scope.basicDetailsEditing = true;
    }
    $scope.cancelBasicDetails = function () {
        $scope.basicDetailsEditing = false;
    }

}]);

//app.service('ProductService', ['$http', function ($http, $scope) {
//    var apiURL = 'http://localhost:13518/api';

//    var product = {};

//    this.getProduct = function (id, $scope) {
//        apiURL += "product?id=" + id;
//        return $http({
//            method: "GET",
//            url: apiURL,
//            headers: { 'Content-Type': 'application/json' }
//        }).then(function successCallback(response) {
//            // this callback will be called asynchronously
//            // when the response is available
//            return response.data;
//            // return product;
//            //  console.log(product);
//        }, function errorCallback(response) {
//            //console.log(response);
//            // called asynchronously if an error occurs
//            // or server returns response with an error status.
//        });
//        //  console.log(product);

//    };
//}]);
