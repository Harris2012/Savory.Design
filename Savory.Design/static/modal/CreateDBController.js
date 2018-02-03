function CreateDBController($scope, DBService, $uibModalInstance) {

    $scope.confirmCreateDB = function () {

        $scope.waiting = true;
        $scope.message = null;

        DBService.createDB($scope.tableName).then(function (response) {

            $scope.waiting = false;

            if (response.status == 1) {
                $uibModalInstance.close($scope.tableName);
            }
            else {
                $scope.message = response.message;
            }
        })
    }
}