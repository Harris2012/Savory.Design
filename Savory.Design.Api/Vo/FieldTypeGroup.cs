using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.Design.Api.Vo
{
    public class FieldTypeGroup
    {
        [JsonProperty("groupName")]
        public string GroupName { get; set; }

        [JsonProperty("fieldList")]
        public List<FieldType> FieldList { get; set; }
    }
}
