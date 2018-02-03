using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.Design.Api.Controllers
{
    /// <summary>
    /// 新建表返回状态码
    /// </summary>
    public enum CreateTableResult
    {
        [Description("操作成功")]
        Success = 0,

        [Description("数据库名称必填")]
        DBNameRequired = 1001,

        [Description("请求主体不能为空")]
        RequestBodyRequired = 1002,

        [Description("数据表名称必填")]
        TableNameRequired = 1003,

        [Description("数据表描述信息必填")]
        DescriptionRequired = 1004,

        [Description("数据表已经存在")]
        TableExisted = 2001,

    }
}
