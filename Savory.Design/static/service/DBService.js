function DBService($resource, $q) {

    var database_resource = $resource('/api', {}, {
        dbList: {
            method: 'POST',
            isArray: true,
            url: '/api/database/dblist'
        },
        tableList: {
            method: 'POST',
            url: '/api/database/tableList?dbName=:dbName'
        },
        fieldList: {
            method: 'POST',
            url: '/api/database/fieldList?dbName=:dbName&tableName=:tableName'
        },
        createDB: {
            method: 'POST',
            url: '/api/database/createDB'
        },
        createTable: {
            method: 'POST',
            url: '/api/database/createTable?dbName=:dbName'
        },
        updateTableSchema: {
            method: 'POST',
            url: '/api/database/updateTableSchema'
        }
    });

    return {
        dbList: function () {
            var d = $q.defer();
            database_resource.dbList({}, {}, function (result) {
                d.resolve(result);
            }, function (result) {
                d.reject(result);
            });
            return d.promise;
        },
        tableList: function (dbName) {
            var d = $q.defer();
            database_resource.tableList({ dbName: dbName }, {}, function (result) {
                d.resolve(result);
            }, function (result) {
                d.reject(result);
            });
            return d.promise;
        },
        fieldList: function (dbName, tableName) {
            var d = $q.defer();
            database_resource.fieldList({ dbName: dbName, tableName: tableName }, {}, function (result) {
                d.resolve(result);
            }, function (result) {
                d.reject(result);
            });
            return d.promise;
        },
        createDB: function (dbName) {
            var d = $q.defer();
            database_resource.createDB({ dbName: dbName }, {}, function (result) {
                d.resolve(result);
            }, function (result) {
                d.reject(result);
            });
            return d.promise;
        },
        createTable: function (dbName, tableVo) {
            var d = $q.defer();
            database_resource.createTable({ dbName: dbName }, tableVo, function (result) {
                d.resolve(result);
            }, function (result) {
                d.reject(result);
            });
            return d.promise;
        },
        updateTableSchema: function (request) {
            var d = $q.defer();
            database_resource.updateTableSchema({}, request, function (result) {
                d.resolve(result);
            }, function (result) {
                d.reject(result);
            });
            return d.promise;
        }
    }
}