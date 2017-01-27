

app.controller("LoginController", ['$scope', '$http','$window','$location', function ($scope, $http,$window,$location) {
    $scope.emailExpression = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    $scope.errortype = 'warning';
    
    $scope.login = function () {
        if ($scope.loginForm.$valid) {
            var user = {
                UserID: $scope.user.UserID,
                UserPwd: $scope.user.loginPwd
            };
            $scope.user.showmessage = true;
            $scope.errortype = 'warning';
            $scope.user.message = GetPleaseWait();
            var Url = window.location.href;
            Url = (Url.indexOf('Account') > -1) ? 'Login' : 'Account/Login';
            $http({
                url: Url,
                method: 'Post',
                data: user
            }).success(function (data) {
                if (data.messagetype == 'validuser') {
                   $window.location.reload();
                }

                if (data.messagetype == 'invaliduser') {
                     $scope.user.showmessage = true;
                    $scope.errortype = 'danger';
                    $scope.user.message = data.message;
                    //$scope.hidePwdSection = false;

                }

            }).error(function (data) {

            })
        }
    }
}])