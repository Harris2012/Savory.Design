using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.Design.Api
{
    /// <summary>
    /// 更新表架构返回
    /// </summary>
    public class UpdateTableSchemaResponse : ResponseBase
    {
        public static implicit operator UpdateTableSchemaResponse(UpdateTableSchemaResult result)
        {
            UpdateTableSchemaResponse response = new UpdateTableSchemaResponse();

            response.Status = (int)result;
            response.Message = EnumExtension.GetDescription(result);

            return response;
        }
    }
}
