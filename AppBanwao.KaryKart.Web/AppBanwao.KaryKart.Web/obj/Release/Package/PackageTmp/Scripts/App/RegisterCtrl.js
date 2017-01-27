
var app = angular.module("karrykartApp", ['ngSanitize']);

app.controller("RegisterController", ['$scope', '$http', function ($scope, $http) {
    $scope.errortype = 'warning';
    $scope.hidePwdSection = false;
    $scope.waitActive = false;
    $scope.emailExpression = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    $scope.pwdExpression = /^[a-zA-Z]\w{8,50}$/;
    $scope.isconfirm=false;
    $scope.register = function () {
        $scope.user.showmessage = false;
        if ($scope.registerForm.$valid) {
            if ($scope.user.UserIdentifier.length > 0 && $scope.user.pwd.length > 0 && $scope.user.cnfpwd.length > 0) {
                var user = {
                    UserIdentifier: $scope.user.UserIdentifier,
                    UserPwd: $scope.user.pwd
                };
                $scope.waitActive = true;
                $scope.user.showmessage = true;
                $scope.errortype = 'warning';
                $scope.user.message = GetPleaseWait();
                $http({
                    url: 'Account/Register',
                    method: 'Post',
                    data: user
                }).success(function (data) {
                    if (data.messagetype == 'emailwitherror' || data.messagetype == 'email') {
                        $scope.errortype = data.messagetype == 'email' ? 'success' : 'info';
                        $scope.user.message = data.message;
                        $scope.hidePwdSection = true;
                    }
                    if (data.messagetype == 'userexist') {
                        $scope.errortype = 'danger';
                        $scope.user.message = data.message;
                        $scope.hidePwdSection = false;

                    }

                }).error(function (data) {

                })
            } else {
                $scope.user.showmessage = true;
                $scope.errortype = 'danger';
                $scope.user.message = 'Please provide necessary details above.';
            }
            $scope.waitActive = false;
        }
    }

    $scope.verifyotp = function () {
        $scope.waitActive = true;
        var otpdetails = {
            UserIdentifier: $scope.user.UserIdentifier,
            Userotp: $scope.user.otp
        };
        $scope.errortype = 'warning';
        $scope.user.message = GetPleaseWait();
        $http({
            url: 'Account/Verifyotp',
            method: 'Post',
            data: otpdetails
        }).success(function (data) {
            if (data.messagetype = 'success') {
                $scope.errortype = 'success';
                $scope.user.message = data.message;
                $scope.user.showmessage = true;
                $scope.hidePwdSection = false;
                
                $scope.user.UserIdentifier = '';
                $scope.user.otp = '';
                $scope.user.pwd = '';
                $scope.user.cnfpwd = '';
            } else if (data.messagetype = 'error') {
                $scope.errortype = 'danger';
                $scope.user.message = data.message;
                $scope.user.showmessage = true;
                $scope.hidePwdSection = false;
               
                $scope.user.UserIdentifier = '';
                $scope.user.otp = '';
                $scope.user.pwd = '';
                $scope.user.cnfpwd = '';
            }
            

        }).error(function (data) {

        })

        $scope.waitActive = false;
    }

    $scope.switchtologin = function () {
        SwitchToTab('Login');
        $scope.user.message = '';

    }

    $scope.compare = function (cPwd)
    {
       
        $scope.isconfirm = $scope.user.pwd == cPwd ? true : false;
        
       
    }
}])
