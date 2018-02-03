function DBsController($scope, DBService, $uibModal) {

    DBService.dbList().then(function (response) {
        $scope.dbList = response;
    })

    $scope.openCreateDBModal = function (size) {

        var modalInstance = $uibModal.open({
            animation: true,//打开时的动画开关
            templateUrl: '/static/modal/CreateDBView.html',
            controller: CreateDBController,
            //size: size,//模态框的大小尺寸
            //resolve: {//这是一个入参,这个很重要,它可以把主控制器中的参数传到模态框控制器中
            //    items: function () {//items是一个回调函数
            //        return $scope.items;//这个值会被模态框的控制器获取到
            //    }
            //}
        });

        modalInstance.result.then(function (response) {
            console.log(response);
        }, function () {
            //console.log('Modal dismissed at: ' + new Date());
        });
    }
}