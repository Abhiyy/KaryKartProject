app.controller("ForgotPwdController", ['$scope', '$http', '$window', function ($scope, $http, $window) {
    $scope.emailExpression = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    $scope.errortype = 'warning';
    $scope.showPwdPanel = false;
    $scope.showOtpPanel = false;
    $scope.validateuser = function () {
        var user = { UserID: $scope.UserID };

        $scope.showmessage = true;
        $scope.errortype = 'warning';
        $scope.message = GetPleaseWait();

        $http({
            url: 'ForgotPassword',
            method: 'Post',
            data: user
        }).success(function (data) {
            if (data.messagetype == 'success') {
                $scope.showOtpPanel = true;
                $scope.errortype = 'success';
                $scope.message = data.message;
            }

            if (data.messagetype == 'validuseremailnotsend') {
                $scope.showmessage = true;
                $scope.errortype = 'info';
                $scope.message = data.message;
                $scope.showOtpPanel = true;
            }

            if (data.messagetype == 'usernotexists') {
                $scope.showmessage = true;
                $scope.errortype = 'danger';
                $scope.message = data.message;
            }

        }).error(function (data) {

        })
    }

    $scope.verifyOtp = function () {
        var user = {
            UserIdentifier: $scope.UserID,
            Userotp: $scope.uOtp
        };

        $scope.showmessage = true;
        $scope.errortype = 'warning';
        $scope.message = GetPleaseWait();

        $http({
            url: 'VerifyUser',
            method: 'Post',
            data: user
        }).success(function (data) {
            if (data.messagetype == 'success') {
                $scope.showPwdPanel = true;
                $scope.errortype = 'info';
                $scope.message = data.message;
                $scope.showOtpPanel = false;
            }

            if (data.messagetype == 'error') {
                $scope.showmessage = true;
                $scope.errortype = 'danger';
                $scope.message = data.message;
            }

        }).error(function (data) {

        })
    }

    $scope.setpassword = function () {
        if ($scope.forgotForm.$valid) {
            var user = {
                UserID: $scope.UserID,
                UserPwd: $scope.newPwd
            };

            $scope.showmessage = true;
            $scope.errortype = 'warning';
            $scope.message = GetPleaseWait();

            $http({
                url: 'SetPassword',
                method: 'Post',
                data: user
            }).success(function (data) {
                if (data.messagetype == 'success') {
                    $scope.errortype = 'success';
                    $scope.message = data.message;
                    $scope.showOtpPanel = false;
                    $scope.showPwdPanel = false;
                }

            }).error(function (data) {

            })
        }
    }

}]);