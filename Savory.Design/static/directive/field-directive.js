function FieldDirective(MetaService) {
    return {
        restrict: 'E',
        templateUrl: '/static/directive/field-view.html',
        replace: true,
        scope: {
            oldField: '=',
            isEditing: '='
        },
        link: function (scope) {

            scope.init = function () {
                //字段列表
                MetaService.fieldTypeGroupList().then(function (response) {
                    scope.fieldTypeGroupList = response;
                }, function (response) {
                    alert("加载字段列表失败");
                });

                scope.newField = {};
                scope.newField.fieldName = scope.oldField.fieldName;
                scope.newField.fieldType = scope.oldField.fieldType;
                scope.newField.fieldLength = scope.oldField.fieldLength;
                scope.newField.fieldNullable = scope.oldField.fieldNullable;
                scope.newField.fieldDescription = scope.oldField.fieldDescription;

                window.filedTypeL1MouseEnter = function (item) {
                    $(item).find('ul').show();
                }
                window.fieldTypeL1MouseLeave = function (item) {
                    $(item).find('ul').hide();
                }
            }

            scope.init();

            scope.getFieldType = function (fieldTypeName) {
                for (var i = 0; i < scope.fieldTypeGroupList.length; i++) {
                    for (var j = 0; j < scope.fieldTypeGroupList[i].fieldList; j++) {
                        if (scope.fieldTypeGroupList[i].fieldList[j].fieldName == fieldTypeName) {
                            return scope.fieldTypeGroupList[i].fieldList[j];
                        }
                    }
                }
                return null;
            }

            scope.fieldTypeBoxClicked = function (event) {
                scope.isEditingFieldType = !scope.isEditingFieldType;
            }

            scope.l2Click = function (event) {

                scope.newField.fieldType = $(event.target).data('type');
                scope.newField.lengthOmissable = $(event.target).data('lengthomissable');

                scope.isEditingFieldType = false;
            }

            scope.openEditField = function () {
                scope.oldField.isEditing = true;
            }

            //删除字段
            scope.deleteField = function () {

                scope.$parent.newFieldList.splice($.inArray(scope.$parent.field, scope.$parent.newFieldList), 1);
            }

            //取消编辑字段
            //新增的字段，点击取消的时候，执行删除
            //原有的字段，点击取消的时候，恢复原来的状态
            scope.cancelEditField = function () {
                scope.oldField.isEditing = false;

                if (scope.oldField.fieldName == null) {

                    scope.$parent.newFieldList.splice($.inArray(scope.$parent.field, scope.$parent.newFieldList), 1);
                }
                else {
                    scope.newField.fieldName = scope.oldField.fieldName;
                }
            }

            //保存字段
            scope.saveField = function () {

                scope.oldField.isEditing = false;

                scope.oldField.fieldName = scope.newField.fieldName;
                scope.oldField.fieldType = scope.newField.fieldType;
                scope.oldField.fieldLength = scope.newField.fieldLength;
                scope.oldField.fieldNullable = scope.newField.fieldNullable;
                scope.oldField.fieldDescription = scope.newField.fieldDescription;
            }
        }
    }
}
