using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Rockhub_backend.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        protected string ControllerSymbol
        {
            get
            {
                string typeName = GetType().Name;
                string symbol;

                // Remove "Controller"
                if (typeName.EndsWith("Controller"))
                {
                    symbol = typeName.Substring(0, typeName.Length - 10);
                }
                else if (typeName.Contains("ControllerV"))
                {
                    symbol = typeName.Substring(0, typeName.Length - 12);
                }
                else
                {
                    symbol = typeName;
                }

                return symbol.ToLower();
            }
        }

        protected ApiResult<T> GetApiResult<T>(string operation, T result, bool success = true)
        {
            return new ApiResult<T>(ControllerSymbol, operation, result, success);
        }

        public struct ApiResult<T>
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("operation")]
            public string Operation { get; set; }

            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("result")]
            public T Result { get; set; }

            public ApiResult(string type, string operation, T result,
                bool success = true)
            {
                Success = success;
                Type = type;
                Operation = operation;
                Result = result;
            }
        }
    }

}
