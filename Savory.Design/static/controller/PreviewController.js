function PreviewController($scope, DatabaseService) {

    $scope.init = function () {

        // ajax demo
        $('#ajax')
        .on("changed.jstree", function (e, data) {
            if (data.selected.length) {
                alert('The selected node is: ' + data.instance.get_node(data.selected[0]).text);
            }
        })
        .jstree({
            'core': {
                'data': {
                    "method": "POST",
                    "url": "/api/database/tablelist?tableName=aa",
                    "dataType": "json" // needed only if you do not supply JSON headers
                }
            }
        });

        $scope.fieldList = [];

        $scope.updateIndex();

        //Fields
        $('#fields').sortable({ placeholder: 'field-placeholder', stop: $scope.fieldSortStop });
    }

    

    $scope.saveTable = function () {
        var table = {};
        table.tableName = "ttt";
        table.fieldList = $scope.fieldList;

        DatabaseService.createDB(table).then(function (response) {

        });
    }
}