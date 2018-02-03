using Newtonsoft.Json;
using Savory.Design.Api.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.Design.Api
{
    /// <summary>
    /// 更新表结构请求
    /// </summary>
    public class UpdateTableSchemaRequest
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        [JsonProperty("dbName")]
        public string DBName { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        [JsonProperty("tableName")]
        public string TableName { get; set; }

        /// <summary>
        /// 当前版本号
        /// </summary>
        [JsonProperty("schemaVersion")]
        public long SchemaVersion { get; set; }

        /// <summary>
        /// 新的表结构
        /// </summary>
        [JsonProperty("fieldList")]
        public List<FieldVo> FieldList { get; set; }
    }
}
