function MetaService($resource, $q) {

    var meta_resource = $resource('/api', {}, {
        fieldTypeGroupList: {
            method: 'POST',
            isArray: true,
            url: '/api/meta/fieldtypegrouplist'
        }
    });

    return {
        fieldTypeGroupList: function () {
            var d = $q.defer();
            meta_resource.fieldTypeGroupList({}, function (result) {
                d.resolve(result);
            }, function (result) {
                d.reject(result);
            });
            return d.promise;
        }
    }
}