using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.Design.Api
{
    /// <summary>
    /// 更新表结构
    /// </summary>
    public enum UpdateTableSchemaResult
    {
        [Description("操作成功")]
        Success = 1,

        [Description("请求主体不能为空")]
        RequestBodyRequired = 1001,

        [Description("数据库名不能为空")]
        DBNameRequired = 1002,

        [Description("表名不能为空")]
        TableNameRequired = 1003,

        [Description("数据架构版本号不匹配")]
        CurrentVersionNotMatch = 1004,

        [Description("数据表至少应该有一个字段")]
        FieldListRequired = 1005,

        [Description("没有找到可以更新的表")]
        TableNotFound = 2001,
    }
}
