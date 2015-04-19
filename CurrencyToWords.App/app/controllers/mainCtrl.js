angular.module('currencyToWordsApp')
    .constant('API_URL', 'http://localhost:32055/api/')
    .controller('MainCtrl',
    function ($scope, $http, API_URL) {
        $scope.input = "";
        $scope.output = "";
        $scope.hasError = false;

        $scope.getWords = function () {
            // reset values
            $scope.hasError = false;
            $scope.output = "";

            // check if input is null
            if (!$scope.input && $scope.input != 0)
                return;

            // check if dollar length is greater than 13......for some reason it couldn't process value greater than 13
            if (!$scope.input.toString().split('.')[0].length >= 14) {
                setError();
                return;
            }

            $http.post(API_URL + 'values/translatetowords', { input: $scope.input })
                .success(function(data, status) {
                    if (($scope.input != null) && data.success) {
                        $scope.hasError = false;
                        $scope.output = data.payload;
                    } else {
                        $scope.hasError = true;
                        $scope.output = (data.errorMessage || data.exception);
                    }
                })
                .error(function () {
                    setError();
                }
            );
        };

        function setError() {
            $scope.hasError = true;
            $scope.output = "ops...something unexpected occured.";
        }
    }
)