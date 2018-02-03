using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.Design.Api.Vo
{
    public class TableVo
    {
        [JsonProperty("tableName")]
        public string TableName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("schemaVersion")]
        public long SchemaVersion { get; set; }

        [JsonProperty("createTime")]
        public DateTime CreateTime { get; set; }

        [JsonProperty("lastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }
    }
}
