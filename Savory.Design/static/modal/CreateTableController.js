function CreateTableController($scope, DBService, $uibModalInstance, dbName) {

    $scope.init = function () {
        $scope.dbName = dbName;
    }

    $scope.init();

    $scope.confirmCreateTable = function () {

        $scope.waiting = true;
        $scope.message = null;

        DBService.createTable($scope.dbName, $scope.table).then(function (response) {

            $scope.waiting = false;
            if (response.status == 0) {
                $uibModalInstance.close($scope.table);
            }
            else {

                $scope.message = response.message;
            }
        }, function (error) {

            $scope.waiting = false;
            if (error.status == 500) {
                $scope.message = "[500]服务器内部错误，请排查Bug";
            } else {
                console.log($scope.message);
            }
        })
    }
}