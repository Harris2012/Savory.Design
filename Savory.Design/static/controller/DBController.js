function DBController($scope, $stateParams, DBService, $uibModal) {

    //初始化
    $scope.init = function () {

        $scope.dbName = $stateParams.dbName;
        $scope.fieldList = [];

        if ($scope.dbName != null) {
            DBService.tableList($scope.dbName).then(function (response) {
                $scope.tableList = response.data;
            })
        }

        //Fields
        $('#fields').sortable({ placeholder: 'field-placeholder', stop: $scope.fieldSortStop });
    }

    //进入编辑模式
    $scope.startEdit = function () {

        var newFieldList = [];
        for (var i = 0; $scope.fieldList != null && i < $scope.fieldList.length; i++) {
            var newField = {};

            newField.fieldName = $scope.fieldList[i].fieldName;
            newField.fieldType = $scope.fieldList[i].fieldType;
            newField.fieldLength = $scope.fieldList[i].fieldLength;
            newField.fieldNullable = $scope.fieldList[i].fieldNullable;
            newField.fieldDescription = $scope.fieldList[i].fieldDescription;
            newField.isEditing = $scope.fieldList[i].isEditing;

            newFieldList.push(newField);
        }
        $scope.newFieldList = newFieldList;

        $scope.updateIndex();

        $scope.isEditing = true;
    }

    //保存,保存所有对表的更改
    $scope.saveEdit = function () {

        $scope.errormsg = null;

        //表内至少有一个字段
        if ($scope.newFieldList.length == 0) {
            $scope.errormsg = "表至少需要一个字段";
            return;
        }

        //检测有没有没被保存的字段
        for (var i = 0; i < $scope.newFieldList.length; i++) {
            if ($scope.newFieldList[i].isEditing) {
                $scope.errormsg = "存在未保存的字段";
                return;
            }
        }

        $scope.isprocessing = true;

        var request = {
            dbName: $scope.dbName,
            tableName: $scope.currentTable.tableName,
            schemaVersion: $scope.currentTable.schemaVersion,
            fieldList: $scope.newFieldList
        };

        DBService.updateTableSchema(request).then(function (response) {

            $scope.isprocessing = false;

            if (response.status == 1) {

                $scope.isEditing = false;

                DBService.tableList($scope.dbName).then(function (response) {
                    $scope.tableList = response.data;
                })

                $scope.fieldList = $scope.newFieldList;

                $scope.newFieldList = null;
            } else {
                $scope.errormsg = response.message;
            }
        })
    }

    //取消，放弃所有对表的更改
    $scope.cancelEdit = function () {

        $scope.errormsg = null;

        $scope.isEditing = false;

        $scope.newFieldList = null;
    }

    //新增字段
    $scope.newField = function () {
        $scope.newFieldList.push({ isEditing: true });

        $scope.updateIndex();
    }

    //更新排序字段
    $scope.updateIndex = function () {

        for (var i = 0; i < $scope.newFieldList.length; i++) {

            $scope.newFieldList[i].fieldIndex = i + 1;
        }
    }

    //重新排序
    $scope.fieldSortStop = function (event, ui) {

        var newFieldList = [];

        $(event.target).children().each(function (index, item) {

            newFieldList.push(angular.element(item).scope().field);
        })

        $scope.$apply(function () {
            $scope.newFieldList = newFieldList;

            $scope.updateIndex();
        });
    }

    //打开新建表弹层
    $scope.openCreateTableModal = function () {

        var modalInstance = $uibModal.open({
            animation: true,//打开时的动画开关
            templateUrl: '/static/modal/CreateTableView.html',
            controller: CreateTableController,
            resolve: {
                dbName: function () {
                    return angular.copy($scope.dbName);
                }
            }
        });

        modalInstance.result.then(function (response) {
            //console.log(response);

            DBService.tableList($scope.dbName).then(function (response) {
                $scope.tableList = response.data;
            })

        }, function () {
            //console.log('Modal dismissed at: ' + new Date());
        });
    }

    //查看表详细信息
    $scope.viewTable = function (table) {

        if ($scope.currentTable == table) {
            return;
        }

        if ($scope.isEditing) {
            console.log("请退出编辑模式后重试");
            return;
        }

        $scope.currentTable = table;

        //设置选中项的样式
        for (var i = 0; i < $scope.tableList.length; i++) {
            $scope.tableList[i].css = null;
        }
        table.css = "info";

        //加载表的所有字段
        DBService.fieldList($scope.dbName, table.tableName).then(function (response) {
            $scope.fieldList = response.data;
        })

    }

    $scope.init();
}