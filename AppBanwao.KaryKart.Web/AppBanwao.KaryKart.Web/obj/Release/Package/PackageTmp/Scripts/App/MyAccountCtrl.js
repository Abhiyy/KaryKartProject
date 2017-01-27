
app.controller('MyAccountController', ['$scope', '$http', '$window', function ($scope, $http, $window) {

    $scope.loaddata = function () {
        $http({
            url: 'GetUser',
            method: 'Post'
        }).success(function (data) {
            if (data!= null) {
                $scope.UserID = data.UserID;
                $scope.Username = data.EmailAddress;
                $scope.Salutaions = data.salutations;
                $scope.fName = data.FirstName;
                $scope.lName = data.LastName;
                $scope.addressline1 = data.AddressLine1;
                $scope.addressline2 = data.AddressLine2;
                $scope.landmark = data.Landmark;
                $scope.mobile = data.Mobile;
                $scope.ctyID = data.CityID!=null?data.CityID:1;
                $scope.cities = data.cities;
                $scope.StateID = data.StateID!=null?data.StateID:1;
                $scope.states = data.states;
                $scope.countryID = data.CountryID != null?data.CountryID:1;
                $scope.countries = data.countries;
                $scope.pincode = data.Pincode;
            }

      
        }).error(function (data) {

        })
    }

    $scope.updateuserdata = function () {
      

        if ($scope.myAccountForm.$valid) {
            var userdetails = {
                UserID: $scope.UserID,
                EmailAddress: $scope.Username,
                FirstName: $scope.fName,
                LastName: $scope.lName,
                AddressLine1: $scope.addressline1,
                AddressLine2: $scope.addressline2,
                Landmark: $scope.landmark,
                Mobile: $scope.mobile,
                CityID: $scope.ctyID,
                StateID: $scope.StateID,
                CountryID: $scope.countryID,
                Pincode: $scope.pincode,
                Salutation: $scope.SalutationID
            };
            $scope.errortype = 'warning';
            $scope.showmessage = true;
            $scope.message = GetPleaseWait();
            $http({
                url: 'UpdateUser',
                method: 'Post',
                data: userdetails
            }).success(function (data) {
                if (data.messagetype == 'success') {
                    $scope.errortype = 'success';
                    $scope.message = data.message;
                }
            }).error(function (data) {

            })
        }
    }
}]);

