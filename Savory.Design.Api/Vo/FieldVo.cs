using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.Design.Api.Vo
{
    public class FieldVo
    {
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [JsonProperty("fieldType")]
        public string FieldType { get; set; }

        [JsonProperty("fieldLength")]
        public int FieldLength { get; set; }

        [JsonProperty("fieldNullable")]
        public bool FieldNullable { get; set; }

        [JsonProperty("fieldDescription")]
        public string FieldDescription { get; set; }
    }
}
