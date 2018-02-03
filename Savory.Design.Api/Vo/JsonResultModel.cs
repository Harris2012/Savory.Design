using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.Design.Api.Vo
{
    /// <summary>
    /// 通用js返回对象
    /// </summary>
    public class JsonResultModel
    {
        /// <summary>
        /// 标识操作的结果
        /// 0 - 失败
        /// 1 - 成功
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// 返回提示信息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 返回结果对象
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}
