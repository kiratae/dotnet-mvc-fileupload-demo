using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace My.Demo.FileUpload.Web
{
    public class AjaxResultModel
    {
        public const string StatusCodeSuccess = "0";
        public const string StatusCodeError = "1";
        public const string StatusCodeBizError = "2";
        public const string StatusCodeUnauthorized = "3";
        public const string StatusCodeRedirect = "4";

        public AjaxResultModel(string statusCode = StatusCodeSuccess)
        {
            StatusCode = statusCode;
            Errors = new List<string>();
            ErrorMap = new Dictionary<string, string>();
        }

        public AjaxResultModel(string statusCode, string message)
            : this(statusCode)
        {
            Message = message;
        }

        public AjaxResultModel(string statusCode, List<string> errors)
            : this(statusCode)
        {
            if (errors != null && errors.Count > 0)
                Errors.AddRange(errors);
        }

        public AjaxResultModel(string statusCode, ModelStateDictionary modelState)
            : this(statusCode)
        {
            foreach (KeyValuePair<string, ModelStateEntry> kvPair in modelState)
            {
                if (!String.IsNullOrWhiteSpace(kvPair.Key))
                {
                    if (ErrorMap.ContainsKey(kvPair.Key))
                        continue;
                    if (kvPair.Value.Errors.Count > 0 && !String.IsNullOrWhiteSpace(kvPair.Value.Errors[0].ErrorMessage))
                        ErrorMap.Add(kvPair.Key, kvPair.Value.Errors[0].ErrorMessage);
                }
                else
                {
                    foreach (ModelError error in kvPair.Value.Errors)
                    {
                        if (String.IsNullOrWhiteSpace(error.ErrorMessage))
                            continue;
                        Errors.Add(error.ErrorMessage);
                    }
                }
            }
        }

        public string StatusCode { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; protected set; }
        public Dictionary<string, string> ErrorMap { get; protected set; }
        public int? Id { get; set; }
        public object? Data { get; set; }
    }
}
