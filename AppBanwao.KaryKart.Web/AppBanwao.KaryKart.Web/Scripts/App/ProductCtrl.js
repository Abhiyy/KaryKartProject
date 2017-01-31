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

    $scope.selected = {};
    $scope.categories;
    $scope.subcategories;
    $scope.brands;

    $http.get("/Product/GetCategories").success(function (data) {
        $scope.categories = data;
    }).error(function (status) {
        //  alert(status);
    });

    $http.get("/Product/GetBrands").success(function (data) {
        $scope.brands = data;
    }).error(function (status) {
        //  alert(status);
    });

    $http.get("/Product/GetSubCategories").success(function (data) {
        $scope.subcategories = data;
    }).error(function (status) {
        //  alert(status);
    });

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
    $scope.updateBasicDetails = function () {
        var basicproductDetails = {
            "ProductID": $scope.product.ProductID,
            "Name": $scope.product.Name,
            "Description": $scope.product.Description,
            "CategoryID": $scope.product.CategoryID,
            "SubCategoryID": $scope.product.SubCategoryID,
            "BrandID": $scope.product.BrandID,
            "Active": $scope.product.Active
        };

        $http.post("/Product/EditBasicProductDetails", { model: basicproductDetails }).success(function (responseData) {
            if (responseData.messagetype = "success") {
                alert(responseData.message);
                $scope.basicDetailsEditing = false;
            } else {
                alert(responseData.message);
            }
        }).error(function (responseData) {
            console.log("Error !" + responseData);
        });
    };


    $scope.deleteProductFeature = function (id) {
        $http.post("/Product/RemoveProductFeature", { ProductID: $scope.product.ProductID, FeatureID: id }).success(function (responseData) {
            if (responseData.messagetype = "success") {
                alert(responseData.message);
            } else {
                alert(responseData.message);
            }
        }).error(function (responseData) {
            console.log("Error !" + responseData);
        });
    };

    $scope.getTemplate = function (feature) {
        if (feature.FeatureID === $scope.selected.FeatureID) return 'edit';
        else return 'display';
    };

    $scope.editFeature = function (feature) {
        $scope.selected = angular.copy(feature);
    };

    $scope.reset = function () {
        $scope.selected = {};
    };

    $scope.saveEditedFeature = function (model) {
        $http.post("/Product/EditProductFeature", { ProductID: $scope.product.ProductID, FeatureID: model.FeatureID, featureText:model.Feature }).success(function (responseData) {
            if (responseData.messagetype = "success") {
                alert(responseData.message);
                $scope.reset();
            } else {
                alert(responseData.message);
            }
        }).error(function (responseData) {
            console.log("Error !" + responseData);
        });

    };
    
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
