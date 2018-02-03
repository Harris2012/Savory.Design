using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.Design.Api.Vo
{
    public class FieldType
    {
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [JsonProperty("lengthOmissable")]
        public bool LengthOmissable { get; set; }

        public static implicit operator FieldType(string fieldName)
        {
            return new FieldType { FieldName = fieldName };
        }
    }
}
