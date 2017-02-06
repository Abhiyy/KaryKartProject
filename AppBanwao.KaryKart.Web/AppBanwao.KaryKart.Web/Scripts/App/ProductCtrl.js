app.controller("ProductController", ['$scope', '$http', '$window', '$location', function ($scope, $http, $window, $location) {
   var apiURL = 'http://localhost:13518/api/';
   // var apiURL = 'http://testapi.karrykart.com/api/';

    var product;
    $scope.basicDetailsEditing = false;

    apiURL += "product?id=" + $location.absUrl().substring(($location.absUrl().lastIndexOf("/") + 1));//$http.get(apiURL + "product?id=" + $location.absUrl().substring(($location.absUrl().lastIndexOf("/") + 1));

    $scope.selected = {};
    $scope.isAddPSS=false;
    $scope.categories;
    $scope.subcategories;
    $scope.brands;
    $scope.isAddFeature = false;
    $scope.isAddImage = false;
    $scope.sizetypes;
    $scope.units;
    $scope.sizes;

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

    $http.get("/Product/GetSizeTypes").success(function (data) {
        $scope.sizetypes = data;
    }).error(function (status) {
        //  alert(status);
    });

    $http.get("/Product/GetUnits").success(function (data) {
        $scope.units = data;
    }).error(function (status) {
        //  alert(status);
    });


    $http.get("/Product/GetSizes").success(function (data) {
        $scope.sizes = data;
    }).error(function (status) {
        //  alert(status);
    });

    LoadProduct();

    function LoadProduct() {
        $http({
            method: "GET",
            url: apiURL
        }).success(function (data, status, header, config) {
            $scope.product = data;
            $scope.productPriceStockMappings = [];
           // var json = JSON.parse(JSON.stringify(data));
            for (var i = 0; i < data.ProductSizeMappings.length; i++) {
                $scope.productPriceStockMappings.push({
                    "ProductSizeMappingID": $scope.product.ProductSizeMappings[i].ProductSizeMappingID,
                    "SizeID": $scope.product.ProductSizeMappings[i].SizeID,
                    "SizeName": $scope.product.ProductSizeMappings[i].SizeName,
                    "UnitID": $scope.product.ProductSizeMappings[i].UnitID,
                    "UnitName": $scope.product.ProductSizeMappings[i].UnitName,
                    "SizeTypeID": $scope.product.ProductSizeMappings[i].SizeTypeID,
                    "SizeTypeName": $scope.product.ProductSizeMappings[i].SizeTypeName,
                    "Stock": $scope.product.ProductSizeMappings[i].Stock,
                    "ShippingCostID": $scope.product.ShippingDetails[i].ShippingCostID,
                    "Cost": $scope.product.ShippingDetails[i].Cost,
                    "PriceID": $scope.product.Prices[i].PriceID,
                    "Price": $scope.product.Prices[i].Price,
                    "CurrencyID": $scope.product.Prices[i].CurrencyID,
                });
            }
        });


    }

    $scope.editBasicDetails = function () {
        $scope.basicDetailsEditing = true;
    };

    $scope.cancelBasicDetails = function () {
        $scope.basicDetailsEditing = false;
    };

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
                LoadProduct();

            } else {
                alert(responseData.message);
            }
        }).error(function (responseData) {
            console.log("Error !" + responseData);
        });
    };

    $scope.getTemplate = function (feature) {
        if (feature.FeatureID === $scope.selected.FeatureID) return 'edit';
        else if (feature.FeatureID == '-1') return 'add';
        else return 'display';
    };

    $scope.getTemplateImages = function (image) {
        if (image.ImageID == '-1') return 'addPicture';
        else return 'displayPicture';
    };

    $scope.getTemplateForProductStockPrice = function (ps) {
        
            if (ps.ProductSizeMappingID == '-1') return 'addPricing';
            else return 'displayPricing';
        
    };

    $scope.editFeature = function (feature) {
        $scope.selected = angular.copy(feature);
    };

    $scope.addFeature = function () {
        $scope.product.Features.push({
            "FeatureID": "-1",
            "Feature": ""
        });

        $scope.isAddFeature = true;
    }

    $scope.addImage = function () {
        $scope.product.Images.push({
            "ImageID": "-1",
            "ImageLink": ""
        });

        $scope.isAddImage = true;
    }

    $scope.addProductStockPriceSize = function(){
        $scope.productPriceStockMappings.push({
            "ProductSizeMappingID":"-1",
        });
        $scope.isAddPSS = true;
    }

    $scope.reset = function (i) {
        if (i == '1') $scope.selected = {};
     
    };



    $scope.saveEditedFeature = function (model) {
        $http.post("/Product/EditProductFeature", { ProductID: $scope.product.ProductID, FeatureID: model.FeatureID, featureText: model.Feature }).success(function (responseData) {
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

    $scope.saveAddedPrice = function (model) {
        $http.post("/Product/AddProductStockPrice", {
            ProductID: $scope.product.ProductID, SizeID: model.SizeID, SizeName: model.SizeName,
            UnitID: model.UnitID, SizeTypeID: model.SizeTypeID, Stock: model.Stock, Cost: model.Cost,
            Price: model.Price
        }).success(function (responseData) {
            if (responseData.messagetype = "success") {
                //$scope.reset(2);
                alert(responseData.message);
                LoadProduct();
                $scope.isAddPSS = false;
            } else {
                alert(responseData.message);
            }
        }).error(function (responseData) {
            console.log("Error !" + responseData);
        });

    };

    $scope.saveNewImage = function (model) {

        var formData = new FormData();
        formData.append("file", $scope.files[0]);
        formData.append("ProductID", $scope.product.ProductID);


        $http.post("/Product/AddNewProductImage", formData, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (responseData) {
            if (responseData.messagetype = "success") {
                alert(responseData.message);
                LoadProduct();//  $scope.product.Images.splice(($scope.product.Images.length - 1), 1, { "ImageID": responseData.id, "ImageLink": responseData.ImageLink });
                $scope.isAddImage = false;

            } else {
                alert(responseData.message);
            }
        }).error(function (responseData) {
            console.log("Error !" + responseData);
        })
    };

    $scope.removeImage = function () {
        $scope.product.Images.splice(($scope.product.Images.length - 1), 1);
        $scope.isAddImage = false;
    };
    $scope.uploadedFile = function (element) {
        $scope.$apply(function ($scope) {
            $scope.files = element.files;
        });
    };

    $scope.removeNewFeature = function () {
        $scope.product.Features.splice(($scope.product.Features.length - 1), 1);
        $scope.isAddFeature = false;
    }
    $scope.removeNewProductSizeSettings = function () {
        $scope.productPriceStockMappings.splice(($scope.productPriceStockMappings.length - 1), 1);
        $scope.isAddPSS = false;
    }

    $scope.deleteImage = function (id) {
        $http.post("/Product/RemoveProductImage", { ProductID: $scope.product.ProductID, ImageID: id }).success(function (responseData) {
            if (responseData.messagetype = "success") {
                alert(responseData.message);
                LoadProduct();

            } else {
                alert(responseData.message);
            }
        }).error(function (responseData) {
            console.log("Error !" + responseData);
        });
    }

    $scope.deleteProductStockPrice = function (ps){
        $http.post("/Product/DeleteProductSizeMapping", { ProductID: $scope.product.ProductID, SizeID: ps.SizeID }).success(function (responseData) {
            if (responseData.messagetype = "success") {
                alert(responseData.message);
                LoadProduct();

            } else {
                alert(responseData.message);
            }
        }).error(function (responseData) {
            console.log("Error !" + responseData);
        });
    };

   

    $scope.saveAddedFeature = function (model) {
        $http.post("/Product/AddProductFeature", { ProductID: $scope.product.ProductID, FeatureID: model.FeatureID, featureText: model.Feature }).success(function (responseData) {
            if (responseData.messagetype = "success") {
                alert(responseData.message);
                LoadProduct();
                $scope.isAddFeature = false;

            } else {
                alert(responseData.message);
            }
        }).error(function (responseData) {
            console.log("Error !" + responseData);
        });
    };
}]);
