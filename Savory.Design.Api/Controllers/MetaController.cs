using Savory.Design.Api.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Savory.Design.Api.Controllers
{
    public class MetaController : ApiController
    {
        public List<FieldTypeGroup> FieldTypeGroupList()
        {
            var types = new string[] { "bit", "tinyint", "smallint", "int", "bigint", "numeric", "decimal", "smallmoney", "money", "float", "real" };

            List<FieldTypeGroup> groups = new List<FieldTypeGroup>();

            groups.Add(new FieldTypeGroup { GroupName = "常用类型", FieldList = new List<FieldType> { "nvarchar", "varchar", "int", "bit", "datetime" } });
            groups.Add(new FieldTypeGroup { GroupName = "精确数字", FieldList = new List<FieldType> { "bit", "tinyint", "smallint", "int", "bigint", "numeric", "decimal", "smallmoney", "money" } });//Exact Numerics
            groups.Add(new FieldTypeGroup { GroupName = "近似数字", FieldList = new List<FieldType> { "float", "real" } });//Approximate Numerics
            groups.Add(new FieldTypeGroup { GroupName = "日期和时间", FieldList = new List<FieldType> { "datetime", "smalldatetime", "date", "time", "datetimeoffset", "datetime2" } });//Date and Time
            groups.Add(new FieldTypeGroup { GroupName = "字符串", FieldList = new List<FieldType> { "char", "varchar", "text" } });//Character Strings
            groups.Add(new FieldTypeGroup { GroupName = "Unicode 字符串", FieldList = new List<FieldType> { "nchar", "nvarchar", "ntext" } });//Unicode Character Strings
            groups.Add(new FieldTypeGroup { GroupName = "二进制字符串", FieldList = new List<FieldType> { "binary", "varbinary", "image" } });//Binary Strings
            groups.Add(new FieldTypeGroup { GroupName = "其他数据类型", FieldList = new List<FieldType> { "sql_variant", "timestamp", "uniqueidentifier", "xml" } });//Other Data Types
            groups.Add(new FieldTypeGroup { GroupName = "CLR数据类型", FieldList = new List<FieldType> { "hierarchyid" } });//CLR Data Types
            groups.Add(new FieldTypeGroup { GroupName = "空间数据类型", FieldList = new List<FieldType> { "geometry", "geography" } });//Special Data Types

            foreach (var group in groups)
            {
                foreach (var fieldType in group.FieldList)
                {
                    if (types.Contains(fieldType.FieldName))
                    {
                        fieldType.LengthOmissable = true;
                    }
                }
            }

            return groups;
        }
    }
}
